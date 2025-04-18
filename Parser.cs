using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

public class Parser
{
    /// <summary>
    /// Class to map the CSV rainfall data to the RainfallRecord class
    /// </summary>
    public class RainfallMap : ClassMap<RainfallRecord>
    {
        public RainfallMap()
        {
            Map(m => m.DeviceId).Name("Device ID");
            Map(m => m.Time).Name("Time");
            Map(m => m.Rainfall).Name("Rainfall");
        }
    }

    /// <summary>
    /// Class to map the CSV device data to the Device class
    /// </summary>
    public class DeviceMap : ClassMap<Device>
    {
        public DeviceMap()
        {
            Map(m => m.DeviceId).Name("Device ID");
            Map(m => m.DeviceName).Name("Device Name");
            Map(m => m.Location).Name("Location");
        }
    }

    /// <summary>
    /// Constructor for the parser. Parses the CSV data, processes it and prints it.
    /// </summary>
    /// <param name="rain1">The path to the first rainfall data CSV file</param>
    /// <param name="rain2">The path to the second rainfall data CSV file</param>
    /// <param name="deviceList">The path to the device list CSV file</param>
    public Parser(string rain1, string rain2, string deviceList)
    {
        // Parses the CSV files and creates a list of rainfall data and device objects
        List<RainfallRecord> rainfall1 = RainfallParser(rain1);
        List<RainfallRecord> rainfall2 = RainfallParser(rain2);
        rainfall1.AddRange(rainfall2);
        List<Device> devices = DeviceParser(deviceList);

        // Calculates the current time from the last record's time stamp
        DateTime currentTime = rainfall1[rainfall1.Count - 1].Time;

        // Processes the raw data to collate each rainfall record with the device that recorded it
        List<RainfallByDevice> rainfallByDevice = ProcessData(rainfall1, devices);

        // Prints the relevant device and rainfall information
        foreach (RainfallByDevice device in rainfallByDevice)
        {
            device.Device.PrintDeviceData();
            device.PrintRainfall(currentTime);
        }
    }

    /// <summary>
    /// A function that processes the raw CSV data, turning it into RainfallByDevice objects that store
    /// the device information and the list of readings that have been taken from that device.
    /// </summary>
    /// <param name="rain">A rainful record to process</param>
    /// <param name="devices">The list of devices</param>
    /// <returns></returns>
    private static List<RainfallByDevice> ProcessData(List<RainfallRecord> rain, List<Device> devices)
    {
        List<RainfallByDevice> allData = [];

        // Iterates through all the devices in the device list and creates a new RainfallByDevice object to
        // store all the rainfall readings for that device.
        foreach (Device device in devices)
        {
            RainfallByDevice rainfallByDevice = new(device);
            allData.Add(rainfallByDevice);
        }

        foreach (RainfallRecord data in rain)
        {
            foreach (RainfallByDevice device in allData)
            {
                if (data.DeviceId == device.Device.DeviceId)
                {
                    device.AddReading(data.Time, data.Rainfall);
                }
            }
        }

        return allData;
    }

    /// <summary>
    /// Reads the CSV file at the given path into a CsvReader and creates a list of rainfall record objects
    /// </summary>
    /// <param name="path">Path to the CSV file</param>
	private static List<RainfallRecord> RainfallParser(string path)
    {
        StreamReader reader = new(path);
        using CsvReader csv = new(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<RainfallMap>();
        List<RainfallRecord> rainfallData = csv.GetRecords<RainfallRecord>().ToList();

        return rainfallData;
    }

    /// <summary>
    /// Reads the CSV file at the given path into a CsvReader and creates a list of device objects
    /// </summary>
    /// <param name="path">Path to the CSV file</param>
	private List<Device> DeviceParser(string path)
    {
        StreamReader reader = new(path);
        using CsvReader csv = new(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<DeviceMap>();
        List<Device> deviceData = csv.GetRecords<Device>().ToList();

        return deviceData;
    }
}


using System;
using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using static CsvMap;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class CsvMap
{
    /// <summary>
    /// 
    /// </summary>
    public class RainfallData
    {
        [Name("Device ID")]
        public int DeviceId { get; set; }

        [Name("Time")]
        public DateTime Time { get; set; }

        [Name("Rainfall")]
        public int Rainfall {  get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeviceData
    {
        [Name("Device ID")]
        public int DeviceId { get; set; }

        [Name("Device Name")]
        public string DeviceName { get; set; }

        [Name("Location")]
        public string Location { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class RainfallMap : ClassMap<RainfallData>
    {
        public RainfallMap()
        {
            Map(m => m.DeviceId).Name("Device ID");
            Map(m => m.Time).Name("Time");
            Map(m => m.Rainfall).Name("Rainfall");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeviceMap : ClassMap<DeviceData>
    {
        public DeviceMap()
        {
            Map(m => m.DeviceId).Name("Device ID");
            Map(m => m.DeviceName).Name("Device Name");
            Map(m => m.Location).Name("Location");
        }
    }

    public CsvMap(string rain1, string rain2, string deviceList)
    {
        List<RainfallData> rainfall1 = CsvMapRainfall(rain1);
        List<RainfallData> rainfall2 = CsvMapRainfall(rain2);
        List<DeviceData> devices = CsvMapDevice(deviceList);

        rainfall1.AddRange(rainfall2);
        List<RainfallByDevice> rainfallByDevice = ProcessData(rainfall1, devices);

        foreach (RainfallByDevice device in rainfallByDevice)
        {
            device.PrintDevice();
            double avg = device.CalculateAverageRainfall();
        }
    }

    private List<RainfallByDevice> ProcessData(List<RainfallData> rain, List<DeviceData> devices)
    {
        List<RainfallByDevice> allData = new List<RainfallByDevice>();

        // Iterates through all the devices in the device list and creates a new RainfallByDevice object to
        // store all the rainfall readings for that device.
        foreach (DeviceData device in devices)
        {
            RainfallByDevice rainfallByDevice = new(device.DeviceId, device.DeviceName);
            allData.Add(rainfallByDevice);
        }

        foreach (RainfallData data in rain)
        {
            foreach (RainfallByDevice device in allData)
            {
                if (data.DeviceId == device.DeviceId)
                {
                    device.AddReading(data.Time, data.Rainfall);
                }
            }
        }

        return allData;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
	public List<RainfallData> CsvMapRainfall(string path)
    {
        StreamReader reader = new(path);
        using CsvReader csv = new(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<RainfallMap>();
        List<RainfallData> rainfallData = csv.GetRecords<RainfallData>().ToList();

        return rainfallData;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
	public List<DeviceData> CsvMapDevice(string path)
    {
        StreamReader reader = new(path);
        using CsvReader csv = new(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<DeviceMap>();
        List<DeviceData> deviceData = csv.GetRecords<DeviceData>().ToList();

        return deviceData;
    }
}


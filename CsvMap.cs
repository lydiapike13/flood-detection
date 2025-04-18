using System;
using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

public class CsvMap
{
    /// <summary>
    /// 
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
    /// 
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

    public CsvMap(string rain1, string rain2, string deviceList)
    {
        List<RainfallRecord> rainfall1 = CsvMapRainfall(rain1);
        List<RainfallRecord> rainfall2 = CsvMapRainfall(rain2);
        List<Device> devices = CsvMapDevice(deviceList);

        rainfall1.AddRange(rainfall2);
        DateTime currentTime = rainfall1[rainfall1.Count - 1].Time;

        List<RainfallByDevice> rainfallByDevice = ProcessData(rainfall1, devices);

        foreach (RainfallByDevice device in rainfallByDevice)
        {
            device.CalculateStatus();
            device.Device.PrintDeviceData();
            device.PrintRainfall();
            double avg = device.CalculateAverageRainfall(currentTime);
        }
    }

    private List<RainfallByDevice> ProcessData(List<RainfallRecord> rain, List<Device> devices)
    {
        List<RainfallByDevice> allData = new List<RainfallByDevice>();

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
    /// 
    /// </summary>
    /// <param name="path"></param>
	public List<RainfallRecord> CsvMapRainfall(string path)
    {
        StreamReader reader = new(path);
        using CsvReader csv = new(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<RainfallMap>();
        List<RainfallRecord> rainfallData = csv.GetRecords<RainfallRecord>().ToList();

        return rainfallData;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
	public List<Device> CsvMapDevice(string path)
    {
        StreamReader reader = new(path);
        using CsvReader csv = new(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<DeviceMap>();
        List<Device> deviceData = csv.GetRecords<Device>().ToList();

        return deviceData;
    }
}


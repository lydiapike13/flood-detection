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

    public CsvMap(string path, string dataType)
    {
        if (path == null) throw new ArgumentNullException("path");

        if (dataType == "rainfall")
        {
            CsvMapRainfall(path);
        }
        else if (dataType == "device")
        {
            CsvMapDevice(path);
        }
        else
        {
            throw new ArgumentException("dataType not recognised.");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
	public void CsvMapRainfall(string path)
    {
        StreamReader reader = new(path);
        using CsvReader csv = new(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<RainfallMap>();
        List<RainfallData> rainfallData = csv.GetRecords<RainfallData>().ToList();

        foreach (RainfallData data in rainfallData)
        {
            Console.WriteLine("Device ID: " + data.DeviceId);
            Console.WriteLine("Time: " + data.Time);
            Console.WriteLine("Rainfall: " + data.Rainfall);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
	public void CsvMapDevice(string path)
    {
        StreamReader reader = new(path);
        using CsvReader csv = new(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<DeviceMap>();
        List<DeviceData> deviceData = csv.GetRecords<DeviceData>().ToList();

        foreach (DeviceData data in deviceData)
        {
            Console.WriteLine("Device ID: " + data.DeviceId);
            Console.WriteLine("Device Name: " + data.DeviceName);
            Console.WriteLine("Location: " + data.Location);
        }
    }
}


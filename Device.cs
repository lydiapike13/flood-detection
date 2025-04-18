using System;
using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

public class Device
{
    [Name("Device ID")]
    public int DeviceId { get; set; }

    [Name("Device Name")]
    public string DeviceName { get; set; }

    [Name("Location")]
    public string Location { get; set; }

	public Device()
    {
    }

    /// <summary>
    /// Function to print out the device data to the console: ID, name and location.
    /// </summary>
    internal void PrintDeviceData()
    {
        Console.WriteLine("Device ID: " + DeviceId);
        Console.WriteLine("Device Name: " + DeviceName);
        Console.WriteLine("Location: " + Location);
    }
}

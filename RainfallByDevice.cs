using System;
using CsvHelper;
using System.Data;
using System.Globalization;
using System.Net.Http.Headers;

public class RainfallByDevice
{
    internal int DeviceId { get; set; }
    private string DeviceName { get; set; }

    private List<DateTime> ReadingTimes = new();
    private List<int> RainfallReadings = new();
    private string Status = string.Empty;

    public RainfallByDevice(int id, string name)
    {
        DeviceId = id;
        DeviceName = name;
    }

    internal void AddReading(DateTime time, int reading)
    {
        ReadingTimes.Add(time);
        RainfallReadings.Add(reading);
    }

    internal double CalculateAverageRainfall()
    {
        return RainfallReadings.Average();
    }

    internal void CalculateStatus()
    {
        if (RainfallReadings.Average() < 10)
        {
            Status = "Green";
        }
        else if (RainfallReadings.Average() < 15)
        {
            Status = "Orange";
        }
        else
        {
            Status = "Red";
        }
        
        foreach (int reading in RainfallReadings)
        {
            if (reading > 30)
            {
                Status = "Red";
            }
        }
    }


    internal void PrintDevice()
    {
        Console.WriteLine("Device ID: " + DeviceId);
        Console.WriteLine("Times:");
        foreach (DateTime time in ReadingTimes)
        {
            Console.WriteLine(time);
        }
        Console.WriteLine("Rainfall:");
        foreach (int rain in RainfallReadings)
        {
            Console.WriteLine(rain);
        }
        Console.WriteLine("Average Rainfall: " + RainfallReadings.Average() + "mm");
        Console.WriteLine("Status: " + Status);
    }
}
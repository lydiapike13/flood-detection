using System;
using CsvHelper;
using System.Data;
using System.Globalization;
using System.Net.Http.Headers;

public class RainfallByDevice
{
    internal Device Device { get; set; }

    private List<DateTime> ReadingTimes = new();
    private List<int> RainfallReadings = new();
    private string Status = string.Empty;

    public RainfallByDevice(Device device)
    {
        Device = device;
    }

    internal void AddReading(DateTime time, int reading)
    {
        ReadingTimes.Add(time);
        RainfallReadings.Add(reading);
    }

    internal double CalculateAverageRainfall(DateTime currentTime)
    {
        List<int> recordsWithinFourHours = new List<int>();

        for (int i = 0; i < RainfallReadings.Count; i++)
        {
            if (ReadingTimes[i].AddHours(4) >= currentTime)
            {
                recordsWithinFourHours.Add(RainfallReadings[i]);
            }
        }
        return recordsWithinFourHours.Average();
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

    internal string CalculateTrend()
    {
        if (RainfallReadings[0] < RainfallReadings[RainfallReadings.Count - 1])
        {
            return "Increasing";
        }
        else
        {
            return "Decreasing";
        }
    }


    internal void PrintRainfall()
    {
        Console.WriteLine("Average Rainfall: " + RainfallReadings.Average() + "mm");

        if (Status == "Red")
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        if (Status == "Orange")
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }
        if (Status == "Green")
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        Console.WriteLine("Status: " + Status);

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Trend: " + CalculateTrend() + "\n\n");
    }
}
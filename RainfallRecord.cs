using System;
using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

public class RainfallRecord
{
    [Name("Device ID")]
    public int DeviceId { get; set; }

    [Name("Time")]
    public DateTime Time { get; set; }

    [Name("Rainfall")]
    public int Rainfall { get; set; }

	public RainfallRecord()
    {
    }
}

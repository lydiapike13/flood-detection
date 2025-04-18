using CsvHelper.Configuration.Attributes;

public class RainfallRecord
{
    [Name("Device ID")]
    public int DeviceId { get; set; }

    [Name("Time")]
    public DateTime Time { get; set; }

    [Name("Rainfall")]
    public int Rainfall { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
	public RainfallRecord()
    {
    }
}

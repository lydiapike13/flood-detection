class Program
{
    static void Main()
    {
        string folder = "C:\\Users\\lydia\\OneDrive\\Documents\\FloodDetectionData\\";
        CsvMap map = new(folder + "Data1.csv", folder + "Data2.csv", folder + "Devices.csv");
    }
}
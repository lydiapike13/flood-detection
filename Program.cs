class Program
{
    static void Main()
    {
        string folder = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\FloodDetectionData\\";
        CsvMap map = new(folder + "Data1.csv", folder + "Data2.csv", folder + "Devices.csv");
    }
}
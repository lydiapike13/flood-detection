class Program
{
    static void Main()
    {
        CsvMap map = new("C:\\Users\\lydia\\OneDrive\\Documents\\FloodDetectionData\\Data1.csv", "rainfall");
        CsvMap map2 = new("C:\\Users\\lydia\\OneDrive\\Documents\\FloodDetectionData\\Data2.csv", "rainfall");
        CsvMap map3 = new("C:\\Users\\lydia\\OneDrive\\Documents\\FloodDetectionData\\Devices.csv", "device");
    }
}
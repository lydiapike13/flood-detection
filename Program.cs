using FloodDetection;

class Program
{
    /// <summary>
    /// Main function of the program. Retrieves the CSV files from their folder and creates the parser
    /// </summary>
    static void Main()
    {
        string folder = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\FloodDetectionData\\";
        Parser parser = new(folder + "Data1.csv", folder + "Data2.csv", folder + "Devices.csv");
    }
}
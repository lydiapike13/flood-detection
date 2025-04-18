using System;
using CsvHelper;
using System.Data;
using System.Globalization;
using System.Net.Http.Headers;

namespace FloodDetection
{
    public class RainfallByDevice
    {
        internal Device Device { get; set; }

        private List<DateTime> ReadingTimes = new();
        private List<int> RainfallReadings = new();
        private string Status = string.Empty;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="device">Device object</param>
        public RainfallByDevice(Device device)
        {
            Device = device;
        }

        /// <summary>
        /// Adds a reading to the device
        /// </summary>
        /// <param name="time">Time of the reading</param>
        /// <param name="reading">Rainfall reading</param>
        internal void AddReading(DateTime time, int reading)
        {
            ReadingTimes.Add(time);
            RainfallReadings.Add(reading);
        }

        /// <summary>
        /// Calculates the average rainfall over the last four hours
        /// </summary>
        /// <param name="currentTime">Current time</param>
        /// <returns>Average rainfall</returns>
        private double CalculateAverageRainfall(DateTime currentTime)
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

        /// <summary>
        /// Calculates the red/green/amber status and assigns the variable.
        /// </summary>
        private void CalculateStatus()
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

        /// <summary>
        /// Calculates whether the rainfall is increasing or decreasing
        /// </summary>
        /// <returns>String indicating increase or decrease</returns>
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

        /// <summary>
        /// Function printing the processed information
        /// </summary>
        /// <param name="currentTime">The current time</param>
        internal void PrintRainfall(DateTime currentTime)
        {
            Console.WriteLine("Average Rainfall: " + CalculateAverageRainfall(currentTime) + "mm");

            CalculateStatus();
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
}
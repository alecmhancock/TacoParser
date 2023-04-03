using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using System.Data.Common;
using System.Net.Http.Headers;
using System.Xml;
using System.Collections.Generic;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            

            logger.LogInfo("Log initialized");
            var lines = File.ReadAllLines(csvPath);
            logger.LogInfo($"Lines: {lines[0]}");

            #region variable declaration
            var parser = new TacoParser();
            var locations = lines.Select(parser.Parse).ToArray();
            ITrackable track1 = new TacoBell();
            ITrackable track2 = new TacoBell();
            double distance = 0.0;
            #endregion

            foreach (var tacobell1 in locations)
            {
                var locA = new GeoCoordinate(tacobell1.Location.Latitude, tacobell1.Location.Longitude);
                foreach (var tacobell2 in locations)
                {
                    var locB = new GeoCoordinate(tacobell2.Location.Latitude, tacobell2.Location.Longitude);
                    if (locA.GetDistanceTo(locB) > distance)
                    {
                        distance = locA.GetDistanceTo(locB);
                        track1 = tacobell1;
                        track2 = tacobell2;
                    }
                }
            }

            var distRead = Convert.ToInt32(distance / 1000 * 1.609);
            Console.WriteLine($"The furthest Tacobells are {track1.Name} and {track2.Name}," +
                $"\nand they are about {distRead} miles away from each other.");

        }
    }
}

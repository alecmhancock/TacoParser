using GeoCoordinatePortable;
using System;
using System.IO;
using System.Linq;

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
            var parser = new TacoParser();
            var locations = lines.Select(parser.Parse).ToArray();

            #region variable declaration
            ITrackable track1 = null;
            ITrackable track2 = null;
            ITrackable track3 = new TacoBell();
            ITrackable track4 = new TacoBell();
            double distance = 0.0;

            var distRead = Convert.ToInt32(distance / 1000 * 1.609);
            #endregion

            Console.WriteLine("Would you like to find the closest or farthest Taco Bells in this list?");
            var userInput = Console.ReadLine();

            #region logic for distance measurement
            switch (userInput.ToLower())
            {
                #region Default (farthest, as per assignment scope)
                default:
                    foreach (var tacobell1 in locations)
                    {
                        var locA = new GeoCoordinate(tacobell1.Location.Latitude, tacobell1.Location.Longitude);
                        foreach (var tacobell2 in locations)
                        {
                            var locB = new GeoCoordinate(tacobell2.Location.Latitude, tacobell2.Location.Longitude);
                            var store = locA.GetDistanceTo(locB);
                            if (store > distance)
                            {
                                distance = store;
                                track1 = tacobell1;
                                track2 = tacobell2;
                            }
                        }
                    }

                    Console.WriteLine($"The furthest Taco Bells are {track1.Name} and {track2.Name}," +
                        $"\n" +
                        $"and they are about {distRead} miles away from each other.");

                    break;
                #endregion

                #region closest
                case "closest":
                    for (var i = 0; i < locations.Length; i++)
                    {
                        var locA = locations[i];
                        var coordA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude);
                        for (var e = 0; e < locations.Length; e++)
                        {
                            var locB = locations[e];
                            var coordB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);
                            var store = coordA.GetDistanceTo(coordB);
                            if (coordA == coordB)
                            {
                                continue;
                            }
                            else if (store < distance && locA != null && locB != null)
                            {
                                distance = store;
                                track3 = locA;
                                track4 = locB;


                            }
                        }
                    }

                    Console.WriteLine($"The closest Taco Bells are {track3.Name} and {track4.Name}," +
                        $"\n" +
                        $"and they are about {distance} miles away from each other.");
                    break;

            }
            #endregion
            #endregion




        }
    }
}

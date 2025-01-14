﻿using GeoCoordinatePortable;
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
            

            #region variable declaration
            ITrackable track1 = new TacoBell();
            ITrackable track2 = new TacoBell();
            double distance = 0.0;
            double distance2 = 1000000000000000.0;
            var parser = new TacoParser();
            var locations = lines.Select(parser.Parse).ToArray();


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
                            if (locA.GetDistanceTo(locB) > distance)
                            {
                                distance = locA.GetDistanceTo(locB);
                                track1 = tacobell1;
                                track2 = tacobell2;
                            }
                        }
                    }
                    var distRead = Convert.ToInt32(distance * 0.00062);
                    Console.WriteLine($"The furthest Taco Bells are {track1.Name} and {track2.Name}," +
                        $"\n" +
                        $"and they are about {distRead} miles away from each other.");

                    break;
                #endregion

                #region closest
                case "closest":
                    foreach (var tacobell1 in locations)
                    {
                        var locA = new GeoCoordinate(tacobell1.Location.Latitude, tacobell1.Location.Longitude);
                        foreach (var tacobell2 in locations)
                        {
                            var locB = new GeoCoordinate(tacobell2.Location.Latitude, tacobell2.Location.Longitude);
                            if (locA == locB || distance2 == 0.00)
                            {
                                continue;
                            }
                            //if (distance2 == 0.00)
                            //{
                            //    continue;
                            //}
                            if (locA.GetDistanceTo(locB) <= distance2)
                            {
                                distance2 = locA.GetDistanceTo(locB);
                                track1 = tacobell1;
                                track2 = tacobell2;
                            }
                        }
                    }
                    distRead = Convert.ToInt32(distance2 * 0.00062);
                    Console.WriteLine($"The furthest Taco Bells are {track1.Name} and {track2.Name}," +
                        $"\n" +
                        $"and they are about {distRead} miles away from each other.");

                    break;

        }
        #endregion
        #endregion




    }
}
}

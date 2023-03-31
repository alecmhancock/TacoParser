namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            var cells = line.Split(','); // Takes your line and use line.Split(',') to split it up into an array of strings, separated by the char ','
            var latitude = 0.0;
            var longitude = 0.0;

            if (cells.Length < 3) // If your array.Length is less than 3, something went wrong 
            {
                logger.LogError("ERR, Cells over 3: " + line);
                return null; 
            }

            latitude = double.Parse(cells[0]);
            longitude = double.Parse(cells[1]);
            var name = cells[2];

            var tacobell = new TacoBell 
            { 
                Name = name, 
                Location = new Point() 
                { 
                    Latitude = latitude, 
                    Longitude = longitude 
                } 
            };

            return tacobell;
        }
    }
}
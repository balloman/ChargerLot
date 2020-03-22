using System;

namespace ChargerLotLib.Handlers
{
    public class DataHandler
    {
        /// <summary>
        /// Gets data about a specific lot
        /// </summary>
        /// <param name="resolution">Resolution in hours</param>
        /// <param name="lot">The lot to get</param>
        public static void GetUserFilledLotData(string lot, double resolution)
        {
            var fh = FirestoreHandler.GetInstance("reports");
            var dbContext = fh.GetDb();
            var collection = dbContext.Collection("reports").WhereGreaterThan("timestamp",
                DateTime.UtcNow.AddHours(-resolution)).WhereEqualTo("lot", lot);
            var count = 0;
            
        }

        public static readonly Lot ParkingGarage = new Lot
        {
            Id = "IMF",
            Name = "Parking Garage",
            Spaces = 200
        };
        
        public struct Lot
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public int Spaces { get; set; }
        }
    }
}
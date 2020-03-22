using System;
using ChargerLotLib.Models;

namespace ChargerLotLib.Handlers
{
    public static class DataHandler
    {
        /// <summary>
        /// Gets data about a specific lot
        /// </summary>
        /// <param name="resolution">Resolution in hours</param>
        /// <param name="lot">The lot to get</param>
        public static LotSnapshot GetUserFilledLotData(Lot lot, double resolution)
        {
            var fh = FirestoreHandler.GetInstance("reports");
            var dbContext = fh.GetDb();
            var collection = dbContext.Collection("reports").WhereGreaterThan("Timestamp",
                DateTime.UtcNow.AddHours(-resolution)).WhereEqualTo("Lot", lot.Id);
            var count = collection.GetSnapshotAsync().Result.Count;
            return new LotSnapshot
            {
                Timestamp = DateTime.UtcNow,
                Parked = count,
                Full = (double)count / lot.Spaces,
                Lot = lot
            };
        }
    }
}
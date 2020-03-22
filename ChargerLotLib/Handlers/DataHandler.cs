using System;
using System.Collections.Generic;
using System.Linq;
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
            var result = collection.GetSnapshotAsync().Result;
            var outList = result.Select(snap => snap.ConvertTo<ParkingReport>()).ToList();
            var count = result.Count;
            return new LotSnapshot
            {
                Timestamp = DateTime.UtcNow,
                Parked = count,
                Full = (double)count / lot.Spaces,
                Lot = lot,
                Reports = outList
            };
        }

        public static double FindLikelyFullness(List<ParkingReport> reports)
        {
            var sumOfWeights = 0.0;
            var sumOfTimes = 0.0;
            foreach (var report in reports)
            {
                var timeValue = DateTime.UtcNow.Subtract(report.Timestamp).TotalHours;
                sumOfWeights += timeValue * (int) report.FullnessFactor;
                sumOfTimes += timeValue;
            }

            try
            {
                return sumOfWeights / sumOfTimes;
            }
            catch (DivideByZeroException)
            {
                return 0;
            }
        }
    }
}
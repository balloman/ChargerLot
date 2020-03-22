using System.Collections.Generic;
using System.Threading.Tasks;
using ChargerLotLib.Handlers;
using ChargerLotLib.Models;
using Google.Cloud.Firestore;

namespace ChargerLotLib
{
    public static class ReportController
    {
        public static Task<DocumentReference> ReportUserPark(ParkingReport report)
        {
            var fh = FirestoreHandler.GetInstance("reports");
            return fh.AddData(report);
        }
    }
}
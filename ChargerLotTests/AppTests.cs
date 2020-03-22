using System;
using ChargerLotLib;
using ChargerLotLib.Handlers;
using ChargerLotLib.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChargerLotTests
{
    [TestClass]
    public class AppTests
    {
        [TestMethod]
        public void ReportTest()
        {
            var task = ReportController.ReportUserPark(new ParkingReport()
            {
                Lot = Lots.ParkingGarage.Id,
                Timestamp = DateTime.UtcNow,
                Uuid = "sampleuuid"
            });
            task.Wait();
            if (!task.IsCompletedSuccessfully)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void QueryTest()
        {
            var count = DataHandler.GetUserFilledLotData(Lots.ParkingGarage, 1);
        }
    }
}
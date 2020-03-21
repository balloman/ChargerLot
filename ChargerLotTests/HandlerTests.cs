using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChargerLotLib.Handlers;

namespace ChargerLotTests
{
    [TestClass]
    public class HandlerTests
    {
        [TestMethod]
        public void EnsureWeHaveProject()
        {
            var fh = FirestoreHandler.GetInstance();
        }
    }
}
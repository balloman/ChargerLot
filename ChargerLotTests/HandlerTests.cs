using System;
using System.Collections.Generic;
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
            var fh = FirestoreHandler.GetInstance(null);
        }

        [TestMethod]
        public void ReadAndWrite()
        {
            var fh = FirestoreHandler.GetInstance("users");
            var task = fh.SetData(new Dictionary<string, object>()
            {
                {"First", "Bernard"},
                {"Last", "Allotey"},
                {"Born", 2001}
            }, "Bernard Allotey");
            task.Wait();
            Assert.IsTrue(fh.GetData("Bernard Allotey").Result.ContainsValue((long) 2001));
        }

        [TestMethod]
        public void EnsureBadDataThrows()
        {
            var fh = FirestoreHandler.GetInstance("users");
            try
            {
                fh.SetData(this, "blah");
                Assert.Fail();
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}
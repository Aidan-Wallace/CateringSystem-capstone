using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class FileAccessTests
    {
        [TestMethod]
        public void OpenItemsTest()
        {
            FileAccess testObject = new FileAccess();

            List<CateringItem> result = testObject.OpenItems();
            // New list of what the properties should be
            List<CateringItem> expected = new List<CateringItem>();

            PropertyInfo[] properties = result.GetType().GetProperties();

            CollectionAssert.AreEqual(properties, expected.GetType().GetProperties());
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Reflection;
using Capstone.Classes;
using System.IO;
using FileAccess = Capstone.Classes.FileAccess;

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
            List<CateringItem> expected = new List<CateringItem>();

            PropertyInfo[] properties = result.GetType().GetProperties();

            CollectionAssert.AreEqual(properties, expected.GetType().GetProperties());
        }

        [TestMethod]
        public void LogTest()
        {
            string filePath = @"C:\Catering\log.txt";
            FileAccess testObject = new FileAccess();
            testObject.Log("TEST", 55.55M, 66.66M);
            string line = "";
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                { line = sr.ReadLine(); }
            }
            string testLine = line.Substring(21);
            Assert.AreEqual("TEST $55.55 $66.66", testLine);
        }


    }
}

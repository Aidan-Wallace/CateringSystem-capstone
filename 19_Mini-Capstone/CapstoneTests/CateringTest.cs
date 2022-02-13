using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CapstoneTests
{
    [TestClass]
    public class CateringTest
    {
        [TestMethod]
        public void Check_that_catering_object_is_created()
        {
            // Arrange 
            Catering catering = new Catering();

            // Act

            //Assert
            Assert.IsNotNull(catering);
        }

        [TestMethod]
        public void GetItemTest()
        {
            // Arrange
            Catering testObject = new Catering();
            CateringItem cateringItem = new CateringItem("A", "A1", "Tropical Fruit Bowl", 3.50M);

            // Act
            CateringItem result = testObject.GetItem("A1");

            // Assert
            Assert.AreEqual(result.Type, cateringItem.Type);
            Assert.AreEqual(result.ProductCode, cateringItem.ProductCode);
            Assert.AreEqual(result.Description, cateringItem.Description);
            Assert.AreEqual(result.Price, cateringItem.Price);

        }

        [TestMethod]
        public void GetInvalidItemTest()
        {
            Catering testObject = new Catering();
            CateringItem result = testObject.GetItem("G6");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetInventoryTest()
        {
            // Run openItems
            // Check if keys from OpenItems() is the same as testObject.Inventory
            Catering testObject = new Catering();

            Dictionary<CateringItem, int> result = testObject.GetInventory();

            foreach (KeyValuePair<CateringItem, int> item in result)
            {
                // If returns null then item is not in dictionary.
                // if getItem never returns null, then result is correct.

                CateringItem returnedItem = testObject.GetItem(item.Key.ProductCode);
                Assert.IsNotNull(returnedItem);
            }
        }

        [TestMethod]
        public void GetOrderTest()
        {
            Catering testObject = new Catering();

            Order result = testObject.GetOrder();
            Order testOrder = new Order();

            Assert.IsInstanceOfType(result, testOrder.GetType());
        }

        [TestMethod]
        public void TakeItemTest()
        {
            Catering testObject = new Catering();
            CateringItem cateringItem = testObject.GetInventory().First().Key;

            testObject.TakeItem(cateringItem, 5);

            int result = testObject.GetInventory()[cateringItem];

            Assert.AreEqual(result, 20);
        }
    }
}

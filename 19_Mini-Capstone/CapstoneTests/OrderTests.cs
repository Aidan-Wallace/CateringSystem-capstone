using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class OrderTests
    {
        private Order testObject = new Order();

        [TestMethod]
        public void ChangeDueTest()
        {
            CateringItem cateringItem = new CateringItem("A", "A1", "Tropical Fruit Bowl", 3.50M);

            testObject.AddMoney(100);

            // execute your code
            bool result = testObject.AddProductToOrder(cateringItem, 5);
            Assert.IsTrue(result);

            // Check to see if string matches template string
            string templateString = "You received (1) Fifties, (1) Twenties, (1) Tens, (2) Ones, (2) Nickels in change.";
            string changeReturned = testObject.ChangeDue();
            Assert.AreEqual(templateString, changeReturned);

        }

        [TestMethod]
        public void AddProductToOrderTest()
        {
            // Create an instance of "CateringItem"
            CateringItem cateringItem = new CateringItem("A", "A1", "Tropical Fruit Bowl", 3.50M);

            // Create a object of "Order"
            testObject.AddMoney(100);

            // act
            // execute your code
            bool result = testObject.AddProductToOrder(cateringItem, 5);

            // asserts
            Assert.IsTrue(result);
            // 1. make sure the order is in items to order
            int insertedItem = testObject.ItemsToOrder[cateringItem];
            Assert.IsNotNull(insertedItem);

            // 2. make sure the quantity is correct
            Assert.AreEqual(5, insertedItem);

            // 3. make sure the account balance is correct
            Assert.AreEqual(82.50M, testObject.AccountBalance);

            // 4. make sure the order total is correct
            Assert.AreEqual(17.50M, testObject.OrderTotal);
        }

        [TestMethod]
        public void UpdatesTheQuantityIfProductAlreadyIsAdded()
        {
            // Create an instance of "CateringItem"
            CateringItem cateringItem = new CateringItem("A", "A1", "Tropical Fruit Bowl", 3.50M);

            // Create a object of "Order"
            testObject.AddMoney(100);

            // act
            // execute your code
            testObject.AddProductToOrder(cateringItem, 5);
            bool result = testObject.AddProductToOrder(cateringItem, 10);

            Assert.IsTrue(result);

            // assert
            // 1. make sure the order is in items to order
            int insertedItem = testObject.ItemsToOrder[cateringItem];
            Assert.IsNotNull(insertedItem);
            // 2. make sure the quantity is correct
            Assert.AreEqual(15, insertedItem);
            Assert.AreEqual(47.50M, testObject.AccountBalance);
        }

        [TestMethod]
        public void AddsTheProductToOrderIfAccountBalanceIsNotSufficient()
        {
            // Create an instance of "CateringItem"
            CateringItem cateringItem = new CateringItem("A", "A1", "Tropical Fruit Bowl", 3.50M);

            // act
            // execute your code
            bool result = testObject.AddProductToOrder(cateringItem, 5);

            // Assert
            Assert.IsFalse(result);
        }
    }
}


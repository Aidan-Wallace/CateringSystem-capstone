using System.Collections.Generic;
//using Capstone.Classes.Catering;
namespace Capstone.Classes
{
    public class Order
    {
        public Dictionary<CateringItem, int> ItemsToOrder { get; private set; }
        public decimal AccountBalance { get; private set; } = 0.00M;

        public Order()
        {
            ItemsToOrder = new Dictionary<CateringItem, int>();
        }

        public bool AddProductToOrder(string productCode)
        {
            // match the string to the items List to get the CateringItem
            //Add that item to ItemsToOrder with an int ++, then Decrement the same item in Inventory by 1,
            //Check to see if there's enough product.
            catering.GetItem(productCode);
            
            return true; // :)
        }

        public bool AddMoney(decimal amount)
        {
            // Balance cannot go above $1,500
            // Cannot add more than $100 at once
            // Only accept 1, 5, 10, 20, 50, 100

            bool addMoneySuccessful = false;

            AccountBalance += amount;

            return addMoneySuccessful;
        }

        public bool CompleteTransaction(decimal amount)
        {
            bool transactionApproved = false;

            // Check if user has available account balance 


            return transactionApproved;
        }
    }
}

using System.Collections.Generic;
using System.Linq;

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

        public bool AddProductToOrder(CateringItem item, int quantity)
        {
            // match the string to the items List to get the CateringItem
            //Add that item to ItemsToOrder with an int ++, then Decrement the same item in Inventory by 1,
            //Check to see if there's enough product, and that there's enough money.

            if (item == null)
            {
                return false;
            }
            else if (ItemsToOrder.ContainsKey(item))
            {
                ItemsToOrder[item] += quantity;
            }
            else
            {
                ItemsToOrder.Add(item, quantity);
            }
            return true;
        }

        public bool AddMoney(int amount)
        {
            // Balance cannot go above $1,500
            // Cannot add more than $100 at once
            // Only accept 1, 5, 10, 20, 50, 100
            decimal newBalance = AccountBalance + amount;

            if (newBalance > 1500) return false;

            AccountBalance += amount;

            return true;
        }

        public bool CompleteTransaction(decimal amount)
        {
            bool transactionApproved = false;

            // Check if user has available account balance 


            return transactionApproved;
        }
    }
}

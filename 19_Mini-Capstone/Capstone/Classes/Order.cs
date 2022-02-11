using System.Collections.Generic;
using System.Linq;

namespace Capstone.Classes
{
    public class Order
    {
        public Dictionary<CateringItem, int> ItemsToOrder { get; }
        public decimal AccountBalance { get; private set; } = 0.00M;
        public decimal OrderTotal { get; private set; } = 0.00M;

        public Order()
        {
            ItemsToOrder = new Dictionary<CateringItem, int>();
        }

        public bool AddProductToOrder(CateringItem item, int quantity)
        {
            // match the string to the items List to get the CateringItem
            //Add that item to ItemsToOrder with an int ++, then Decrement the same item in Inventory by 1,
            //Check to see if there's enough product, and that there's enough money.
            decimal currentPrice = item.Price * quantity;
            if (currentPrice > AccountBalance) { return false; }
            if (ItemsToOrder.ContainsKey(item))
            {
                ItemsToOrder[item] += quantity;
            }
            else
            {
                ItemsToOrder.Add(item, quantity);
            }
            AccountBalance -= currentPrice;
            OrderTotal =+ currentPrice;
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

        public void CompleteTransaction()
        {
            AccountBalance = 0;
        }
       public string ChangeDue()
        { 
            int numberOfFifties = 0;
            int numberOfTwenties = 0;
            int numberOfTens = 0;
            int numberOfFives = 0;
            int numberOfOnes = 0;
            int numberOfQuarters = 0;
            int numberOfDimes = 0;
            int numberOfNickels = 0;

            while(OrderTotal >= 50) { OrderTotal -= 50; numberOfFifties++;}
            while(OrderTotal >= 20) { OrderTotal -= 20; numberOfTwenties++; }
            while (OrderTotal >= 10) { OrderTotal -= 10; numberOfTens++; }
            while (OrderTotal >= 5) { OrderTotal -= 5; numberOfFives++; }
            while (OrderTotal >= 1) { OrderTotal -= 1; numberOfOnes++; }
            while (OrderTotal >= 0.25M) { OrderTotal -= 0.25M; numberOfQuarters++; }
            while (OrderTotal >= 0.10M) { OrderTotal -= 0.10M; numberOfDimes++; }
            while (OrderTotal >= 0.05M) { OrderTotal -= 0.05M; numberOfNickels++; }

            string changeMessage = "You received ";
            changeMessage += numberOfFifties != 0 ? $"({numberOfFifties}) Fifties, " : "";
            changeMessage += numberOfTwenties != 0 ? $"({numberOfTwenties}) Twenties, " : "";
            changeMessage += numberOfTens != 0 ? $"({numberOfTens}) Tens, " : "";
            changeMessage += numberOfFives != 0 ? $"({numberOfFives}) Fives, " : "";
            changeMessage += numberOfOnes != 0 ? $"({numberOfOnes}) Ones, " : "";
            changeMessage += numberOfQuarters != 0 ? $"({numberOfQuarters}) Quarters, " : "";
            changeMessage += numberOfDimes != 0 ? $"({numberOfDimes}) Dimes, " : "";
            changeMessage += numberOfNickels != 0 ? $"({numberOfNickels}) Nickels, " : "";
            changeMessage += "in change.";
            
            int commaOccurrence = (changeMessage.IndexOf("in change.") - 2);
           
            return changeMessage.Remove(commaOccurrence, 1);
        }
    }
}

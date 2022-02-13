using System.Collections.Generic;

namespace Capstone.Classes
{
    public class Order
    {
        public Dictionary<CateringItem, int> ItemsToOrder { get; }
        public decimal AccountBalance { get; private set; } = 0.00M;
        public decimal OrderTotal { get; private set; } = 0.00M;
        public FileAccess log = new FileAccess();
        public Order()
        {
            ItemsToOrder = new Dictionary<CateringItem, int>();

        }

        public bool AddProductToOrder(CateringItem item, int quantity)
        {
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
            OrderTotal = +currentPrice;
            log.Log($"{quantity} {item.Description} {item.ProductCode}", currentPrice, AccountBalance);
            return true;
        }

        public bool AddMoney(int amount)
        {

            decimal newBalance = AccountBalance + amount;

            if (newBalance > 1500) return false;

            AccountBalance += amount;

            log.Log($"ADD MONEY:", amount, AccountBalance);

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

            log.Log($"GIVE CHANGE:", AccountBalance, 0);

            if (AccountBalance == 0) return "You received no change.";

            while (AccountBalance >= 50) { AccountBalance -= 50; numberOfFifties++; }
            while (AccountBalance >= 20) { AccountBalance -= 20; numberOfTwenties++; }
            while (AccountBalance >= 10) { AccountBalance -= 10; numberOfTens++; }
            while (AccountBalance >= 5) { AccountBalance -= 5; numberOfFives++; }
            while (AccountBalance >= 1) { AccountBalance -= 1; numberOfOnes++; }
            while (AccountBalance >= 0.25M) { AccountBalance -= 0.25M; numberOfQuarters++; }
            while (AccountBalance >= 0.10M) { AccountBalance -= 0.10M; numberOfDimes++; }
            while (AccountBalance >= 0.05M) { AccountBalance -= 0.05M; numberOfNickels++; }

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
            ItemsToOrder.Clear(); //Clearing items from the order after change is returned.
            return changeMessage.Remove(commaOccurrence, 1);

        }
    }
}

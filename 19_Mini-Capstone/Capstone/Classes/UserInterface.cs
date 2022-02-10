using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Capstone.Classes
{
    public class UserInterface
    {
        // This class provides all user communications, but not much else.
        // All the "work" of the application should be done elsewhere

        // ALL instances of Console.ReadLine and Console.WriteLine should 
        // be in this class.
        // NO instances of Console.ReadLine or Console.WriteLIne should be
        // in any other class.

        private Catering catering = new Catering();

        public void RunInterface()
        {
            bool done = false;
            while (!done)
            {
                Order currentCustomer = new Order();

                Console.WriteLine("(1) Display Catering Items");
                Console.WriteLine("(2) Order");
                Console.WriteLine("(3) Quit");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1": DisplayCateringItems(); break;
                    case "2": Order(currentCustomer); break;
                    case "3": done = true; break;
                    default: Console.WriteLine("\nPlease enter a valid selection."); break;
                }
            }

        }
        public void DisplayCateringItems()
        {
            Console.WriteLine();
            Console.WriteLine(String.Format("{0,-13} {1,-20} {2, -10} {3,-7}", "Product Code", "Description", "Qty", "Price"));

            foreach (KeyValuePair<CateringItem, int> kvp in catering.Inventory.OrderBy(key => key.Key.ProductCode))
            {
                string quantity = kvp.Value == 0 ? "Sold Out" : kvp.Value.ToString();

                Console.WriteLine(String.Format("{0,-13} {1,-20} {2, -10} {3,-7}", kvp.Key.ProductCode, kvp.Key.Description, quantity, kvp.Key.Price.ToString("C2")));
            }
            Console.WriteLine();
        }

        public void Order(Order currentCustomer)
        {
            bool isOrdering = true;
            while (isOrdering)
            {
                Console.WriteLine("(1) Add Money");
                Console.WriteLine("(2) Select Products");
                Console.WriteLine("(3) Complete Transaction");

                Console.WriteLine("Current Account Balance: {0}", "$0");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        // Add money
                        break;
                    case "2":
                        DisplayCateringItems();
                        Console.Write("Please select product >>> ");

                        currentCustomer.AddProductToOrder(Console.ReadLine());

                        break;
                    case "3":
                        isOrdering = false;
                        break;
                    default:
                        Console.WriteLine("\nPlease enter a valid selection.");
                        break;
                }
            }

        }
    }
}

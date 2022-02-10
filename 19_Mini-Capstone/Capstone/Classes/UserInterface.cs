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

        public Catering catering { get; private set; } = new Catering();
        
        public void RunInterface()
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine("(1) Display Catering Items");
                Console.WriteLine("(2) Order");
                Console.WriteLine("(3) Quit");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1": DisplayCateringItems(); break;
                    case "2": Order(); break;
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

        public void Order()
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
                        SelectProduct();
                        
                       // if (catering.order.AddProductToOrder(catering.GetItem(Console.ReadLine()))) //returns true if the item was added to the order, need to remove it from inventory                        
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
        public void SelectProduct()
        {
            bool pickingQuantity = true;
            DisplayCateringItems();
            Console.WriteLine("Please select product:");
            CateringItem product = catering.GetItem(Console.ReadLine());
            if (product == null) { Console.WriteLine("Product not found.\nPlease try again.\n"); pickingQuantity = false; } //need to check for sufficient stock, AND sufficient funds.
            if (catering.Inventory[product] == 0) { Console.WriteLine("Item is Sold out."); pickingQuantity = false; }
            while (pickingQuantity)
            {
                Console.WriteLine("Please enter a quantity:");
                try { int quantity = int.Parse(Console.ReadLine());
                    if (quantity > catering.Inventory[product])
                    {
                        Console.WriteLine("Insufficient Stock for the quantity requested. Please enter a smaller value"); continue;
                    }
                    catering.order.AddProductToOrder(product, quantity);
                        pickingQuantity = false; } //AddProductToOrder needs to be an int return so can account for money issues, right now it's bool
                catch { Console.WriteLine("Invalid quantity. Please try again."); }
                
            }
        }

    }
}

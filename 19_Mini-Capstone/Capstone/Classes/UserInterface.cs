using System;
using System.Collections.Generic;
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

        readonly int[] acceptableBills = new int[6] { 1, 5, 10, 20, 50, 100 };

        public Catering @Catering { get; private set; } = new Catering();
        public Dictionary<CateringItem, int> Inventory
        {
            get
            { return Catering.GetInventory(); }
        }
        public Order CurrentOrder
        {
            get
            {
                return Catering.GetOrder();
            }
        }
        public void RunInterface()
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine("(1) Display Catering Items");
                Console.WriteLine("(2) Order");
                Console.Write("(3) Quit\n ");
                string input = Console.ReadLine();
                Console.WriteLine();
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
           // Console.WriteLine();
            Console.WriteLine(String.Format("{0,-13} {1,-20} {2, -10} {3,-7}", "Product Code", "Description", "Qty", "Price"));

            foreach (KeyValuePair<CateringItem, int> kvp in Inventory.OrderBy(key => key.Key.ProductCode))
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

                Console.Write("Current Account Balance: {0}\n ", CurrentOrder.AccountBalance.ToString("C2"));
                string input = Console.ReadLine();
                
                switch (input)
                {
                    case "1":
                        int inputBills = 0;
                        while (true)

                        {
                            try
                            {
                                Console.Write("Please enter amount\n $");
                                inputBills = int.Parse(Console.ReadLine());
                                Console.WriteLine();
                            }
                            catch
                            {
                                Console.WriteLine("Bill values must be entered as a number.\n");
                                continue;
                            }
                            if (!acceptableBills.Contains(inputBills))
                            {
                                Console.WriteLine("Invalid bill denomination.");
                                continue;
                            }
                            if (!CurrentOrder.AddMoney(inputBills))
                            {
                                Console.WriteLine("Account balance cannot exceed $1500.00.\n");
                            }
                            break;
                        }

                        break;
                    case "2":
                        if (CurrentOrder.AccountBalance == 0) { Console.WriteLine("Please add funds first."); continue; }
                        Console.WriteLine();
                        SelectProduct();
                        break;
                    case "3":
                        Console.WriteLine();
                        DisplayReceipt();
                        CurrentOrder.CompleteTransaction();
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
            Console.Write("Please select product.\n ");
            CateringItem product = Catering.GetItem(Console.ReadLine().ToUpper());

            if (product == null)
            {
                Console.WriteLine("Product not found.\nPlease try again.\n");
                pickingQuantity = false;
            }
            else if (Inventory[product] == 0)
            {
                Console.WriteLine("Item is sold out.\n");
                pickingQuantity = false;
            }

            while (pickingQuantity)
            {
                Console.Write("Please enter a quantity.\n ");
                try
                {
                    int quantity = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    if (quantity > Inventory[product])
                    {
                        Console.WriteLine("Insufficient Stock for the quantity requested.\n");
                        continue;
                    }
                    if (!CurrentOrder.AddProductToOrder(product, quantity)) { Console.WriteLine("Insufficient funds for the current selection."); continue; }
                    Catering.TakeItem(product, quantity);
                    pickingQuantity = false;
                }
                catch
                {
                    Console.WriteLine("Invalid quantity.\nPlease try again.\n");
                }
            }
        }
        public void DisplayReceipt()
        {
            Dictionary<string, string[]> productType = new Dictionary<string, string[]>()
            {
                { "B", new string[2]{"Beverage", "Don't forget ice."}},
                { "A", new string[2]{"Appetizer", "You might need extra plates." }},
                { "E", new string[2]{ "Entrée", "Did you remember Dessert?" } },
                { "D", new string[2]{"Dessert", "Coffee goes with dessert."} }
            };

            foreach (KeyValuePair<CateringItem, int> kvp in CurrentOrder.ItemsToOrder.OrderBy(key => key.Key.Type))
            {
                if (CurrentOrder.ItemsToOrder.Count == 0)
                {
                    break;
                }

                Console.WriteLine(String.Format("{0,-5} {1,-10} {2, -20} {3, -7} {4, -25}",
                    kvp.Value,
                    productType[kvp.Key.Type][0],
                    kvp.Key.Description,
                    kvp.Key.Price.ToString("C2"),
                    productType[kvp.Key.Type][1])
                );
            }
            Console.WriteLine($"\nTotal: {CurrentOrder.OrderTotal:C2}\n");
            Console.WriteLine(CurrentOrder.ChangeDue());
            Console.WriteLine();

        }
    }
}
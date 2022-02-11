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
        readonly int[] acceptableBills = new int[6] { 1, 5, 10, 20, 50, 100 };

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
            decimal addedMoney = 0.00M;
            bool isOrdering = true;
            while (isOrdering)
            {
                Console.WriteLine("(1) Add Money");
                Console.WriteLine("(2) Select Products");
                Console.WriteLine("(3) Complete Transaction");

                Console.WriteLine("Current Account Balance: {0}", catering.order.AccountBalance.ToString("C2"));
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        int inputBills = 0;

                        while (true)
                        //while (addedMoney <= 100.00M)
                        {
                            try
                            {
                                Console.Write("Please enter amount\n ");
                                inputBills = int.Parse(Console.ReadLine());
                            }
                            catch
                            {
                                /* Situations that could occur:
                                 * User doesn't enter a integer
                                 * AddMoney returns false - wouldn't result in a catch
                                */
                                Console.WriteLine("Please insert a valid dollar\n");
                                continue;
                            }

                            if (!acceptableBills.Contains(inputBills))
                            {
                                Console.WriteLine("Please enter a valid bill");
                                continue;
                            }

                            if (addedMoney + inputBills > 100)
                            {
                                // Added to much money
                                Console.WriteLine("You cannot insert more than $100 at once");
                                break;
                            }
                            if (catering.order.AddMoney(inputBills))
                            {
                                // Successfully added  money
                                // AccountBalance < 1500
                                addedMoney += inputBills;
                            }
                            else
                            {
                                // Account balance would be > than 1500
                                Console.WriteLine("Account balance cannot exceed $1500\n");
                            }
                            break;
                        }

                        break;
                    case "2":
                        SelectProduct();
                        // if (catering.order.AddProductToOrder(catering.GetItem(Console.ReadLine()))) //returns true if the item was added to the order, need to remove it from inventory
                        addedMoney = 0.00M; 
                        break;
                    case "3":
                        isOrdering = false;
                        addedMoney = 0.00M;
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
            CateringItem product = catering.GetItem(Console.ReadLine().ToUpper());

            //need to check for sufficient stock, AND sufficient funds.
            if (product == null)
            {
                Console.WriteLine("Product not found.\nPlease try again.\n");
                pickingQuantity = false;
            }
            else if (catering.Inventory[product] == 0)
            {
                Console.WriteLine("Item is Sold out.\n");
                pickingQuantity = false;
            }

            while (pickingQuantity)
            {
                Console.WriteLine("Please enter a quantity:");
                try
                {
                    int quantity = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    if (quantity > catering.Inventory[product])
                    {
                        Console.WriteLine("Insufficient Stock for the quantity requested. Please enter a smaller value\n");
                        continue;
                    }
                    catering.order.AddProductToOrder(product, quantity); // *
                    pickingQuantity = false;
                } //AddProductToOrder needs to be an int return so can account for money issues, right now it's bool
                catch
                {
                    Console.WriteLine("Invalid quantity.\nPlease try again.\n");
                }

            }
        }

    }
}
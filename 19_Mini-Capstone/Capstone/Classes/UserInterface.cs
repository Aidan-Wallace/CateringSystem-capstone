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
                Console.WriteLine("(1) Display Catering Items");
                Console.WriteLine("(2) Order");
                Console.WriteLine("(3) Quit;");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1": DisplayCateringItems(); break;
                    case "2": Order(); break;
                    case "3": done = true; break;
                    default: Console.WriteLine("Please enter a valid selection."); break;
                }
            }

        }
        public void DisplayCateringItems()
        {
            Console.WriteLine();
            Console.WriteLine(String.Format("{0,-13} {1,-20} {2, -8} {3,-7}", "Product Code","Description", "Qty", "Price"));
            foreach (KeyValuePair<CateringItem, int> kvp in catering.Inventory.OrderBy(key => key.Key.ProductCode))
            {
                Console.WriteLine(String.Format("{0,-13} {1,-20} {2, -8} {3,-7}", kvp.Key.ProductCode, kvp.Key.Description, kvp.Value, kvp.Key.Price.ToString("C2")));
               // Console.WriteLine(String.Format("{0,-2} {1,-20} {2, -2} {3,-7}", kvp.Key.ProductCode, kvp.Key.Description, kvp.Value, kvp.Key.Price));
                // Console.WriteLine(String.Format("{0,-10} | {1,-10} | {2,5}", "Bill", "Gates", 51));
            }
            Console.WriteLine();
        }

        public void Order()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Catering
    {
        // This class should contain all the "work" for catering

        private List<CateringItem> items = new List<CateringItem>();
        private Dictionary<CateringItem, int> Inventory = new Dictionary<CateringItem, int>();

        public Catering ()
        {
            // Add to Inventory dictionary
            foreach(CateringItem item in items)
            {
            //items.Add();
            }
        }
    }
}

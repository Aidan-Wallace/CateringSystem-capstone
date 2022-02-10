using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Catering
    {
        // This class should contain all the "work" for catering

        public Order order { get; set; } = new Order(); 
        private List<CateringItem> items = new List<CateringItem>();
        public Dictionary<CateringItem, int> Inventory = new Dictionary<CateringItem, int>();
        
        public Catering ()
        {
            FileAccess fileOpener = new FileAccess();
            items = fileOpener.OpenItems();
            foreach (CateringItem item in items)
            {
                Inventory.Add(item, 25);
               
            }
        }
        public CateringItem GetItem(string productCode)
        {
            CateringItem foundItem = null;
            foreach (CateringItem item in items)
            {
                if (productCode == item.ProductCode)
                {
                    foundItem = item;
                }
                
            } return foundItem;
        }
        //public TakeItem 
    }
}

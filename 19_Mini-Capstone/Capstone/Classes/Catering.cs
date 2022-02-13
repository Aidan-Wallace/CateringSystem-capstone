using System;
using System.Collections.Generic;

namespace Capstone.Classes
{
    public class Catering
    {
        // This class should contain all the "work" for catering
        private Order CurrentOrder { get; set; } = new Order();
        private List<CateringItem> items = new List<CateringItem>();
        private Dictionary<CateringItem, int> inventory = new Dictionary<CateringItem, int>();

        public Catering()
        {
            FileAccess fileOpener = new FileAccess();
            items = fileOpener.OpenItems();
            foreach (CateringItem item in items)
            {
                inventory.Add(item, 25);
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
            }
            return foundItem;
        }
        public Dictionary<CateringItem, int> GetInventory() 
        {
            return inventory;
        }
        public Order GetOrder() 
        {
            return CurrentOrder;
        }
        public void TakeItem(CateringItem product, int quantity) 
        {
            inventory[product] -= quantity;
        }
    }
}

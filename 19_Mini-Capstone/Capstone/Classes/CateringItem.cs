using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class CateringItem
    {
        // This class should contain the definition for one catering item
        public string Type { get; }
        public string ProductCode { get; }
        public string Description { get; }
        public decimal Price { get; }

        public CateringItem(string type, string productCode, string description, decimal price)
        {
            Type = type;
            ProductCode = productCode;
            Description = description;
            Price = price;
        }
    }
}

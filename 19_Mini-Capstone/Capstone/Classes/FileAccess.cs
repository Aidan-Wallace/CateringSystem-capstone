using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Classes
{

    public class FileAccess
    {
        // all files for this application should in this directory
        // you will likely need to create it on your computer
        private string filePath = @"C:\Catering";
        // This class should contain any and all details of access to files

        public List<CateringItem> OpenItems()
        {
            // New list to hold new items
            List<CateringItem> populateInventory = new List<CateringItem>();

            string fileName = "cateringsystem.csv";
            string itemsPath = Path.Combine(filePath, fileName);

            using (StreamReader sr = new StreamReader(itemsPath))
            {
                while (!sr.EndOfStream)
                {
                    string currentLine = sr.ReadLine();
                    string[] splitLine = currentLine.Split("|");

                    CateringItem currentItem = new CateringItem(splitLine[0], splitLine[1], splitLine[2], decimal.Parse(splitLine[3]));

                    populateInventory.Add(currentItem);
                }
            }
            return populateInventory;
        }
    }
}

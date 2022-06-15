using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject.Models
{
    public class Product//מודל בשביל הטבלה Store
    {
        public int ProductSerialNumber { get; set; }//המספר הסידורי של המוצר
        public int Price { get; set; }// מחיר המוצר
        /// <summary>
        /// הפעולה מציגה את המודל
        /// </summary>
        /// <returns>מחרוזת של פרטי המודל</returns>
        public override string ToString()
        {
            return "product id: " + ProductSerialNumber + " price: " + Price;
        }
    }
}

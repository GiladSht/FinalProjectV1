using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject.Models
{
    public  class Purchase// מודל בשביל הטבלה Purchase
    {
        public int UserId { get; set; } //המספר סידורי של המשתמש
        public int ProductSerialNumber{ get; set; }//המספר הסידורי של המוצרים שהמשתמש מחזיק בהם 
        /// <summary>
        /// הפעולה מציגה את פרטי המודל
        /// </summary>
        /// <returns>מחרוזת של פרטי המודל</returns>
        public override string ToString()
        {
            return "User Id: " + UserId + " Serial of product: " + ProductSerialNumber;
        }
    }
}

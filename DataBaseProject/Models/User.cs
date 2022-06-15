using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject.Models
{
    public class User //מודל בשביל הטבלה USER
    {
        public int ID { get; set; }//מספר סידורי
        public string Name { get; set; }//שם משתמש
        public string Password { get; set; }//סיסמא
        public int Score { get; set; }//ניקוד שיא
        public int Money { get; set; } //הכסף שצבר המשתמש
        public int CurrentCharacter { get; set; } //דמות נוכחית של שחקן
        public string Mail { get; set; } //מייל המשתמש
    }
}

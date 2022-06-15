using DataBaseProject.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using Windows.Storage;

namespace DataBaseProject
{
    public static class DataBaseMethods//פעולות על מסד הנתונים
    {
        private static string dbPath = ApplicationData.Current.LocalFolder.Path;//מיקום מסד הנתונים
        private static string connectionString = "Filename=" + dbPath + "\\DBGame.db";//מחזרוזת המשלבת את המיקום
        /// <summary>
        ///  הפעולה יוצרת רשימה שמסודרת בסדר עולה הופכת אותה ומחזירה רשימה בסדר יורד
        /// </summary>
        /// <returns>רשימת משתמשים בסדר יורד</returns>
        public static List<User> GetUsers()
        {
            string query = $"SELECT * FROM User ORDER BY Score";
            List<User> users = new List<User>();
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open(); //פתיחת החיבור
                SqliteCommand command = new SqliteCommand(query, connection); //הפקודה
                SqliteDataReader reader = command.ExecuteReader(); //ביצוע הפקודה
                if (reader.HasRows) //האם יש נתונים
                {
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Password = reader.GetString(2),
                            Score = reader.GetInt32(3),
                            Money = reader.GetInt32(4),
                            CurrentCharacter = reader.GetInt32(5),
                            Mail = reader.GetString(6),
                        };
                        users.Add(user);

                    } 
                    
                    
                }
            }
            users.Reverse();
            return users;

        }
        /// <summary>
        /// הפעולה מתחברת למסד הנתונים ומחזירה את המשתמש אם קיים
        /// </summary>
        /// <param name="username">שם משתמש</param>
        /// <param name="password">סיסמה</param>
        /// <param name="mail">מייל</param>
        /// <returns>מחזירה את המשתמש על מנת שתוצג הסיסמה</returns>
        public static User GetUserForgetPassword(string username, string password,string mail)
        {
            string query = $"SELECT * FROM User WHERE Name= '{username}' AND Password = '{password}' AND Mail = '{mail}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open(); //פתיחת החיבור
                SqliteCommand command = new SqliteCommand(query, connection); //הפקודה
                SqliteDataReader reader = command.ExecuteReader(); //ביצוע הפקודה
                if (reader.HasRows) //האם יש נתונים
                {
                    reader.Read(); // קריאת שורה אחת
                    User user = new User
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Password = reader.GetString(2),
                        Score = reader.GetInt32(3),
                        Money = reader.GetInt32(4),
                        CurrentCharacter = reader.GetInt32(5),
                        Mail = reader.GetString(6),
                    };
                     return user;
                }
            }
            return null; // אם המשתמש לא קיים, נחזיר נאל 
        }
        /// <summary>
        /// הפעולה מתחברת למסד נתונים ומחפשת משתמש המתאים לתנאים
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>מחזירה משתמש המתאים לתנאים</returns>
        public static User GetUser(string username, string password)
        {
            string query = $"SELECT * FROM User WHERE Name= '{username}' AND Password = '{password}' ";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open(); //פתיחת החיבור
                SqliteCommand command = new SqliteCommand(query, connection); //הפקודה
                SqliteDataReader reader = command.ExecuteReader(); //ביצוע הפקודה
                if (reader.HasRows) //האם יש נתונים
                {
                    reader.Read(); // קריאת שורה אחת
                    User user = new User
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Password = reader.GetString(2),
                        Score = reader.GetInt32(3),
                        Money = reader.GetInt32(4),
                        CurrentCharacter = reader.GetInt32(5),
                        Mail = reader.GetString(6),
                    };
                    user.Money = user.Money;

                     return user;
                }
            }
            return null; // אם המשתמש לא קיים, נחזיר נאל 
        }
        /// <summary>
        /// הפעולה מוסיפה משתמש למסד הנתונים
        /// </summary>
        /// <param name="name">שם משתמש</param>
        /// <param name="password">סיסמה</param>
        /// <param name="mail">מייל</param>
        /// <returns>מחזירה את המשתמש שנרשם</returns>
        public static User AddUser(string name, string password, string mail)
        {
            User user = GetUser(name, password);  //נסיון שליפת המשתמש במאגר
            if (user != null) // משמע המשתמש כבר קיים
                return null; //הגעת למקום הלא נכון, אתה משתמש קיים גש להתחברות
            string query = $"INSERT INTO User (Name, Password, Score, Money, CurrentCharacter, Mail) VALUES('{name}', '{password}', 0, 0, 1, '{mail}')";
            Execute(query); // המשתמש החדש ברגע זה מתווסף למאגר המשתמשים  הקיימים
            user = GetUser(name, password); // קבלת המשתמש שהתווסף כרגע
            query = $"INSERT INTO Purchase (UserId, ProductSerialNumber) VALUES ({user.ID}, '1')";
            Execute(query);
            return (user);
        }
        /// <summary>
        /// מפעילה שאילתה על מסד הנתונים
        /// </summary>
        /// <param name="query"></param>
        private static void Execute(string query)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// מחזירה רשימת רכישות של המשתמש
        /// </summary>
        /// <param name="userId">המשתמש הרוצים את רכישות העבר שלו</param>
        /// <returns>מחזירה את הרשימה של הרכישות</returns>
        public static List<Purchase> GetPurchases(int userId)
        {
            List<Purchase> products = new List<Purchase>();
            string query = $"SELECT * FROM Purchase WHERE UserId = {userId} ORDER BY ProductSerialNumber";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Purchase purchase = new Purchase
                        {
                            UserId = reader.GetInt32(0),
                            ProductSerialNumber = reader.GetInt32(1),
                        };
                        products.Add(purchase);
                    }
                }
            }
            return products;
        }
        /// <summary>
        /// מחזירה רשימה של מוצרים שמתאימים למצב הפיננסי
        /// </summary>
        /// <param name="Money">כסף</param>
        /// <returns>רשימה של מוצרים המתאימים למשתמש</returns>
       public static List<Product> GetProducts(int Money)
        {
            List<Product> products = new List<Product>();
            string query = $"SELECT * FROM Store WHERE Price<={Money} ORDER BY ProductId";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            ProductSerialNumber = reader.GetInt32(0),
                            Price = reader.GetInt32(1)
                        };
                        products.Add(product);

                    }
                }
            }
            return products;
        }
        /// <summary>
        /// עדכון פרטי המשתמש
        /// </summary>
        /// <param name="user">משתמש שרוצים לעדכן את פרטיו</param>
        public static void UpdateCurrentProductAndMoney(User user)
        {
            string query = $"UPDATE User SET CurrentCharacter = {user.CurrentCharacter}, Money = {user.Money} WHERE ID = {user.ID}";
            Execute(query);
        }
        /// <summary>
        /// מעדכן את כמות הכסף של המשתמש
        /// </summary>
        /// <param name="user">משתמש שרוצים לעדכן את פרטיו</param>
        public static void UpdateMoney(User user)
        {
            string query = $"UPDATE User SET Money= {user.Money} WHERE ID = '{user.ID}'"; 
            Execute(query);
        }
        /// <summary>
        /// יוצרת שאילתה שנועדדה לרכוש מוצר
        /// </summary>
        /// <param name="userId">מספר סידורי של משתמש</param>
        /// <param name="productSerialNumber">מספר סידורי של מוצר שרוצים לקנות</param>
        /// <returns></returns>
        public static bool AddPurchase(int userId, int productSerialNumber)
        {
            if (IsExistInPurchases(userId, productSerialNumber))
                return false;

            string query = $"INSERT INTO Purchase (UserId, ProductSerialNumber) VALUES ({userId}, {productSerialNumber})";
            Execute(query);

            return true;
        }
        /// <summary>
        /// בדיקה האם משתמש רכש את המוצרים בעבר
        /// </summary>
        /// <param name="userId">מספר סידורי של משתמש</param>
        /// <param name="productSerialNumber">מספר סידורי של מוצר</param>
        /// <returns>האם למשתמש יש מוצרים</returns>
        private static bool IsExistInPurchases(int userId, int productSerialNumber)
        {
            
            string query = $"SELECT * FROM Purchase WHERE UserId= {userId} AND ProductSerialNumber={productSerialNumber}";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader =  command.ExecuteReader();

                return reader.HasRows;
            }
                
        }
        /// <summary>
        /// מפעילה שאילתה לרכישה
        /// </summary>
        /// <param name="user">משתמש</param>
        public static void ExecutePurchase(User user)
        {
            string query;
            query = $"UPDATE User SET CurrentCharacter= '{user.CurrentCharacter}',Money= '{user.Money}' WHERE ID= {user.ID}";
            Execute(query); // המשתמש החדש ברגע זה מתווסף למאגר המשתמשים  הקיימים
        }
        /// <summary>
        /// פעולה המציגה את הסיסמה של המשתמש
        /// </summary>
        /// <param name="userName">שם המשתמש </param>
        /// <param name="Mail">מייל המשתמש</param>
        /// <returns>מחזירה את המשתמש</returns>
        public static User GetUserForgotPassword(string userName, string Mail)
        {
            string query = $"SELECT * FROM [User] WHERE Name='{userName}' AND Mail='{Mail}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    User user = new User
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Password = reader.GetString(2),
                        Score = reader.GetInt32(3),
                        Money = reader.GetInt32(4),
                        CurrentCharacter = reader.GetInt32(5),
                        Mail = reader.GetString(6),
                    };
                    return user;
                }
                    
            }
            return null;
        }
        /// <summary>
        /// מעדכנת שיא ניקוד 
        /// </summary>
        /// <param name="user">משתמש</param>
        public static void UpdateHighScore(User user)
        {
            string query = $"UPDATE User SET Score= {user.Score} WHERE ID = {user.ID}";
                
            Execute(query);
        }
        /// <summary>
        /// פעולה זו משנה את הסיסמה ל המשתמש 
        /// </summary>
        /// <param name="userName">משתמש שרוצים לשנות את סיסמתו </param>
        /// <param name="Password">סיסמה חדשה</param>
        public static void ChangePassword(string userName, string Password)
        {
            string query = $"UPDATE [User] SET Password={Password} WHERE Name='{userName}' ";
            Execute(query);
            
        }
    } 
} 

        
  

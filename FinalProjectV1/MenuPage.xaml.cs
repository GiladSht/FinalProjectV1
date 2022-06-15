using DataBaseProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FinalProjectV1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MenuPage : Page//עמוד התפריט
    {
        private User user;//משתמש המשחק
        static string dbPath = ApplicationData.Current.LocalFolder.Path;//מיקום מסד הנתונים
        /// <summary>
        /// פעולה זו מציגה את הרכיבים על המסך
        /// </summary>
        public MenuPage()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// פעולה זו מקבלת את המשתמש אם קיים ושומרת אותו.
        /// היא גם מפעילה את הכפתורים שהיו נעולים לכאלה שלא התחברו
        /// </summary>
        /// <param name="e">משתמש</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            if (e.Parameter != null&& e.Parameter!="")
            {
                user = (User)e.Parameter;
                playButton.IsEnabled = true;
                LoginButton.Content = "Logout";
                StoreButton.IsEnabled = true;
                ScoreBoard.IsEnabled = true;
                optionButton.IsEnabled = true;
                ChangePassword.IsEnabled = true;
            }
            else
            {
                ChangePassword.IsEnabled = false;
                optionButton.IsEnabled = false;
                playButton.IsEnabled = false;
                StoreButton.IsEnabled = false;
                ScoreBoard.IsEnabled = false;
                LoginButton.Content = "login";
            }
        }
        /// <summary>
        /// הפעולה מעבירה את המשתמש לדף עזרה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.HelpPage),user);
        }
        /// <summary>
        /// מעבר לדף התחברות או הרשמה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.RegisterPage),user);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// מעבר לדף החנות
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StoreButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.StorePage),user);
        }
        /// <summary>
        /// מעבר לדף המשחק
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.GamePage),user);
        }
        /// <summary>
        /// מעבר לדף שיאים
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScoreBoard_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.ScoreBoard), user);
        }
        /// <summary>
        /// מעבר לדף רכישות
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.PurchasesPage),user);
        }
        /// <summary>
        /// מעב לדף שינוי סיסמה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.ChangePassword),user);
        }
    }
}

using DataBaseProject;
using DataBaseProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProjectV1.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ScoreBoard : Page//דף שיאים
    {
        private User user;//משתמש
        /// <summary>
        /// מציגה את הרכיבים
        /// </summary>
        public ScoreBoard()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// כאשר מגיעים לדף שומרים את המשתמש
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            user = (User)e.Parameter;
        }
        /// <summary>
        /// כפתור חזרה לדף הראשי
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void home_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage),user);
        }
        /// <summary>
        /// יצירת רשימה של משתמשים וסידור חמשת השחקנים המובילים בטבלה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<User> users = DataBaseMethods.GetUsers();
            for (int i = 0; i < users.Count&&i<5; i++)
            {
                if (i<users.Count) 
                {
                    TextBlock name = new TextBlock();
                    ScoreBoard1.Children.Add(name);
                    name.Text = users[i].Name;
                    name.HorizontalAlignment = HorizontalAlignment.Center;
                    name.VerticalAlignment = VerticalAlignment.Center;
                    name.FontSize = 20;
                    Grid.SetColumn(name, 1);
                    Grid.SetRow(name, i + 2);
                    TextBlock HighScore = new TextBlock();
                    HighScore.Text = users[i].Score.ToString();
                    HighScore.HorizontalAlignment = HorizontalAlignment.Center;
                    HighScore.VerticalAlignment = VerticalAlignment.Center;
                    HighScore.FontSize = 20;
                    ScoreBoard1.Children.Add(HighScore);
                    Grid.SetColumn(HighScore, 2);
                    Grid.SetRow(HighScore, i + 2);
                }
                
            }   
                   
            
        }
    }
}

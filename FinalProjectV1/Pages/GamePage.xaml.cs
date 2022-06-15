using DataBaseProject;
using DataBaseProject.Models;
using FinalProjectV1.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class GamePage : Page//דף המשחק
    {
        private User user;//משתמש
        Manager manager;//מנהל המשחק
        int score;//תוצאה
        /// <summary>
        /// מצציגה את הרכיבים
        /// </summary>
        public GamePage()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// שמירת המשתמש
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            user = (User)e.Parameter;
        }
        /// <summary>
        /// המצגת נתוני השחקן כמו מאזן כספי ,שיא נקודות ועוד
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (user != null)
            {
                user.Money+=this.score;
                DataBaseMethods.UpdateCurrentProductAndMoney(user);
                this.score = 0;
                ScoreTextBlock.Text = "Score: 0";
                manager = new Classes.Manager(arena,user);
                manager.ShowScore += Manager_ShowScore;
                manager.Restart += Manager_Restart;
                
                HighScore.Text = "high score: "+ user.Score.ToString();
                balance.Text = "balance: " + user.Money.ToString();
            }
                
            //Window.Current.CoreWindow.PointerPressed += CoreWindow_PointerPressed;
        }
        /// <summary>
        /// פעולה התתרחש כאשר השחקן נפסל ורוצים להתחיל מחדש
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Manager_Restart(object sender, EventArgs e)
        {
            manager.Restart -= Manager_Restart;

            ContentDialog PopUp = new ContentDialog()
            {
                Title = "Game over ",
                Content = "your score is " + this.score,
                CloseButtonText = "Try again"

            };
            await PopUp.ShowAsync();
           
            if (this.score > user.Score)
            {
                user.Score = this.score;
                DataBaseMethods.UpdateHighScore(user);

            }

            Frame.Navigate(typeof(GamePage),this.user);
        }
        /// <summary>
        /// כאשר נקלע סל התוצאה מתעדכנת
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Manager_ShowScore(object sender, EventArgs e)
        {
            score = (int)sender;
            ScoreTextBlock.Text = "score: "+score.ToString();
            
        }
        /// <summary>
        /// חזרה לתפריט הראשי
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void home_Click(object sender, RoutedEventArgs e)
        {
            manager.Restart -= Manager_Restart;
            Frame.Navigate(typeof(MenuPage),user);
        }
        /// <summary>
        /// כאשר נלחץ על המסך הלחיצה מעבירה למהל המשחק את
        ///הנקודה שתהיה הנקודה הגבוהה ביותר במהלך תנועת הכדור 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void arena_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            manager.Throw(e.GetCurrentPoint(arena));
        }

        

    }
}

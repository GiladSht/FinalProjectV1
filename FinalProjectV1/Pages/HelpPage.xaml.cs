using DataBaseProject.Models;
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
    public sealed partial class HelpPage : Page//עמוד עזרה
    {
        private User user;//משתמש המשחק 
        /// <summary>
        /// מציגה את הרכיבים
        /// </summary>
        public HelpPage()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// כאשר מגיעים לעמוד אם קיים משתמש העמוד שומר אותו
        /// </summary>
        /// <param name="e">משתמש</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (user != null)
                user = (User)e.Parameter;
        }
        /// <summary>
        /// כפתור חזרה לתפריט הראשי
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage),user);
        }
    }
}

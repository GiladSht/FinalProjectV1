using DataBaseProject;
using DataBaseProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class ChangePassword : Page//שינוי סיסמה
    {
        User user;//משתמש 
        /// <summary>
        /// מציגה את הרכיבים 
        /// </summary>
        public ChangePassword()
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
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        /// <summary>
        /// בדיקת הסיסמאות ואם התנאים מתקיימים שינוי הסיסמה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            if ( Password1.Password != "" && Password2.Password != "")
            {
                if (Password1.Password.Equals(Password2.Password) == true)
                {
                    user.Password = Password1.Password;
                    DataBaseMethods.ChangePassword(user.Name, user.Password);
                    Frame.Navigate(typeof(MenuPage), user);
                }
                else
                {
                    var dialog = new MessageDialog("Passwords are not the same!");
                    dialog.Title = "System notice";
                    dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                    await dialog.ShowAsync();
                }
            }
            else
            {
                var dialog = new MessageDialog("One of fields is empty!");
                dialog.Title = "System notice";
                dialog.Commands.Add(new UICommand { Label = "OK", Id = 0 });
                await dialog.ShowAsync();
            }
        }
    }
    
}

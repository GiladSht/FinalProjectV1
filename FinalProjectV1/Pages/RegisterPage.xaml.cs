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
    public sealed partial class RegisterPage : Page
    {
        private User user; 
        /// <summary>
        /// יוצרת את הרכיבים על המסך
        /// </summary>
        public RegisterPage()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// בודקת האם הפרטים שהונו תואמים למשתמש שקיים במערכת ואם כן אז מחברת את המשתמש
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void submit_Click(object sender, RoutedEventArgs e)
        {
            this.user = DataBaseMethods.GetUser(LoginUserName.Text, LoginPassword.Password);
            if (this.user != null)
                Frame.Navigate(typeof(MenuPage), this.user);
            else
            {
                // הצגת הודעה קופצת
                var dialog = new MessageDialog("User does not exist, Register first!");
                dialog.Title = "System notice";
                dialog.Commands.Add(new UICommand { Label = "ok", Id = 0 });
                await dialog.ShowAsync();
            }
        }
        /// <summary>
        /// הפעולה בודת האם הפרטים שהוזנו
        /// תקינים ומציגה הודעות מתאימות
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ForgetPassword_Click(object sender, RoutedEventArgs e)
        {
            string userName;
            string userMail;
            var nameBox = new TextBox
            {
                Width = 150,
                Height = 30,
                FontSize = 15,
                Foreground = new SolidColorBrush(Colors.Black),
                FontFamily = new FontFamily("Times New Roman"),
                PlaceholderText = "Enter username",

            };
            var mailBox = new TextBox
            {
                Width = 150,
                Height = 30,
                FontSize = 15,
                Foreground = new SolidColorBrush(Colors.Black),
                FontFamily = new FontFamily("Times New Roman"),
                PlaceholderText = "Enter mail",
            };
            var passBlock = new TextBlock
            {
                Width = 300,
                Height = 30,
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black),
                FontFamily = new FontFamily("Times New Roman"),
            };
            StackPanel panel = new StackPanel
            {
                Width = 200
            };
            panel.Orientation = Orientation.Vertical;
            panel.Children.Add(nameBox);
            panel.Children.Add(mailBox);
            ContentDialog firstPopUp = new ContentDialog()
            {
                Title = "Enter your userName and your mail: ",
                Content = panel,
                Background = new SolidColorBrush(Colors.Gray),
                Width = 400,
                PrimaryButtonText = "Ok",
                SecondaryButtonText = "Cancel",
                Foreground = new SolidColorBrush(Colors.Black),
                FontFamily = new FontFamily("Times New Roman"),

            };
            ContentDialog secondPopUp = new ContentDialog()
            {
                Title = "Your password is: ",
                Content = passBlock,
                Background = new SolidColorBrush(Colors.Gray),
                Width = 200,
                PrimaryButtonText = "Ok",
                Foreground = new SolidColorBrush(Colors.Black)

            };
            var answer = await firstPopUp.ShowAsync();
            if (answer == ContentDialogResult.Primary)
            {
                TextBox nameText = (TextBox)((StackPanel)firstPopUp.Content).Children[0];
                TextBox mailText = (TextBox)((StackPanel)firstPopUp.Content).Children[1];
                userName = nameText.Text;
                userMail = mailText.Text;
                User user = DataBaseMethods.GetUserForgotPassword(userName, userMail);
                if (user != null)
                    ((TextBlock)secondPopUp.Content).Text = user.Password;
                else
                    ((TextBlock)secondPopUp.Content).Text = "The data you entered is incorrect";
                await secondPopUp.ShowAsync();
            }
        }
        /// <summary>
        /// מחזירה את המשתמש חזרה לעמוד הבית
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage),user);
        }
        /// <summary>
        /// הפעולה בודקת תנאים להרשמה ואם לא מתקיימת תקלה יירשם משתמש חדש
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SubmitRegister_Click(object sender, RoutedEventArgs e)
        {
            if (RegisterUserName.Text != "" && email.Text != "" && password1.Password != "" && password2.Password != "")
            {
                if (password1.Password.Equals(password2.Password) == true)
                {
                    this.user = DataBaseMethods.AddUser(RegisterUserName.Text, password1.Password, email.Text);

                    if (this.user != null)
                    {
                        var dialog = new MessageDialog("User added");
                        dialog.Title = "System notice";
                        dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                        await dialog.ShowAsync();
                        Frame.Navigate(typeof(MenuPage), this.user);
                    }
                    else
                    {
                        var dialog = new MessageDialog("The user already exits!");
                        dialog.Title = "System notice";
                        dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                        await dialog.ShowAsync();
                    }
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

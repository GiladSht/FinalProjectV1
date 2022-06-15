using DataBaseProject;
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
    public sealed partial class PurchasesPage : Page//עמוד רכישות
    {
        private User user;//משתמש השחקן
        /// <summary>
        /// מציגה את הרכיבים
        /// </summary>
        public PurchasesPage()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// שמירת המשתמש
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.user = (User)e.Parameter;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Purchase> purchases = new List<Purchase>();
            if (this.user != null)
            {
                purchases = DataBaseMethods.GetPurchases(this.user.ID);
                foreach (Purchase purchase in purchases)
                                ProductsListView.Items.Add(purchase);
            }

        } 
        /// <summary>
        /// הפעולה לוקחת את רכישות העבר של המשתמש ושומרת אותו ברשימה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void home_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsListView.SelectedItem != null)             //הבחירה בוצעה

            {
                this.user.CurrentCharacter = ((Purchase)ProductsListView.SelectedItem).ProductSerialNumber;
                DataBaseMethods.ExecutePurchase(user);
                ContentDialog PopUp = new ContentDialog()
                {

                    Title = "Your storage: ",
                    Content = "your choice is accepted",
                    CloseButtonText = "Ok"
                };
                await PopUp.ShowAsync();
            }
            else
            {
                ContentDialog PopUp = new ContentDialog()
                {
                    Title = "Your storage: ",
                    Content = "you didnt choose or your list is empty",
                    CloseButtonText = "Ok"
                };
                await PopUp.ShowAsync();
            }
            Frame.Navigate(typeof(MenuPage),user);
        }
    }
}

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
    public sealed partial class StorePage : Page//עמוד החנות
    {
        List<Product> products;//רשימת רכישות עבר
        private User user;//משתמש המשחק
        private Product ChooseProduct;// המוצר הנבחר
        public StorePage()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// פעולה זו רושמת את המשתמש
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.user = (User)e.Parameter;
        }
        /// <summary>
        /// פעולת חזרה לתפריט הראשי
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void home_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage),user);
        }
        /// <summary>
        /// הפעולה בודקת את ערך הקנייה ואם הערך מתאים היא מוסיפה
        /// רכישה לטבלת הרכישות ומתיגה הודעות מתאימות האם נרכש בהצלחה
        /// מחסור או שמוצר זה כבר שייך למשתמש
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void submitPurchase_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog PopUp;
            if (StoreInput.Text=="2" || StoreInput.Text == "3" || StoreInput.Text == "4")
            {
                 foreach (Product product in products)
                {
                    if (StoreInput.Text == product.ProductSerialNumber.ToString())
                        ChooseProduct = product;
                }
                if (ChooseProduct != null)
                {
                    if (DataBaseMethods.AddPurchase(this.user.ID, this.ChooseProduct.ProductSerialNumber))
                    {
                        PopUp = new ContentDialog()
                        {
                            Content = "you succesfully purchased the item ",
                            CloseButtonText = "ok"
                        };
                        await PopUp.ShowAsync();
                        this.user.CurrentCharacter = ChooseProduct.ProductSerialNumber;
                        this.user.Money -= ChooseProduct.Price;
                        DataBaseMethods.UpdateCurrentProductAndMoney(user);

                    }
                    else
                    {
                        PopUp = new ContentDialog()
                        {
                            Content = "you have it already",
                            CloseButtonText = "ok"
                        };
                        await PopUp.ShowAsync();
                    }

                }
                else
                {
                    PopUp = new ContentDialog()
                    {
                        Content = "you doesnt have enough money to buy this item",
                        CloseButtonText = "ok"
                    };
                    await PopUp.ShowAsync();
                }

                

            }
            else
            {
                PopUp = new ContentDialog()
                {
                    Content = "you entered wrong values",
                    CloseButtonText = "ok"
                };
                await PopUp.ShowAsync();
            }
            
            //Frame.Navigate(typeof(MenuPage), user);
        }
        /// <summary>
        /// לקיחת רשימת הרכישות הקודמות של המשתמש 
        /// וכתיבת המאזן הכספי של המשתמש
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (this.user != null)
            {
                products = DataBaseMethods.GetProducts(this.user.Money);
                balance.Foreground = new SolidColorBrush(Colors.White);
                balance.Text = "balance: "+ this.user.Money.ToString();
            }
        }

       
    }
}

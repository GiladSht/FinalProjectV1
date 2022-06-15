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
    public sealed partial class OptionsPage : Page
    {
        public OptionsPage()
        {
            this.InitializeComponent();
        }

        private void VolButton_Click(object sender, RoutedEventArgs e)
        {
            if (VolText.Text == "On") VolText.Text = "Off";
            else if (VolText.Text == "Off") VolText.Text = "On";
        }

        private void MusicButton_Click(object sender, RoutedEventArgs e)
        {
            if (MusicText.Text == "On") MusicText.Text = "Off";
            else if (MusicText.Text == "Off") MusicText.Text = "On";
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace GendacCoreApp.WinPhone
{
    public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            global::Xamarin.Forms.Forms.Init();
        }

        private void btnMaps_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Maps.xaml", UriKind.Relative));
        }

        private void btnComms_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Comms.xaml", UriKind.Relative));
        }

        private void btnVision_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Vision.xaml", UriKind.Relative));
        }
    }
}

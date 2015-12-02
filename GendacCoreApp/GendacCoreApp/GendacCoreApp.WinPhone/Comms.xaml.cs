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
    public partial class Page2 : PhoneApplicationPage
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void btnBLE_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/BLE.xaml", UriKind.Relative));
        }

        private void btnCONF_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/CONF.xaml", UriKind.Relative));
        }

        private void btnNFC_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NFC.xaml", UriKind.Relative));
        }
    }
}
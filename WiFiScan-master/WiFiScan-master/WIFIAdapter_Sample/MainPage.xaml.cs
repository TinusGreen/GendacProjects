using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Devices.WiFi;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.Security.Credentials;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WIFIAdapter_Sample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(400, 600);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            WiFiList.ItemsSource = new List<string>();
        }

        WiFiAdapter wifiAdapter;
        WiFiAvailableNetwork availableNetwork;
        private Task task;
        int ScanTotal = 0;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var access = await WiFiAdapter.RequestAccessAsync();

            if (access == WiFiAccessStatus.Allowed)
            {
                DataContext = this;

                var result = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(WiFiAdapter.GetDeviceSelector());
                if (result.Count >= 1)
                {
                    wifiAdapter = await WiFiAdapter.FromIdAsync(result[0].Id);
                }
            }
            await WiFiAdapter.FindAllAdaptersAsync();
            while (true)
            {
                await wifiAdapter.ScanAsync();
               
                await Task.Delay(5000);
                    
                    List<string> ls = new List<string>();
                    List<string> sortedList = new List<string>();
                    foreach (var network in wifiAdapter.NetworkReport.AvailableNetworks)
                    {
                        var _dist = Math.Pow(10, 0.05 * (-20 * Math.Log10(network.ChannelCenterFrequencyInKilohertz / 1000) + 27.55 + Math.Abs(network.NetworkRssiInDecibelMilliwatts)));
                        ls.Add(Math.Round(_dist, 1) + "m " + " Name : " + network.Ssid + " , dB signal RSSID : " + network.NetworkRssiInDecibelMilliwatts);
                    }


                    sortedList = ls.OrderBy(s => double.Parse(s.Substring(0, s.IndexOf('m')))).ToList();

                    WiFiList.ItemsSource = sortedList;
                       
                //connectpanel.Visibility = Visibility.Collapsed;
            }
        }


        /*private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            wifiAdapter.Disconnect();
            connectpanel.Visibility = Visibility.Collapsed;
        }

        private async void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            await WiFiAdapter.FindAllAdaptersAsync();
            await wifiAdapter.ScanAsync();
            List<string> ls = new List<string>();
            foreach (var network in wifiAdapter.NetworkReport.AvailableNetworks)
            {
                var _dist = Math.Pow(10, 0.05 * (-20 * Math.Log10(network.ChannelCenterFrequencyInKilohertz / 1000) + 27.55 + Math.Abs(network.NetworkRssiInDecibelMilliwatts)));
                ls.Add("Name :" + network.Ssid + " , dB signal RSSID : " + network.NetworkRssiInDecibelMilliwatts + " , Distance : " + _dist);
            }

            WiFiList.ItemsSource = ls;
            connectpanel.Visibility = Visibility.Collapsed;
        }*/

        /*private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            WiFiReconnectionKind reconnectionKind = WiFiReconnectionKind.Manual;
            if (ConnectAutomatically.IsChecked.HasValue && ConnectAutomatically.IsChecked.Value)
            {
                reconnectionKind = WiFiReconnectionKind.Automatic;
            }

            if (availableNetwork.SecuritySettings.NetworkAuthenticationType == NetworkAuthenticationType.Open80211)
            {
                await wifiAdapter.ConnectAsync(availableNetwork, reconnectionKind);
            }
            else
            {
                var credential = new PasswordCredential();
                if (!string.IsNullOrEmpty(securityKey.Password))
                {
                    credential.Password = securityKey.Password;
                }

                await wifiAdapter.ConnectAsync(availableNetwork, reconnectionKind, credential);
            }
        }*/

        /*private void WiFiList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as string;
            foreach (var selecteditem in wifiAdapter.NetworkReport.AvailableNetworks)
            {
                if (selecteditem.Ssid == item)
                {
                    availableNetwork = selecteditem;
                }
            }

            connectpanel.Visibility = Visibility.Visible;
            if (availableNetwork.SecuritySettings.NetworkAuthenticationType == NetworkAuthenticationType.Open80211)
            {
                Credentials.Visibility = Visibility.Collapsed;
            }
            else
            {
                Credentials.Visibility = Visibility.Visible;
            }
        }*/
    }
}

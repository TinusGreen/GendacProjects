using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MobileCRM.Shared.Pages
{
    public class Comms : ContentPage
    {
        Geocoder geoCoder = new Geocoder();

        // Dictionary to get Color from color name.
        Dictionary<string, int> nameToFunction = new Dictionary<string, int>
        {
            { "Bluetooth", 1 }, { "NFC", 2}
        };

        public Comms()
        {
            Label header = new Label
            {
                Text = "Please choose a function",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start

            };

            Picker picker = new Picker
            {
                Title = "Function",
                VerticalOptions = LayoutOptions.Start
            };

            foreach (string functionName in nameToFunction.Keys)
            {
                picker.Items.Add(functionName);
            }

            Label function = new Label
            {
                Text = "Function Call",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };

            Label lblScan = new Label
            {
                Text = "Please scan tag now...",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                IsVisible = false
            };

            Button btnGeo = new Button()
            {
                Text = "GeoTest",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End
            };

            btnGeo.Clicked += async (sender, args) =>
            {
                var address = "72 Marija street Wonderboom";
                var approximateLocations = await geoCoder.GetPositionsForAddressAsync(address);
                foreach (var position in approximateLocations)
                {
                    function.Text += position.Latitude + ", " + position.Longitude + "\n";
                }
            };

            picker.SelectedIndexChanged += (sender, args) =>
            {
                if (picker.SelectedIndex == -1)
                {
                    lblScan.IsVisible = false;
                }
                else
                {
                    string functionName = picker.Items[picker.SelectedIndex];
                    if (nameToFunction[functionName] == 1)
                    {
                        function.Text = "Bluetooth Low Energy Selected";
                        lblScan.IsVisible = true;
                    }
                    else
                    {
                        if (nameToFunction[functionName] == 2)
                        {
                            function.Text = "Near field communication Selected";
                            lblScan.IsVisible = true;
                        }
                    }
                }
            };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    picker,
                    function,
                    lblScan,
                    btnGeo
                }
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MobileCRM.Shared.Pages
{
    public class Vision : ContentPage
    {
        // Dictionary to get Color from color name.
        Dictionary<string, int> nameToFunction = new Dictionary<string, int>
        {
            { "QR Code", 1 }, { "Lisence Plate", 2},
            {"Odometer", 3 }, {"ID Document", 4 },
            {"Drivers Lisence", 5 }
        };

        public Vision()
        {
            Label header = new Label
            {
                Text = "Please choose a object",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start

            };

            Picker picker = new Picker
            {
                Title = "Objects",
                VerticalOptions = LayoutOptions.Start
            };

            foreach (string functionName in nameToFunction.Keys)
            {
                picker.Items.Add(functionName);
            }

            Label function = new Label
            {
                Text = "Object Call",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };

            Label lblScan = new Label
            {
                Text = "Please select an image now",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                IsVisible = false
            };

            Button btnImage = new Button
            {
                Text = "Select an image",
                VerticalOptions = LayoutOptions.End,
                IsVisible = false
            };

            Button btnPhoto = new Button
            {
                Text = "Take a photo",
                VerticalOptions = LayoutOptions.End,
                IsVisible = false
            };

            picker.SelectedIndexChanged += (sender, args) =>
            {
                if (picker.SelectedIndex == -1)
                {
                    lblScan.IsVisible = false;
                    btnPhoto.IsVisible = false;
                    btnImage.IsVisible = false;
                }
                else
                {
                    string functionName = picker.Items[picker.SelectedIndex];
                    if (nameToFunction[functionName] == 1)
                    {
                        function.Text = "QR Code selected";
                        lblScan.IsVisible = true;
                        btnPhoto.IsVisible = true;
                        btnImage.IsVisible = true;
                    }
                    else
                    {
                        if (nameToFunction[functionName] == 2)
                        {
                            function.Text = "Lisence plate selected";
                            lblScan.IsVisible = true;
                            btnPhoto.IsVisible = true;
                            btnImage.IsVisible = true;
                        }
                        else
                        {
                            if (nameToFunction[functionName] == 3)
                            {
                                function.Text = "Odometer selected";
                                lblScan.IsVisible = true;
                                btnPhoto.IsVisible = true;
                                btnImage.IsVisible = true;
                            }
                            else
                            {
                                if (nameToFunction[functionName] == 4)
                                {
                                    function.Text = "ID document selected";
                                    lblScan.IsVisible = true;
                                    btnPhoto.IsVisible = true;
                                    btnImage.IsVisible = true;
                                }
                                else
                                {
                                    if (nameToFunction[functionName] == 1)
                                    {
                                        function.Text = "Drivers lisence selected";
                                        lblScan.IsVisible = true;
                                        btnPhoto.IsVisible = true;
                                        btnImage.IsVisible = true;
                                    }
                                }
                            }
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
                    btnImage,
                    btnPhoto
                }
            };
        }
    }
}

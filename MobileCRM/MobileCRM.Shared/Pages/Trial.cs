using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using MobileCRM.Shared.Pages;
using MobileCRM.Models;
using MobileCRM.Services;
using System.Collections.Generic;

namespace MobileCRM.Shared.Pages
{
    public class Trail : ContentPage
    {
        public Trail()
        {
            Button btnMaps = new Button
            {
                Text = "Map App",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };

            btnMaps.Clicked += async (sender, args) =>
            {
                await Navigation.PushModalAsync(new MainPage());

            };
            Content = new StackLayout
            {
                Children =
                {
                    btnMaps
                }
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobileCRM;
using MobileCRM.Shared.Pages;


using Xamarin.Forms;

namespace MobileCRM.Shared.Pages
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            Title = "Main Page";

            Label lblText = new Label
            {
                Text = "Welcome to the Gendac Trial App. This App is used to test various features for future used. This main focus on Xamarin with subcomponents in communication and vision systems.",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };

            Button btnMaps = new Button
            {
                Text = "Map App",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };

            btnMaps.Clicked += async (sender, args) =>
            {
                await Navigation.PushModalAsync(new RootPage());

            };

            Button btnComms = new Button
            {
                Text = "Communication Systems",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };

            btnComms.Clicked += async (sender, args) =>
            {
                await Navigation.PushModalAsync(new Comms());
            };

            Button btnVision = new Button
            {
                Text = "Computer Vision Systems",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };

            btnVision.Clicked += async (sender, args) =>
            {
                await Navigation.PushModalAsync(new Vision());
            };

            Content = new StackLayout
            {
                Children =
                {
                    lblText,
                    btnMaps,
                    btnComms,
                    btnVision
                }
            };
        }


    }
}

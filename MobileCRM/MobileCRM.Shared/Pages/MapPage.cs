using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileCRM.Models;
using MobileCRM.Shared.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

/*

Xamarin.Forms uses the native MAP control on each platform.

Both Android and Windows Phone require additional configuration to make MAPs work.

See the document here for more information:
http://developer.xamarin.com/guides/cross-platform/xamarin-forms/working-with/maps/

If you see either of these errors in the console output when running on Android, you have not correctly configured your Google Maps API v2 Key.

[Google Maps Android API] Failed to contact Google servers. Another attempt will be made when connectivity is established.
[Google Maps Android API] Failed to load map. Error contacting Google servers. This is probably an authentication issue (but could be due to network errors).

Refer to the notes in the MainActivity.cs file in the Android project for how to add an API Key.

*/
using MobileCRM.Shared.Helpers;


namespace MobileCRM.Shared.Pages
{
    public class MapPage<T> : ContentPage where T: class, IContact, new()
    {
        private Map _map;
        private Slider slider;
        private double latlongdegrees;

        private MapViewModel<T> ViewModel
        {
            get { return BindingContext as MapViewModel<T>; }
        }

        private bool updateMap()
        {
            //double temp_scale = _map.Scale;
            _map.IsShowingUser = false;
            _map.IsShowingUser = true;
            return true;
        }

        private bool updateMapDisp()
        {
            if (_map.VisibleRegion != null)
            {

                _map.MoveToRegion(new MapSpan(_map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
            }
            return true;
        }

        public MapPage(MapViewModel<T> viewModel)
        {
            BindingContext = viewModel;

            this.SetBinding(Page.TitleProperty, "Title");
            this.SetBinding(Page.IconProperty, "Icon");

            _map = MakeMap();
            slider = new Slider(1, 18, 1);
            slider.Value = 12;
            latlongdegrees = 360 / (Math.Pow(2, slider.Value));

            slider.ValueChanged += (sender, e) => {
                var zoomLevel = e.NewValue; // between 1 and 18
                latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
                _map.MoveToRegion(new MapSpan(_map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
            };

            Dictionary<string, Int16> nameToColor = new Dictionary<string, Int16>
            {
                { "Street", 1 }, { "Hybrid", 2 },
                { "Saterlite", 3 }
            };

            Picker picker = new Picker
            {
                Title = "View",
            };

            foreach (string colorName in nameToColor.Keys)
            {
                picker.Items.Add(colorName);
            }

            picker.SelectedIndexChanged += (sender, args) =>
            {
                if (picker.SelectedIndex == -1)
                {
                    _map.MapType = MapType.Street;
                }
                else
                {
                    string viewName = picker.Items[picker.SelectedIndex];
                    if (nameToColor[viewName] == 1)
                    {
                        _map.MapType = MapType.Street;
                    }
                    if (nameToColor[viewName] == 2)
                    {
                        _map.MapType = MapType.Hybrid;
                    }
                    if (nameToColor[viewName] == 3)
                    {
                        _map.MapType = MapType.Satellite;
                    }

                }
            };

            var stack = new StackLayout { Spacing = 0 };
            Device.StartTimer(TimeSpan.FromSeconds(10), updateMap);
            Device.StartTimer(TimeSpan.FromSeconds(1), updateMapDisp);







#if __ANDROID__ || __IOS__
            var searchAddress = new SearchBar { Placeholder = "Search Address", BackgroundColor = Xamarin.Forms.Color.White };

            searchAddress.SearchButtonPressed += async (e, a) =>
            {
                var addressQuery = searchAddress.Text;
                searchAddress.Text = "";
                searchAddress.Unfocus();

                var positions = (await (new Geocoder()).GetPositionsForAddressAsync(addressQuery)).ToList();
                if (!positions.Any())
                    return;

                var position = positions.First();
                _map.MoveToRegion(MapSpan.FromCenterAndRadius(position,
                        Distance.FromMiles(0.1)));
                _map.Pins.Add(new Pin
                    {
                        Label = addressQuery,
                        Position = position,
                        Address = addressQuery
                    });
            };

          //  stack.Children.Add(searchAddress);

#elif WINDOWS_PHONE
            //    ToolbarItems.Add(new ToolbarItem("filter", "filter.png", async () =>
            //     {
            //         var page = new ContentPage();
            //         var result = await page.DisplayAlert("Title", "Message", "Accept", "Cancel");
            //         Debug.WriteLine("success: {0}", result);
            //     }));

            //   ToolbarItems.Add(new ToolbarItem("search", "search.png", () =>
            //     {
            //     }));
#endif

            _map.VerticalOptions = LayoutOptions.FillAndExpand;
            _map.HeightRequest = 100;
            _map.WidthRequest = 960;

            stack.Children.Add(_map);
            stack.Children.Add(slider);
            stack.Children.Add(picker);
            Content = stack;
        }

        public Map MakeMap()
        {
         //   List<Pin> pins = ViewModel.LoadPins();

            // TODO: Uncomment once Xamarin.Forms supports this, hopefully w/ version 1.1.
            //var dict = pins.Zip(ViewModel.Models, (p, m)=>new KeyValuePair<Pin,T>(p, m)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            //PinMap = dict;

            Map map = new Map();

          //  if (pins.Count < 1)
        //    {
       //         map = new Map();
        //    }
       //     else
       //     {
        //       Pin centerPin = pins[0];
//
       //      if (pins.Count == 1)
       //      {
       //            map = new Map(MapSpan.FromCenterAndRadius(centerPin.Position, Distance.FromKilometers(0.25)));
       //        }
       //         else
       //         {
       //             IEnumerable<Pin> otherPins = pins.Where(p => p != pins[0]);
       //
       //             double radiusFromCenter = GetGreatestDistanceBetweenCenterPinAndOtherPinsInKilometers(centerPin, otherPins);
//
       //             map = new Map(MapSpan.FromCenterAndRadius(centerPin.Position, Distance.FromKilometers(radiusFromCenter + 0.25)));
       //         }
       //     }

            List<Pin> toets = new List<Pin>();
            map.IsShowingUser = true;

            

            double lon = 028.2736;
            double lat = -25.7459;

            var position = new Position(lat, lon); 
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "My eie pin"
            };

            toets.Add(pin);

            //map.Pins.Add(pin);

            double lon2 = 028.2738;
            double lat2 = -25.7451;

            var position2 = new Position(lat2, lon2);
            var pin2 = new Pin
            {
                Type = PinType.Place,
                Position = position2,
                Label = "My eie pin2222"
            };
            toets.Add(pin2);

          //  map.Pins.Add(pin2);


            toets.ForEach(map.Pins.Add);

            // TODO: Uncomment once Xamarin.Forms supports this, hopefully w/ version 1.1.
//            map.PinSelected += (sender, args)=>
//            {
//                var pin = args.SelectedItem as Pin;
//                var details = PinMap[pin];
//                var page = new DetailPage<T>(details);
//                Navigation.PushAsync(page);
//            };

           return map;
        }

        private double GetGreatestDistanceBetweenCenterPinAndOtherPinsInKilometers(Pin centerPin, IEnumerable<Pin> otherPins)
        {
            double greatestDistanceInKm = 0;

            LatLon centerLatLon = new LatLon(centerPin.Position.Latitude, centerPin.Position.Longitude);

            otherPins.ToList().ForEach(p =>
                {
                    LatLon otherLatLon = new LatLon(p.Position.Latitude, p.Position.Longitude);

                    double distance = Haversine.GetDistanceKM(centerLatLon, otherLatLon);

                    if (distance > greatestDistanceInKm)
                    {
                        greatestDistanceInKm = distance;
                    }
                });

            return greatestDistanceInKm;
        }
    }
}

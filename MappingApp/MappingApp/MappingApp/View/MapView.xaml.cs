using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using MappingApp.Services;
using MappingApp.ViewModel;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;

namespace MappingApp.View
{
    public partial class MapView
    {
        private MapViewModel _mapViewModel;
        private Pin _previousPin;
        const string apiUrl = @"http://41.180.4.119:56302/api/contact";

        public MapView()
        {
            InitializeComponent();
          //  _mapViewModel = Resolver.Resolve<MapViewModel>();
           // _mapViewModel.PositionChanged += (sender, args) => Device.BeginInvokeOnMainThread(UpdatePosition);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _mapViewModel = BindingContext as MapViewModel;
            if (_mapViewModel != null)
            {
                GetWeb();
                _mapViewModel.PositionChanged += OnPositionChange;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (_mapViewModel != null)
            {
                _mapViewModel.PositionChanged -= OnPositionChange;
            }
        }

        private void OnPositionChange(object sender, EventArgs args)
        {
            Device.BeginInvokeOnMainThread(UpdatePosition);
        }

        private void UpdatePosition()
        {
            if (_previousPin != null)
            {
                _map.Pins.Remove(_previousPin);
            }
            else
            {
                _previousPin = new Pin();
            }

            _map.Pins.Clear();

            var zoomLat = _map.VisibleRegion == null ? 6.0 : _map.VisibleRegion.LatitudeDegrees;
            var zoomLon = _map.VisibleRegion == null ? 6.0 : _map.VisibleRegion.LongitudeDegrees;
            var zoomRad = _map.VisibleRegion == null ? 6.0 : _map.VisibleRegion.Radius.Miles;
            
            _map.MoveToRegion(new MapSpan(_mapViewModel.UserPosition, zoomLat, zoomLon ));

            // _map.MoveToRegion(MapSpan.FromCenterAndRadius(_mapViewModel.UserPosition, Distance.FromMiles(zoomRad)));
            MapSpan temp = _map.VisibleRegion;

            GetWeb();

            //Add the pin.
            Position pinPos = new Position(_mapViewModel.UserPosition.Latitude, _mapViewModel.UserPosition.Longitude);
            _previousPin.Position = pinPos;
            _previousPin.Label = "My Location";
            _previousPin.Type = PinType.Generic;
            _map.Pins.Add(_previousPin);

            UpdateServerUser();
        }

        private async void UpdateServerUser()
        {
            HttpClient webClient = new HttpClient();
            string name = UserN.Text;
            string t1 = "1";
            string t2 = "test";
            if (name == "")
            {
                t2 = _previousPin.Label;
            }
            else
            {
                t2 = name;
            }
            string t3 = _previousPin.Position.Latitude.ToString();
            string t4 = _previousPin.Position.Longitude.ToString();

            var uri = string.Format(@"http://41.180.4.119:56302/api/contact?Id={0}&User={1}&Lat={2}&Lon={3}", t1, t2, t3, t4);
            var response2 = await webClient.GetStringAsync(uri);
        }

        private async void GetWeb()
        {
            HttpClient webClient = new HttpClient();
            try
            {
                //var response = await webClient.GetAsync(apiUrl);
                //    List<Person> people = new List<Person>();
                var response = await webClient.GetStringAsync(apiUrl);
                var people = JsonConvert.DeserializeObject<List<Person>>(response);

                //Remove old pins 
<<<<<<< HEAD
              //  _map.Pins.Clear();
=======
                //_map.Pins.Clear();
>>>>>>> 64cd4717bdc84e0b3e0b889a2cc3b5fdf6e15fb0

                //Convert the persons to pins.
                foreach (Person p in people)
                {
<<<<<<< HEAD
                    if (p.Name != null)
                    {
                        Pin temp = new Pin();
                        temp.Label = p.Name;
                        Position pTemp = new Position(Convert.ToDouble(p.Lat), Convert.ToDouble(p.Long));
                        temp.Position = pTemp;
                        temp.Type = PinType.Generic;
                        _map.Pins.Add(temp);
                    }
=======
                    Pin temp = new Pin();
                    temp.Label = p.Name;
                    double lat = double.Parse(p.Lat, System.Globalization.CultureInfo.InvariantCulture);
                    double lon = double.Parse(p.Long, System.Globalization.CultureInfo.InvariantCulture);
                  //  double lat = Convert.ToDouble(p.Lat);
                 //   double lon = Convert.ToDouble(p.Long);
                    Position pTemp = new Position(lat, lon);
                    temp.Position = pTemp;
                    temp.Type = PinType.Generic;
                    _map.Pins.Add(temp);
>>>>>>> 64cd4717bdc84e0b3e0b889a2cc3b5fdf6e15fb0
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

        }
    }
}
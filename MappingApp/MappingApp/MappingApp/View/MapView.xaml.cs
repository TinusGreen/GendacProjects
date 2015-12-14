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
        const string apiUrl = @"http://172.28.2.83:56302/api/contact";

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
                _map.Pins.Clear();

                //Convert the persons to pins.
                foreach (Person p in people)
                {
                    Pin temp = new Pin();
                    temp.Label = p.Name;
                    Position pTemp = new Position(Convert.ToDouble(p.Lat), Convert.ToDouble(p.Long));
                    temp.Position = pTemp;
                    temp.Type = PinType.Generic;
                    _map.Pins.Add(temp);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

        }
    }
}
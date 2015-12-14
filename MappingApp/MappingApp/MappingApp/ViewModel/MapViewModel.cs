using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using MappingApp.Services;
using MappingApp.View;
using Newtonsoft.Json;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Services.Geolocation;
using Position = Xamarin.Forms.Maps.Position;

namespace MappingApp.ViewModel
{
    public class MapViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        private readonly IGeolocator _geolocator;
        private string _heading = "Mapping System";
        const string apiUrl = @"http://172.28.2.83:56302/api/contact";
        private Position _userPosition;

        public MapViewModel()
        {
            _geolocator = Resolver.Resolve<IGeolocator>();
           // GetWeb();
            GetPosition();
            NavigateToBack = new Command(() => Navigation.PopAsync());
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

                

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

        }

        public Command NavigateToBack { get; }


        public string Heading
        {
            get { return _heading; }
            set { SetProperty(ref _heading, value, () => Heading); }
        }

        public Position UserPosition
        {
            get { return _userPosition; }
            set
            {
                SetProperty(ref _userPosition, value, () => UserPosition);
                if (PositionChanged != null)
                {
                    PositionChanged(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler PositionChanged;

        private async void GetPosition()
        {
            try
            {
                var position = await _geolocator.GetPositionAsync(10000);
                UserPosition = position == null
                    ? new Position(0, 0)
                    : new Position(position.Latitude, position.Longitude);
                _geolocator.PositionChanged +=
                (sender, args) => UserPosition = new Position(args.Position.Latitude, args.Position.Longitude);
                _geolocator.StartListening(10000, 5);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
    }
}
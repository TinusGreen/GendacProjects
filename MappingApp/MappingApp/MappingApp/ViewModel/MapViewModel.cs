using System;
using System.Diagnostics;
using XLabs.Ioc;
using XLabs.Platform.Services.Geolocation;
using Position = Xamarin.Forms.Maps.Position;

namespace MappingApp.ViewModel
{
    public class MapViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        private readonly IGeolocator _geolocator;
        private string _heading = "Mapping System";

        private Position _userPosition;

        public MapViewModel()
        {
            _geolocator = Resolver.Resolve<IGeolocator>();
            GetPosition();
        }

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
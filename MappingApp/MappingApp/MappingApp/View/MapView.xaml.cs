using MappingApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MappingApp.View
{
    public partial class MapView
    {
        private readonly MapViewModel _mapViewModel;
        private Pin _previousPin;

        public MapView()
        {
            InitializeComponent();
            _mapViewModel = ViewModelLocator.Map;
            BindingContext = _mapViewModel;
            _mapViewModel.PositionChanged += (sender, args) => Device.BeginInvokeOnMainThread(UpdatePosition);
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

            //Add the pin.
            Position pinPos = new Position(_mapViewModel.UserPosition.Latitude, _mapViewModel.UserPosition.Longitude);
            _previousPin.Position = pinPos;
            _previousPin.Label = "My Location";
            _previousPin.Type = PinType.Generic;

            var zoomLat = _map.VisibleRegion == null ? 6.0 : _map.VisibleRegion.LatitudeDegrees;
            var zoomLon = _map.VisibleRegion == null ? 6.0 : _map.VisibleRegion.LongitudeDegrees;
            _map.MoveToRegion(new MapSpan(_mapViewModel.UserPosition, zoomLat, zoomLon));

            _map.Pins.Add(_previousPin);
        }
    }
}
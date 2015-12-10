using System;
using MappingApp.ViewModel;
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

            //Add the pin.
            Position pinPos = new Position(_mapViewModel.UserPosition.Latitude, _mapViewModel.UserPosition.Longitude);
            _previousPin.Position = pinPos;
            _previousPin.Label = "My Location";
            _previousPin.Type = PinType.Generic;

            var zoomLat = _map.VisibleRegion == null ? 6.0 : _map.VisibleRegion.LatitudeDegrees;
            var zoomLon = _map.VisibleRegion == null ? 6.0 : _map.VisibleRegion.LongitudeDegrees;
            var zoomRad = _map.VisibleRegion == null ? 6.0 : _map.VisibleRegion.Radius.Miles;
            
            _map.MoveToRegion(new MapSpan(_mapViewModel.UserPosition, zoomLat, zoomLon ));

            // _map.MoveToRegion(MapSpan.FromCenterAndRadius(_mapViewModel.UserPosition, Distance.FromMiles(zoomRad)));
            _map.Pins.Add(_previousPin);
            MapSpan temp = _map.VisibleRegion;
        }
    }
}
using MappingApp.View;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Platform.Services.Geolocation;

namespace MappingApp.ViewModel
{
    public class MainViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        private string _heading = "Main Page";


        public MainViewModel()
        {
            NavigateToMappingView = new Command(() => Navigation.PushAsync<MapViewModel>());
            NavigateToCommunicationView = new Command(() => Navigation.PushAsync<CameraViewModel>());
            NavigateToComputerView = new Command(() => Navigation.PushAsync<CameraViewModel>());
            NavigateToCodeView = new Command(() => Navigation.PushAsync<CodeScannerViewModel>());
        }

        public Command NavigateToMappingView { get; }
        public Command NavigateToCommunicationView { get; }
        public Command NavigateToComputerView { get; }
        public Command NavigateToCodeView { get; }

        public string Heading
        {
            get { return _heading; }
            set
            {
                SetProperty(ref _heading, value, () => Heading);
            }
        }

        
    }
}
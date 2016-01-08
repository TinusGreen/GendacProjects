using MappingApp.View;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Platform.Services.Geolocation;

//This is the homepage for the application with buttons to navigate to the various components. This page becomes a NavigationPage and links to the MainView.xaml
namespace MappingApp.ViewModel
{
    public class MainViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        //Heading binding
        private string _heading = "Main Page";

        //Init
        public MainViewModel()
        {
            //Navigation to various pages in the app.
            NavigateToMappingView = new Command(() => Navigation.PushAsync<MapViewModel>());
            NavigateToCommunicationView = new Command(() => Navigation.PushAsync<CommunicationViewModel>());
            NavigateToComputerView = new Command(() => Navigation.PushAsync<CameraViewModel>());
            NavigateToCodeView = new Command(() => Navigation.PushAsync<CodeScannerViewModel>());
            NavigateToNavigationView = new Command(() => Navigation.PushAsync<NavigationViewModel>());
        }

        //Command bindigs
        public Command NavigateToMappingView { get; }
        public Command NavigateToCommunicationView { get; }
        public Command NavigateToComputerView { get; }
        public Command NavigateToCodeView { get; }
        public Command NavigateToNavigationView { get; }

        //Text bindings
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
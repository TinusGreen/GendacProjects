using MappingApp.View;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Platform.Services.Geolocation;

namespace MappingApp.ViewModel
{
    public class MainViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        private Command _navigateToViewModel;
        private string _navigateToViewModelButtonText = "Navigate to another view model";
        private string _address = "Hello World";
        public string Address
        {
            get { return _address; }
            set
            {
                SetProperty(ref _address, value, () => Address);
            }
        }


        public string NavigateToViewModelButtonText
        {
            get
            {
                return _navigateToViewModelButtonText;
            }
            set
            {
                SetProperty(ref _navigateToViewModelButtonText, value);
            }
        }
    }

    
}
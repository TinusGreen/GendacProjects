using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Platform.Services.Geolocation;

namespace MappingApp.ViewModel
{
    public class MainViewModel : XLabs.Forms.Mvvm.ViewModel
    {
       
        private string _address = "Hello World";
        public string Address
        {
            get { return _address; }
            set
            {
                SetProperty(ref _address, value, () => Address);
            }
        }
    }

    
}
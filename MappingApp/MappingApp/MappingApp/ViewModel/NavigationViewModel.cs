using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Xamarin.Forms;

namespace MappingApp.ViewModel
{
    class NavigationViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        private string _address;
        private string _mainText;

        public NavigationViewModel()
        {
            _mainText = "Navigation System";
            _address = "Please type in address";
            Navigate = new Command(() => ScanCode());

        }

        public Command Navigate { get; private set; }

        private void ScanCode()
        {
            var address = Address;

            switch (Device.OS)
            {
                case TargetPlatform.iOS:
                    Device.OpenUri(
                      new Uri(string.Format("http://maps.apple.com/?q={0}", WebUtility.UrlEncode(address))));
                    break;
                case TargetPlatform.Android:
                    Device.OpenUri(
                      new Uri(string.Format("geo:0,0?q={0}", WebUtility.UrlEncode(address))));
                    break;
                case TargetPlatform.Windows:
                case TargetPlatform.WinPhone:
                    Device.OpenUri(
                      new Uri(string.Format("bingmaps:?where={0}", Uri.EscapeDataString(address))));
                    break;
            }
        }

        public string MainText
        {
            get { return _mainText; }
            set
            {
                SetProperty(ref _mainText, value, () => MainText);
            }
        }

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

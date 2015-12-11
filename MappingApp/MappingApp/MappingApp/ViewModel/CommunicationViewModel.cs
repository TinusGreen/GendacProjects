using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;
using MappingApp.View;

namespace MappingApp.ViewModel
{
    class CommunicationViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        private ObservableCollection<String> _menuOptions = new ObservableCollection<string>();

        private string _status;
        private string _heading = "Communication System";

        public string Heading
        {
            get { return _heading; }
            set { SetProperty(ref _heading, value, () => Heading); }
        }

        public IEnumerable<string> MenuOptions
        {
            get
            {
                if (_menuOptions.Count == 0)
                {
                    _menuOptions.Add("BLE");
                    _menuOptions.Add("NFC");
                }
                return _menuOptions;
            }
        }

        private String _selectedOption;

        public String SelectedOption
        {
            get { return _selectedOption; }
            set { _selectedOption = value; }
        }

        

    }
}

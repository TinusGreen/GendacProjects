using System.Threading.Tasks;
using MappingApp.View;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using ZXing.Mobile;
using System.Collections.Generic;

namespace MappingApp.ViewModel
{
    public class CodeScannerViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        private string _heading = "Code Scanner Page";


        /// <summary>
        /// The process info
        /// </summary>
        private string _info;


        public CodeScannerViewModel()
        {
            CodeScan = new Command(async () => await ScanCode());
            NavigateToBack = new Command(() => Navigation.PopAsync());
        }

        public Command NavigateToBack { get; }

        public Command CodeScan { get; private set; }

        public async Task ScanCode()
        {
            MobileBarcodeScanner scanner;
            scanner = new MobileBarcodeScanner();
            

            

  
                       
            //scannerO.TryHarder = true;
            
            //Tell our scanner to use the default overlay
            scanner.UseCustomOverlay = false;
            //We can customize the top and bottom text of our default overlay
            scanner.TopText = "Hold camera up to barcode";
            scanner.BottomText = "Camera will automatically scan barcode\r\n\r\nPress the 'Back' button to Cancel";
           

            var options = new MobileBarcodeScanningOptions();
            //options.CameraResolutionSelector = ;
            //options.PossibleFormats = new List<ZXing.BarcodeFormat>() {ZXing.BarcodeFormat.Ean8, ZXing.BarcodeFormat.Ean13};



            //options.PossibleFormats = new List<ZXing.BarcodeFormat>() { ZXing.BarcodeFormat.PDF_417 };
            //options.TryHarder = false;
           
            //options.UseNativeScanning = true;

 
            var result = await scanner.Scan(options);
            HandleScanResult(result);
        }

        public string Info
        {
            get
            {
                return _info;
            }
            set
            {
                SetProperty(ref _info, value, () => Info);
            }
        }

        void HandleScanResult(ZXing.Result result)
        {
            string msg = "";

            if (result != null && !string.IsNullOrEmpty(result.Text))
                if (result.Text[0] == '%')
                {
                    string _edit = result.Text;
                    msg = _edit;
                    msg = msg + "\n\n" + "--- Car registration found ---";

                    for (int k = 0; k < 5; k++) {
                        while (_edit[0] != '%')
                        {
                            _edit = _edit.Substring(1,_edit.Length-1);

                        }
                        _edit = _edit.Substring(1, _edit.Length - 1);
                    }

                    msg = msg + "\nNr: ";
                    while (_edit[0] != '%')
                    {
                        msg = msg + _edit.Substring(0, 1);
                        _edit = _edit.Substring(1, _edit.Length - 1);
                    }
                    _edit = _edit.Substring(1, _edit.Length - 1);


                    msg = msg + "\nNumber plate: ";
                    while (_edit[0] != '%')
                    {
                        msg = msg + _edit.Substring(0,1);
                        _edit = _edit.Substring(1, _edit.Length - 1);
                    }
                    _edit = _edit.Substring(1, _edit.Length - 1);


                    msg = msg + "\nVeh. reg: ";
                    while (_edit[0] != '%')
                    {
                        msg = msg + _edit.Substring(0, 1);
                        _edit = _edit.Substring(1, _edit.Length - 1);
                    }
                    _edit = _edit.Substring(1, _edit.Length - 1);

                    msg = msg + "\nType: ";
                    while (_edit[0] != '%')
                    {
                        msg = msg + _edit.Substring(0, 1);
                        _edit = _edit.Substring(1, _edit.Length - 1);
                    }
                    _edit = _edit.Substring(1, _edit.Length - 1);


                    msg = msg + "\nManufacturer: ";
                    while (_edit[0] != '%')
                    {
                        msg = msg + _edit.Substring(0, 1);
                        _edit = _edit.Substring(1, _edit.Length - 1);
                    }
                    _edit = _edit.Substring(1, _edit.Length - 1);

                    msg = msg + "\nModel: ";
                    while (_edit[0] != '%')
                    {
                        msg = msg + _edit.Substring(0, 1);
                        _edit = _edit.Substring(1, _edit.Length - 1);
                    }
                    _edit = _edit.Substring(1, _edit.Length - 1);

                    msg = msg + "\nColour: ";
                    while (_edit[0] != '%')
                    {
                        msg = msg + _edit.Substring(0, 1);
                        _edit = _edit.Substring(1, _edit.Length - 1);
                    }
                    _edit = _edit.Substring(1, _edit.Length - 1);

                    msg = msg + "\nVIN: ";
                    while (_edit[0] != '%')
                    {
                        msg = msg + _edit.Substring(0, 1);
                        _edit = _edit.Substring(1, _edit.Length - 1);
                    }
                    _edit = _edit.Substring(1, _edit.Length - 1);


                    msg = msg + "\nEngin no: ";
                    while (_edit[0] != '%')
                    {
                        msg = msg + _edit.Substring(0, 1);
                        _edit = _edit.Substring(1, _edit.Length - 1);
                    }
                    _edit = _edit.Substring(1, _edit.Length - 1);


                    msg = msg + "\nExp date: ";
                    while (_edit[0] != '%')
                    {
                        msg = msg + _edit.Substring(0, 1);
                        _edit = _edit.Substring(1, _edit.Length - 1);
                    }
                    

                }
                else
                {
                    msg = "Found Barcode: " + result.Text;
                }
                
            else
                msg = "Scanning Canceled!";
            Info = msg;
        }


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
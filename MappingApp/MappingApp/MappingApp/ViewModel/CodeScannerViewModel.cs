using System.Threading.Tasks;
using MappingApp.View;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using ZXing.Mobile;

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
        }

        public Command CodeScan { get; private set; }

        public async Task ScanCode()
        {
            MobileBarcodeScanner scanner;
            scanner = new MobileBarcodeScanner();
            
            //Tell our scanner to use the default overlay
            scanner.UseCustomOverlay = false;
            //We can customize the top and bottom text of our default overlay
            scanner.TopText = "Hold camera up to barcode";
            scanner.BottomText = "Camera will automatically scan barcode\r\n\r\nPress the 'Back' button to Cancel";

            //Start scanning
            var result = await  scanner.Scan();
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
                msg = "Found Barcode: " + result.Text;
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
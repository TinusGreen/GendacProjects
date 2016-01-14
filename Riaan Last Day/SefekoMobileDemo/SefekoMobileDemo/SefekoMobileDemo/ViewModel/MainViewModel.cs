#region

using System.Threading.Tasks;
using SefekoMobileDemo.Services;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;

#endregion

namespace SefekoMobileDemo.ViewModel
{
	/// <summary>
	///   This class contains properties that the main View can data bind to.
	///   <para>
	///     Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
	///   </para>
	///   <para>
	///     You can also use Blend to data bind with the tool's support.
	///   </para>
	///   <para>
	///     See http://www.galasoft.ch/mvvm
	///   </para>
	/// </summary>
	public class MainViewModel : XLabs.Forms.Mvvm.ViewModel
	{
		#region Fields

		private bool _canScan = true;
		private bool _canPanic = true;
		private bool _canCallMe = true;
		private readonly IScan _scanner;
		private readonly IBleBeaconWatcher _beaconWatcher;

		#endregion

		#region ctor

		/// <summary>
		///   Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainViewModel()
		{
			_scanner = Resolver.Resolve<IScan>();
			_beaconWatcher = Resolver.Resolve<IBleBeaconWatcher>();
			////if (IsInDesignMode)
			////{
			////    // Code runs in Blend --> create design time data.
			////}
			////else
			////{
			////    // Code runs "for real"
			////}

			MainText = "Sefeko Demo";
			Scan = new Command(OnScan, () => _canScan);
			CallMe = new Command(OnCallMe, () => _canCallMe);
			Panic = new Command(OnPanic, () => _canPanic);
			Settings = new Command(() => Navigation.PushAsync<SettingsViewModel>());
			//MessengerInstance.Register<BleBeaconMessage>(this, OnBeaconMessage);
			_beaconWatcher.Start();
		}

		#endregion

		#region Properties

		private string _mainText;

		public string MainText
		{
			get { return _mainText; }
			set { SetProperty(ref _mainText, value, () => MainText); }
		}

		private Color _scanColor = Color.Blue;

		public Color ScanColor
		{
			get { return _scanColor; }
			set { SetProperty(ref _scanColor, value, () => ScanColor); }
		}

		private Color _callMeColor = Color.Green;

		public Color CallMeColor
		{
			get { return _callMeColor; }
			set { SetProperty(ref _callMeColor, value, () => CallMeColor); }
		}

		private Color _panicColor = Color.Red;

		public Color PanicColor
		{
			get { return _panicColor; }
			set { SetProperty(ref _panicColor, value, () => PanicColor); }
		}

		#endregion

		#region Commands

		public Command Scan { get; }
		public Command CallMe { get; }
		public Command Panic { get; }

		public Command Settings { get;  }

		private async void OnScan()
		{
			_canScan = false;
			Scan.ChangeCanExecute();
			await _scanner.Scan();
			_canScan = true;
			Scan.ChangeCanExecute();
		}

		private async void OnCallMe()
		{
			_canCallMe = false;
			CallMe.ChangeCanExecute();
			await Task.Delay(1000);
			_canCallMe = true;
			CallMe.ChangeCanExecute();
		}

		private async void OnPanic()
		{
			_canPanic = false;
			Panic.ChangeCanExecute();
			await Task.Delay(1000);
			_canPanic = true;
			Panic.ChangeCanExecute();
		}



		#endregion

		#region Private methods

		private void OnBeaconMessage(BleBeaconMessage msg)
		{
		}

		#endregion
	}
}
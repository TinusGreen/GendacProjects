#region

using System;
using System.Diagnostics;
using System.Linq;
using Windows.Devices.Bluetooth.Advertisement;
using SefekoMobileDemo.Services;
using UniversalBeaconLibrary.Beacon;

#endregion

namespace SefekoMobileDemo.Uwp.Services
{
	public class BleBeaconWatcher : IBleBeaconWatcher
	{
		// Bluetooth Beacons
		private BluetoothLEAdvertisementWatcher _watcher;

		private BeaconManager _beaconManager;

		public void Start()
		{
			// Construct the Universal Bluetooth Beacon manager
			_beaconManager = new BeaconManager();

			// Create & start the Bluetooth LE watcher from the Windows 10 UWP
			_watcher = new BluetoothLEAdvertisementWatcher {ScanningMode = BluetoothLEScanningMode.Active};
			_watcher.Received += WatcherOnReceived;
			_watcher.Start();
		}


		private void WatcherOnReceived(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs btAdv)
		{
			// Let the library manager handle the advertisement to analyse & store the advertisement
			_beaconManager.ReceivedAdvertisement(btAdv);
			foreach (var bluetoothBeacon in _beaconManager.BluetoothBeacons.ToList())
			{
				Debug.WriteLine("\nBeacon: " + bluetoothBeacon.BluetoothAddressAsString);
				Debug.WriteLine("Type: " + bluetoothBeacon.BeaconType);
				Debug.WriteLine("Last Update: " + bluetoothBeacon.Timestamp);
				Debug.WriteLine("RSSI: " + bluetoothBeacon.Rssi);
				foreach (var beaconFrame in bluetoothBeacon.BeaconFrames.ToList())
				{
					// Print a small sample of the available data parsed by the library
					if (beaconFrame is UidEddystoneFrame)
					{
						Debug.WriteLine("Eddystone UID Frame");
						Debug.WriteLine("ID: " + ((UidEddystoneFrame) beaconFrame).NamespaceIdAsNumber.ToString("X") + " / " +
						                ((UidEddystoneFrame) beaconFrame).InstanceIdAsNumber.ToString("X"));
					}
					else if (beaconFrame is UrlEddystoneFrame)
					{
						Debug.WriteLine("Eddystone URL Frame");
						Debug.WriteLine("URL: " + ((UrlEddystoneFrame) beaconFrame).CompleteUrl);
					}
					else if (beaconFrame is TlmEddystoneFrame)
					{
						Debug.WriteLine("Eddystone Telemetry Frame");
						Debug.WriteLine("Temperature [°C]: " + ((TlmEddystoneFrame) beaconFrame).TemperatureInC);
						Debug.WriteLine("Battery [mV]: " + ((TlmEddystoneFrame) beaconFrame).BatteryInMilliV);
					}
					else
					{
						Debug.WriteLine("Unknown frame - not parsed by the library, write your own derived beacon frame type!");
						Debug.WriteLine("Payload: " + BitConverter.ToString(((UnknownBeaconFrame) beaconFrame).Payload));
					}
				}
			}

			// Optional: distinguish beacons based on the Bluetooth address (btAdv.BluetoothAddress)
			// Check if it's a beacon by Apple
			/*if (btAdv.Advertisement.ManufacturerData.Any())
			{
				foreach (var manufacturerData in btAdv.Advertisement.ManufacturerData)
				{
					// 0x4C is the ID assigned to Apple by the Bluetooth SIG
					if (manufacturerData.CompanyId == 0x4C)
					{
						// Parse the beacon data according to the Apple iBeacon specification
						// Access it through: var manufacturerDataArry = manufacturerData.Data.ToArray();
						var data = manufacturerData.Data.ToArray();
						Messenger.Default.Send(new BleBeaconMessage {Data = data});
					}
				}
			}*/
		}
	}
}
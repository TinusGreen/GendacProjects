using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Views;
using Android.Widget;
using Android.OS;
using RadiusNetworks.IBeaconAndroid;
using Color = Android.Graphics.Color;
using Android.Support.V4.App;
using System.Collections.Generic;

namespace FindTheMonkey.Droid
{
	[Activity(Label = "Find The Monkey", MainLauncher = true, LaunchMode = LaunchMode.SingleTask)]
	public class MainActivity : Activity, IBeaconConsumer
	{




        private List<string> temp = new List<string>();
        private List<string> beaconActive = new List<string>();
        private List<int> beaconTime = new List<int>();
        private List<double> beaconDist = new List<double>();
        //private List<string> UUID = new List<string>();
        private string UUID2 = "2f234454-cf6d-4a0f-adf2-f4911ba9ffa6";
        private int numberBLE = 10;


        private const string monkeyId = "Monkey";

		bool _paused;
		View _view;
		IBeaconManager _iBeaconManager;
        MonitorNotifier _monitorNotifier;
		RangeNotifier _rangeNotifier;
		Region _monitoringRegion;
		Region _rangingRegion;
        Region _monitoringRegion2;
        Region _rangingRegion2;
        TextView _text;

		int _previousProximity;

		public MainActivity()
		{
            temp.Add("R1");
            temp.Add("R2");
            temp.Add("R3");
            temp.Add("R4");
            temp.Add("R5");
            temp.Add("R6");
            temp.Add("R7");
            temp.Add("R8");
            temp.Add("R9");
            temp.Add("R10");


            for (int k = 0; k < numberBLE; k++)
            {
                beaconActive.Add("0");
            }

            for (int k = 0; k < numberBLE; k++)
            {
                beaconTime.Add(0);
            }

            for (int k = 0; k < numberBLE; k++)
            {
                beaconDist.Add(0.0);
            }
            


            _iBeaconManager = IBeaconManager.GetInstanceForApplication(this);


            _monitorNotifier = new MonitorNotifier();
			_rangeNotifier = new RangeNotifier();

			_monitoringRegion = new Region(monkeyId, UUID2, null, null);
			_rangingRegion = new Region(monkeyId, UUID2, null, null);

            //_monitoringRegion2 = new Region(monkeyId, UUID[2], null, null);
            //_rangingRegion2 = new Region(monkeyId, UUID[2], null, null);

        }

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
            temp.Add("R1");
            temp.Add("R2");
            temp.Add("R3");
            temp.Add("R4");
            temp.Add("R5");
            temp.Add("R6");
            temp.Add("R7");
            temp.Add("R8");
            temp.Add("R9");
            temp.Add("R10");

            for (int k = 0; k < numberBLE; k++)
            {
                beaconActive.Add("0");
            }

            for (int k = 0; k < numberBLE; k++)
            {
                beaconTime.Add(0);
            }

            for (int k = 0; k < numberBLE; k++)
            {
                beaconDist.Add(0.0);
            }

            SetContentView(Resource.Layout.Main);

			_view = FindViewById<RelativeLayout>(Resource.Id.findTheMonkeyView);
			_text = FindViewById<TextView>(Resource.Id.monkeyStatusLabel);

			_iBeaconManager.Bind(this);

            _monitorNotifier.EnterRegionComplete += EnteredRegion;
			_monitorNotifier.ExitRegionComplete += ExitedRegion;

			_rangeNotifier.DidRangeBeaconsInRegionComplete += RangingBeaconsInRegion;
		}

		protected override void OnResume()
		{
			base.OnResume();
			_paused = false;
		}

		protected override void OnPause()
		{
			base.OnPause();
			_paused = true;
		}

		void EnteredRegion(object sender, MonitorEventArgs e)
		{
			if(_paused)
			{
				ShowNotification();
			}
		}

		void ExitedRegion(object sender, MonitorEventArgs e)
		{
		}

		void RangingBeaconsInRegion(object sender, RangeEventArgs e)
		{          

			if (e.Beacons.Count > 0)
			{

                var beacon = e.Beacons.FirstOrDefault();
				var message = string.Empty;

				for (int i = 0; i < e.Beacons.Count; i++)
                {
                   
                    var beacon2 = e.Beacons.ElementAt(i);
                    if (beacon2.Major == 10)
                    {
                        var rsi = beacon2.Rssi;
                        beaconTime[beacon2.Minor - 1] += 10;
                        if (beaconTime[beacon2.Minor - 1] > 10)
                        {
                            beaconTime[beacon2.Minor - 1] = 10;
                        }

                        //Reduce all beacons
                        for (int k = 0; k < beaconTime.Count; k++)
                        {
                            beaconTime[k] -= 1;
                            if (beaconTime[k] < 0)
                            {
                                beaconTime[k] = 0;
                                beaconActive[k] = "0";
                            }
                        }
                        double exponent = 0.0;
                        switch ((ProximityType)beacon2.Proximity)
			        	{
					        case ProximityType.Immediate:
                                beaconActive[beacon2.Minor - 1] = "3";
                                exponent = (Math.Abs(beacon2.Rssi)-59)/(10.0*2.0);
                                beaconDist[beacon2.Minor - 1] = Math.Pow(10, exponent);
                                //beaconDist[beacon2.Minor - 1] = beacon2.Rssi;
                                //UpdateDisplayAdd("You are at " +  temp[beacon2.Minor - 1], Color.Green);
                                break;
					        case ProximityType.Near:
                                beaconActive[beacon2.Minor - 1] = "2";
                                exponent = (Math.Abs(beacon2.Rssi)-59) / (10.0 * 2.0);
                                beaconDist[beacon2.Minor - 1] = Math.Pow(10, exponent);
                                //beaconDist[beacon2.Minor - 1] = beacon2.Rssi;
                                //UpdateDisplayAdd("You are nearby " + temp[beacon2.Minor - 1], Color.Yellow);
                                break;
					        case ProximityType.Far:
                                beaconActive[beacon2.Minor - 1] = "1";
                                exponent = (Math.Abs(beacon2.Rssi) - 59) / (10.0 * 2.0);
                                beaconDist[beacon2.Minor - 1] = Math.Pow(10, exponent);
                                //beaconDist[beacon2.Minor - 1] = beacon2.Rssi;
                                //UpdateDisplayAdd("", Color.Blue);
                                break;
					        case ProximityType.Unknown:
                               // UpdateDisplayAdd("", Color.Red);
                                break;
				}
                         UpdateDisplay("", Color.Blue);
                    }
                    else
                    {
                        //UpdateDisplayAdd("Not found", Color.Yellow);
                    }

                }

                //  UpdateDisplayAdd("R2", Color.Green);
                //  UpdateDisplayAdd("R3", Color.Green);
                //  UpdateDisplayAdd("R4", Color.Green);
                //  UpdateDisplayAdd("R5", Color.Green);
                //  UpdateDisplayAdd("R6", Color.Green);
                //  UpdateDisplayAdd("R7", Color.Green);
                //  UpdateDisplayAdd("R8", Color.Green);
                //  UpdateDisplayAdd("R9", Color.Green);
                //UpdateDisplayAdd("R10", Color.Green);

                _previousProximity = beacon.Proximity;
			}
		}

		#region IBeaconConsumer impl
		public void OnIBeaconServiceConnect()
		{
			_iBeaconManager.SetMonitorNotifier(_monitorNotifier);
			_iBeaconManager.SetRangeNotifier(_rangeNotifier);

            _iBeaconManager.StartMonitoringBeaconsInRegion(_monitoringRegion);
			_iBeaconManager.StartRangingBeaconsInRegion(_rangingRegion);
          //  _iBeaconManager.StartMonitoringBeaconsInRegion(_monitoringRegion2);
          //  _iBeaconManager.StartRangingBeaconsInRegion(_rangingRegion2);

           // var te = _iBeaconManager.MonitoredRegions;

        }
		#endregion

		private void UpdateDisplay(string message, Color color)
		{
            //Get the buildup message
            string tempMessage = "";
            int l = 0;
            for (l = 0; l < beaconTime.Count; l++)
            {
                if (beaconActive[l] == "0")
                {
                    //Do nothing
                }
                else if (beaconActive[l] == "3")
                {
                    tempMessage = tempMessage + "\n You are at " + temp[l] + "   Distance:" + String.Format("  {0:F2}", beaconDist[l], beaconDist[l]) + "m";
                }
                else if (beaconActive[l] == "2")
                {
                    tempMessage = tempMessage + "\n You are near " + temp[l] + "   Distance:" + String.Format("  {0:F2}", beaconDist[l], beaconDist[l]) + "m";
                }
                else if (beaconActive[l] == "1")
                {
                    tempMessage = tempMessage + "\n You are far from " + temp[l] + "   Distance:" + String.Format("  {0:F2}", beaconDist[l], beaconDist[l]) + "m";
                }
            }
			RunOnUiThread(() =>
			{
				_text.Text = tempMessage;
				//_view.SetBackgroundColor(color);
			});
		}

        private void UpdateDisplayAdd(string message, Color color)
        {
            RunOnUiThread(() =>
            {
                _text.Text = _text.Text + "\n" + message;
                //_view.SetBackgroundColor(color);
            });
        }

        private void ShowNotification()
		{
			/*var resultIntent = new Intent(this, typeof(MainActivity));
			resultIntent.AddFlags(ActivityFlags.ReorderToFront);
			var pendingIntent = PendingIntent.GetActivity(this, 0, resultIntent, PendingIntentFlags.UpdateCurrent);
			var notificationId = Resource.String.monkey_notification;

			var builder = new NotificationCompat.Builder(this)
				.SetSmallIcon(Resource.Drawable.Xamarin_Icon)
				.SetContentTitle(this.GetText(Resource.String.app_label))
				.SetContentText(this.GetText(Resource.String.monkey_notification))
				.SetContentIntent(pendingIntent)
				.SetAutoCancel(true);

			var notification = builder.Build();

			var notificationManager = (NotificationManager)GetSystemService(NotificationService);
			notificationManager.Notify(notificationId, notification);*/
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			_monitorNotifier.EnterRegionComplete -= EnteredRegion;
			_monitorNotifier.ExitRegionComplete -= ExitedRegion;

			_rangeNotifier.DidRangeBeaconsInRegionComplete -= RangingBeaconsInRegion;

			_iBeaconManager.StopMonitoringBeaconsInRegion(_monitoringRegion);
			_iBeaconManager.StopRangingBeaconsInRegion(_rangingRegion);
			_iBeaconManager.UnBind(this);
		}
	}
}
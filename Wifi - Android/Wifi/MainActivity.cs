using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;


using Android.Net;
using Android.Net.Wifi;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace Wifi
{
    [Activity(Label = "Wifi", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        private static WifiManager _wifi = (WifiManager)Application.Context.GetSystemService(Context.WifiService);
        public List<ScanResult> FOUND;
        public double[][] Distances = new double[100][];
        public string[][] Identities = new string[100][];
        public static List<ScanResult> TEMP;
        TextView lblOutput;
        int index = 0;





        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            lblOutput = FindViewById<TextView>(Resource.Id.LBLscan);
            if (_wifi.IsWifiEnabled == false) {
                _wifi.SetWifiEnabled(true);
            }
            for (int p = 0; p < 100; p++){
                Distances[p] = new double[4];
            }
            for (int p = 0; p < 100; p++)
            {
                Identities[p] = new string[2];
            }
            updateView();
        }


        private void setdist(string mac, string ssid, int dbm, int freq)
        {
            try {
                index = -1;
                int count = 0;
                for (int t = 0; t < 100; t++)
                {
                    if ((Identities[t][0] == mac) && (Identities[t][1] == ssid))
                    {
                        index = t;
                    }
                }
                if (index == -1)
                {
                    count = 0;
                    for (int t = 0; t < 100; t++)
                    {
                        if (Identities[t][0] != null)
                        {
                            count++;
                        }
                    }
                    double exponent = ((Math.Abs(dbm) + 27.55 - (20 * (Math.Log10(freq)))) / 20);
                    Distances[count][0] = Math.Pow(10, exponent);
                    Identities[count][0] = mac;
                    Identities[count][1] = ssid;

                }
                else {
                    count = 0;
                    for (int t = 0; t < 4; t++)
                    {
                        if (Distances[index][t] != 0)
                        {
                            count++;
                        }

                    }
                    if (count == 4)
                    {
                        for (int t = 0; t < 3; t++)
                        {
                            Distances[index][t] = Distances[index][t + 1];
                        }
                        double exponent = ((Math.Abs(dbm) + 27.55 - (20 * (Math.Log10(freq)))) / 20);
                        Distances[index][3] = Math.Pow(10, exponent);
                    }
                    else
                    {
                        double exponent = ((Math.Abs(dbm) + 27.55 - (20 * (Math.Log10(freq)))) / 20);
                        Distances[index][count] = Math.Pow(10, exponent);
                    }
                }
            }
            catch (Exception e) {

            }

        }

        private double getdist(string mac, string ssid)
        {

                int index = 0;
            int count = 0;
            double total = 0;
            for (int t = 0; t < 100; t++) {
                if ((Identities[t][0] == mac) && (Identities[t][1] == ssid)) {
                    index = t;
                }
            }
            for (int t = 0; t < 4; t++)
            {
                if (Distances[index][t] != null)
                {
                    if (Distances[index][t] > 0)
                    {
                        total += Distances[index][t];
                        count++;
                    }
                }

            }
            return total/count;

}


        private async void updateView()
        {
            //

            while (true)
            {
                //int k = 0;
                //int j = 0;
                lblOutput.Text = string.Empty;
                _wifi.ScanResults.Clear();

                //Application.Context.RegisterReceiver(_wifiReceiver, new IntentFilter(WifiManager.ScanResultsAvailableAction));
                _wifi.StartScan();
                FOUND = new List<ScanResult>();
                TEMP = new List<ScanResult>();
                try
                {
                    for (int j = 0; j < _wifi.ScanResults.Count; j++)
                    {
                        setdist(_wifi.ScanResults[j].Bssid, _wifi.ScanResults[j].Ssid, _wifi.ScanResults[j].Level, _wifi.ScanResults[j].Frequency);
                    }

                    for (int x = 0; x < _wifi.ScanResults.Count; x++)
                    {
                        TEMP.Add(_wifi.ScanResults[x]);
                    }
                }
                catch (Exception e)
                {
                    MetroLog.InternalLogger.Current.Error("Error occurred at display", e);
                    Console.WriteLine("Double for : " + e);
                }
                int bestindex = 0;
                double bestmeter = 9999.9;


                try {
                    for (int k = 0; 0 < TEMP.Count; k++)
                    {
                        bestmeter = 9999.9;
                        for (int j = 0; j < TEMP.Count; j++)
                        {
                            if (getdist(TEMP[j].Bssid, TEMP[j].Ssid) < bestmeter)
                            {
                                bestmeter = getdist(TEMP[j].Bssid, TEMP[j].Ssid);
                                bestindex = j;
                            }
                        }

                        FOUND.Add(TEMP[bestindex]);
                        TEMP.RemoveAt(bestindex);

                    }

                }
                            
                catch (Exception e)
                {
                    MetroLog.InternalLogger.Current.Error("Error occurred at display", e);
                    Console.WriteLine("Sorting : " + e);
                }


                //Display Results
                try
                {

                    for (int k = 0; k < FOUND.Count; k++)
                    {
                        lblOutput.Text = lblOutput.Text + "SSID: " + FOUND[k].Ssid + "\nMAC: " + FOUND[k].Bssid + "  RSSI: " + FOUND[k].Level + "dBm  Frequency: " + FOUND[k].Frequency + "MHz\nDistance from access point: " + String.Format("  {0:F2}", getdist(FOUND[k].Bssid, FOUND[k].Ssid)) + "m" + "\n\n";
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Display : " + e);
                    MetroLog.InternalLogger.Current.Error("Error occurred at display", e);
                }

                await Task.Delay(4000);
            }

        }


    }
}


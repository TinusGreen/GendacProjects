using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace GendacCoreApp.Droid
{
    [Activity(Label = "Gendac Core App", Icon = "@drawable/icon", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.homescreen);

            // Our code will go here
            // Get our UI controls from the loaded layout:
            Button btnMaps = FindViewById<Button>(Resource.Id.btnMaps);
            Button btnComms = FindViewById<Button>(Resource.Id.btnComms);
            Button btnVision = FindViewById<Button>(Resource.Id.btnVision);

            btnMaps.Click += (object sender, EventArgs e) =>
            {
                StartActivity(typeof(Maps));
                
            };

            btnComms.Click += (object sender, EventArgs e) =>
            {
                StartActivity(typeof(Comms));

            };

            btnVision.Click += (object sender, EventArgs e) =>
            {
                StartActivity(typeof(vision));

            };

        }

        

    }
}


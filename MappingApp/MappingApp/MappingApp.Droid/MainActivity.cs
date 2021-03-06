﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XLabs.Ioc;
using XLabs.Platform.Services.Geolocation;
using XLabs.Platform.Device;
using Android.Locations;
using Android.Content;
using Android.Util;
using XLabs.Platform.Services.Media;
using XLabs.Forms;
using XLabs.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Mvvm;
using XLabs.Platform.Services;
using XLabs.Platform.Services.Geolocation;
using XLabs.Platform.Services.Media;

namespace MappingApp.Droid
{
    [Activity(Label = "MappingApp", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Xamarin.FormsMaps.Init(this, bundle);

            /*var container = ViewModel.ViewModelLocator.Init();
            container.Register<IGeolocator, Geolocator>();
            container.Register<IMediaPicker, MediaPicker>();
            */

            SetIoC();
            LoadApplication(new App());
        }

        private void SetIoC()
        {
            var resolverContainer = new SimpleContainer();

            var app = new XFormsAppDroid();

            resolverContainer.Register(t => AndroidDevice.CurrentDevice)
                .Register<IDependencyContainer>(t => resolverContainer)
                .Register<IXFormsApp>(app)
                .Register<IGeolocator, Geolocator>()
                .Register<IMediaPicker, MediaPicker>();

            Resolver.SetResolver(resolverContainer.GetResolver());
        }
    }
}


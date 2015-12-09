using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using MappingApp.View;
using MappingApp.ViewModel;
using Xamarin.Forms;
using XLabs.Forms.Controls;
using XLabs.Forms.Mvvm;
using XLabs.Forms.Services;
using XLabs.Ioc;
using XLabs.Platform.Services;

namespace MappingApp
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage =  new View.CameraView();
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

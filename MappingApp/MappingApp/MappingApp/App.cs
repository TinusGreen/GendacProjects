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
using XLabs.Platform.Mvvm;
using XLabs.Platform.Services;

namespace MappingApp
{
    public class App : Application
    {
        public App()
        {
            var app = Resolver.Resolve<IXFormsApp>();
            if (app == null)
            {
                return;
            }

            app.Closing += (o, e) => Debug.WriteLine("Application Closing");
            app.Error += (o, e) => Debug.WriteLine("Application Error");
            app.Initialize += (o, e) => Debug.WriteLine("Application Initialized");
            app.Resumed += (o, e) => Debug.WriteLine("Application Resumed");
            app.Rotation += (o, e) => Debug.WriteLine("Application Rotated");
            app.Startup += (o, e) => Debug.WriteLine("Application Startup");
            app.Suspended += (o, e) => Debug.WriteLine("Application Suspended");

            ViewFactory.Register<MainView, MainViewModel>();
            ViewFactory.Register<MapView, MapViewModel>();
            ViewFactory.Register<View.CameraView, CameraViewModel>();
            ViewFactory.Register<CodeScannerView, CodeScannerViewModel>();
            ViewFactory.Register<CommunicationView, CommunicationViewModel>();

            var mainPage = (Page)ViewFactory.CreatePage(typeof(MainViewModel));

            Resolver.Resolve<IDependencyContainer>()
                             .Register<INavigationService>(t => new NavigationService(mainPage.Navigation));
            MainPage = new NavigationPage(mainPage);


            // The root page of your application
           // MainPage =  new View.CameraView();
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

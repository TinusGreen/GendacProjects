using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SefekoMobileDemo.View;
using SefekoMobileDemo.ViewModel;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Forms.Services;
using XLabs.Ioc;
using XLabs.Platform.Mvvm;
using XLabs.Platform.Services;

namespace SefekoMobileDemo
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

			ViewFactory.Register<MainPage, MainViewModel>();
			ViewFactory.Register<SettingsView, SettingsViewModel>();
			
			var mainPage = (Page)ViewFactory.CreatePage(typeof(MainViewModel));
			Resolver.Resolve<IDependencyContainer>()
							 .Register<INavigationService>(t => new NavigationService(mainPage.Navigation));
			MainPage = new NavigationPage(mainPage);

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

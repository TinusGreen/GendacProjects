using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace SefekoMobileDemo.ViewModel
{
	public class SettingsViewModel : XLabs.Forms.Mvvm.ViewModel
	{

		public SettingsViewModel()
		{
			Save = new Command(OnSave);
		}

		public Command Save { get; private set; }

		private async void OnSave()
		{
			try
			{
				await Navigation.PopAsync();
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}
	}
}
﻿#region

using Windows.UI.Xaml.Controls;

#endregion

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SefekoMobileDemo.Uwp
{
	/// <summary>
	///   An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage 
	{
		public MainPage()
		{
			InitializeComponent();
			LoadApplication(new SefekoMobileDemo.App());
		}
	}
}
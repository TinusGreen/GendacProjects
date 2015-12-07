using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using IntroToSQLite;

namespace IntroTroSQLite
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = GetMainPage();
        
        }

        public static Page GetMainPage()
        {
            var database = new RandomThoughtDatabase();
           
            return new NavigationPage(new RandomThoughtsPage(database));

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

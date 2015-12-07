using System;
using Xamarin.Forms;

namespace IntroToSQLite
{
    public class ThoughtEntryPage : ContentPage
    {
        private RandomThoughtsPage _parent;
        private RandomThoughtDatabase _database;

        public ThoughtEntryPage(RandomThoughtsPage parent, RandomThoughtDatabase database)
        {
            _parent = parent;
            _database = database;
            Title = "Enter name lat and lon";

            var entry = new Entry()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                Text = "Enter lat and lon"
            };
            var entry2 = new Entry()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                Text = "Enter Name"
            };
            var button = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                Text = "Add"
            };

            button.Clicked += async (object sender, EventArgs e) => {
                string Input = entry.Text;
                string Name = entry2.Text;
                double Lat = Convert.ToDouble(Input.Substring(0, Input.IndexOf(" ",0) - 1));
                double Lon = Convert.ToDouble(Input.Substring(Input.IndexOf(" ", 0) + 1, Input.Length));

                _database.AddThought(Name, Lat, Lon);

                await Navigation.PopAsync();


                _parent.Refresh();
            };

            Content = new StackLayout
            {
                Spacing = 20,
                Padding = new Thickness(20),
                Children = {entry2 , button},
            };
        }
    }
}
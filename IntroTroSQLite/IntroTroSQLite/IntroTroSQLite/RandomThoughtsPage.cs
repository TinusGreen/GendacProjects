using System;
using Xamarin.Forms;

namespace IntroToSQLite
{
    public class RandomThoughtsPage : ContentPage
    {
        private RandomThoughtDatabase _database;
        private ListView _thoughtList;

        public RandomThoughtsPage(RandomThoughtDatabase database)
        {
            _database = database;
            Title = "Testing SQLite";
            var thoughts = _database.GetThoughts();

            _thoughtList = new ListView();
            _thoughtList.ItemsSource = thoughts;
            _thoughtList.ItemTemplate = new DataTemplate(typeof(TextCell));
           _thoughtList.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");
           _thoughtList.ItemTemplate.SetBinding(TextCell.DetailProperty, "Lat");
            _thoughtList.ItemTemplate.SetBinding(TextCell.DetailColorProperty, "Lon");

            var toolbarItem = new ToolbarItem
            {
                Name = "Add",
                Command = new Command(() => Navigation.PushAsync(new ThoughtEntryPage(this, database)))
            };

            ToolbarItems.Add(toolbarItem);

            Content = _thoughtList;
        }

        public void Refresh()
        {
            _thoughtList.ItemsSource = _database.GetThoughts();
        }
    }
}
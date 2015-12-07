using System;
using System.IO;
using Xamarin.Forms;
using IntroToSQLite.WinPhone;
using Windows.Storage;


[assembly: Dependency(typeof(SQLite_WinPhone))]


namespace IntroToSQLite.WinPhone
{
    public class SQLite_WinPhone : ISQLite
    {
        public SQLite_WinPhone()
        {
        }

        #region ISQLite implementation

        public SQLite.Net.SQLiteConnection GetConnection()
        {
            var fileName = "RandomThought.db3";

            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, fileName);

            var platform = new SQLite.Net.Platform.WindowsPhone8.SQLitePlatformWP8();
            var connection = new SQLite.Net.SQLiteConnection(platform, path);

            return connection;
        }

        #endregion
    }
}
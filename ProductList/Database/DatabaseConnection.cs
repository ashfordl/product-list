using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Interop;
using SQLite.Net.Platform.Generic;
using ProductList.Models;

namespace ProductList.Database
{
    public class DatabaseConnection
    {
        public static SQLiteConnection CreateDatabase(string path)
        {
            SQLiteConnection db = new SQLiteConnection(new SQLitePlatformGeneric(), path);
            db.CreateTable<Category>();
            db.CreateTable<Product>();

            return db;
        }
    }
}

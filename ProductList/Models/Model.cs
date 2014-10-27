using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;

namespace ProductList.Models
{
    public abstract class Model
    {
        public SQLiteConnection DatabaseConnection { get; protected set; }

        public Model(SQLiteConnection database)
        {
            if (database == null)
            {
                throw new ArgumentNullException("Argument 0 may not be null.");
            }

            DatabaseConnection = database;
        }

        public void Save()
        {
            DatabaseConnection.Insert(this);
        }

        public void Delete()
        {
            DatabaseConnection.Delete(this);
        }
    }
}

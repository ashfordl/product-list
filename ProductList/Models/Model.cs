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
        public static void Save(SQLiteConnection con, Model m)
        {
            con.Insert(m);
        }

        public static void Delete(SQLiteConnection con, Model m)
        {
            con.Delete(m);
        }
    }
}

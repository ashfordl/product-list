using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Extensions;
using SQLiteNetExtensions.Attributes;
using SQLite.Net;

namespace ProductList.Models
{
    [Table("categories")]
    public class Category : Model
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Product> Products { get; set; }

        public static IEnumerable<Category> SelectAll(SQLiteConnection con)
        {
            List<Category> category = new List<Category>();

            foreach(var cat in con.Table<Category>())
            {
                category.Add(con.GetWithChildren<Category>(cat.Id));
            }

            return category;
        }
    }
}

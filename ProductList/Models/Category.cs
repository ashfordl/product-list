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

        public static IEnumerable<Category> SelectAllWithChildren(SQLiteConnection con)
        {
            // Select the children also
            var cats = con.GetAllWithChildren<Category>();

            // Remove categories that do not have products
            cats.RemoveAll(category => category.Products.Count == 0);

            return cats;
        }
    }
}

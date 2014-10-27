using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using SQLite.Net;

namespace ProductList.Models
{
    [Table("products")]
    public class Product : Model
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public int? Price { get; set; }

        public string Remarks { get; set; }

        [ForeignKey(typeof(Category))]
        public int? CategoryId { get; set; }

        [ManyToOne]
        public Category Category { get; set; }

        public Product(SQLiteConnection con) : base(con)
        {
            // Do nothing
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_4
{
    public class Product
    {
        [Column("ProductId")]
        public int Id { get; set; }
        public String ProductName { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public String QuantityUnit { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Orderdetail> OrderDetails { get; set; }
    }
}

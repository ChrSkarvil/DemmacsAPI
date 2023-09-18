using System;
using System.Collections.Generic;

namespace DemmacsAPIv2.Data.Entities
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            Orderitems = new HashSet<Orderitem>();
            Orders = new HashSet<Order>();
            ProductColors = new HashSet<ProductColor>();
            StockProducts = new HashSet<StockProduct>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public string Description { get; set; } = null!;
        public string Dimensions { get; set; } = null!;
        public decimal Weight { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
        public byte[]? Image { get; set; } = null!;

        public virtual Category Category { get; set; } = null!;
        public virtual Manufacturer Manufacture { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Orderitem> Orderitems { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProductColor> ProductColors { get; set; }
        public virtual ICollection<StockProduct> StockProducts { get; set; }
    }
}

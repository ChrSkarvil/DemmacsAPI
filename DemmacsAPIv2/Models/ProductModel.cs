﻿namespace DemmacsAPIv2.Models
{
    public class ProductModel
    {
        public string ProductName { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public string Description { get; set; } = null!;
        public string Dimensions { get; set; } = null!;
        public decimal Weight { get; set; }
        public string CategoryName { get; set; }
        public string ManufacturerName { get; set; }
    }
}

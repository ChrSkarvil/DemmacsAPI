namespace DemmacsAPIv2.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public string Description { get; set; } = null!;
        public string Dimensions { get; set; } = null!;
        public decimal Weight { get; set; }
        public string CategoryName { get; set; }
        public string ManufacturerName { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
        public byte[]? Image { get; set; } = null!;
    }
}

namespace DemmacsAPIv2.Models
{
    public class ProductModelCreate
    {
        public string ProductName { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public string Description { get; set; } = null!;
        public string Dimensions { get; set; } = null!;
        public decimal Weight { get; set; }
        public int CategoryID { get; set; }
        public int ManufacturerID { get; set; }
        public byte[]? Image { get; set; }
    }
}

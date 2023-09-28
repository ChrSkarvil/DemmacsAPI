namespace DemmacsAPIv2.Models
{
    public class ManufacturerModel
    {
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; } = null!;
        public string ManufacturerAddr { get; set; } = null!;
        public long ManufacturerPhone { get; set; }
        public string ManufacturerEmail { get; set; } = null!;
        public int CountryId { get; set; }
        public string PostalCode { get; set; } = null!;
    }
}

namespace DemmacsAPIv2.Models
{
    public class CustomerModelCreate
    {
        public string CustomerFname { get; set; } = null!;
        public string CustomerSname { get; set; } = null!;
        public string CustomerAddr { get; set; } = null!;
        public long? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; } = null!;
        public int CountryId { get; set; }
        public string PostalCode { get; set; } = null!;
    }
}

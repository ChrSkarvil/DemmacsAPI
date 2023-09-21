namespace DemmacsAPIv2.Models
{
    public class CustomerModel
    {
        public string CustomerFname { get; set; } = null!;
        public string CustomerSname { get; set; } = null!;
        public string CustomerAddr { get; set; } = null!;
        public long? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; } = null!;
        public string Country { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
        public string PostalCode { get; set; } = null!;
    }
}

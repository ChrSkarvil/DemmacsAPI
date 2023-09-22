namespace DemmacsAPIv2.Models
{
    public class DeliveryModel
    {
        public string DeliveryAddr { get; set; } = null!;
        public DateTime? DispatchedDate { get; set; }
        public DateTime EstDeliveryDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public decimal DeliveryFee { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; }
        public int CountryId { get; set; }
    }
}

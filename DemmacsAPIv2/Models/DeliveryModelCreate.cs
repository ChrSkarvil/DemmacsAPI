namespace DemmacsAPIv2.Models
{
    public class DeliveryModelCreate
    {
        public string DeliveryAddr { get; set; } = null!;
        public DateTime? DispatchedDate { get; set; }
        public DateTime EstDeliveryDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public decimal DeliveryFee { get; set; }
        public int CountryId { get; set; }
        public string PostalCode { get; set; } = null!;
    }
}

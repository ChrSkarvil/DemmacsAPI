namespace DemmacsAPIv2.Models
{
    public class OrderModelCreate
    {
        public DateTime? OrderDate { get; set; }
        public int PaymentId { get; set; }
        public int CustomerId { get; set; }
        public int DeliveryId { get; set; }

    }
}

namespace DemmacsAPIv2.Models
{
    public class OrderModelCreate
    {
        public decimal TotalPrice { get; set; }
        public int OrderItemId { get; set; }
        public int PaymentId { get; set; }
        public int CustomerId { get; set; }
        public int DeliveryId { get; set; }

    }
}

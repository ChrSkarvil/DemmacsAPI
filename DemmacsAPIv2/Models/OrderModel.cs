using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DemmacsAPIv2.Models
{
    public class OrderModel
    {
        //public string ProductName { get; set; }
        //public int Quantity { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentDate { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime EstDeliveryDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public decimal DeliveryFee { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderItemId { get; set; }
        public int PaymentId { get; set; }
        public int CustomerId { get; set; }
        public int DeliveryId { get; set; }

        public List<OrderItemModel> OrderItems { get; set; }

    }
}

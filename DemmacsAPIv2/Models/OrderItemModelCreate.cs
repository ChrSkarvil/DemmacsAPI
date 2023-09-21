namespace DemmacsAPIv2.Models
{
    public class OrderItemModelCreate
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
    }
}

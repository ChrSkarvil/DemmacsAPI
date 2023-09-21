namespace DemmacsAPIv2.Models
{
    public class OrderItemModel
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
    }
}

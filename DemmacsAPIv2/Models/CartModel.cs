namespace DemmacsAPIv2.Models
{
    public class CartModel
    {
        public string Product { get; set; }
        public string Customer { get; set; }
        public DateTime? Date { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }

    }
}

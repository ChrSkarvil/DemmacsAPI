namespace DemmacsAPIv2.Models
{
    public class CartModelCreate
    {
        public int Quantity { get; set; }
        public DateTime? Date { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
    }
}

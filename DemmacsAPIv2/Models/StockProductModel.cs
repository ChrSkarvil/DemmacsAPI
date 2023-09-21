namespace DemmacsAPIv2.Models
{
    public class StockProductModel
    {
        public string Product { get; set; }
        public string Address { get; set; }
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

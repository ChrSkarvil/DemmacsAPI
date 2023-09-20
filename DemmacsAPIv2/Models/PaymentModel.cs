namespace DemmacsAPIv2.Models
{
    public class PaymentModel
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public DateTime PaymentDate { get; set; }
        public string FullName { get; set; }
        public int CustomerId { get; set; }
    }
}

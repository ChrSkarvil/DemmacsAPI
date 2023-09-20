namespace DemmacsAPIv2.Models
{
    public class PaymentModelCreate
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public DateTime PaymentDate { get; set; }
        public int CustomerId { get; set; }
    }
}

namespace DemmacsAPIv2.Models
{
    public class LoginModelCreate
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public ulong UserType { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
    }
}

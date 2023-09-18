namespace DemmacsAPIv2.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public ulong UserType { get; set; }

        public string FullName { get; set; }
        public string? Role { get; set; }
    }
}

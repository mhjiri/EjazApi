namespace Ejaz.DTOs
{
    public class UserUpdateKeyDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirebaseToken { get; set; }
        public string NewPhoneNumber { get; set; }
        public string NewEmail { get; set; }
    }
}
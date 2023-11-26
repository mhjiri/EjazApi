namespace Ejaz.DTOs
{
    public class UserDto
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Image { get; set; }
        public string Username { get; set; }
        public string FirebaseUID { get; set; }
        public string FirebaseToken { get; set; }
        public string Language { get; set; }
        public bool IsSubscribed { get; set; }
    }
}
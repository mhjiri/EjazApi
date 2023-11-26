using System.ComponentModel.DataAnnotations;

namespace Ejaz.DTOs
{
    public class RegisterOAuthDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirebaseUID { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirebaseToken { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Language { get; set; }
    }
}
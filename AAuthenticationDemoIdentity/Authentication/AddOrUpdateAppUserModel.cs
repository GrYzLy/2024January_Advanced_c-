using System.ComponentModel.DataAnnotations;

namespace AAuthenticationDemoIdentity.Authentication
{
    public class AddOrUpdateAppUserModel
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; } = string.Empty;

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }

        public string Role { get; set; }
    }
}

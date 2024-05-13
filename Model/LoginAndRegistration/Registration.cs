using System.ComponentModel.DataAnnotations;

namespace Login_With_JWT_Authentication.Model.LoginAndRegistration
{
    public class Registration
    {
        public Guid Id { get; set; }

        [Required (ErrorMessage ="Username is required")]
        public string Username { get; set; } = string.Empty;

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Enter a valid email address.")]
        [Required(ErrorMessage = "Email is Compulsory")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage ="Please Provide a Data Of Birth of the User")]
        public DateOnly DateOfBirth { get; set; }

        [MaxLength(8)]
        [MinLength(8)]
        [Required(ErrorMessage = "Enter The Valid Password")]
        public string Password { get; set; } = string.Empty;

        [MaxLength(8)]
        [MinLength(8)]
        [Compare(nameof(Password))]
        [Required(ErrorMessage = "Enter The Valid Password")]
        public string ConfirmPassword { get; set; } = string.Empty;


    }
}

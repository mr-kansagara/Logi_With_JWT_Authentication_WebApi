using System.ComponentModel.DataAnnotations;

namespace Login_With_JWT_Authentication.Model.LoginAndRegistration
{
    public class Registration
    {
        public Guid Id { get; set; }

        [Required (ErrorMessage ="Provide a Unique UserName")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,12}$", ErrorMessage = "Username must contain at least 8 to 12 characters including uppercase, lowercase, digits, and special characters.")]
        public string Username { get; set; } = string.Empty;

        [RegularExpression(@"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$", ErrorMessage = "Enter a valid email address.")]
        [Required(ErrorMessage = "Email is Compulsory and proper Format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage ="Please Provide a Data Of Birth of the User")]
        public DateTime DateOfBirth { get; set; }

        [MinLength(8)]
        [MaxLength(8)]
        [Required(ErrorMessage = "Enter The Valid Password")]
        public string Password { get; set; } = string.Empty;

        [MinLength(8)]
        [MaxLength(8)]
        [Compare(nameof(Password))]
        [Required(ErrorMessage = "Enter The Valid Password")]
        public string ConfirmPassword { get; set; } = string.Empty;


    }
}

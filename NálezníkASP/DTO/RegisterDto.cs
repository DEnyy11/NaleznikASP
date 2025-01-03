using System.ComponentModel.DataAnnotations;

namespace NálezníkASP.DTO {
    public class RegisterDto {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
		[DataType(DataType.Password, ErrorMessage = "Passwords must contain uppercase letters, lowercase letters, numbers, and symbols.")]
		public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
       
    }
}

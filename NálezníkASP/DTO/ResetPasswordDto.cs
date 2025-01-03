using System.ComponentModel.DataAnnotations;

namespace NálezníkASP.DTO {
    public class ResetPasswordDto {
        public string Token { get; set; } 
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Passwords must contain uppercase letters, lowercase letters, numbers, and symbols.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } 
    }
}

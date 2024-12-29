using System.ComponentModel.DataAnnotations;

namespace NálezníkASP.DTO {
    public class LoginDto {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace NálezníkASP.Models {
    public class AppUser : IdentityUser {
		public DateTime RegistrationDate { get; set; }

		public string? Bio {  get; set; }

		public string? Detector { get; set; }

		public byte[]? ProfilePicture { get; set; }

	}
}

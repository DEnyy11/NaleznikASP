namespace NálezníkASP.DTO {
	public class ProfileDto {
		public string UserName { get; set; }

        public string Email {  get; set; }

		public string? Bio { get; set; }

        public string? Detector { get; set; }

        public DateTime RegistrationDate {  get; set; }

		public IFormFile? ProfilePicture { get; set; }
		public byte[]? ProfilePictureReview { get; set; }

	}
}

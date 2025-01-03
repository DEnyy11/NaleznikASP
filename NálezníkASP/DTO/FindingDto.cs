using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace NálezníkASP.DTO {
    public class FindingDto {
        public int Id {  get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [MinLength(5, ErrorMessage = "Name must be atleast 5 characters long")]
		[MaxLength(30, ErrorMessage = "Name cant be longer then 30 characters")]
		public string Name { get; set; }

        [Required(ErrorMessage = "Description cannot be empty")]
        [MinLength(10, ErrorMessage = "Description must be atleast 10 characters long")]
        [MaxLength(300, ErrorMessage = "Description cant be longer then 300 characters")]
        public string Description { get; set; }
        public DateTime? FindingDate { get; set; }
        public double? Depth { get; set; }
        public bool Coin { get; set; }
        public double? LocationLatitude { get; set; }
        public double? LocationLongitude { get; set; }
        public int? MintingYear { get; set; }
        public int? Nominal {  get; set; }
		
		public string? Username { get; set; }
        public IFormFile? FindingPhoto { get; set; }
        public IFormFile?  AfterCleanPhoto { get; set; } 
        public byte[]? AfterCleanPhotoReview { get; set; }
        public byte[]? FindingPhotoReview { get; set; }



    }
}

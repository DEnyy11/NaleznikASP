using System.ComponentModel.DataAnnotations;

namespace NálezníkASP.DTO {
    public class FindingDto {
        public int Id {  get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime FindingDate { get; set; }
        public double Depth { get; set; }
        public bool Coin { get; set; }
        public double LocationLatitude { get; set; }
        public double LocationLongitude { get; set; }
        public int? MintingYear { get; set; }
        public int? Nominal {  get; set; }
        public IFormFile? FindingPhoto { get; set; }
        public IFormFile?  AfterCleanPhoto { get; set; } 
        public byte[]? AfterCleanPhotoReview { get; set; }
        public byte[]? FindingPhotoReview { get; set; }



    }
}

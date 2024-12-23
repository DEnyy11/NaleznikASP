using System.ComponentModel.DataAnnotations;

namespace NálezníkASP.DTO {
    public class FindingDto {
        public int Id {  get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime FindingDate { get; set; }
        public double Depth { get; set; }
        public bool Coin { get; set; }
        public int LocationLatitude { get; set; }
        public int LocationLangtitude { get; set; }
        public int? MintingYear { get; set; }
        public int? Nominal {  get; set; }
        public byte[]? FindingPhoto { get; set; }
        public byte[]?  AfterCleanPhoto { get; set; } 


    }
}

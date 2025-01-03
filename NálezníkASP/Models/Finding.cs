﻿using System.ComponentModel.DataAnnotations;

namespace NálezníkASP.Models {
    public class Finding {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? FindingDate { get; set; }
        public double? Depth { get; set; }
        public bool Coin { get; set; }
        public double? LocationLatitude { get; set; }
        public double? LocationLongitude { get; set; }
        public int? MintingYear { get; set; }
        public int? Nominal { get; set; }
        public byte[]? FindingPhoto { get; set; }
        public byte[]? AfterCleanPhoto { get; set; }
        [Required]
        public string userId { get; set; }
		
		public string? Username { get; set; }

    }
}

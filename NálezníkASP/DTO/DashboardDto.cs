﻿using NálezníkASP.Models;

namespace NálezníkASP.DTO {
    public class DashboardDTO {
        public int TotalFindings { get; set; }
        public Finding? OldestFinding { get; set; }
        public Finding? NewestFinding { get; set; }
        public Finding? DeepestFinding { get; set; }
        public int CoinsCount { get; set; }
        public int ArtifactCount { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using NálezníkASP.DTO;
using NálezníkASP.Models;

namespace NálezníkASP.Services {
    public class DashboardService {
        private readonly ApplicationDbContext dbContext;
        public DashboardService(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        internal async Task<DashboardDTO> GetDashboardData(AppUser user) {
            var allUserFindings = await dbContext.findings.Where(id => id.userId == user.Id).ToListAsync();

            var model = new DashboardDTO {
                TotalFindings = allUserFindings.Count(),
                OldestFinding = allUserFindings.OrderBy(x => x.FindingDate).FirstOrDefault(),
                NewestFinding = allUserFindings.OrderByDescending(x => x.FindingDate).FirstOrDefault(),
                DeepestFinding = allUserFindings.OrderByDescending(x => x.Depth).FirstOrDefault(),
                CoinsCount = allUserFindings.Count(x => x.Coin == true),
                ArtifactCount = allUserFindings.Count(x => x.Coin == false)
            };

            return model;

        }
    }
}

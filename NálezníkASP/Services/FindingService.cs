using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using NálezníkASP.DTO;
using NálezníkASP.Models;

namespace NálezníkASP.Services {
    public class FindingService {
        private ApplicationDbContext dbContext;
		

        public FindingService(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

		internal async Task<IEnumerable<FindingDto>> GetAllFindings(AppUser user) {
			var allUserFindings = await dbContext.findings.Where(id => id.userId == user.Id).ToListAsync();
			var Findings = new List<FindingDto>();
			foreach (var finding in allUserFindings) {
				Findings.Add(ModelToDto(finding));
			}
			return Findings;
		}

		private FindingDto ModelToDto(Finding finding) {
			return new FindingDto {
				Id = finding.Id,
				Name = finding.Name,
				Description = finding.Description,
				FindingDate = finding.FindingDate,
				Depth = finding.Depth,
				Coin = finding.Coin,
				LocationLangtitude = finding.LocationLangtitude,
				LocationLatitude = finding.LocationLatitude,
				MintingYear = finding.MintingYear,
				Nominal = finding.Nominal,
			};
		}

		internal async Task SaveFindingAsync(FindingDto finding, AppUser user, bool IsCoin) {
			await dbContext.findings.AddAsync(DtoToModel(finding, user, IsCoin));
            await dbContext.SaveChangesAsync();
		}

		private Finding DtoToModel(FindingDto finding, AppUser user, bool IsCoin) {

			return new Finding {
				Id = finding.Id,
				Name = finding.Name,
				Description = finding.Description,
				FindingDate = finding.FindingDate,
				Depth = finding.Depth,
				Coin = IsCoin,
				LocationLangtitude = finding.LocationLangtitude,
				LocationLatitude = finding.LocationLatitude,
				MintingYear = finding.MintingYear,
				Nominal = finding.Nominal,
				userId = user.Id

			};
		}

        internal async Task<FindingDto> GetByIdAsync(int id, AppUser user) {
			var finding = await dbContext.findings.Where(finding => finding.userId == user.Id && finding.Id == id).FirstOrDefaultAsync();
			if (finding != null) {
                return ModelToDto(finding);
            }
            return null;
        }

        internal async Task DeleteAsync(int id) {
            var Finding = await dbContext.findings.FirstOrDefaultAsync(x => x.Id == id);
            dbContext.Remove(Finding);
            await dbContext.SaveChangesAsync();
        }

		internal async Task UpdateAsync(int id, FindingDto finding, AppUser? user, bool IsCoin) {
			dbContext.Update(DtoToModel(finding, user, IsCoin));
			await dbContext.SaveChangesAsync();
		}
	}
}

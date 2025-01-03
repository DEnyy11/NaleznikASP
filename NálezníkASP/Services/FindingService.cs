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
                LocationLongitude = finding.LocationLongitude,
                LocationLatitude = finding.LocationLatitude,
                MintingYear = finding.MintingYear,
                Nominal = finding.Nominal,
                AfterCleanPhotoReview = finding.AfterCleanPhoto,
                FindingPhotoReview = finding.FindingPhoto,
                Username = finding.Username,
			};
        }

        internal async Task SaveFindingAsync(FindingDto finding, AppUser user, bool IsCoin) {
            await dbContext.findings.AddAsync(DtoToModel(finding, user, IsCoin));
            await dbContext.SaveChangesAsync();
        }

        private Finding DtoToModel(FindingDto finding, AppUser user, bool IsCoin) {

            byte[]? findingPhoto = null;
            byte[]? AfterCleanPhoto = null;

            if (finding.FindingPhoto != null) {
                using (var memoryStream = new MemoryStream()) {
                    finding.FindingPhoto.CopyTo(memoryStream);
                    findingPhoto = memoryStream.ToArray();
                }
            }
            else {
                findingPhoto = finding.FindingPhotoReview;
            }
            if (finding.AfterCleanPhoto != null) {
                using (var memoryStream = new MemoryStream()) {
                    finding.AfterCleanPhoto.CopyTo(memoryStream);
                    AfterCleanPhoto = memoryStream.ToArray();
                }
            }
            else {
                AfterCleanPhoto = finding.AfterCleanPhotoReview;
            }


            return new Finding {
                Id = finding.Id,
                Name = finding.Name,
                Description = finding.Description,
                FindingDate = finding.FindingDate,
                Depth = finding.Depth,
                Coin = IsCoin,
                LocationLongitude = finding.LocationLongitude,
                LocationLatitude = finding.LocationLatitude,
                MintingYear = finding.MintingYear,
                Nominal = finding.Nominal,
                FindingPhoto = findingPhoto,
                AfterCleanPhoto = AfterCleanPhoto,
                userId = user.Id,
                Username = user.UserName
            };
        }

        internal async Task<FindingDto> GetByIdAsync(int id, AppUser user) {
            var finding = await dbContext.findings.Where(finding => finding.userId == user.Id && finding.Id == id).FirstOrDefaultAsync();
            if (finding != null) {
                return ModelToDto(finding);
            }
            return null;
        }
		internal async Task<FindingDto> GetByIdWithoutUserAsync(int id) {
			var finding = await dbContext.findings.Where(finding => finding.Id == id).FirstOrDefaultAsync();
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
		internal IEnumerable<FindingDto> GetFilteredFindigns(AppUser user, string filterType, string filterName) {
			var findings = dbContext.findings.Where(id => id.userId == user.Id).AsQueryable();
           
            if (!string.IsNullOrEmpty(filterType)) {
                if (filterType ==  "Coin") {
                    findings = findings.Where(x => x.Coin == true);
                }
                else if (filterType == "Artifact") {
                    findings = findings.Where(x => x.Coin == false);
                }
            }

            if (!string.IsNullOrEmpty(filterName)) {
                findings = findings.Where(x =>x.Name.Contains(filterName));
            }
            
            var findingslist = findings.ToList();
            var findingDtos = new List <FindingDto>();
            foreach (var finding in findingslist) {
                findingDtos.Add(ModelToDto(finding));
            }

			return findingDtos;
		}

		internal async Task<IEnumerable<FindingDto>> GetAllFindings() {
			var allFindings = await dbContext.findings.ToListAsync();
			var Findings = new List<FindingDto>();
			foreach (var finding in allFindings) {
				Findings.Add(ModelToDto(finding));
			}
			return Findings;
		}
		internal IEnumerable<FindingDto> GetFilteredFindigns(string filterName, string filterNameUser, string filterType) {
			var findings = dbContext.findings.AsQueryable();

            if (!string.IsNullOrEmpty(filterType)) {
				if (filterType == "Coin") {
					findings = findings.Where(x => x.Coin == true);
				}
				else if (filterType == "Artifact") {
					findings = findings.Where(x => x.Coin == false);
				}
			}

			if (!string.IsNullOrEmpty(filterName)) {
				findings = findings.Where(x => x.Name.Contains(filterName));
			}

			if (!string.IsNullOrEmpty(filterNameUser)) {
				findings = findings.Where(x => x.Username.Contains(filterNameUser));
			}

			var findingslist = findings.ToList();
			var findingDtos = new List<FindingDto>();
			foreach (var finding in findingslist) {
				findingDtos.Add(ModelToDto(finding));
			}

			return findingDtos;
		}
	}
}

using Microsoft.EntityFrameworkCore;
using NálezníkASP.DTO;
using NálezníkASP.Models;

namespace NálezníkASP.Services {
	public class ProfileService {
		private ApplicationDbContext dbContext;
		public ProfileService(ApplicationDbContext dbContext) {
			this.dbContext = dbContext;
		}
		internal async Task<ProfileDto> GetProfileData(AppUser userId) {
			var userData = await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId.Id);

            var profileData = ModelToDto(userData);
            return profileData;
		}

        internal async Task UpdateProfile(ProfileDto profile, AppUser user) {
            var userDB = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
			userDB.Bio = profile.Bio;
			userDB.Detector = profile.Detector;

			dbContext.SaveChanges();
        }

        internal async Task UpdateProfilePicture(AppUser user, IFormFile profilePicture) {
            var userDB = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            using (var memoryStream = new MemoryStream()) {
				await profilePicture.CopyToAsync(memoryStream);
				userDB.ProfilePicture = memoryStream.ToArray();
			}
			await dbContext.SaveChangesAsync();
        }

        private ProfileDto ModelToDto(AppUser user) {
			return new ProfileDto {
				UserName = user.UserName,
				Email = user.Email,
				Bio = user.Bio,
				Detector = user.Detector,
				ProfilePictureReview = user.ProfilePicture,
				RegistrationDate = user.RegistrationDate,
			};

		}

        internal async Task<ProfileDto> GetUserProfileReview (string username) {
			var user = dbContext.Users.FirstOrDefault(user => user.UserName == username);

			var profileData = await GetProfileData(user);
			return profileData;
		}



    }
}

using Microsoft.AspNetCore.Identity;

namespace NálezníkASP.Models {
    public class RoleEdit {
        public IdentityRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> nonMembers { get; set; }

    }
}

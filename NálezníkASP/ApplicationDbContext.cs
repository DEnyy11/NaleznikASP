using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NálezníkASP.Models;

namespace NálezníkASP {
    public class ApplicationDbContext : IdentityDbContext<AppUser> {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) { }

        public DbSet<Finding> findings { get; set; }

    }
}

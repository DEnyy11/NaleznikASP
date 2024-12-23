namespace NálezníkASP.Services {
    public class FindingService {
        private ApplicationDbContext dbContext;

        public FindingService(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
    }
}

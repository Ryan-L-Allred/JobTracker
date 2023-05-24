using JobTracker2.Models;

namespace JobTracker2.Repositories
{
    public interface IPropertyRepository
    {
        List<ExperienceLevel> GetAllExpLevels();
        List<JobType> GetAllJobTypes();
        List<JobSite> GetAllJobSites();
    }
}

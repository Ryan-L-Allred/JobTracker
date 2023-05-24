using Microsoft.Extensions.Configuration;
using JobTracker2.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Runtime.InteropServices;
using Azure;
using JobTracker2.Utils;
using Microsoft.Identity.Client;

namespace JobTracker2.Repositories
{
    public class PropertyRepository : BaseRepository, IPropertyRepository
    {
        public PropertyRepository(IConfiguration configuration) : base(configuration) { }

        public List<ExperienceLevel> GetAllExpLevels()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            SELECT Id, [Name]
                              FROM ExperienceLevel";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var expLevels = new List<ExperienceLevel>();
                        while (reader.Read())
                        {
                            expLevels.Add(new ExperienceLevel()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name")
                            });
                        }

                        return expLevels;
                    }
                }
            }
        }

        public List<JobType> GetAllJobTypes()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            SELECT Id, [Name]
                              FROM JobType";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var jobTypes = new List<JobType>();
                        while (reader.Read())
                        {
                            jobTypes.Add(new JobType()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name")
                            });
                        }

                        return jobTypes;
                    }
                }
            }
        }

        public List<JobSite> GetAllJobSites()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            SELECT Id, [Name]
                              FROM JobSite";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var jobSites = new List<JobSite>();
                        while (reader.Read())
                        {
                            jobSites.Add(new JobSite()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name")
                            });
                        }

                        return jobSites;
                    }
                }
            }
        }

    }
}

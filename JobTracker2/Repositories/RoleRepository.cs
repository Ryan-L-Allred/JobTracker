using Microsoft.Extensions.Configuration;
using JobTracker2.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Runtime.InteropServices;
using Azure;
using JobTracker2.Utils;

namespace JobTracker2.Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(IConfiguration configuration) : base(configuration) { }

        public List<Role> GetAllRoles()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT r.Id, r.Title, r.Company, r.Location, r.Skills, r.IsRejected, r.IsAccepted, r.GotInterview, r.ExperienceLevelId, r.JobTypeId, r.JobSiteId, r.UserProfileId,
	                           el.Name as ExpLevelName,
	                           jt.Name as JobTypeName,
	                           js.Name as JobSiteName
                         FROM Role r
                         JOIN ExperienceLevel el ON r.ExperienceLevelId = el.Id
                         JOIN JobType jt ON r.JobTypeId = jt.Id
                         JOIN JobSite js ON r.JobSiteId = js.Id
                     ORDER BY Title";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var roles = new List<Role>();
                        while (reader.Read())
                        {
                            roles.Add(new Role()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Title = DbUtils.GetString(reader, "Title"),
                                Company = DbUtils.GetString(reader, "Company"),
                                Location = DbUtils.GetString(reader, "Location"),
                                Skills = DbUtils.GetString(reader, "Skills"),
                                IsRejected = reader.GetBoolean(reader.GetOrdinal("IsRejected")),
                                IsAccepted = reader.GetBoolean(reader.GetOrdinal("IsAccepted")),
                                GotInterview = reader.GetBoolean(reader.GetOrdinal("GotInterview")),
                                ExperienceLevelId = DbUtils.GetInt(reader, "ExperienceLevelId"),
                                ExperienceLevel = new ExperienceLevel()
                                {
                                    Id = DbUtils.GetInt(reader, "ExperienceLevelId"),
                                    Name = DbUtils.GetString(reader, "ExpLevelName"),
                                },
                                JobTypeId = DbUtils.GetInt(reader, "JobTypeId"),
                                JobType = new JobType()
                                {
                                    Id = DbUtils.GetInt(reader, "JobTypeId"),
                                    Name = DbUtils.GetString(reader, "JobTypeName")
                                },
                                JobSiteId = DbUtils.GetInt(reader, "JobSiteId"),
                                JobSite = new JobSite()
                                {
                                    Id = DbUtils.GetInt(reader, "JobSiteId"),
                                    Name = DbUtils.GetString(reader, "JobSiteName")
                                },
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId")
                            });

                        }

                        return roles;
                    }
                }
            }    
        }
    }
}

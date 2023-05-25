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
                     ";

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

        public Role GetRoleById(int id)
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
                           WHERE r.Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Role role = null;
                        if (reader.Read())
                        {
                            role = new Role()
                            {
                                Id = id,
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
                            };
                        }

                        return role;
                    }
                }
            }
        }
        
        public void AddRole(Role role)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Role (Title, Company, Location, Skills, IsRejected, IsAccepted, GotInterview, ExperienceLevelId, JobTypeId, JobSiteId, UserProfileId)
                        OUTPUT INSERTED.ID
                        VALUES (@Title, @Company, @Location, @Skills, @IsRejected, @IsAccepted, @GotInterview, @ExperienceLevelId, @JobTypeId, @JobSiteId, @UserProfileId)";

                    DbUtils.AddParameter(cmd, "@Title", role.Title);
                    DbUtils.AddParameter(cmd, "@Company", role.Company);
                    DbUtils.AddParameter(cmd, "@Location", role.Location);
                    DbUtils.AddParameter(cmd, "@Skills", role.Skills);
                    DbUtils.AddParameter(cmd, "@IsRejected", role.IsRejected);
                    DbUtils.AddParameter(cmd, "@IsAccepted", role.IsAccepted);
                    DbUtils.AddParameter(cmd, "@GotInterview", role.GotInterview);
                    DbUtils.AddParameter(cmd, "@ExperienceLevelId", role.ExperienceLevelId);
                    DbUtils.AddParameter(cmd, "@JobTypeId", role.JobTypeId);
                    DbUtils.AddParameter(cmd, "@JobSiteId", role.JobSiteId);
                    DbUtils.AddParameter(cmd, "@UserProfileId", role.UserProfileId);

                    role.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateRole(Role role)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Role
                           SET Title = @Title,
                               Company = @Company,
                               Location = @Location,
                               Skills = @Skills,
                               IsRejected = @IsRejected,
                               IsAccepted = @IsAccepted,
                               GotInterview = @GotInterview,
                               ExperienceLevelId = @ExperienceLevelId,
                               JobTypeId = @JobTypeId,
                               JobSiteId = @JobSiteId,
                               UserProfileId = @UserProfileId
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Title", role.Title);
                    DbUtils.AddParameter(cmd, "@Company", role.Company);
                    DbUtils.AddParameter(cmd, "@Location", role.Location);
                    DbUtils.AddParameter(cmd, "@Skills", role.Skills);
                    DbUtils.AddParameter(cmd, "@IsRejected", role.IsRejected);
                    DbUtils.AddParameter(cmd, "@IsAccepted", role.IsAccepted);
                    DbUtils.AddParameter(cmd, "@GotInterview", role.GotInterview);
                    DbUtils.AddParameter(cmd, "@ExperienceLevelId", role.ExperienceLevelId);
                    DbUtils.AddParameter(cmd, "@JobTypeId", role.JobTypeId);
                    DbUtils.AddParameter(cmd, "@JobSiteId", role.JobSiteId);
                    DbUtils.AddParameter(cmd, "@UserProfileId", role.UserProfileId);
                    DbUtils.AddParameter(cmd, "@Id", role.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteRole(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Role WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

using Microsoft.Extensions.Configuration;
using JobTracker2.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Runtime.InteropServices;
using Azure;

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
                        SELECT r.Id, r.Title, r.Company, r.Location, r.Skills, r.IsRejected, r.IsAccepted, r.GotInterview, r.JobTypeId, r.JobSiteId, r.UserProfileId,
	                           jt.Name as JobTypeName,
	                           js.Name as JobSiteName
                         FROM Role r
                         JOIN JobType jt ON r.JobTypeId = jt.Id
                         JOIN JobSite js ON r.JobSiteId = js.Id
                     ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var roles = new List<Role>();
                        while (reader.Read())
                        {
                            var role = new Role()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Company = reader.GetString(reader.GetOrdinal("Company")),
                                Location = reader.GetString(reader.GetOrdinal("Location")),
                                Skills = reader.GetString(reader.GetOrdinal("Skills")),
                                IsRejected = reader.GetString(reader.GetOrdinal("IsRejected")),
                                IsAccepted = reader.GetString(reader.GetOrdinal("IsAccepted")),
                                GotInterview = reader.GetString(reader.GetOrdinal("GotInterview")),
                                JobTypeId = reader.GetInt32(reader.GetOrdinal("JobTypeId")),
                                JobType = new JobType()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("JobTypeId")),
                                    Name = reader.GetString(reader.GetOrdinal("JobTypeName"))
                                },
                                JobSiteId = reader.GetInt32(reader.GetOrdinal("JobSiteId")),
                                JobSite = new JobSite()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("JobSiteId")),
                                    Name = reader.GetString(reader.GetOrdinal("JobSiteName"))
                                },
                                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId"))
                            };
                            roles.Add(role);
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
                          SELECT r.Id, r.Title, r.Company, r.Location, r.Skills, r.IsRejected, r.IsAccepted, r.GotInterview, r.JobTypeId, r.JobSiteId, r.UserProfileId,
	                             jt.Name as JobTypeName,
	                             js.Name as JobSiteName
                            FROM Role r
                            JOIN JobType jt ON r.JobTypeId = jt.Id
                            JOIN JobSite js ON r.JobSiteId = js.Id
                           WHERE r.Id = @Id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Role role = null;
                        if (reader.Read())
                        {
                            role = new Role()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Company = reader.GetString(reader.GetOrdinal("Company")),
                                Location = reader.GetString(reader.GetOrdinal("Location")),
                                Skills = reader.GetString(reader.GetOrdinal("Skills")),
                                IsRejected = reader.GetString(reader.GetOrdinal("IsRejected")),
                                IsAccepted = reader.GetString(reader.GetOrdinal("IsAccepted")),
                                GotInterview = reader.GetString(reader.GetOrdinal("GotInterview")),
                                JobTypeId = reader.GetInt32(reader.GetOrdinal("JobTypeId")),
                                JobType = new JobType()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("JobTypeId")),
                                    Name = reader.GetString(reader.GetOrdinal("JobTypeName"))
                                },
                                JobSiteId = reader.GetInt32(reader.GetOrdinal("JobSiteId")),
                                JobSite = new JobSite()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("JobSiteId")),
                                    Name = reader.GetString(reader.GetOrdinal("JobSiteName"))
                                },
                                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId"))
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
                        INSERT INTO Role (Title, Company, Location, Skills, IsRejected, IsAccepted, GotInterview, JobTypeId, JobSiteId, UserProfileId)
                        OUTPUT INSERTED.ID
                        VALUES (@Title, @Company, @Location, @Skills, @IsRejected, @IsAccepted, @GotInterview, @JobTypeId, @JobSiteId, @UserProfileId)";

                    cmd.Parameters.AddWithValue("@Title", role.Title);
                    cmd.Parameters.AddWithValue("@Company", role.Company);
                    cmd.Parameters.AddWithValue("@Location", role.Location);
                    cmd.Parameters.AddWithValue("@Skills", role.Skills);
                    cmd.Parameters.AddWithValue("@IsRejected", role.IsRejected);
                    cmd.Parameters.AddWithValue("@IsAccepted", role.IsAccepted);
                    cmd.Parameters.AddWithValue("@GotInterview", role.GotInterview);
                    cmd.Parameters.AddWithValue("@JobTypeId", role.JobTypeId);
                    cmd.Parameters.AddWithValue("@JobSiteId", role.JobSiteId);
                    cmd.Parameters.AddWithValue("@UserProfileId", role.UserProfileId);

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
                               JobTypeId = @JobTypeId,
                               JobSiteId = @JobSiteId,
                               UserProfileId = @UserProfileId
                         WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Title", role.Title);
                    cmd.Parameters.AddWithValue("@Company", role.Company);
                    cmd.Parameters.AddWithValue("@Location", role.Location);
                    cmd.Parameters.AddWithValue("@Skills", role.Skills);
                    cmd.Parameters.AddWithValue("@IsRejected", role.IsRejected);
                    cmd.Parameters.AddWithValue("@IsAccepted", role.IsAccepted);
                    cmd.Parameters.AddWithValue("@GotInterview", role.GotInterview);
                    cmd.Parameters.AddWithValue("@JobTypeId", role.JobTypeId);
                    cmd.Parameters.AddWithValue("@JobSiteId", role.JobSiteId);
                    cmd.Parameters.AddWithValue("@UserProfileId", role.UserProfileId);
                    cmd.Parameters.AddWithValue("@Id", role.Id);

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
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
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
                            var jobType = new JobType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            };
                            jobTypes.Add(jobType);
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
                            var jobSite = new JobSite()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            };
                            jobSites.Add(jobSite);
                        }

                        return jobSites;
                    }
                }
            }
        }
    }
}

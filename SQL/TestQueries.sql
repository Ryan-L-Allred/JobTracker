--Query for GetAllRoles method
SELECT r.Id, r.Title, r.Company, r.Location, r.Skills, r.IsRejected, r.IsAccepted, r.GotInterview,
	   el.Name as ExpLevelName,
	   jt.Name as JobTypeName,
	   js.Name as JobSiteName
 FROM  Role r
 JOIN ExperienceLevel el ON r.ExperienceLevelId = el.Id
 JOIN JobType jt ON r.JobTypeId = jt.Id
 JOIN JobSite js ON r.JobSiteId = js.Id
ORDER BY Title
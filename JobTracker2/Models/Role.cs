﻿namespace JobTracker2.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string Skills { get; set; }
        public string IsRejected { get; set; }
        public string IsAccepted { get; set; }
        public string GotInterview { get; set; }

        public int JobTypeId { get; set; }
        public JobType JobType { get; set; }
        public int JobSiteId { get; set; }
        public JobSite JobSite { get; set; }
        public int UserProfileId { get; set; }
    }
}

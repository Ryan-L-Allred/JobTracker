using JobTracker2.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace JobTracker2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private IPropertyRepository _propertyRepo;

        public PropertyController(IPropertyRepository propertyRepo)
        {
            _propertyRepo = propertyRepo;
        }

        [HttpGet("ExpLevel")]
        public IActionResult GetExpLevels()
        {
            return Ok(_propertyRepo.GetAllExpLevels());
        }

        [HttpGet("JobType")]
        public IActionResult GetJobTypes()
        {
            return Ok(_propertyRepo.GetAllJobTypes());
        }

        [HttpGet("JobSite")]
        public IActionResult GetJobSites()
        {
            return Ok(_propertyRepo.GetAllJobSites());
        }
    }
}

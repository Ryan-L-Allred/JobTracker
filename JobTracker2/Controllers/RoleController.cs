using JobTracker2.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleRepository _roleRepo;
        private readonly IUserProfileRepository _userProfileRepo;

        public RoleController(IRoleRepository roleRepo, IUserProfileRepository userProfileRepo)
        {
            _roleRepo = roleRepo;
            _userProfileRepo = userProfileRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_roleRepo.GetAllRoles());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var role = _roleRepo.GetRoleById(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }
    }
}

﻿using JobTracker2.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobTracker2.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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

        [HttpPost]
        public IActionResult Post(Role role)
        {
            //var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var userProfile = _userProfileRepo.GetByFirebaseUserId(firebaseUserId);

            //role.UserProfileId = userProfile.Id;
            _roleRepo.AddRole(role);
            return CreatedAtAction("Get", new { id = role.Id }, role);
        }

        
        [HttpPut]
        public IActionResult Put(int id, Role role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }

            _roleRepo.UpdateRole(role);
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _roleRepo.DeleteRole(id);
            return NoContent();
        }

        
        [HttpGet("JobType")]
        public IActionResult GetJobTypes()
        {
            return Ok(_roleRepo.GetAllJobTypes());
        }

        
        [HttpGet("JobSite")]
        public IActionResult GetJobSites()
        {
            return Ok(_roleRepo.GetAllJobSites());
        }
    }
}

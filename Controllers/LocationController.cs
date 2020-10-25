
using AutoMapper;
using FindIt.Backend.Entities;
using FindIt.Backend.Models.Accounts;
using FindIt.Backend.Models.Location;
using FindIt.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FindIt.Backend.Controllers
{
     
        [Route("api/location")]
        [ApiController]
        public class LocationsController : ControllerBase
        {

            ILocationsService _locationsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocationsController(ILocationsService locationsRepository, IHttpContextAccessor httpContextAccessor)
            {
                _locationsRepository = locationsRepository ?? throw new ArgumentNullException(nameof(locationsRepository));
            _httpContextAccessor = httpContextAccessor;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locations>>> Get()
        {
            var locations = await _locationsRepository.GetAllAsync();
         
            return Ok(locations);
        }

      
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Locations>> Get(string id)
        {
            var existingLoc = await _locationsRepository.GetAsync(id);
            if (existingLoc == null)
                return NotFound("Location could not be found");

            return existingLoc;
        }


        //authorized for demo purposes
        [Authorize(Roles = "Admin")]
        [AllowAnonymous]
            [HttpPost("addloc")]
            public async Task<ActionResult<Locations>> Create([FromBody]LocationVM locationDto)
            {

            string GetUserRole() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            var rolecheck = GetUserRole().Equals("1");
            if (!rolecheck)
            {
                return Unauthorized("You are not authorized to add new locations");
            }
            await _locationsRepository.CreateAsync(locationDto);

            return Ok(new { message = "Added a Location successfully" });
        }

       
      
        [HttpPut]
        public async Task<ActionResult<LocationVM>> Update([FromBody] Locations location)
        {
          

            var existingLoc = await _locationsRepository.GetAsync(location.Id);
            if (existingLoc == null)
                return NotFound();

            var updatedLoc = await _locationsRepository.UpdateAsync(location);

            return Ok(updatedLoc);
        }

     
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingLoc = await _locationsRepository.GetAsync(id);
            if (existingLoc == null)
                return NotFound();

            await _locationsRepository.DeleteAsync(id);

            return NoContent();
        }
    }
    }


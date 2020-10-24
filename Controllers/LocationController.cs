
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
using System.Threading.Tasks;

namespace FindIt.Backend.Controllers
{
     
        [Route("api/location")]
        [ApiController]
        public class LocationsController : ControllerBase
        {

            ILocationsRepositoryAsync _locationsRepository;
            public LocationsController(ILocationsRepositoryAsync locationsRepository)
            {
                _locationsRepository = locationsRepository ?? throw new ArgumentNullException(nameof(locationsRepository));
            }

        /// <summary>
        /// Gets all location items
        /// </summary>
        /// <returns>A collection of location items</returns>
        /// <response code="200">Returns all location items</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locations>>> Get()
        {
            var locations = await _locationsRepository.GetAllAsync();
           // var locationDTOs = locations.Select(loc => loc.ToLocationDto());
            return Ok(locations);
        }

        /// <summary>
        /// Gets a location item by id
        /// </summary>
        /// <returns>A location item</returns>
        /// <response code="200">Returns the requested location item</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Locations>> Get(string id)
        {
            var existingLoc = await _locationsRepository.GetAsync(id);
            if (existingLoc == null)
                return NotFound();

            return existingLoc;
        }

        /// <summary>
        /// Creates a new location item
        /// </summary>
        /// <returns>The created location item</returns>
        /// <response code="200">Returns the created location item</response>
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
            [HttpPost("addloc")]
            public async Task<ActionResult<Locations>> Create([FromBody]LocationVM locationDto)
            {
             //   var location = locationDto.ToLocation();

                var createdLoc = await _locationsRepository.CreateAsync(locationDto);

                return Created("location", createdLoc);
            }

        /// <summary>
        /// Updates an existing location item
        /// </summary>
        /// <returns>The updated location item</returns>
        /// <response code="200">Returns the updated location item</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<ActionResult<LocationVM>> Update(string id, [FromBody] Locations location)
        {
           // locationDto.Id = id;
          //  var location = locationDto.ToLocation();

            var existingLoc = await _locationsRepository.GetAsync(location.Id);
            if (existingLoc == null)
                return NotFound();

            var updatedLoc = await _locationsRepository.UpdateAsync(location);

            return Ok(updatedLoc);
        }

        /// <summary>
        /// Deletes a location item
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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


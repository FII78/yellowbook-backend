using FindIt.API.Helpers;
using FindIt.API.Models;
using FindIt.API.Models.GeoLocation;
using FindIt.Backend.Entities;
using FindIt.Backend.Models;
using FindIt.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindIt.Backend.Controllers
{

    [Route("api/geo")]
    [Authorize]
    [ApiController]
    public class GeoController : ControllerBase
    {

        IGeoService _geoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GeoController(IGeoService geoRepository, IHttpContextAccessor httpContextAccessor)
        {
            _geoRepository = geoRepository ?? throw new ArgumentNullException(nameof(geoRepository));
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<NodeVM>>> Get()
        {
            var accounts = await _geoRepository.GetAllAsync();

            return Ok(accounts);
        }


        [HttpGet("getbyid/{id}")]
        public ActionResult<NodeVM> GetbyId(string id)
        {
            var existingAcc = _geoRepository.Get(id);
            if (existingAcc == null)
                return NotFound("Location could not be found");

            return existingAcc;
        }

        [HttpGet("getbytag/{tag}")]
        public async Task<ActionResult<NodeVM>> GetbyTagAsync(string tag)
        {
            var existingAcc = await _geoRepository.GetByTagAsync(tag);
            if (existingAcc == null)
                return NotFound("Location could not be found");

            return Ok(existingAcc);
        }

        [HttpGet("NearLoc")]
        public ActionResult<IEnumerable<NodeVM>> GetAddress([FromBody] NodeForNearestVM points)
        {
            var locations = _geoRepository.GetAddress(points);
            if (locations == null)
            {
                return BadRequest();
            }
            else 
            {
                var json = JsonConvert.SerializeObject(locations);
                
                return Ok(json); 
            }
        }



        [HttpPost("addgeo")]
        [ValidateModel]
        public async Task<ActionResult<GeocodeModel>> CreateAsync(NodeVM basem)
        {

           await _geoRepository.AddEntryAsync(basem);

            return Ok(new { message = "Added a Location successfully" });

        }

        [HttpPut("updategeo")]
        [ValidateModel]
        public async Task<ActionResult<NodeVM>> Update([FromBody] NodeVM location)
        {


            var existingLoc =   _geoRepository.Get(location.Id);
            if (existingLoc == null)
                return NotFound();

            var updatedLoc = await _geoRepository.UpdateAsync(location);

            return Ok(updatedLoc);
        }

        [HttpPatch("updategeoname")]
        [ValidateModel]
        public async Task<ActionResult> UpdateByName([FromBody] NodeForUpdateName location)
        {


            var existingLoc = _geoRepository.Get(location.Id);
            if (existingLoc == null)
                return NotFound();

             await _geoRepository.UpdatebyNameAsync(location);

            return Ok(new { message = "Updated a Location successfully" });
        }


        [HttpDelete("deletebyid/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingLoc =  _geoRepository.Get(id);
            if (existingLoc == null)
                return NotFound();

            await _geoRepository.DeleteAsync(id);

            return Ok(new { message = "deleted a Location successfully" });
        }



    }
}


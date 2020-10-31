using FindIt.Backend.Entities;
using FindIt.Backend.Models;
using FindIt.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindIt.Backend.Controllers
{

    [Route("api/geo")]
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeocodeModel>>> Get()
        {
            var accounts = await _geoRepository.GetAllAsync();

            return Ok(accounts);
        }


        [HttpGet("{id}")]
        public ActionResult<NodeVM> GetbyId(string id)
        {
            var existingAcc = _geoRepository.Get(id);
            if (existingAcc == null)
                return NotFound("Location could not be found");

            return existingAcc;
        }

        [HttpGet("NearLoc")]
        public ActionResult<IEnumerable<GeocodeModel>> GetAddress(double lat, double lng, int radius)
        {
            var locations = _geoRepository.GetAddress(lat, lng, radius);
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
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeocodeModel>> CreateAsync(NodeVM basem)
        {


           await _geoRepository.AddEntryAsync(basem);

            return Ok(new { message = "Added a Location successfully" });

        }

        [HttpPut]
        public async Task<ActionResult<NodeVM>> Update([FromBody] NodeVM location)
        {


            var existingLoc =   _geoRepository.Get(location.Id);
            if (existingLoc == null)
                return NotFound();

            var updatedLoc = await _geoRepository.UpdateAsync(location);

            return Ok(updatedLoc);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingLoc =  _geoRepository.Get(id);
            if (existingLoc == null)
                return NotFound();

            await _geoRepository.DeleteAsync(id);

            return NoContent();
        }



    }
}


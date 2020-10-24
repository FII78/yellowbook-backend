using AutoMapper;
using AutoMapper.Configuration;
using FindIt.Backend.Entities;
using FindIt.Backend.Helpers;
using FindIt.Backend.Models.Location;
using FindIt.Backend.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Backend.Services.Implementations
{
  
        public class LocationRepositary : ILocationsRepositoryAsync
        {
            private readonly AuthDbContext _context;
            private readonly IMapper _mapper;
       //     private readonly IConfiguration _config;

            public LocationRepositary(AuthDbContext context)
            {
                _context = context;
            }

            public LocationRepositary(

               IMapper mapper,
               IOptions<Settings> settings
                //IConfiguration config
                )
            {
                _context = new AuthDbContext(settings);
                _mapper = mapper;
                //_config = config;
            }

        public async Task<IList<Locations>> GetAllAsync()
        {
            return await _context.Locations.Find(Locations=>true).ToListAsync();
        }

        public async Task<Locations> GetAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return null;
            }

            var filterId = Builders<Locations>.Filter.Eq("_id", objectId);
            var model = await _context.Locations.Find(filterId).FirstOrDefaultAsync();

            return model;

        }

        public async Task<Locations> CreateAsync(LocationVM location)
            {
              //  location.Id = Guid.NewGuid().ToString();
                var locationcreated = new Locations
                {
                   
                    Name = location.Name,
                    Description = location.Description,
                    Latitude = location.Latitude,
                    Longitude = location.Longitude

                };
                await _context.Locations.InsertOneAsync(locationcreated);
               
                return locationcreated;
            }
        //change it to update view model
        public async Task<LocationVM> UpdateAsync(Locations location)
        {
           
            var loc = await _context.Locations.Find(emp => emp.Id == location.Id).FirstOrDefaultAsync();
            if (loc != null)
            {
                loc.Id = location.Id;
                loc.Description = location.Description;
                loc.Name = location.Name;
                loc.Latitude = location.Latitude;
                loc.Longitude = location.Longitude;
            }
            var updatedLoc = new LocationVM
            {
                Name = loc.Name,
                Description = loc.Description,
                Latitude = loc.Latitude,
                Longitude = loc.Longitude
            };
            return updatedLoc;
        }

        public async Task DeleteAsync(string id)
        {
            ObjectId.TryParse(id, out ObjectId objectId);
            
            var filterId = Builders<Locations>.Filter.Eq("_id", objectId);

            var loc = await _context.Locations.Find(filterId).FirstOrDefaultAsync();
           
            if (loc != null)
            {
                await _context.Locations.DeleteOneAsync(a => a.Id == id);
              
            }

        }

    }
    }


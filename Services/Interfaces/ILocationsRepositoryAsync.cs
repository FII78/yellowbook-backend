using FindIt.Backend.Entities;
using FindIt.Backend.Models.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Backend.Services.Interfaces
{
    public interface ILocationsRepositoryAsync
    {
        Task<IList<Locations>> GetAllAsync();
        Task<Locations> GetAsync(string id);
        Task<Locations> CreateAsync(LocationVM location);
        Task<LocationVM> UpdateAsync(Locations location);
        Task DeleteAsync(string id);
        double searchDistanceBetweenLocationsAsync(LocationVM location1, LocationVM location2);
    }
}

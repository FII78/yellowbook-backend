using FindIt.API.Models;
using FindIt.API.Models.GeoLocation;
using FindIt.Backend.Entities;
using FindIt.Backend.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Backend.Services.Interfaces
{
    public interface IGeoService
    {
        Task AddEntryAsync(NodeVM model);
        IEnumerable<NodeVM> GetAddress(NodeForNearestVM points);
        Task<IEnumerable<NodeVM>> GetAllAsync();
        NodeVM  Get(string id);
        Task<NodeVM> UpdateAsync(NodeVM location);
        Task DeleteAsync(string id);
        Task<IEnumerable<NodeVM>> GetByTagAsync(string tag);
        Task UpdatebyNameAsync(NodeForUpdateName location);
    }
}

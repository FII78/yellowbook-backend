using AutoMapper;
using FindIt.Backend.Entities;
using FindIt.Backend.Models;
using FindIt.Backend.Models.Accounts;

namespace FindIt.Backend.Helpers
{
    public class AutoMapperProfile : Profile
    {
        // mappings between model and entity objects
        public AutoMapperProfile()

        {
            CreateMap<RegisterRequest, Account>();
            
            CreateMap<AuthenticateResponse, Account>();
            CreateMap<GeocodeModel, NodeVM>();

        }
    }
}

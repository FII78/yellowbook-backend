using AutoMapper;
using FindIt.Backend.Entities;
using FindIt.Backend.Helpers;
using FindIt.Backend.Models;
using FindIt.Backend.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FindIt.Backend.Services.Implementations
{
    public class GeoServices: IGeoService
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
         

        public GeoServices(AuthDbContext context)
        {
            _context = context;
        }

        public GeoServices(

           IMapper mapper,
           IOptions<MongoSettings> settings
       
            )
        {
            _context = new AuthDbContext(settings);
            _mapper = mapper;
           
        }


        public async Task AddEntryAsync(NodeVM model)
        {
            try

            {
                var point = new GeoJson2DCoordinates(model.Location[0], model.Location[1]);
                var pnt = new GeoJsonPoint<GeoJson2DCoordinates>(point);
                //double[] pnt =
                //{
                //    model.Location.Lon,
                //    model.Location.Lat
                //};

                var modelcreated = new GeocodeModel
                {
                    Name = model.Name,
                    Location = pnt
                };


                await _context.GeocodeModel.InsertOneAsync(modelcreated);

            }


            catch (Exception)
            {
                //do something;
            }
        }

        public IEnumerable<NodeVM> GetAddress(double lat, double lng, int radius,string tag)
        {
            try
            {

                var filterPoint = GeoJson.Point(new GeoJson2DCoordinates(lng, lat));

                var filter = new FilterDefinitionBuilder<GeocodeModel>()
                             .NearSphere(n => n.Location, filterPoint, radius);

                var model = new GeocodeModel();
           
                  
              return _context.GeocodeModel.Find(filter).ToList()
                    .Select(n =>
                {
                    var modelGeo = new NodeVM();

                    var cor = new double[]
                    {
                        n.Location.Coordinates.X,
                        n.Location.Coordinates.Y
                    };

                    modelGeo.Id = n.Id.ToString();
                    modelGeo.Name = n.Name;
                    modelGeo.Tag = n.Tag;
                    modelGeo.Location = cor;
                    if (modelGeo.Tag == tag)
                        return modelGeo;
                    else
                        return null;
                }); 


            }
            catch (Exception)
            {

            }
            return null;
        }


        public async Task<IList<GeocodeModel>> GetAllAsync()
        {
           return await _context.GeocodeModel.Find(Locations => true).ToListAsync();
        }

        public NodeVM Get(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return null;
            }

            var filterId = Builders<GeocodeModel>.Filter.Eq("_id", objectId);
            var model = _context.GeocodeModel.Find(filterId).FirstOrDefault();
            var loca = new double[]
          {
                model.Location.Coordinates.X,
                model.Location.Coordinates.Y
          };
            var locationsVM = new NodeVM
            {
                Id = model.Id,
                Name = model.Name,
                Location = loca
            };
            return locationsVM;

        }
        //var filterId = Builders<GeocodeModel>.Filter.Eq("Tag", tag);
        //var model = _context.GeocodeModel.Find(filterId).ToList();
        ////var modArr= model.ToArray();
        ////var loca = new double[] 
        ////{
        ////modArr
        ////} 
        ////var locationsVM = new NodeVM
        ////{
        ////    Id = model.Id,
        ////    Name = model.Name,
        ////    Location = loca
        ////}; 
        ////to geocodemodel lewuchiw
        //return model;
        public async Task<IEnumerable<NodeVM>> GetByTagAsync(string tag)
        {
           
            var loc = await _context.GeocodeModel.Find(emp => emp.Tag == tag).ToListAsync();
            GeocodeModel loca=new GeocodeModel();
            var nodes=new List<NodeVM>();
            for (int i=0;i<loc.Count;i++)
            {
                loca=loc[i];
                var locationVM = new double[]
                {
                loca.Location.Coordinates.X,
                loca.Location.Coordinates.Y
                };
                var updatedLoc = new NodeVM
                {
                    Id = loca.Id,
                    Name = loca.Name,
                    Location = locationVM
                };
                nodes.Append(updatedLoc);
                
            }
            return nodes;
            

        }

        public async Task<NodeVM> UpdateAsync(NodeVM location)
        {

            var loc = await _context.GeocodeModel.Find(emp => emp.Id == location.Id).FirstOrDefaultAsync();
            var point = new GeoJson2DCoordinates(location.Location[0], location.Location[1]);
            var pnt = new GeoJsonPoint<GeoJson2DCoordinates>(point);

            if (loc != null)
            {
                loc.Id = location.Id;
                loc.Name = location.Name;
                loc.Location = pnt;
            }
            var locationVM = new double[]
            {
                loc.Location.Coordinates.X,
                loc.Location.Coordinates.Y
            };
            var updatedLoc = new NodeVM
            {
                Id=loc.Id,
                Name = loc.Name,
                Location=locationVM
            };
            return updatedLoc;
        }

        public async Task DeleteAsync(string id)
        {
            ObjectId.TryParse(id, out ObjectId objectId);

            var filterId = Builders<GeocodeModel>.Filter.Eq("_id", objectId);

            var loc = await _context.GeocodeModel.Find(filterId).FirstOrDefaultAsync();

            if (loc != null)
            {
                await _context.GeocodeModel.DeleteOneAsync(a => a.Id == id);

            }

        }


    }
}

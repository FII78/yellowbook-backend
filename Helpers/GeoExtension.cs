using FindIt.Backend.Entities;
using FindIt.Backend.Models;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.API.Helpers
{
    public static class GeoExtension
    {

        public static GeocodeModel ConvertToDomain(this NodeVM nodeViewModel)
        {
            var point = new GeoJson2DCoordinates(nodeViewModel.Location[0], nodeViewModel.Location[1]);
            var pnt = new GeoJsonPoint<GeoJson2DCoordinates>(point);

            GeocodeModel geocodeModel = new GeocodeModel()
            {
                Name = nodeViewModel.Name,
                Tag = nodeViewModel.Tag,
                Description = nodeViewModel.Description,
                Location = pnt

            };            
            return geocodeModel;
        }

        public static IEnumerable<NodeVM> ConvertAllToViewModels(this IEnumerable<GeocodeModel> geoDomains)
        {
            foreach (GeocodeModel geo in geoDomains)
            {
                yield return geo.ConvertToViewModel();
            }
        }
        /// <summary>
        /// COnverts to NodeVM from GeoCodeModel
        /// </summary>
        /// <param name="loca"></param>
        /// <returns></returns>
        public static NodeVM ConvertToViewModel(this GeocodeModel loca)
        {
            var locationVM = new double[]
               {
                loca.Location.Coordinates.X,
                loca.Location.Coordinates.Y
               };
            NodeVM geoViewModel = new NodeVM()
            {
                Id = loca.Id,
                Name = loca.Name,
                Tag = loca.Tag,
                Description = loca.Description,
                Location = locationVM

            };


            return geoViewModel;
        }
    }
}

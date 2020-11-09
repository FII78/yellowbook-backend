using FindIt.Backend.Entities;
using FindIt.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.API.Helpers
{
    public static class GeoExtension
    {
        //public static GeocodeModel ConvertToDomain(this PlayerVM playerViewModel)
        //{

        //    Player playerRes = new Player()
        //    {
        //        HighScore = playerViewModel.HighScore,
        //        No_of_Coins = playerViewModel.No_of_Coins,
        //        FirstName = playerViewModel.FirstName,
        //        UserName = playerViewModel.UserName,
        //        Email = playerViewModel.Email,
        //        PrevLevel = playerViewModel.PrevLevel,
        //        CurrLevel = playerViewModel.CurrLevel,
        //        NextLevel = playerViewModel.CurrLevel

        //    };
        //    //      playerRes.PlayerId = Guid.NewGuid().ToString("N");
        //    playerRes.Role = "Player";
        //    playerRes.Created = DateTime.UtcNow;
        //    playerRes.PasswordHash = BC.HashPassword(playerViewModel.Password);
        //    return playerRes;
        //}

        public static IEnumerable<NodeVM> ConvertAllToViewModels(this IEnumerable<GeocodeModel> geoDomains)
        {
            foreach (GeocodeModel geo in geoDomains)
            {
                yield return geo.ConvertToViewModel();
            }
        }

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

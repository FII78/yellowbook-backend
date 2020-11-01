using MongoDB.Driver.GeoJsonObjectModel;

namespace FindIt.Backend.Entities
{
    public class Helper
    {
        public  GeoJson2DGeographicCoordinates GetCoordinates(double longitude, double latitude)
        {
            return new GeoJson2DGeographicCoordinates(longitude, latitude);
        }

        public  GeoJsonPoint<GeoJson2DGeographicCoordinates> GetJsonPoint(double x, double y)
        {
            GeoJson2DGeographicCoordinates output = GetCoordinates(x, y);
            if (output == null)
            {
                return null;
            }

            return new GeoJsonPoint<GeoJson2DGeographicCoordinates>(output);
        }
    }
}

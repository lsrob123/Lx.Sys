using System.Data.Entity.Spatial;

namespace Lx.Utilities.Services.Spatial
{
    public static class SpatialExtensions
    {
        public static DbGeography FromLongitudeAndLatitude(decimal longitude, decimal latitude)
        {
            var location = DbGeography.FromText($"POINT({longitude} {latitude})");
            return location;
        }
    }
}
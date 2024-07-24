using NetTopologySuite.Geometries;

namespace BackEnd.Models
{
    public class Location
    {
        public Point? Latitude { get; set; }
        public Point? Longitude { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;


namespace OlioEsimerkki
{
    /// <summary>
    /// Class for location with coordinates
    /// </summary>
    [Serializable]
    public class Location : ILocate
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public Location (double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        
        /// <summary>
        /// Returns coordinates in string
        /// </summary>
        /// <returns>string</returns>
        public string GetCoordinates()
        {
            string coordinates;

            if (Latitude >= 0)
            {
                coordinates = "N " + Latitude.ToString();
            }
            else
            {
                coordinates = "S " + (Latitude * -1).ToString();
            }

            if (Longitude >= 0)
            {
                coordinates = coordinates + " ; E " + Longitude.ToString();
            }
            else
            {
                coordinates = coordinates + " ; W " + (Longitude * -1).ToString();
            }
            return coordinates;

        }
        /// <summary>
        /// Returns distance in meters to other Location
        /// </summary>
        /// <returns>double</returns>
        public virtual double GetDistance(Location location)
        {
            GeoCoordinate geoCoordinate1 = new GeoCoordinate(Latitude,Longitude);
            GeoCoordinate geoCoordinate2 = new GeoCoordinate(location.Latitude, location.Longitude);
            return geoCoordinate1.GetDistanceTo(geoCoordinate2);
        }
    }
}

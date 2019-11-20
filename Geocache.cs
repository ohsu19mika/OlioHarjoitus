using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlioEsimerkki
{
    /// <summary>
    /// Presents one geocache which has:
    /// - GC_Code
    /// - Hidetime
    /// - Difficulty and terrain raiting
    /// - Additional waypoints (optional)
    /// </summary>
    [Serializable]
    public class Geocache : Location
    {
        private List<Waypoint> waypoints = new List<Waypoint>();

        public string HideTime { get; }
        public string GC_Code { get; }
        public double Difficulty { get; }
        public double Terrain { get; }

        public Geocache(string hideTime, string gc_code, double latitude, double longitude, double difficulty, double terrain) : base(latitude, longitude)
        {
            HideTime = hideTime;
            GC_Code = gc_code;
            Difficulty = difficulty;
            Terrain = terrain;
        }

        /// <summary>
        /// Returns sum of starts
        /// </summary>
        /// <returns></returns>
        public double GetStarsSum()
        {
            return Difficulty + Terrain;
        }

        /// <summary>
        /// Adds waypoint to cache
        /// </summary>
        /// <param name="code"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="type"></param>
        public void AddWaypoint(string code, double latitude, double longitude, Waypoint.WaypointType type)
        {
            waypoints.Add(new Waypoint(code, latitude, longitude, type));
        }

        /// <summary>
        /// Returns count of waypoints in cache
        /// </summary>
        /// <returns></returns>
        public int GetWaypointCount()
        {
            return waypoints.Count;
        }

        /// <summary>
        /// Returns shortest distance in meters from other Location to cache itself or its waypoint
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public override double GetDistance(Location location)
        {
            if (waypoints.Count == 0)
            {
                return base.GetDistance(location);
            }
            else
            {
                double distance = base.GetDistance(location);
                foreach (var item in waypoints)
                {
                    distance = Math.Min(distance, item.GetDistance(location));
                }
                return distance;
            }
        }

 
        /// <summary>
        /// Returns distance in meters to specified (by type) waypoint.
        /// Type options are: Cache and WaypointType types
        /// </summary>
        /// <param name="location"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public double GetDistance(Location location, string type)
        {
            if (waypoints.Count == 0)
            {
                if (type != "Cache")
                {
                    return -1;
                }
                return base.GetDistance(location);
            }
            else
            {
                if (type == "Cache") return base.GetDistance(location);

                foreach (var item in waypoints)
                {
                    if (item.Type.ToString() == type)
                    {
                        return item.GetDistance(location);
                    }
                }
                return -2;
            }
        }
    }
}

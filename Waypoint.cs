using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OlioEsimerkki
{
    /// <summary>
    /// Presents loacation with specific type (WaypointType)
    /// </summary>
    [Serializable]
    public class Waypoint : Location
    {
        public enum WaypointType
        {
            Parking = 0,
            Trailhead = 1,
            VirtualStage = 2,
            ReferencePoint = 3
        }
        public string Code { get; }
        public WaypointType Type { get; }

        
        public Waypoint (string code, double latitude, double longitude, WaypointType type) : base(latitude, longitude)
        {
            Code = code;
            Type = type;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlioEsimerkki
{
    /// <summary>
    /// Interface for classes with coordinates
    /// </summary>
    interface ILocate
    {
        double Latitude { get; }
        double Longitude { get; }
    }
}

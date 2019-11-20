using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlioEsimerkki
{
    /// <summary>
    /// Cachelist has list for Geocaches and ID(string) for identifier
    /// </summary>
    [Serializable]
    public class CacheList
    {
        public string ID;
        public List<Geocache> caches = new List<Geocache>();
    }
}

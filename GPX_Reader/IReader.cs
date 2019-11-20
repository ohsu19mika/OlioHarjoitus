using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlioEsimerkki.GPX_Reader
{
    /// <summary>
    /// Interface for using GPX reader
    /// </summary>
    interface IReader
    {
        int GeocacheCount { get; }
        
        int GetCacheID(string Code);
        Geocache GetCache(int CacheID);
        string GetCacheInfo(int CacheID, CacheInfo Info);
        List<string> GetCacheInfo(CacheInfo Info);
        List<string> GetCaches(CacheInfo Info, string Search);


    }
    public enum CacheInfo
    {
        HideTime = 0,
        GC_Code = 1,
        Desc = 2,
        Url = 3,
        UrlName = 4,
        Symbol = 5,
        Type = 6,
        GS_Cache = 7,
        Name = 10,
        PlacedBy = 11,
        Owner = 12,
        GS_Type = 13,
        Container = 14,
        Attributes = 15,
        Difficulty = 16,
        Terrain = 17,
        Country = 18,
        State = 19,
        ShortDesc = 20,
        LongDesc = 21,
        Hints = 22,
        Logs = 23,
        Travelbugs = 24
    }
}

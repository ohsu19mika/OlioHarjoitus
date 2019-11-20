using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OlioEsimerkki.GPX_Reader
{
    /// <summary>
    /// Class <c>Reader</c> reads GPX file of my finds pocket query.
    /// </summary>
    public class Reader : IReader
    {
        private static XmlDocument GPX = new XmlDocument();
        private static int WptCount;
        private static List<int> geoCaches = new List<int>();
        private static List<int> waypoints = new List<int>();
        private static string find = "";
        
        /// <summary>Get list(<value>XmlNodeList</value>) of wpts in GPX.</summary>
        public XmlNodeList CacheList { get; }

        /// <summary>Get count of caches in GPX.</summary>
        public int GeocacheCount {
            get
            {
                return geoCaches.Count;
            }
            }


        /// <summary> </summary>
        public Reader(string file)
        {

            GPX.Load(file);
            CacheList = GPX.GetElementsByTagName("wpt");
            WptCount = CacheList.Count;

            for (int i = 0; i < WptCount; i++)
            {
                if (CacheList.Item(i).ChildNodes.Item((int)CacheInfo.Type).InnerText.StartsWith("Geocache"))
                {
                    geoCaches.Add(i);
                }
                else if (CacheList.Item(i).ChildNodes.Item((int)CacheInfo.Type).InnerText.StartsWith("Waypoint"))
                {
                    waypoints.Add(i);
                }
            }
        }

        
        /// <summary>
        /// Gets Id for GC_code. Id can be used with orher functions.
        /// </summary>
        /// <param name="Code"></param>
        /// <returns>Cache id</returns>
        public int GetCacheID(string Code)
        {
            for (int i = 0; i < WptCount; i++)
            {
                if (CacheList.Item(i).ChildNodes.Item((int)CacheInfo.GC_Code).InnerText == Code)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns Geocache from GPX by ID
        /// </summary>
        /// <param name="CacheID"></param>
        /// <returns></returns>
        public Geocache GetCache(int CacheID)
        {
            return new Geocache(GetCacheInfo(CacheID, CacheInfo.HideTime),
                GetCacheInfo(CacheID, CacheInfo.GC_Code),
                Convert.ToDouble((CacheList.Item(CacheID).Attributes.GetNamedItem("lat").InnerText), new CultureInfo("en-US")),
                Convert.ToDouble((CacheList.Item(CacheID).Attributes.GetNamedItem("lon").InnerText), new CultureInfo("en-US")),
                Convert.ToDouble(GetCacheInfo(CacheID, CacheInfo.Difficulty), new CultureInfo("en-US")),
                Convert.ToDouble(GetCacheInfo(CacheID, CacheInfo.Terrain), new CultureInfo("en-US")));
        }

        /// <summary>Returns info of one cache.</summary>
        public string GetCacheInfo(int CacheID, CacheInfo Info)
        {
            if ((int)Info < 10)
            {
                return CacheList.Item(CacheID).ChildNodes.Item((int)Info).InnerText;
            }
            else
            {
                return CacheList.Item(CacheID).ChildNodes.Item(7).ChildNodes.Item((int)Info-10).InnerText;
            }
        }

        /// <summary>Returns (<value>List<string></value>) info of all caches.</summary>
        public List<string> GetCacheInfo(CacheInfo Info)
        {
            var CacheInfos = new List<string>();
            for (int i = 0; i < WptCount; i++)
            {
                CacheInfos.Add(GetCacheInfo(i,Info));
            }
            return CacheInfos;
        }



        /// <summary>
        /// Returns cache infos which has string(given as <value>Search</value>) in it(given by <value>Info</value>).
        /// </summary>
        /// <param name="Info"></param>
        /// <param name="Search"></param>
        /// <returns><value>List<string></value></returns>
        public List<string> GetCaches(CacheInfo Info, string Search)
        {
            find = Search.ToUpper();
            var Caches = new List<string>();
            Caches = GetCacheInfo(Info).FindAll(FindText);
            return Caches;
        }

        //
        private static bool FindText(string Text)
        {
            return Text.Contains(find);
        }


        /// <summary>
        /// Removes descriptions, hints and logs from GPX and saves it with name "finds_light.gpx"
        /// </summary>
        /// <returns>1 = OK, negative something to remove missing</returns>
        public int Remove()
        {
            int error = 1;
            for (int i = 0; i < WptCount; i++)
            {
                Console.WriteLine(CacheList.Item(i).ChildNodes.Item(7).ChildNodes.Count);
                if (CacheList.Item(i).ChildNodes.Item(7).ChildNodes.Count < 11) continue;
                if (CacheList.Item(i).ChildNodes.Item(7).ChildNodes.Item(10).Name == "groundspeak:short_description")
                {
                    CacheList.Item(i).ChildNodes.Item(7).ChildNodes.Item(10).RemoveAll();
                }
                else
                {
                    error = -1;
                }
                if (CacheList.Item(i).ChildNodes.Item(7).ChildNodes.Count < 12) continue;
                if (CacheList.Item(i).ChildNodes.Item(7).ChildNodes.Item(11).Name == "groundspeak:long_description")
                {
                    CacheList.Item(i).ChildNodes.Item(7).ChildNodes.Item(11).RemoveAll();
                }
                else
                {
                    error = -2;
                }
                if (CacheList.Item(i).ChildNodes.Item(7).ChildNodes.Count < 13) continue;
                if (CacheList.Item(i).ChildNodes.Item(7).ChildNodes.Item(12).Name == "groundspeak:encoded_hints")
                {
                    CacheList.Item(i).ChildNodes.Item(7).ChildNodes.Item(12).RemoveAll();
                }
                else
                {
                    error = -3;
                }
                if (CacheList.Item(i).ChildNodes.Item(7).ChildNodes.Count < 14) continue;
                if (CacheList.Item(i).ChildNodes.Item(7).ChildNodes.Item(13).Name == "groundspeak:logs")
                {
                    CacheList.Item(i).ChildNodes.Item(7).ChildNodes.Item(13).RemoveAll();
                }
                else
                {
                    error = -4;
                }
            }
            
            GPX.Save("finds_light.gpx");
            return error;
        }

        /// <summary>
        /// Changes coordinates to have random offsets and saves GPX with name finds_random.gpx
        /// </summary>
        /// <returns></returns>
        public void FakeCoordinates()
        {
            Random random = new Random();

            NumberFormatInfo format = new NumberFormatInfo();
            format.NumberDecimalSeparator = ".";

            for (int i = 0; i < WptCount; i++)
            {
                if (random.Next()%2 == 0)
                {
                    CacheList.Item(i).Attributes.Item(0).Value = (Convert.ToDouble(CacheList.Item(i).Attributes.Item(0).Value, new CultureInfo("en-US")) + random.NextDouble()).ToString(format);
                    CacheList.Item(i).Attributes.Item(1).Value = (Convert.ToDouble(CacheList.Item(i).Attributes.Item(1).Value, new CultureInfo("en-US")) - random.NextDouble()).ToString(format);
                }
                else
                {
                    CacheList.Item(i).Attributes.Item(0).Value = (Convert.ToDouble(CacheList.Item(i).Attributes.Item(0).Value, new CultureInfo("en-US")) - random.NextDouble()).ToString(format);
                    CacheList.Item(i).Attributes.Item(1).Value = (Convert.ToDouble(CacheList.Item(i).Attributes.Item(1).Value, new CultureInfo("en-US")) + random.NextDouble()).ToString(format);
                }
            }
            GPX.Save("finds_random.gpx");
        }
    }
}

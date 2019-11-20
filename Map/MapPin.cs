using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OlioEsimerkki.Map
{
    /// <summary>
    /// Class for map pin placed with coordinates
    /// </summary>
    class MapPin : ILocate
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public MapPin(ILocate locate)
        {
            Latitude = locate.Latitude;
            Longitude = locate.Longitude;
        }

        //Open maps and adds pin on it
        public void ShowOnMap()
        {
            Map map = new Map();
            string address;

            //Formating decimal separator
            NumberFormatInfo format = new NumberFormatInfo();
            format.NumberDecimalSeparator = ".";

            //Address for web browser
            address = @"http://asiointi.maanmittauslaitos.fi/karttapaikka/api/linkki?x=" + Latitude.ToString(format)
              + "&y=" + Longitude.ToString(format) + "&srs=EPSG:4258&text=TestPin&scale=16000&mode=rasta&lang=fi";
            map.SetWebpage(address);

            //Opens map
            Application.Run(map);
        }

        //Open maps and adds pin on it with specified name
        public void ShowOnMap(string pinName)
        {
            Map map = new Map();
            string address;

            //Formating decimal separator
            NumberFormatInfo format = new NumberFormatInfo();
            format.NumberDecimalSeparator = ".";

            //Address for web browser
            address = @"http://asiointi.maanmittauslaitos.fi/karttapaikka/api/linkki?x=" + Latitude.ToString(format)
              + "&y=" + Longitude.ToString(format) + "&srs=EPSG:4258&text=" + pinName +"&scale=16000&mode=rasta&lang=fi";
            map.SetWebpage(address);

            //Opens map
            Application.Run(map);
        }
    }
}

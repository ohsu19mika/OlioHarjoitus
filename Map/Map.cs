using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OlioEsimerkki.Map
{
    public partial class Map : Form
    {
        public Map()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets address for web browser element
        /// </summary>
        /// <param name="address"></param>
        public void SetWebpage(string address)
        {
            webBrowser1.Navigate(address);
        }
    }
}

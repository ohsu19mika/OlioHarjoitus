using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OlioEsimerkki.GPX_Reader;


namespace OlioEsimerkki
{
    static class Program
    {
        static Reader myReader;
        static ConsoleUI consoleUI;

        [STAThread]
        static void Main(string[] args)
        {
            String File = "finds.gpx";
            myReader = new Reader(File);
            consoleUI = new ConsoleUI(myReader);

            //Opens console UI
            consoleUI.StartConsoleUI();
        }
    }
}

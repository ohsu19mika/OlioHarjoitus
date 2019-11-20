using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OlioEsimerkki.Map;

namespace OlioEsimerkki
{
    /// <summary>
    /// Console based UI for GPX raeder program
    /// </summary>
    class ConsoleUI
    {
        private OlioEsimerkki.GPX_Reader.IReader myReader;
        private Location home = new Location(62.78813, 22.82231);
        private GPX_Reader.CacheInfo cacheInfo = GPX_Reader.CacheInfo.GC_Code;
        private CacheList cacheList = new CacheList();
        private BinaryFileHandling fileHandling = new BinaryFileHandling();

        public ConsoleUI(OlioEsimerkki.GPX_Reader.IReader reader)
        {
            myReader = reader;
        }

        public void StartConsoleUI()
        {
            Console.WriteLine("GPX reader");

            //Main menu
            while (true)
            {
                char command;

                Console.Write("\n\nGive command (H = help, Q = quit): ");
                command = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                switch (command)
                {
                    case 'H':
                        PrintHelp();
                        break;

                    case 'F':
                        FileHandling();
                        break;

                    case 'D':
                        Distance();
                        break;

                    case 'I':
                        ShowInfos();
                        break;

                    case 'L':
                        ListFunctions();
                        break;

                    case 'M':
                        ShowOnMap();
                        break;

                    case 'P':
                        Print();
                        break;

                    case 'S':
                        SetInfo();
                        break;

                    case 'Q':
                        return;

                    default:
                        Console.WriteLine("Command ({0}) not valid!",command);
                        break;
                        
                }
            }
        }

        //Prints helps
        private void PrintHelp()
        {
            Console.WriteLine("\n");
            Console.WriteLine("D = distance from home");
            Console.WriteLine("F = File save/load");
            Console.WriteLine("I = show possible info selection");
            Console.WriteLine("L = caches to internal list");
            Console.WriteLine("M = show on map");
            Console.WriteLine("P = Print");
            Console.WriteLine("S = Set info for other functions");
        }

        //Filehandling submenu and functionality
        private void FileHandling()
        {
            char command;
            Console.WriteLine("File handling:\nS = Save\nL = Load");
            command = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine("");

            switch (command)
            {
                //Save cachelist to file
                case 'S':
                    if (0 != cacheList.caches.Count)
                    {
                        fileHandling.Save("Caches.bin", cacheList);
                        Console.WriteLine("Saved Caches.bin");
                    }
                    else
                        Console.WriteLine("Internal cache list is empty");
                    break;

                //Load cachelist from file
                case 'L':
                    object obj = fileHandling.Load("Caches.bin");
                    //Check that obj is not null
                    if (obj != null)
                        //Check that obj is wanted type
                        if (obj is CacheList)
                        {
                            cacheList = (CacheList)obj;
                            Console.WriteLine("Loaded Caches.bin");
                            break;
                        }

                    Console.WriteLine("Error in file loading");
                    break;

                default:
                    Console.WriteLine("Command ({0}) not valid!", command);
                    break;
            }
        }

        //Distance calculation
        private void Distance()
        {
            string code;

            Console.WriteLine("Give GC code which distance is shown");
            code = Console.ReadLine();
            int id = myReader.GetCacheID(code);

            if (id != -1)
            {
                Geocache geocache = myReader.GetCache(id);

                Console.WriteLine("Distance to {0} is {1:f2}m",code,geocache.GetDistance(home));
            }
            else
                Console.WriteLine("Not found \"{0}\"", code);
        }

        //Prints possible info options
        private void ShowInfos()
        {
            Console.WriteLine("\nCurrent set info is {0}", cacheInfo);
            Console.WriteLine("\nInfo options:");
            Console.WriteLine(GPX_Reader.CacheInfo.GC_Code.ToString());
            Console.WriteLine(GPX_Reader.CacheInfo.Difficulty.ToString());
            Console.WriteLine(GPX_Reader.CacheInfo.Terrain.ToString());
        }

        //Internal cache list filling
        private void ListFunctions()
        {
            string value;
            Console.WriteLine("Adds caches which includes given characters to internal cache list.\nGive characters to find from {0}:", cacheInfo.ToString());
            value = Console.ReadLine();

            cacheList.ID = cacheInfo.ToString() + ": " + value;
            foreach (var item in myReader.GetCaches(cacheInfo, value))
            {
                cacheList.caches.Add(myReader.GetCache(myReader.GetCacheID(item)));
            }
            Console.WriteLine("\n{0} caches in internal list",cacheList.caches.Count);
        }

        //Shows on map user specified cache
        private void ShowOnMap()
        {
            string code;

            Console.WriteLine("Give GC code which show on map");
            code = Console.ReadLine();
            int id = myReader.GetCacheID(code);

            if (id != -1)
            {
                Geocache geocache = myReader.GetCache(id);

                MapPin mapPin = new MapPin(geocache as ILocate);
                mapPin.ShowOnMap(geocache.GC_Code);
            }
            else
                Console.WriteLine("Not found \"{0}\"", code);
        }

        //Printing submenu
        private void Print()
        {
            char command;
            Console.WriteLine("G = All GC codes");
            Console.WriteLine("L = Internal cache list");
            command = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine("");

            switch (command)
            {
                case 'G':
                    PrintCodes();
                    break;

                case 'L':
                    PrintList();
                    break;

                default:
                    Console.WriteLine("Command ({0}) not valid!", command);
                    break;
            }
        }

        //Prints all GC codes in GPX to console
        private void PrintCodes()
        {
            List<string> codes = myReader.GetCacheInfo(GPX_Reader.CacheInfo.GC_Code);

            foreach (var item in codes)
            {
                Console.Write(item + ", ");
            }
            
        }

        //Prints all GC codes from internal list
        private void PrintList()
        {
            Console.WriteLine("");

            if (0 != cacheList.caches.Count)
            {
                foreach (var item in cacheList.caches)
                {
                    Console.WriteLine(item.GC_Code);
                }
            }
            else
                Console.WriteLine("Internal cache list is empty");
        }

        //Sets info for internal list filling
        private void SetInfo()
        {
            Console.WriteLine("Give info to set");
            string info = Console.ReadLine();

            if (GPX_Reader.CacheInfo.GC_Code.ToString() == info)
                cacheInfo = GPX_Reader.CacheInfo.GC_Code;
            else if (GPX_Reader.CacheInfo.Difficulty.ToString() == info)
                cacheInfo = GPX_Reader.CacheInfo.Difficulty;
            else if (GPX_Reader.CacheInfo.Terrain.ToString() == info)
                cacheInfo = GPX_Reader.CacheInfo.Terrain;
            else
                Console.WriteLine("Not correct info {0}", info);
        }
    }
}

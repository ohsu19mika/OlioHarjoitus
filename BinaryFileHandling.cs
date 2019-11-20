using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace OlioEsimerkki
{
    /// <summary>
    /// Saves & Loads object(s) to/from binary file
    /// </summary>
    class BinaryFileHandling
    {
        /// <summary>
        /// Saves object(s) to file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="savedObject"></param>
        public void Save(string filename, object savedObject)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);

            try
            {
                binaryFormatter.Serialize(fileStream, savedObject);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Virhe tallenuksessa: " + ex.ToString());
            }
            finally
            {
                fileStream.Close();
            }
        }


        /// <summary>
        /// Loads object(s) from file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public object Load(string filename)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
            object obj = new object();
            try
            {
                obj = binaryFormatter.Deserialize(fileStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Virhe latauksessa: " + ex.ToString());
            }
            finally
            {
                fileStream.Close();
            }
            return obj;
        }
    }
}

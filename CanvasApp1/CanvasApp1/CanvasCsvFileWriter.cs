using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace CanvasApp1
{
    class CanvasCsvFileWriter
    {
        public static void Write(string path, List<JObject> collection)
        {
            if (collection.Count < 1) {
                Console.WriteLine("There is nothing to serialize into a csv file.");
                return;
            }

            Console.WriteLine("Writing the CSV file...");

            List<string> properties = collection[0].Properties().Select(prop => prop.Name).ToList();
            try
            {
                //Put our streamwriters in using blocks so that they automatically flush when they fall out of scope
                using (StreamWriter writer = new StreamWriter(path))
                using (CsvWriter csv = new CsvWriter(writer))
                {
                    foreach (string p in properties)
                    {
                        csv.WriteField(p);
                    }
                    csv.NextRecord();

                    foreach (JObject job in collection)
                    {
                        foreach (string p in properties)
                        {
                            try
                            {
                                JToken value = job.Property(p).Value;
                                csv.WriteField(value != null ? value.ToString().Replace('\n', '\r') : " ");

                            }
                            catch (ArgumentException e)
                            {
                                csv.WriteField(" ");
                            }
                            catch (NullReferenceException e)
                            {
                                csv.WriteField(" ");
                            }

                        }
                        csv.NextRecord();
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error opening the file.", e);
            }catch (Exception e)
            {
                Console.WriteLine("There was an error writing the CSV file.", e);
            }
        }
    }
}

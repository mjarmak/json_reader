using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeneralKnowledge.Test.App.Tests
{
    /// <summary>
    /// Basic data retrieval from JSON test
    /// </summary>
    public class jsonObject
    {
        public string date { get; set; }
        public string temperature { get; set; }
        public string pH { get; set; }
        public string phosphate { get; set; }
        public string chloride { get; set; }
        public string nitrate { get; set; }
    }
    public class SamplesObject
    {
        public List<jsonObject> samples { get; set; }
    }

    public class JsonReadingTest : ITest
    {
        public string Name { get { return "JSON Reading Test";  } }

        public void Run()
        {
            var jsonData = Resources.SamplePoints;

            // TODO: 
            // Determine for each parameter stored in the variable below, the average value, lowest and highest number.
            // Example output
            // parameter   LOW AVG MAX
            // temperature   x   y   z
            // pH            x   y   z
            // Chloride      x   y   z
            // Phosphate     x   y   z
            // Nitrate       x   y   z

            // I created a jsonObjet class, and deserialized the jsonData using the NewtonSoft.json library
            // and I store all of it's values in a jsonObject list, then I compute low, avg and max for each
            // parameter of the jsonObject list.
            PrintOverview(jsonData);
        }

        private void PrintOverview(byte[] data)
        {
            float[] LOW = new float[5];
            float[] MAX = new float[5];
            float[] AVG = new float[5];
            float[] SUM = new float[5];
            float[] COUNT = new float[5];
            for (int i = 0; i < 5; i++) {
                LOW[i] = 10 ^ 16;
                MAX[i] = -10 ^ 16;
                AVG[i] = 0;
                SUM[i] = 0;
                COUNT[i] = 0;
            }
            float temp;
            int n = 0;
            int nb_samples = 0;
            string jsonStr = Encoding.UTF8.GetString(data);
            var variables = JsonConvert.DeserializeObject<SamplesObject>(jsonStr);
            foreach (jsonObject v in variables.samples)
            {
                nb_samples++;

                if (v.temperature != null)
                {
                    temp = float.Parse(v.temperature);
                    n = 0;
                    COUNT[n] ++;
                    SUM[n] += temp;
                    if (temp > MAX[n])
                        MAX[n] = temp;
                    else if (temp < LOW[n])
                        LOW[n] = temp;
                }

                if (v.pH != null)
                {
                    temp = float.Parse(v.pH);
                    n = 1;
                    COUNT[n]++;
                    SUM[n] += temp;
                    if (temp > MAX[n])
                        MAX[n] = temp;
                    else if (temp < LOW[n])
                        LOW[n] = temp;
                }

                if (v.phosphate != null)
                {
                    temp = float.Parse(v.phosphate);
                    n = 2;
                    COUNT[n]++;
                    SUM[n] += temp;
                    if (temp > MAX[n])
                        MAX[n] = temp;
                    else if (temp < LOW[n])
                        LOW[n] = temp;
                }

                if (v.chloride != null)
                {
                    temp = float.Parse(v.chloride);
                    n = 3;
                    COUNT[n]++;
                    SUM[n] += temp;
                    if (temp > MAX[n])
                        MAX[n] = temp;
                    else if (temp < LOW[n])
                        LOW[n] = temp;
                }

                if (v.nitrate != null)
                {
                    temp = float.Parse(v.nitrate);
                    n = 4;
                    COUNT[n]++;
                    SUM[n] += temp;
                    if (temp > MAX[n])
                        MAX[n] = temp;
                    else if (temp < LOW[n])
                        LOW[n] = temp;
                }
            }


            Console.WriteLine(String.Format("{0,-12} {1,-12} {2,-12} {3,-12}", "Parameter", "LOW", "AVG", "MAX"));
            string[] parameters = { "temperature", "pH", "phosphate", "chloride", "nitrate" };
            for (int i = 0; i < 5; i++)
            {
                if(COUNT[i] != 0)
                    AVG[i] = SUM[i] / COUNT[i];
                Console.WriteLine(String.Format("{0,-12} {1,-12} {2,-12} {3,-12}", parameters[i], LOW[i], AVG[i], MAX[i]));
            }
        }
    }
}

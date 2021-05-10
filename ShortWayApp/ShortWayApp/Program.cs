using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortWayApp
{
    public static class Program
    {
        public static string path;
        public static StringBuilder str1;
        public static StringBuilder str2;
        public static string[] citysArray;
        public static Dijkstra dijkstra;
        public static Graph g;

        [STAThread]
        static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            str1 = new StringBuilder();
            str2 = new StringBuilder();
            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"File.txt");
            citysArray = new string[6];
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    str1.Append(await sr.ReadToEndAsync());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            citysArray = CreateCitysArray(str1, citysArray);
            g = new Graph();
            
            foreach(var point in citysArray)
            {
                g.AddVertex(point);
            }

            GraphCreate(g, str1);

            dijkstra = new Dijkstra(g);
            dijkstra.InitInfo();
            Application.Run(new Form1());
        }

        public static string[] CreateCitysArray(StringBuilder str1, string[] citysArray)
        {
            for (int i = 0, j = 0; i < str1.Length; i++)
            {
                if (char.IsLetter(str1[i]))
                {
                    if (citysArray[0] == null)
                    {
                        citysArray[0] = str1[i].ToString();
                    }
                    else
                    {
                        int count = 0;
                        foreach (var city in citysArray)
                        {
                            if (str1[i].ToString() != city)
                            {
                                count++;
                            }
                        }
                        if (count + 1 != citysArray.Length)
                        {
                            citysArray[j] = str1[i].ToString();
                            j++;
                        }
                    }
                }
            }
            return citysArray;
        }

        public static void GraphCreate(Graph g, StringBuilder str1)
        {
            for (int i = 0; i < str1.Length; i++)
            {
                if (char.IsLetter(str1[i]) && char.IsLetter(str1[i + 1]))
                {
                    string name = str1[i] + "" + str1[i + 1];
                    i += 5;
                    if (char.IsNumber(str1[i]))
                    {
                        StringBuilder st = new StringBuilder();
                        while (char.IsNumber(str1[i]))
                        {
                            st.Append(str1[i]);
                            if (i + 1 == str1.Length) break;
                            else i++;
                        }
                        g.AddEdge(name[0].ToString(), name[1].ToString(), int.Parse(st.ToString()));
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortWayApp
{
    public class Graph
    {
        public List<GraphVertex> Vertices { get; }

        public Graph()
        {
            Vertices = new List<GraphVertex>();
        }

        public void AddVertex(string vertexName)
        {
            Vertices.Add(new GraphVertex(vertexName));
        }

        public GraphVertex FindVertex(string vertexName)
        {
            foreach (var v in Vertices)
            {
                if (v.Name.Equals(vertexName))
                {
                    return v;
                }
            }

            return null;
        }

        public GraphVertex[] FindVertex(string[] vertexName)
        {
            int count = 0;
            foreach (var v in Vertices)
            {
                int index = Array.IndexOf(vertexName, v.Name);
                if (index >  -1)
                {
                    count++;
                }
            }
            GraphVertex[] arr = new GraphVertex[count];
            count = 0;
            foreach (var v in Vertices)
            {
                int index = Array.IndexOf(vertexName, v.Name);
                if (index > -1)
                {
                    arr[count] = v;
                    count++;
                }
            }

            return arr;
        }

        public void AddEdge(string firstName, string secondName, int weight)
        {
            var v1 = FindVertex(firstName);
            var v2 = FindVertex(secondName);
            if (v2 != null && v1 != null)
            {
                v1.AddEdge(v2, weight);
                v2.AddEdge(v1, weight);
            }
        }
    }
}

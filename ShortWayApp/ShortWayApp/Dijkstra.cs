using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortWayApp
{
    public class Dijkstra
    {
        Graph graph;

    List<GraphVertexInfo> infos;

    public Dijkstra(Graph graph)
    {
        this.graph = graph;
    }

    public void InitInfo()
    {
        infos = new List<GraphVertexInfo>();
        foreach (var v in graph.Vertices)
        {
            infos.Add(new GraphVertexInfo(v));
        }
    }

    // Получение информации о вершине графа
    public GraphVertexInfo GetVertexInfo(GraphVertex v)
    {

        foreach (var i in infos)
        {
            if (i.Vertex.Equals(v))
            {
                return i;
            }
        }

        return null;
    }

    // Поиск непосещенной вершины с минимальным значением суммы
    public GraphVertexInfo FindUnvisitedVertexWithMinSum()
    {
        var minValue = int.MaxValue;
        GraphVertexInfo minVertexInfo = null;
        foreach (var i in infos)
        {
            if (i.IsUnvisited && i.EdgesWeightSum < minValue)
            {
                minVertexInfo = i;
                minValue = i.EdgesWeightSum;
            }
        }

        return minVertexInfo;
    }

    // Поиск кратчайшего пути по названиям вершин
    public string FindShortestPath(string startName, string finishName)
    {
        return FindShortestPath(graph.FindVertex(startName), graph.FindVertex(finishName));
    }

        public string FindShortestPath(string startName, string[] finishName, string previousVert)
        {
            return FindShortestPath(graph.FindVertex(startName), graph.FindVertex(finishName), graph.FindVertex(previousVert));
        }


        // Поиск кратчайшего пути по вершинам
        public string FindShortestPath(GraphVertex startVertex, GraphVertex finishVertex)
    {
        InitInfo();
        var first = GetVertexInfo(startVertex);
        first.EdgesWeightSum = 0;
        while (true)
        {
            var current = FindUnvisitedVertexWithMinSum();
            if (current == null)
            {
                break;
            }

            SetSumToNextVertex(current);
        }

        return GetPath(startVertex, finishVertex);
    }

        public string FindShortestPath(GraphVertex startVertex, GraphVertex[] finishVertex, GraphVertex previousVert)
        {
            InitInfo();
            var first = GetVertexInfo(startVertex);
            first.EdgesWeightSum = 0;
            int minVal = int.MaxValue;
            GraphVertexInfo v = null;
            while (true)
            {
                var current = FindUnvisitedVertexWithMinSum();
                if (current == null)
                {

                    break;
                }
                else
                {
                    SetSumToNextVertex(current);
                    int index = -1;
                    foreach (var t in finishVertex)
                    {
                        if (t.Name == current.Vertex.Name)
                        {
                            index = 0;
                        }
                    }  
                    if(minVal > current.EdgesWeightSum && index == 0 && current.Vertex != startVertex && current.IsUnvisited == false && current.Vertex != previousVert)
                    {
                        minVal = current.EdgesWeightSum;
                        v = current;
                    }
                }
            }
            if (v.PreviousVertex != previousVert && previousVert != null)
            {
                return FindShortestPath(startVertex, v.Vertex).Substring(1);
            }
            else if(v.PreviousVertex == previousVert)
            {
                Array.Resize(ref Form1.checkedCitys, Form1.checkedCitys.Length + 1);
                return previousVert.Name;
            }
            
            return v.Vertex.Name;
        }

        public void SetSumToNextVertex(GraphVertexInfo info)
    {
        info.IsUnvisited = false; // Вершина не посещена
        foreach (var e in info.Vertex.Edges) // перебор ребер вершины
        {
            var nextInfo = GetVertexInfo(e.ConnectedVertex);
            var sum = info.EdgesWeightSum + e.EdgeWeight;
            if (sum < nextInfo.EdgesWeightSum)
            {
                nextInfo.EdgesWeightSum = sum;
                nextInfo.PreviousVertex = info.Vertex;
            }
        }
    }

        string GetPath(GraphVertex startVertex, GraphVertex endVertex)
    {
        var path = endVertex.ToString();
        while (startVertex != endVertex)
        {
            endVertex = GetVertexInfo(endVertex).PreviousVertex;
            path = endVertex.ToString() + path;
        }

        return path;
    }
    }
}

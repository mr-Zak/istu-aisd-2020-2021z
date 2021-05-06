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

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="graph">Граф</param>
    public Dijkstra(Graph graph)
    {
        this.graph = graph;
    }

    /// <summary>
    /// Инициализация информации
    /// </summary>
    public void InitInfo()
    {
        infos = new List<GraphVertexInfo>();
        foreach (var v in graph.Vertices)
        {
            infos.Add(new GraphVertexInfo(v));
        }
    }

    /// <summary>
    /// Получение информации о вершине графа
    /// </summary>
    /// <param name="v">Вершина</param>
    /// <returns>Информация о вершине</returns>
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

    /// <summary>
    /// Поиск непосещенной вершины с минимальным значением суммы
    /// </summary>
    /// <returns>Информация о вершине</returns>
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

    /// <summary>
    /// Поиск кратчайшего пути по названиям вершин
    /// </summary>
    /// <param name="startName">Название стартовой вершины</param>
    /// <param name="finishName">Название финишной вершины</param>
    /// <returns>Кратчайший путь</returns>
    public string FindShortestPath(string startName, string finishName)
    {
        return FindShortestPath(graph.FindVertex(startName), graph.FindVertex(finishName));
    }

        public string FindShortestPath(string startName, string[] finishName, string result, int count)
        {
            return FindShortestPath(graph.FindVertex(startName), graph.FindVertex(finishName), result, count);
        }


        /// <summary>
        /// Поиск кратчайшего пути по вершинам
        /// </summary>
        /// <param name="startVertex">Стартовая вершина</param>
        /// <param name="finishVertex">Финишная вершина</param>
        /// <returns>Кратчайший путь</returns>
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

        public string FindShortestPath(GraphVertex startVertex, GraphVertex[] finishVertex, string result, int count)
        {
            InitInfo();
            var first = GetVertexInfo(startVertex);
            first.EdgesWeightSum = 0;
            int minVal = int.MaxValue;
            int s = 0;
            GraphVertexInfo v = null;
            while (true)
            {
                s = 0;
                foreach(var vert in finishVertex)
                {
                    int id = result.IndexOf(vert.Name);
                    if(id > -1)
                    {
                        s++;
                    }
                }
                if(s == finishVertex.Length - 1)
                {
                    break;
                }
                var current = FindUnvisitedVertexWithMinSum();
                if(current == null)
                {

                        if(v.IsUnvisited == false)
                        {
                            result += v.Vertex.Name + "(город адресат)";
                            v.IsUnvisited = true;
                        }
                        else
                        {
                            result += v.Vertex.Name + "(Транзитный город)";
                        }
                        startVertex = v.Vertex;
                        FindShortestPath(startVertex, finishVertex, result, 0);
                    
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
                    if(minVal > current.EdgesWeightSum && index == 0 && current.Vertex != startVertex && current.IsUnvisited == false)
                    {
                        minVal = current.EdgesWeightSum;
                        v = current;
                    }
                }
                
            }
            return result;
        }

        /// <summary>
        /// Вычисление суммы весов ребер для следующей вершины
        /// </summary>
        /// <param name="info">Информация о текущей вершине</param>
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

        public string SetSumToNextVertexX(GraphVertexInfo info)
        {
            info.IsUnvisited = false; // Вершина не посещена
            int summa = 0;
            foreach (var e in info.Vertex.Edges) // перебор ребер вершины
            {
                var nextInfo = GetVertexInfo(e.ConnectedVertex);
                var sum = info.EdgesWeightSum + e.EdgeWeight;
                if (sum < nextInfo.EdgesWeightSum)
                {
                    nextInfo.EdgesWeightSum = sum;
                    summa = sum;
                    nextInfo.PreviousVertex = info.Vertex;
                }
            }
            return summa.ToString();
        }

        /// <summary>
        /// Формирование пути
        /// </summary>
        /// <param name="startVertex">Начальная вершина</param>
        /// <param name="endVertex">Конечная вершина</param>
        /// <returns>Путь</returns>
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

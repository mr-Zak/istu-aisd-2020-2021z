﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortWayApp
{
    public class GraphVertex
    {
        /// <summary>
        /// Название вершины
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Список ребер
        /// </summary>
        public List<GraphEdge> Edges { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="vertexName">Название вершины</param>
        public GraphVertex(string vertexName)
        {
            Name = vertexName;
            Edges = new List<GraphEdge>();
        }

        /// <summary>
        /// Добавить ребро
        /// </summary>
        /// <param name="newEdge">Ребро</param>
        public void AddEdge(GraphEdge newEdge)
        {
            Edges.Add(newEdge);
        }

        /// <summary>
        /// Добавить ребро
        /// </summary>
        /// <param name="vertex">Вершина</param>
        /// <param name="edgeWeight">Вес</param>
        public void AddEdge(GraphVertex vertex, int edgeWeight)
        {
            AddEdge(new GraphEdge(vertex, edgeWeight));
        }

        /// <summary>
        /// Преобразование в строку
        /// </summary>
        /// <returns>Имя вершины</returns>
        public override string ToString() => Name;
    }
}

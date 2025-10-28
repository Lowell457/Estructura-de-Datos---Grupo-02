using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyALGraph<T>
{
    // Each vertex has a list of (neighbor, weight)
    private Dictionary<T, List<(T, int)>> adjacencyList = new Dictionary<T, List<(T, int)>>();

    /// Returns all vertices in the graph.
    public IEnumerable<T> Vertices => adjacencyList.Keys;


    /// Adds a new vertex if it doesn't exist yet.
    public void AddVertex(T vertex)
    {
        if (!adjacencyList.ContainsKey(vertex))
        {
            adjacencyList[vertex] = new List<(T, int)>();
        }
    }


    /// Removes a vertex and all edges connected to it.
    public void RemoveVertex(T vertex)
    {
        if (!adjacencyList.ContainsKey(vertex))
            return;

        adjacencyList.Remove(vertex);

        // Remove all edges that point to this vertex
        foreach (var list in adjacencyList.Values)
        {
            list.RemoveAll(edge => EqualityComparer<T>.Default.Equals(edge.Item1, vertex));
        }
    }

    /// Adds an edge from 'from' to 'to' with a given weight.
    public void AddEdge(T from, (T to, int weight) edge)
    {
        if (!adjacencyList.ContainsKey(from))
            AddVertex(from);
        if (!adjacencyList.ContainsKey(edge.to))
            AddVertex(edge.to);

        // Prevent duplicate edges
        if (!ContainsEdge(from, edge.to))
            adjacencyList[from].Add(edge);
    }


    /// Removes the edge from 'from' to 'to' if it exists.
    public void RemoveEdge(T from, T to)
    {
        if (adjacencyList.ContainsKey(from))
        {
            adjacencyList[from].RemoveAll(e => EqualityComparer<T>.Default.Equals(e.Item1, to));
        }
    }

    /// Returns true if a vertex exists.
    public bool ContainsVertex(T vertex) => adjacencyList.ContainsKey(vertex);

    /// Returns true if an edge exists between two vertices.
    public bool ContainsEdge(T from, T to)
    {
        return adjacencyList.ContainsKey(from) && adjacencyList[from].Any(e => EqualityComparer<T>.Default.Equals(e.Item1, to));
    }

    /// Returns the weight of an edge if it exists, or int.MinValue if not.
    public int GetWeight(T from, T to)
    {
        if (adjacencyList.ContainsKey(from))
        {
            var edge = adjacencyList[from].FirstOrDefault(e => EqualityComparer<T>.Default.Equals(e.Item1, to));
            if (!EqualityComparer<(T, int)>.Default.Equals(edge, default))
                return edge.Item2;
        }

        return int.MinValue; // indicates "no edge"
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model
{
	public class Graph<T>
	{
		private Dictionary<T, List<T>> _adjacencyList = new Dictionary<T, List<T>>();

		// Add a node to the graph
		public void AddNode(T node)
		{
			if (!_adjacencyList.ContainsKey(node))
			{
				_adjacencyList[node] = new List<T>();
			}
		}

		// Add an edge between two nodes (directed edge from node1 to node2)
		public void AddEdge(T node1, T node2)
		{
			if (!_adjacencyList.ContainsKey(node1))
			{
				AddNode(node1);
			}

			if (!_adjacencyList.ContainsKey(node2))
			{
				AddNode(node2);
			}

			_adjacencyList[node1].Add(node2);
		}

		// Get adjacent nodes for a given node
		public List<T> GetAdjacentNodes(T node)
		{
			return _adjacencyList.ContainsKey(node) ? _adjacencyList[node] : new List<T>();
		}
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
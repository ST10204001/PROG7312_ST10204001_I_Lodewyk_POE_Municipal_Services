using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model
{
	public class Graph
	{
		public Dictionary<int, ServiceRequest> Requests { get; set; }
		public Dictionary<int, List<Edge>> AdjacencyList { get; set; }

		public Graph()
		{
			Requests = new Dictionary<int, ServiceRequest>();
			AdjacencyList = new Dictionary<int, List<Edge>>();
		}

		// Add request to the graph
		public void AddRequest(ServiceRequest request)
		{
			if (!Requests.ContainsKey(request.Id))
			{
				Requests.Add(request.Id, request);
				AdjacencyList[request.Id] = new List<Edge>();
			}
		}


		// Add an edge between two service requests
		public void AddEdge(int fromId, int toId, double weight)
		{
			// Ensure both nodes exist in Requests dictionary
			if (!Requests.ContainsKey(fromId))
			{
				Requests[fromId] = new ServiceRequest { Id = fromId };
			}

			if (!Requests.ContainsKey(toId))
			{
				Requests[toId] = new ServiceRequest { Id = toId };
			}

			// Ensure both nodes exist in the adjacency list
			if (!AdjacencyList.ContainsKey(fromId))
			{
				AdjacencyList[fromId] = new List<Edge>();
			}

			if (!AdjacencyList.ContainsKey(toId))
			{
				AdjacencyList[toId] = new List<Edge>();
			}

			// Create and add an edge
			var edge = new Edge(Requests[fromId], Requests[toId], weight);
			AdjacencyList[fromId].Add(edge);
		}



		public List<Edge> GetAllEdges()
		{
			var edges = new List<Edge>();
			foreach (var edgeList in AdjacencyList.Values)
			{
				edges.AddRange(edgeList);
			}
			return edges;
		}

		public int NodeCount => Requests.Count;

	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
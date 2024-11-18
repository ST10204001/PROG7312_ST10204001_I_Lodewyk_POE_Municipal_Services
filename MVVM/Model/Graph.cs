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
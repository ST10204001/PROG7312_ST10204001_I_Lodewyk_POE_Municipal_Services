using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.DataStructures
{
	public class MinimumSpanningTree
	{
		public List<Edge> GenerateMST(Graph graph, int startNodeId)
		{
			List<Edge> mstEdges = new List<Edge>();

			// Example: Kruskal's Algorithm
			var edges = graph.GetAllEdges(); // This should return all the edges in the graph
			var sortedEdges = edges.OrderBy(e => e.Weight).ToList(); // Sort edges by weight

			var disjointSet = new DisjointSet(graph.NodeCount); // Implement DisjointSet for cycle detection

			foreach (var edge in sortedEdges)
			{
				// Use the Id of the ServiceRequest for the disjoint set operations
				startNodeId = edge.Start.Id; // Use the Id of the start request
				int endNodeId = edge.End.Id;     // Use the Id of the end request

				// If the nodes are not in the same set, add this edge to the MST
				if (disjointSet.Find(startNodeId) != disjointSet.Find(endNodeId))
				{
					mstEdges.Add(edge);
					disjointSet.Union(startNodeId, endNodeId); // Union the sets by Id
				}
			}

			return mstEdges;
		}
	}

}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
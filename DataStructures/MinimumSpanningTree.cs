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

			// Map node IDs to sequential indices
			var idMapping = graph.Requests.Keys.Select((id, index) => new { id, index })
											   .ToDictionary(x => x.id, x => x.index);

			// Initialize DisjointSet with the number of unique nodes
			var disjointSet = new DisjointSet(idMapping.Count);

			// Get all edges and sort them by weight
			var edges = graph.GetAllEdges(); // Assuming this returns all edges in the graph
			var sortedEdges = edges.OrderBy(e => e.Weight).ToList();

			foreach (var edge in sortedEdges)
			{
				// Use the mapped indices for Find and Union operations
				int startMapped = idMapping[edge.Start.Id];
				int endMapped = idMapping[edge.End.Id];

				if (disjointSet.Find(startMapped) != disjointSet.Find(endMapped))
				{
					mstEdges.Add(edge);
					disjointSet.Union(startMapped, endMapped);
				}
			}

			return mstEdges;
		}

	}

}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
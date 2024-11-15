using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model
{
	public class Graph
	{
		public Dictionary<int, List<int>> AdjacencyList { get; set; } = new Dictionary<int, List<int>>();

		public void AddEdge(int requestId1, int requestId2)
		{
			if (!AdjacencyList.ContainsKey(requestId1))
				AdjacencyList[requestId1] = new List<int>();

			if (!AdjacencyList.ContainsKey(requestId2))
				AdjacencyList[requestId2] = new List<int>();

			AdjacencyList[requestId1].Add(requestId2);
			AdjacencyList[requestId2].Add(requestId1); // For an undirected graph
		}

		public List<int> TraverseBFS(int startRequestId)
		{
			var visited = new HashSet<int>();
			var queue = new Queue<int>();
			var result = new List<int>();

			queue.Enqueue(startRequestId);
			visited.Add(startRequestId);

			while (queue.Count > 0)
			{
				int current = queue.Dequeue();
				result.Add(current);

				foreach (int neighbor in AdjacencyList[current])
				{
					if (!visited.Contains(neighbor))
					{
						queue.Enqueue(neighbor);
						visited.Add(neighbor);
					}
				}
			}

			return result;
		}
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
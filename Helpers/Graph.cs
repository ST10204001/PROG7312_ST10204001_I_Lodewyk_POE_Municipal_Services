using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.Classes
{
	public class Graph
	{
		private Dictionary<int, List<int>> _adjacencyList;

		public Graph()
		{
			_adjacencyList = new Dictionary<int, List<int>>();
		}

		public void AddDependency(int requestID, int dependentRequestID)
		{
			if (!_adjacencyList.ContainsKey(requestID))
				_adjacencyList[requestID] = new List<int>();
			_adjacencyList[requestID].Add(dependentRequestID);
		}

		public List<int> GetDependencies(int requestID)
		{
			return _adjacencyList.ContainsKey(requestID) ? _adjacencyList[requestID] : new List<int>();
		}
	}
}

using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.DataStructures;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.Helpers
{
	public class RequestManager
	{
		private Dictionary<int, HashSet<ServiceRequest>> _allRequests = new Dictionary<int, HashSet<ServiceRequest>>();

		// Add a new request (ensuring uniqueness based on Id)
		public void AddRequest(ServiceRequest request)
		{
			if (!_allRequests.ContainsKey(request.Id))
			{
				_allRequests[request.Id] = new HashSet<ServiceRequest>();
			}

			_allRequests[request.Id].Add(request);
		}

		public int GetNextAvailableId()
		{
			return _allRequests.Keys.DefaultIfEmpty(0).Max() + 1;
		}

		// Display requests
		public void DisplayRequests()
		{
			foreach (var requestSet in _allRequests.Values)
			{
				foreach (var request in requestSet)
				{
					Console.WriteLine($"Request ID: {request.Id}, Description: {request.Description}, Status: {request.Status}, Priority: {request.Priority}");
				}
			}
		}
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
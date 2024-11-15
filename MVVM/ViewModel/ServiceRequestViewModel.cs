using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.Helpers;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.ViewModel
{
	public class ServiceRequestViewModel : ViewModelBase
	{
		private AVLTree<ServiceRequest> _serviceRequests;
		private Graph _serviceRequestGraph;

		public ObservableCollection<ServiceRequest> ServiceRequests { get; set; }

		public ServiceRequestViewModel()
		{
			_serviceRequests = new AVLTree<ServiceRequest>();
			ServiceRequests = new ObservableCollection<ServiceRequest>();
			_serviceRequestGraph = new Graph();

			// Add hardcoded data
			AddHardcodedData();
			TestAVLTree();
			TestGraph();
		}

		private void TestAVLTree()
		{
			Console.WriteLine("AVL Tree Root ID: " + _serviceRequests.Root?.Value.Id);
			Console.WriteLine("Is Tree Balanced: " + _serviceRequests.IsBalanced());
		}

		private void TestGraph()
		{
			// Example: Traverse from the first request and print related IDs
			var relatedRequests = _serviceRequestGraph.TraverseBFS(1);
			Console.WriteLine("Related Service Requests:");
			foreach (var requestId in relatedRequests)
				Console.WriteLine(requestId);
		}


		private void AddHardcodedData()
		{
			_serviceRequestGraph = new Graph();
			for (int i = 1; i <= 50; i++)
			{
				var serviceRequest = new ServiceRequest
				{
					Id = i,
					Description = $"Service request #{i}",
					Status = i % 2 == 0 ? "Completed" : "Pending", // Alternating between "Completed" and "Pending"
					RequestDate = DateTime.Now.AddDays(-i), // Stagger the request dates
					Priority = (i % 3 == 0) ? "High" : (i % 2 == 0 ? "Medium" : "Low") // Alternating priorities
				};

				_serviceRequests.Insert(serviceRequest); // Assuming AVLTree has an Insert method
				ServiceRequests.Add(serviceRequest); // Add to ObservableCollection for UI binding
				
				if (i > 1)
				{
					_serviceRequestGraph.AddEdge(i, i - 1); // Sample edge between consecutive requests
				}
			}
		}

		public void AddRequest(ServiceRequest request)
		{
			_serviceRequests.Insert(request);
			ServiceRequests.Add(request);  // Update observable collection for UI binding
		}

		public ServiceRequest SearchRequest(int id)
		{
			ServiceRequest searchRequest = new ServiceRequest { Id = id };
			return _serviceRequests.Find(searchRequest);  // Pass ServiceRequest object
		}

		public void UpdateRequestStatus(int id, string status)
		{
			var request = SearchRequest(id);
			if (request != null)
			{
				request.Status = status;
				OnPropertyChanged(nameof(ServiceRequests));
			}
		}

	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
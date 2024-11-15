using Haley.Utils;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.Helpers;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.View.Windows;
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
		// Private members for the service requests, graph, selected request, and filters
		private AVLTree<ServiceRequest> _serviceRequests;
		private Graph _serviceRequestGraph;
		public ObservableCollection<ServiceRequest> ServiceRequests { get; set; }
		private ServiceRequest _selectedRequest;
		private ObservableCollection<ServiceRequest> _filteredRequests;

		// Filter properties for search, status, and priority
		private string _searchText;
		private string _selectedStatusFilter = "All";  // Default to "All"
		private string _selectedPriorityFilter = "All";  // Default to "All"

		// Property for filtered requests, triggers property changed notification
		public ObservableCollection<ServiceRequest> FilteredRequests
		{
			get { return _filteredRequests; }
			set
			{
				_filteredRequests = value;
				OnPropertyChanged(nameof(FilteredRequests));
			}
		}

		// Property for search text input, triggers filtering when changed
		public string SearchText
		{
			get { return _searchText; }
			set
			{
				_searchText = value;
				OnPropertyChanged(nameof(SearchText));
				FilterRequests();  // Apply filter when search text changes
			}
		}

		// Property for selected status filter, triggers filtering when changed
		public string SelectedStatusFilter
		{
			get { return _selectedStatusFilter; }
			set
			{
				_selectedStatusFilter = value;
				OnPropertyChanged(nameof(SelectedStatusFilter));
				FilterRequests();  // Apply filter when status filter changes
			}
		}

		// Property for selected priority filter, triggers filtering when changed
		public string SelectedPriorityFilter
		{
			get { return _selectedPriorityFilter; }
			set
			{
				_selectedPriorityFilter = value;
				OnPropertyChanged(nameof(SelectedPriorityFilter));
				FilterRequests();  // Apply filter when priority filter changes
			}
		}

		// Property for the selected service request, triggers request detail view
		public ServiceRequest SelectedRequest
		{
			get => _selectedRequest;
			set
			{
				_selectedRequest = value;
				OnPropertyChanged(nameof(SelectedRequest));
				if (_selectedRequest != null)
				{
					ShowRequestDetails();  // Show details when a request is selected
				}
			}
		}

		//----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Method to filter the list of service requests based on search text, status, and priority
		/// </summary>
		public void FilterRequests()
		{
			// Get all service requests using in-order traversal
			var serviceRequestList = _serviceRequests.InOrderTraversal();

			// Apply filters for search text, status, and priority
			var filtered = serviceRequestList
				.Where(r =>
					!string.IsNullOrEmpty(r.Description) &&
					(string.IsNullOrEmpty(SearchText) || r.Description.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0) &&
					(string.IsNullOrEmpty(SelectedStatusFilter) || SelectedStatusFilter == "All" || r.Status == SelectedStatusFilter) &&
					(string.IsNullOrEmpty(SelectedPriorityFilter) || SelectedPriorityFilter == "All" || r.Priority == SelectedPriorityFilter))
				.ToList();

			// Clear the existing items in ServiceRequests
			ServiceRequests.Clear();

			// Add the filtered items to ServiceRequests
			foreach (var request in filtered)
			{
				ServiceRequests.Add(request);
			}
		}
		//----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Method to show details of the selected service request in a new window
		/// </summary>
		private void ShowRequestDetails()
		{
			var detailWindow = new RequestDetailWindow(SelectedRequest);
			detailWindow.ShowDialog();
		}
		//----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Default Constructor: Initializes data structures, adds hardcoded data, and tests AVLTree and Graph functionality
		/// </summary>
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
		//----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Method to test AVL Tree functionality (root and balance status)
		/// </summary>
		private void TestAVLTree()
		{
			Console.WriteLine("AVL Tree Root ID: " + _serviceRequests.Root?.Value.Id);
			Console.WriteLine("Is Tree Balanced: " + _serviceRequests.IsBalanced());
		}
		//----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Method to test Graph functionality (BFS traversal)
		/// </summary>
		private void TestGraph()
		{
			// Example: Traverse from the first request and print related IDs
			var relatedRequests = _serviceRequestGraph.TraverseBFS(1);
			Console.WriteLine("Related Service Requests:");
			foreach (var requestId in relatedRequests)
				Console.WriteLine(requestId);
		}
		//----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Method to add hardcoded service requests to both the AVL Tree and ObservableCollection
		/// </summary>
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
		//----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Method to add a new service request to the AVL Tree and ObservableCollection
		/// </summary>
		/// <param name="request"></param>
		public void AddRequest(ServiceRequest request)
		{
			_serviceRequests.Insert(request);
			ServiceRequests.Add(request);  // Update observable collection for UI binding
		}
		//----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Method to search for a service request by ID in the AVL Tree
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ServiceRequest SearchRequest(int id)
		{
			ServiceRequest searchRequest = new ServiceRequest { Id = id };
			return _serviceRequests.Find(searchRequest);  // Pass ServiceRequest object
		}
		//----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Method to update the status of a service request by its ID
		/// </summary>
		/// <param name="id"></param>
		/// <param name="status"></param>
		public void UpdateRequestStatus(int id, string status)
		{
			var request = SearchRequest(id);
			if (request != null)
			{
				request.Status = status;
				OnPropertyChanged(nameof(ServiceRequests));
			}
		}
		//----------------------------------------------------------------------------------------------------------------------------//
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

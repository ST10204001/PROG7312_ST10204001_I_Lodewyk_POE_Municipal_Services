using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.DataStructures;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.ViewModel
{
	public class ServiceRequestViewModel : ViewModelBase
	{
		// ----------------------------------------------------------------------------------------------------------------------------
		// Private Members
		private AVLTree<ServiceRequest> _serviceRequests;
		private Graph _serviceRequestGraph;
		private HashSet<int> _usedIds = new HashSet<int>();
		private ServiceRequest _selectedRequest;
		private ObservableCollection<ServiceRequest> _filteredRequests;
		private int _serviceRequestCount = 0;

		// Filter properties
		private string _searchText;
		private string _selectedStatusFilter = "All";
		private string _selectedPriorityFilter = "All";

		// Static file path for persistence
		private static readonly string FILEPATH = GetFilePath();

		// ----------------------------------------------------------------------------------------------------------------------------
		// Properties

		public int NextAvailableId
		{
			get
			{
				int nextId = GetNextRequestId();
				return nextId;
			}
		}

		public ObservableCollection<ServiceRequest> ServiceRequests { get; set; }

		public ObservableCollection<ServiceRequest> FilteredRequests
		{
			get => _filteredRequests;
			set
			{
				_filteredRequests = value;
				OnPropertyChanged(nameof(FilteredRequests));
			}
		}

		public string SearchText
		{
			get => _searchText;
			set
			{
				_searchText = value;
				OnPropertyChanged(nameof(SearchText));
				FilterRequests(); // Trigger filtering
			}
		}

		public string SelectedStatusFilter
		{
			get => _selectedStatusFilter;
			set
			{
				_selectedStatusFilter = value;
				OnPropertyChanged(nameof(SelectedStatusFilter));
				FilterRequests(); // Trigger filtering
			}
		}

		public string SelectedPriorityFilter
		{
			get => _selectedPriorityFilter;
			set
			{
				_selectedPriorityFilter = value;
				OnPropertyChanged(nameof(SelectedPriorityFilter));
				FilterRequests(); // Trigger filtering
			}
		}

		public ServiceRequest SelectedRequest
		{
			get => _selectedRequest;
			set
			{
				_selectedRequest = value;
				OnPropertyChanged(nameof(SelectedRequest));
				if (_selectedRequest != null)
				{
					ShowRequestDetails(); // Display details on selection
				}
			}
		}

		// ----------------------------------------------------------------------------------------------------------------------------
		// Default Constructor

		/// <summary>
		/// Initializes the ServiceRequestViewModel by loading existing requests and setting up structures.
		/// </summary>
		public ServiceRequestViewModel()
		{
			_usedIds.Clear();

			_serviceRequests = new AVLTree<ServiceRequest>();
			ServiceRequests = new ObservableCollection<ServiceRequest>();
			_serviceRequestGraph = new Graph();

			LoadExistingRequests();

			TestAVLTree();
			//TestGraph();
		}

		// ----------------------------------------------------------------------------------------------------------------------------
		// Public Methods

		/// <summary>
		/// Adds a new service request to the AVL Tree and ObservableCollection.
		/// </summary>
		public void AddNewRequest(string description, string status, string priority, DateTime date)
		{
			var newRequest = new ServiceRequest
			{
				Id = NextAvailableId,
				Description = description,
				Status = status,
				Priority = priority,
				RequestDate = date
			};
     
			AddRequest(newRequest);
		}

		/// <summary>
		/// Searches for a service request by ID in the AVL Tree.
		/// </summary>
		public ServiceRequest SearchRequest(int id) =>
			_serviceRequests.Find(new ServiceRequest { Id = id });


		/// <summary>
		/// Filters service requests based on search text, status, and priority.
		/// </summary>
		public void FilterRequests()
		{
			var serviceRequestList = _serviceRequests.InOrderTraversal();

			var filtered = serviceRequestList
				.Where(r =>
					(string.IsNullOrEmpty(SearchText) || r.Description?.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0) &&
					(SelectedStatusFilter == "All" || r.Status == SelectedStatusFilter) &&
					(SelectedPriorityFilter == "All" || r.Priority == SelectedPriorityFilter))
				.ToList();

			ServiceRequests.Clear();
			foreach (var request in filtered)
			{
				ServiceRequests.Add(request);
			}
		}

		// ----------------------------------------------------------------------------------------------------------------------------
		// Private Methods

		/// <summary>
		/// Loads existing service requests from the JSON file.
		/// </summary>
		private void LoadExistingRequests()
		{
			_usedIds.Clear();

			_serviceRequests = AVLTree<ServiceRequest>.LoadFromJson(FILEPATH, request =>
			{
				ServiceRequests.Add(request);
				_usedIds.Add(request.Id);
			}) ?? new AVLTree<ServiceRequest>();

			// Ensure NextAvailableId is updated after loading
			OnPropertyChanged(nameof(NextAvailableId));
		}


		/// <summary>
		/// Adds a new service request to the data structures.
		/// </summary>
		private void AddRequest(ServiceRequest request)
		{
			// Ensure the ID is unique before adding it to the used IDs set
			if (_usedIds.Contains(request.Id))
			{
				throw new InvalidOperationException($"Duplicate ID detected: {request.Id}");
			}

			// Add the new request to the AVL tree and the ObservableCollection
			_serviceRequests.Insert(request);
			ServiceRequests.Add(request);

			// Add the new ID to the used IDs set now that it's confirmed to be unique
			_usedIds.Add(request.Id);

			// Save the updated requests to the JSON file
			_serviceRequests.SaveToJson(FILEPATH);
		}

		/// <summary>
		/// Generates a unique ID for a new service request.
		/// </summary>
		private int GetNextRequestId()
		{
			int newId = _usedIds.DefaultIfEmpty(0).Max() + 1; // Generate the next ID based on the maximum used ID

			// Ensure that the ID is unique by checking before returning it
			while (_usedIds.Contains(newId))
			{
				newId++;  // If the ID already exists, increment until a unique one is found
			}

			Console.WriteLine("Generated Request ID: " + newId);

			// Return the unique ID (it will never conflict with any existing ones)
			return newId;
		}

		/// <summary>
		/// Displays details of the selected service request.
		/// </summary>
		private void ShowRequestDetails()
		{
			var detailWindow = new RequestDetailWindow(SelectedRequest);
			detailWindow.ShowDialog();
		}

		/// <summary>
		/// Tests AVL Tree functionality (root and balance status).
		/// </summary>
		private void TestAVLTree()
		{
			Console.WriteLine("AVL Tree Root ID: " + _serviceRequests.Root?.Value.Id);
			Console.WriteLine("Is Tree Balanced: " + _serviceRequests.IsBalanced());
		}

		/// <summary>
		/// Tests graph functionality (BFS traversal).
		/// </summary>
		private void TestGraph()
		{
			Graph graph = new Graph();
			//graph.LoadFromJson(FILEPATH);
			var relatedRequests = _serviceRequestGraph.TraverseBFS(1);
			Console.WriteLine("Related Service Requests:");
			foreach (var requestId in relatedRequests)
				Console.WriteLine(requestId);
		}

		/// <summary>
		/// Ensures the directory for the JSON file exists.
		/// </summary>
		private static void EnsureDirectoryExists(string filePath)
		{
			string directoryPath = Path.GetDirectoryName(filePath);
			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}
		}

		/// <summary>
		/// Gets the file path for storing service requests.
		/// </summary>
		private static string GetFilePath()
		{
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			string filePath = Path.Combine(baseDirectory, "File", "requests.json");
			EnsureDirectoryExists(filePath);
			return filePath;
		}
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
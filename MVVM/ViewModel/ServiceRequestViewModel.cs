using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.DataStructures;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.Helpers;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.ViewModel
{
	public class ServiceRequestViewModel : ViewModelBase
	{
		// Private Members
		private AVLTree<ServiceRequest> _serviceRequests;
		private readonly PriorityQueue<ServiceRequest> _priorityQueue; // Priority Queue for service requests based on priority
		private readonly Graph<ServiceRequest> _serviceRequestGraph; // Graph for managing relationships between requests
		private readonly List<int> _usedIds = new List<int>(); // Track used IDs
		private readonly RequestManager _requestManager;

		//Commands
		public ICommand SearchCommand { get; }
		public ICommand ClearCommand { get; }
		public ICommand NewRequestCommand { get; }

		// Dependencies


		// ----------------------------------------------------------------------------------------------------------------------------
		// Properties
		public ObservableCollection<ServiceRequest> ServiceRequests { get; } = new ObservableCollection<ServiceRequest>();
		public ObservableCollection<string> SelectedStatuses { get; set; } = new ObservableCollection<string> { "All" };
		public ObservableCollection<string> SelectedPriorities { get; set; } = new ObservableCollection<string> { "All" };
		public string SearchText { get; set; }
		public ServiceRequest SelectedRequest { get; set; }

		// ----------------------------------------------------------------------------------------------------------------------------
		// Default Constructor

		/// <summary>
		/// Initializes the ServiceRequestViewModel by loading existing requests and setting up structures.
		/// </summary>
		public ServiceRequestViewModel()
		{
			_requestManager = new RequestManager();

			// Clear used IDs if necessary
			_usedIds.Clear();

			// Initialize data structures
			_serviceRequests = new AVLTree<ServiceRequest>();
			_priorityQueue = new PriorityQueue<ServiceRequest>();
			_serviceRequestGraph = new Graph<ServiceRequest>();

			// Priority queue for priorities
			//_serviceRequestGraph = new Graph(); // Graph for relationships


			// Initialize commands with appropriate methods
			SearchCommand = new RelayCommand(OnSearch);
			ClearCommand = new RelayCommand(OnClear);
			NewRequestCommand = new RelayCommand(OnNewRequest);

			// Load data from an existing source
			LoadExistingRequests();

			// Test/debug methods
			TestDataStructures();
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
				Id = GetNextRequestId(),
				Description = description,
				Status = status,
				Priority = priority,
				RequestDate = date
			};

			if (_usedIds.Contains(newRequest.Id))
			{
				throw new InvalidOperationException($"Duplicate ID detected: {newRequest.Id}");
			}

			// Add the new request to all relevant data structures
			_serviceRequests.Insert(newRequest); // AVL Tree
			_priorityQueue.Enqueue(newRequest); // PriorityQueue
			ServiceRequests.Add(newRequest); // ObservableCollection for UI
			_usedIds.Add(newRequest.Id);	
			
			_serviceRequests.SaveToJson();
		}

		/// <summary>
		/// Filters service requests based on search text, status, and priority.
		/// </summary>
		private void FilterRequests()
		{
			// Convert the AVL Tree to a list and then apply filtering using LINQ
			var filteredRequests = _serviceRequests.ToList()
				.Where(r => MatchesSearchText(r) && MatchesStatusFilter(r) && MatchesPriorityFilter(r))
				.ToList();

			ServiceRequests.Clear();
			foreach (var request in filteredRequests)
			{
				ServiceRequests.Add(request);
			}
		}

		// Check if a request matches the search text
		private bool MatchesSearchText(ServiceRequest request)
		{
			return string.IsNullOrEmpty(SearchText) ||
				   request.Description.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
		}

		// Check if a request matches the selected status filter
		private bool MatchesStatusFilter(ServiceRequest request)
		{
			return SelectedStatuses.Contains("All") || SelectedStatuses.Contains(request.Status);
		}

		// Check if a request matches the selected priority filter
		private bool MatchesPriorityFilter(ServiceRequest request)
		{
			return SelectedPriorities.Contains("All") || SelectedPriorities.Contains(request.Priority);
		}


		// ----------------------------------------------------------------------------------------------------------------------------
		// Private Methods

		/// <summary>
		/// Loads existing service requests from the JSON file.
		/// </summary>
		private void LoadExistingRequests()
		{
			try
			{
				_usedIds.Clear();
				_serviceRequests = AVLTree<ServiceRequest>.LoadFromJson(request =>
				{
					ServiceRequests.Add(request);  // Add request to the ObservableCollection
					_usedIds.Add(request.Id);     // Add to used IDs to prevent duplicates
				}) ?? new AVLTree<ServiceRequest>(); // If no data, initialize an empty AVL tree
				FilterRequests();

			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error loading requests: {ex.Message}");
			}
		}

		// Get the priority level of a service request
		private int GetPriorityLevel(string priority)
		{
			switch (priority)
			{
				case "High":
					return 1;
				case "Medium":
					return 2;
				case "Low":
					return 3;
				default:
					return 4; // Default case for unknown priority
			}
		}


		/// <summary>
		/// Generates a unique ID for a new service request.
		/// </summary>
		private int GetNextRequestId()
		{
/*			int newId = _usedIds.DefaultIfEmpty(0).Max() + 1; // Generate the next ID based on the maximum used ID

			// Ensure that the ID is unique by checking before returning it
			while (_usedIds.Contains(newId))
			{
				newId++;  // If the ID already exists, increment until a unique one is found
			}*/

			int newId = _requestManager.GetNextAvailableId(); // Get ID from RequestManager logic

			Console.WriteLine("Generated Request ID: " + newId);

			// Return the unique ID (it will never conflict with any existing ones)
			return newId;
		}

		// For testing AVL tree, priority queue, and graph functionality
		private void TestDataStructures()
		{
			Console.WriteLine("Testing AVL Tree:");
			Console.WriteLine("Root ID: " + _serviceRequests.Root?.Value.Id);
			Console.WriteLine("Is AVL Tree Balanced: " + _serviceRequests.IsBalanced());

			Console.WriteLine("Testing Priority Queue:");
			//var highestPriorityRequest = _priorityQueue.Dequeue();
		//	Console.WriteLine("Highest Priority Request: " + highestPriorityRequest.Description);

			Console.WriteLine("Testing Graph:");
			// Add some test relationships if needed
		}
		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		// Command		
		private void OnSearch()
		{
			// Filtering logic
			FilterRequests();
		}

		private void OnClear()
		{
			// Clear all filters
			SearchText = string.Empty;
			SelectedStatuses = new ObservableCollection<string> { "All" };
			SelectedPriorities = new ObservableCollection<string> { "All" };

			// Reapply filters (this resets the list)
			FilterRequests();
		}

		private void OnNewRequest()
		{
			// Open new request window logic
			var newRequestWindow = new NewRequestWindow();
			newRequestWindow.ShowDialog();
		}

	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
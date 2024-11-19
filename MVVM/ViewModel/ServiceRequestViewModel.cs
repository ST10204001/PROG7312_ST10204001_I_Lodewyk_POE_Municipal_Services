using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.DataStructures;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.Helpers;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.ViewModel
{
	public class ServiceRequestViewModel : ViewModelBase
	{
		// Private Members
		private AVLTree<ServiceRequest> _serviceRequests;
		private readonly PriorityQueue<ServiceRequest> _priorityQueue; // Priority Queue for service requests based on priority
		private readonly Graph _serviceRequestGraph; // Graph for managing relationships between requests
		private readonly List<int> _usedIds = new List<int>(); // Track used IDs
		private ServiceRequest _selectedRequest;
		private string _graphCanvas = string.Empty;

		//Commands
		public ICommand SearchCommand { get; }
		public ICommand ClearCommand { get; }
		public ICommand NewRequestCommand { get; }
		public ICommand DisplayGraphCommand { get; set; }

		// ----------------------------------------------------------------------------------------------------------------------------
		// Properties
		public ObservableCollection<ServiceRequest> ServiceRequests { get; } = new ObservableCollection<ServiceRequest>();
		// Selected status filter
		private string _selectedStatusFilter = "All";
		public string SelectedStatusFilter
		{
			get => _selectedStatusFilter;
			set
			{
				if (_selectedStatusFilter != value)
				{
					_selectedStatusFilter = value;
					OnPropertyChanged(nameof(SelectedStatusFilter));
					FilterRequests();
				}
			}
		}

		// Selected priority filter
		private string _selectedPriorityFilter = "All";
		public string SelectedPriorityFilter
		{
			get => _selectedPriorityFilter;
			set
			{
				if (_selectedPriorityFilter != value)
				{
					_selectedPriorityFilter = value;
					OnPropertyChanged(nameof(SelectedPriorityFilter));
					FilterRequests();
				}
			}
		}
		public string GraphCanvas
		{
			get => _graphCanvas;
			set
			{
				_graphCanvas = value;
				OnPropertyChanged(nameof(GraphCanvas));  // Notify the UI to update
			}
		}

		public string SearchText { get; set; }
		public ServiceRequest SelectedRequest
		{
			get => _selectedRequest;
			set
			{
				if (_selectedRequest != value)
				{
					_selectedRequest = value;
					OnPropertyChanged(nameof(SelectedRequest));

					// Log or debug to check if it updates
					Console.WriteLine($"Selected Request ID: {_selectedRequest?.Id}");

					// Optionally, open the window when an item is selected
					OpenRequestDetailsWindow();
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

			// Clear used IDs if necessary
			_usedIds.Clear();

			// Initialize data structures
			_serviceRequests = new AVLTree<ServiceRequest>();
			_priorityQueue = new PriorityQueue<ServiceRequest>();
			_serviceRequestGraph = new Graph(); // Graph for relationships

			// Immediately apply the filters after setting default values
			FilterRequests();

			// Initialize commands with appropriate methods
			SearchCommand = new RelayCommand(OnSearch);
			ClearCommand = new RelayCommand(OnClear);
			NewRequestCommand = new RelayCommand(OnNewRequest);
			DisplayGraphCommand = new RelayCommand(DisplayGraph);

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

			// Add to graph as a new node (example: each request is a node)
			_serviceRequestGraph.AddRequest(newRequest);  // Assuming AddNode is a method in Graph

			_serviceRequests.SaveToJson();
		}

		public void DisplayGraph()
		{
			Logger.Log("Starting DisplayGraph method.");

			// Generate the graph description as a string
			Logger.Log("Generating graph description.");
			string graphText = GenerateGraphText(_serviceRequestGraph);

			// Generate MST description
			Logger.Log("Generating Minimum Spanning Tree (MST).");
			MinimumSpanningTree mstGenerator = new MinimumSpanningTree();
			var mstEdges = mstGenerator.GenerateMST(_serviceRequestGraph, 0);
			string mstText = GenerateMSTText(mstEdges);

			Logger.Log("Combining graph and MST descriptions.");
			// Combine the graph and MST text
			string fullGraphText = graphText + "\nMinimum Spanning Tree:\n" + mstText;

			// Set the combined text to the UI (e.g., GraphCanvas TextBox)
			GraphCanvas = fullGraphText; // Assuming GraphCanvas is a bound UI property

			Logger.Log("DisplayGraph method completed.");
		}

		private string GenerateGraphText(Graph graph)
		{
			Logger.Log("Starting GenerateGraphText method.");
			var graphStringBuilder = new StringBuilder();

			// Convert AVLTree to List
			var serviceRequestsList = _serviceRequests.ToList();
			Logger.Log($"Converted AVLTree to List with {serviceRequestsList.Count} items.");

			// Traverse the AVL Tree and add service requests to the graph
			foreach (var serviceRequest in serviceRequestsList)
			{
				if (!graph.Requests.ContainsKey(serviceRequest.Id))
				{
					Logger.Log($"Adding ServiceRequest with ID {serviceRequest.Id} to graph.");
					graph.AddRequest(serviceRequest);
				}

				foreach (var otherRequest in serviceRequestsList)
				{
					if (serviceRequest.Id != otherRequest.Id)
					{
						if (serviceRequest.Priority == otherRequest.Priority)
						{
							Logger.Log($"Connecting ServiceRequest {serviceRequest.Id} and {otherRequest.Id} based on Priority.");
							graph.AddEdge(serviceRequest.Id, otherRequest.Id, 1.0);
						}
						else if (serviceRequest.Status == otherRequest.Status)
						{
							Logger.Log($"Connecting ServiceRequest {serviceRequest.Id} and {otherRequest.Id} based on Status.");
							graph.AddEdge(serviceRequest.Id, otherRequest.Id, 1.0);
						}
					}
				}
			}

			// Traverse the graph's adjacency list
			foreach (var node in graph.AdjacencyList)
			{
				var request = ServiceRequests.FirstOrDefault(r => r.Id == node.Key);
				if (request != null)
				{
					Logger.Log($"Processing adjacency list for ServiceRequest ID {request.Id}.");
					graphStringBuilder.AppendLine($"Request ID: {request.Id}, Description: {request.Description}");

					foreach (var neighbour in node.Value)
					{
						graphStringBuilder.AppendLine($"  -> Connected to Request ID: {neighbour.End.Id}, Description: {neighbour.End.Description}");
					}
				}
				graphStringBuilder.AppendLine();
			}

			Logger.Log("GenerateGraphText method completed.");
			return graphStringBuilder.ToString();
		}

		private string GenerateMSTText(List<Edge> mstEdges)
		{
			Logger.Log("Starting GenerateMSTText method.");
			var mstStringBuilder = new StringBuilder();

			foreach (var edge in mstEdges)
			{
				Logger.Log($"Adding MST edge from {edge.Start.Id} to {edge.End.Id} with weight {edge.Weight}.");
				mstStringBuilder.AppendLine($"Edge from Request ID {edge.Start.Id} to Request ID {edge.End.Id} with weight {edge.Weight}");
			}

			Logger.Log("GenerateMSTText method completed.");
			return mstStringBuilder.ToString();
		}


		// ----------------------------------------------------------------------------------------------------------------------------
		// Private Methods

		/// <summary>
		/// Filters service requests based on search text, status, and priority.
		/// </summary>
		private void FilterRequests()
		{
			// Clear the current list
			ServiceRequests.Clear();

			// Only apply filters if data exists
			if (_serviceRequests.CountNodes() > 0)
			{
				// Convert the AVL Tree to a list and filter using LINQ
				var filteredRequests = _serviceRequests.ToList()
					.Where(r => MatchesSearchText(r) && MatchesStatusFilter(r) && MatchesPriorityFilter(r))
					.ToList();

				// Add the filtered requests back to the ObservableCollection
				foreach (var request in filteredRequests)
				{
					ServiceRequests.Add(request);
				}
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
			return SelectedStatusFilter == "All" || request.Status == SelectedStatusFilter;
		}

		// Check if a request matches the selected priority filter
		private bool MatchesPriorityFilter(ServiceRequest request)
		{
			return SelectedPriorityFilter == "All" || request.Priority == SelectedPriorityFilter;
		}

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
			SelectedStatusFilter = "All";
			SelectedPriorityFilter = "All";
			GraphCanvas = string.Empty;

			// Reapply filters (this resets the list)
			FilterRequests();
		}

		private void OnNewRequest()
		{
			// Open new request window logic
			var newRequestWindow = new NewRequestWindow();
			newRequestWindow.ShowDialog();
		}

		private void OpenRequestDetailsWindow()
		{
			if (SelectedRequest != null)
			{
				var requestDetailsWindow = new RequestDetailWindow(_selectedRequest)
				{
					DataContext = SelectedRequest // Pass the selected request to the new window
				};
				requestDetailsWindow.ShowDialog(); // Display the details window as a modal
			}
		}


	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
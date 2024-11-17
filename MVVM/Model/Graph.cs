using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model
{
	public class Graph
	{
		// Dictionary mapping ServiceRequest ID to the ServiceRequest object
		public Dictionary<int, ServiceRequest> Nodes { get; set; } = new Dictionary<int, ServiceRequest>();

		// Adjacency list mapping ServiceRequest ID to its connected nodes
		public Dictionary<int, List<int>> AdjacencyList { get; set; } = new Dictionary<int, List<int>>();

		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Add a node to the graph
		/// </summary>
		/// <param name="serviceRequest"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public void AddNode(ServiceRequest serviceRequest)
		{
			if (serviceRequest == null)
				throw new ArgumentNullException(nameof(serviceRequest), "Service request cannot be null.");

			if (!Nodes.ContainsKey(serviceRequest.Id))
			{
				Nodes[serviceRequest.Id] = serviceRequest;
				AdjacencyList[serviceRequest.Id] = new List<int>();
			}
			else
			{
				Console.WriteLine($"Node with ID {serviceRequest.Id} already exists.");
			}
		}
		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Add an edge between two ServiceRequest nodes
		/// </summary>
		/// <param name="requestId1"></param>
		/// <param name="requestId2"></param>
		/// <exception cref="Exception"></exception>
		public void AddEdge(int requestId1, int requestId2)
		{
			if (!Nodes.ContainsKey(requestId1) || !Nodes.ContainsKey(requestId2))
				throw new Exception("One or both nodes do not exist.");

			if (!AdjacencyList[requestId1].Contains(requestId2))
				AdjacencyList[requestId1].Add(requestId2);

			if (!AdjacencyList[requestId2].Contains(requestId1)) // For undirected graphs
				AdjacencyList[requestId2].Add(requestId1);
		}
		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Traverse the graph using BFS, returning the traversed ServiceRequests
		/// </summary>
		/// <param name="startRequestId"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public List<ServiceRequest> TraverseBFS(int startRequestId)
		{
			var visited = new HashSet<int>();
			var queue = new Queue<int>();
			var result = new List<ServiceRequest>();

			// Ensure the start node exists before proceeding
			if (!Nodes.ContainsKey(startRequestId))
			{
				Console.WriteLine($"Error: Start node with ID {startRequestId} does not exist.");
				throw new Exception("Start node does not exist.");
			}

			// Initialize traversal
			queue.Enqueue(startRequestId);
			visited.Add(startRequestId);

			while (queue.Count > 0)
			{
				int current = queue.Dequeue();
				result.Add(Nodes[current]);

				// Enqueue all unvisited neighbours
				foreach (int neighbour in AdjacencyList[current])
				{
					if (!visited.Contains(neighbour))
					{
						queue.Enqueue(neighbour);
						visited.Add(neighbour);
					}
				}
			}

			return result;
		}
		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Save the graph to a JSON file
		/// </summary>
		/// <param name="filePath"></param>
		/// <exception cref="ArgumentException"></exception>
		public void SaveToJson(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
				throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

			var jsonData = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
			File.WriteAllText(filePath, jsonData);
		}
		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Load the graph from a JSON file
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="FileNotFoundException"></exception>
		/// <exception cref="Exception"></exception>
		public static Graph LoadFromJson(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
				throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

			if (!File.Exists(filePath))
				throw new FileNotFoundException($"The specified file was not found: {filePath}");

			var jsonData = File.ReadAllText(filePath);

			// Deserialize to a List<ServiceRequest>
			var serviceRequests = JsonSerializer.Deserialize<List<ServiceRequest>>(jsonData);

			if (serviceRequests == null)
				throw new Exception("Failed to deserialize service requests from JSON.");

			var graph = new Graph();

			// Add nodes to the graph from the deserialized list
			foreach (var serviceRequest in serviceRequests)
			{
				graph.AddNode(serviceRequest);
			}

			return graph;
		}
		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Check if a node exists by ID
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		public bool NodeExists(int requestId)
		{
			return Nodes.ContainsKey(requestId);
		}
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
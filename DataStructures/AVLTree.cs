using Newtonsoft.Json;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.DataStructures
{
	/// <summary>
	/// Implements an AVL tree with self-balancing properties.
	/// </summary>
	/// <typeparam name="T">Type of values stored in the tree.</typeparam>
	public class AVLTree<T> where T : IComparable<T>
	{
		// FILEPATH
		private static readonly string _filePath = GetFilePath();
	
		// Root node of the tree
		private AVLTreeNode<T> root;

		// Public method to return the count of nodes in the tree
		public int CountNodes()
		{
			return CountNodes(root);
		}

		// Recursive helper method to count nodes starting from a given node
		private int CountNodes(AVLTreeNode<T> node)
		{
			if (node == null)
				return 0;

			return 1 + CountNodes(node.Left) + CountNodes(node.Right);
		}

		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Public property to access the root node of the tree.
		/// </summary>
		public AVLTreeNode<T> Root => root;
		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Gets file path
		/// </summary>
		/// <returns></returns>
		private static string GetFilePath()
		{
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			string filePath = Path.Combine(baseDirectory, "File", "requests.json");
			EnsureDirectoryExists(filePath);
			return filePath;
		}
		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// 
		/// </summary>
		/// <param name="filePath"></param>
		private static void EnsureDirectoryExists(string filePath)
		{
			// Get the directory path from the full file path.
			string directoryPath = Path.GetDirectoryName(filePath);

			// Check if the directory exists, and create it if it doesn't.
			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath); // Create the directory if it doesn't exist.
			}
		}

		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Inserts a new value into the AVL tree, maintaining balance.
		/// </summary>
		/// <param name="value">The value to insert.</param>
		public void Insert(T value)
		{
			root = Insert(root, value);
		}
		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Helper method to recursively insert a value and balance the tree
		/// </summary>
		/// <param name="node"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		private AVLTreeNode<T> Insert(AVLTreeNode<T> node, T value)
		{
			if (node == null)
				return new AVLTreeNode<T> { Value = value, Height = 1 };

			int compareResult = value.CompareTo(node.Value);
			if (compareResult < 0)
				node.Left = Insert(node.Left, value);
			else if (compareResult > 0)
				node.Right = Insert(node.Right, value);
			else
				return node; // Duplicate values are not allowed

			node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
			int balance = GetHeightDifference(node);

			// Balance the tree with rotations as needed
			if (balance > 1 && value.CompareTo(node.Left.Value) < 0)
				return RotateRight(node);

			if (balance < -1 && value.CompareTo(node.Right.Value) > 0)
				return RotateLeft(node);

			if (balance > 1 && value.CompareTo(node.Left.Value) > 0)
			{
				node.Left = RotateLeft(node.Left);
				return RotateRight(node);
			}

			if (balance < -1 && value.CompareTo(node.Right.Value) < 0)
			{
				node.Right = RotateRight(node.Right);
				return RotateLeft(node);
			}

			return node;
		}

		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Gets the height of a node.
		/// </summary>
		/// <param name="node">The node to measure.</param>
		/// <returns>The height of the node.</returns>
		private int GetHeight(AVLTreeNode<T> node) => node?.Height ?? 0;

		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Calculates the balance factor of a node.
		/// </summary>
		/// <param name="node">The node to check.</param>
		/// <returns>The balance factor.</returns>
		private int GetHeightDifference(AVLTreeNode<T> node) => GetHeight(node.Left) - GetHeight(node.Right);

		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Performs a right rotation for balancing the AVL tree.
		/// </summary>
		/// <param name="y">The unbalanced node.</param>
		/// <returns>The new root after rotation.</returns>
		private AVLTreeNode<T> RotateRight(AVLTreeNode<T> y)
		{
			AVLTreeNode<T> x = y.Left;
			AVLTreeNode<T> T2 = x.Right;

			x.Right = y;
			y.Left = T2;

			y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
			x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

			return x;
		}

		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Performs a left rotation for balancing the AVL tree.
		/// </summary>
		/// <param name="x">The unbalanced node.</param>
		/// <returns>The new root after rotation.</returns>
		private AVLTreeNode<T> RotateLeft(AVLTreeNode<T> x)
		{
			AVLTreeNode<T> y = x.Right;
			AVLTreeNode<T> T2 = y.Left;

			y.Left = x;
			x.Right = T2;

			x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
			y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

			return y;
		}

		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Searches for a value in the AVL tree.
		/// </summary>
		/// <param name="value">The value to search for.</param>
		/// <returns>The value if found, otherwise default(T).</returns>
		public T Find(T value)
		{
			var node = Find(root, value);
			return node != null ? node.Value : default;
		}
		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Recursive helper for searching the tree
		/// </summary>
		/// <param name="node"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		private AVLTreeNode<T> Find(AVLTreeNode<T> node, T value)
		{
			if (node == null) return null;
			int compareResult = value.CompareTo(node.Value);
			if (compareResult < 0)
				return Find(node.Left, value);
			else if (compareResult > 0)
				return Find(node.Right, value);
			else
				return node;
		}

		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Checks if the AVL tree is balanced.
		/// </summary>
		/// <returns>True if balanced, otherwise false.</returns>
		public bool IsBalanced()
		{
			return CheckBalance(root) != -1;
		}
		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Recursive helper to check balance
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		private int CheckBalance(AVLTreeNode<T> node)
		{
			if (node == null) return 0;

			int leftHeight = CheckBalance(node.Left);
			if (leftHeight == -1) return -1;

			int rightHeight = CheckBalance(node.Right);
			if (rightHeight == -1) return -1;

			if (Math.Abs(leftHeight - rightHeight) > 1) return -1;

			return Math.Max(leftHeight, rightHeight) + 1;
		}

		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Performs an in-order traversal of the AVL tree.
		/// </summary>
		/// <returns>A list of values in sorted order.</returns>
		public List<T> InOrderTraversal()
		{
			var list = new List<T>();
			InOrderTraversal(root, list);
			return list;
		}
		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Recursive helper for in-order traversal
		/// </summary>
		/// <param name="node"></param>
		/// <param name="list"></param>
		private void InOrderTraversal(AVLTreeNode<T> node, List<T> list)
		{
			if (node == null) return;
			InOrderTraversal(node.Left, list);   // Traverse left subtree
			list.Add(node.Value);                 // Add current node's value
			InOrderTraversal(node.Right, list);  // Traverse right subtree
		}

		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Save Tree to JSON
		/// </summary>
		public void SaveToJson()
		{
			try
			{
				var data = InOrderTraversal();
				Console.WriteLine($"Saving data: {data.Count} items");

				var json = JsonConvert.SerializeObject(data, Formatting.Indented);
				Console.WriteLine($"Saving to: {_filePath}");

				EnsureDirectoryExists(_filePath);  // Ensure directory exists before writing.

				File.WriteAllText(_filePath, json);
				Console.WriteLine("File saved successfully.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error saving file: {ex.Message}");
			}
		}

		//-----------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Load Tree from JSON
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static AVLTree<T> LoadFromJson(Action<T> onItemLoaded = null)
		{
			try
			{
				if (!File.Exists(_filePath))
				{
					Console.WriteLine("File does not exist.");
					return null;
				}

				var json = File.ReadAllText(_filePath);
				var data = JsonConvert.DeserializeObject<List<T>>(json);

				if (data == null)
				{
					throw new Exception("Failed to deserialize the data.");
				}

				var tree = new AVLTree<T>();

				foreach (var item in data)
				{
					tree.Insert(item);
					onItemLoaded?.Invoke(item);
				}

				InOrderTraversalHelper(tree.Root, onItemLoaded);

				return tree;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error loading tree from JSON: {ex.Message}");
				return null;
			}
		}

		private static void InOrderTraversalHelper(AVLTreeNode<T> node, Action<T> onItemLoaded)
		{
			if (node == null) return;
			InOrderTraversalHelper(node.Left, onItemLoaded);
			onItemLoaded?.Invoke(node.Value);
			InOrderTraversalHelper(node.Right, onItemLoaded);
		}

		/// <summary>
		/// Searches the tree based on a predicate.
		/// </summary>
		public IEnumerable<T> Search(Func<T, bool> predicate)
		{
			var results = new List<T>();
			SearchRecursive(root, predicate, results);
			return results;
		}

		private void SearchRecursive(AVLTreeNode<T> node, Func<T, bool> predicate, List<T> results)
		{
			if (node == null) return;

			// If the predicate matches, add to results
			if (predicate(node.Value)) results.Add(node.Value);

			// Recursively search the left and right subtrees
			SearchRecursive(node.Left, predicate, results);
			SearchRecursive(node.Right, predicate, results);
		}

		public List<T> ToList()
		{
			var list = new List<T>();
			if (root != null)
			{
				InOrderTraversal(root, list);
			}
			return list;
		}
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
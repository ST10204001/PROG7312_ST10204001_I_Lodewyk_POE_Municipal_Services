using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.DataStructures
{
	public class RedBlackTree<Issue> where Issue : IComparable<Issue>
	{
		private Node _root;

		// Define colors for the tree nodes
		private enum Color
		{
			Red,
			Black
		}

		// Define the structure of a node in the Red-Black Tree
		private class Node
		{
			public Issue Value;
			public Node Left;
			public Node Right;
			public Node Parent;
			public Color NodeColor;

			public Node(Issue value)
			{
				Value = value;
				NodeColor = Color.Red; // New nodes are always red
			}
		}

		// Method to load the tree from a file
		public void LoadFromFile(string filePath)
		{
			try
			{
				if (File.Exists(filePath))
				{
					var json = File.ReadAllText(filePath);
					var issues = JsonSerializer.Deserialize<List<Issue>>(json);

					if (issues != null)
					{
						foreach (var issue in issues)
						{
							Insert(issue);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error loading from file: {ex.Message}");
			}
		}

		// Method to insert an item into the Red-Black Tree
		public void Insert(Issue value)
		{
			Node newNode = new Node(value);
			if (_root == null)
			{
				_root = newNode;
			}
			else
			{
				InsertHelper(_root, newNode);
				BalanceAfterInsert(newNode);
			}
		}

		// Helper method to insert the node into the correct position
		private void InsertHelper(Node root, Node newNode)
		{
			if (newNode.Value.CompareTo(root.Value) < 0)
			{
				if (root.Left == null)
					root.Left = newNode;
				else
					InsertHelper(root.Left, newNode);
			}
			else
			{
				if (root.Right == null)
					root.Right = newNode;
				else
					InsertHelper(root.Right, newNode);
			}

			newNode.Parent = root;
		}

		// Balancing the tree after insertion (fixes violations)
		private void BalanceAfterInsert(Node node)
		{
			while (node.Parent?.NodeColor == Color.Red)
			{
				if (node.Parent == node.Parent.Parent?.Left)
				{
					Node uncle = node.Parent.Parent?.Right;
					if (uncle?.NodeColor == Color.Red)
					{
						// Case 1: Uncle is red, recolor
						node.Parent.NodeColor = Color.Black;
						uncle.NodeColor = Color.Black;
						node.Parent.Parent.NodeColor = Color.Red;
						node = node.Parent.Parent;
					}
					else
					{
						if (node == node.Parent?.Right)
						{
							// Case 2: Node is the right child, rotate left
							node = node.Parent;
							RotateLeft(node);
						}

						// Case 3: Node is the left child, rotate right
						node.Parent.NodeColor = Color.Black;
						node.Parent.Parent.NodeColor = Color.Red;
						RotateRight(node.Parent.Parent);
					}
				}
				else
				{
					Node uncle = node.Parent.Parent?.Left;
					if (uncle?.NodeColor == Color.Red)
					{
						// Case 1: Uncle is red, recolor
						node.Parent.NodeColor = Color.Black;
						uncle.NodeColor = Color.Black;
						node.Parent.Parent.NodeColor = Color.Red;
						node = node.Parent.Parent;
					}
					else
					{
						if (node == node.Parent?.Left)
						{
							// Case 2: Node is the left child, rotate right
							node = node.Parent;
							RotateRight(node);
						}

						// Case 3: Node is the right child, rotate left
						node.Parent.NodeColor = Color.Black;
						node.Parent.Parent.NodeColor = Color.Red;
						RotateLeft(node.Parent.Parent);
					}
				}
			}

			_root.NodeColor = Color.Black;
		}

		// Perform a left rotation
		private void RotateLeft(Node node)
		{
			Node rightChild = node.Right;
			node.Right = rightChild.Left;

			if (rightChild.Left != null)
			{
				rightChild.Left.Parent = node;
			}

			rightChild.Parent = node.Parent;

			if (node.Parent == null)
			{
				_root = rightChild;
			}
			else if (node == node.Parent.Left)
			{
				node.Parent.Left = rightChild;
			}
			else
			{
				node.Parent.Right = rightChild;
			}

			rightChild.Left = node;
			node.Parent = rightChild;
		}

		// Perform a right rotation
		private void RotateRight(Node node)
		{
			Node leftChild = node.Left;
			node.Left = leftChild.Right;

			if (leftChild.Right != null)
			{
				leftChild.Right.Parent = node;
			}

			leftChild.Parent = node.Parent;

			if (node.Parent == null)
			{
				_root = leftChild;
			}
			else if (node == node.Parent.Right)
			{
				node.Parent.Right = leftChild;
			}
			else
			{
				node.Parent.Left = leftChild;
			}

			leftChild.Right = node;
			node.Parent = leftChild;
		}

		// Method to convert the tree to a list (for display or processing)
		public List<Issue> ToList()
		{
			List<Issue> list = new List<Issue>();
			InOrderTraversal(_root, list);
			return list;
		}

		// Perform an in-order traversal to populate the list
		private void InOrderTraversal(Node node, List<Issue> list)
		{
			if (node != null)
			{
				InOrderTraversal(node.Left, list);
				list.Add(node.Value);
				InOrderTraversal(node.Right, list);
			}
		}
	}
}
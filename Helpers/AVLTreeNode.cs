using System;
using System.Collections.Generic;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.Helpers
{
	public class AVLTreeNode<T> where T : IComparable<T>
	{
		public T Value { get; set; }
		public AVLTreeNode<T> Left { get; set; }
		public AVLTreeNode<T> Right { get; set; }
		public int Height { get; set; }
	}

	public class AVLTree<T> where T : IComparable<T>
	{
		private AVLTreeNode<T> root;

		// Public Root property to access the root node
		public AVLTreeNode<T> Root => root;

		// Insert method with balancing
		public void Insert(T value)
		{
			root = Insert(root, value);
		}

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

		// Get the height of a node
		private int GetHeight(AVLTreeNode<T> node)
		{
			return node?.Height ?? 0;
		}

		// Get the balance factor of a node
		private int GetHeightDifference(AVLTreeNode<T> node)
		{
			return GetHeight(node.Left) - GetHeight(node.Right);
		}

		// Rotate right operation for balancing
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

		// Rotate left operation for balancing
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

		// Find method with null-checks for safe use
		public T Find(T value)
		{
			var node = Find(root, value);
			return node != null ? node.Value : default;
		}

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

		// Check if the tree is balanced
		public bool IsBalanced()
		{
			return CheckBalance(root) != -1;
		}

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

		// In-order traversal to collect tree elements in sorted order
		public List<T> InOrderTraversal()
		{
			var list = new List<T>();
			InOrderTraversal(root, list);
			return list;
		}

		// Recursive helper for in-order traversal
		private void InOrderTraversal(AVLTreeNode<T> node, List<T> list)
		{
			if (node == null) return;
			InOrderTraversal(node.Left, list);   // Traverse left subtree
			list.Add(node.Value);                 // Add current node's value
			InOrderTraversal(node.Right, list);  // Traverse right subtree
		}
	}
}

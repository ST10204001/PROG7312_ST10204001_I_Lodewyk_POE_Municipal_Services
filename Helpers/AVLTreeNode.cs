using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.Classes
{
	public class AVLTreeNode<T> where T : IComparable<T>
	{
		public T Value { get; set; }
		public AVLTreeNode<T> Left { get; set; }
		public AVLTreeNode<T> Right { get; set; }
		public int Height { get; set; }

		public AVLTreeNode(T value)
		{
			Value = value;
			Height = 1;
		}
	}

	public class AVLTree<T> where T : IComparable<T>
	{
		private AVLTreeNode<T> root;

		public void Insert(T value)
		{
			root = Insert(root, value);
		}

		private AVLTreeNode<T> Insert(AVLTreeNode<T> node, T value)
		{
			// Standard AVL insertion code with balancing logic goes here
			// For now, only the structure is shown to illustrate the setup.
			return node;  // Adjusted node after insertion and balancing
		}

		public T Find(T value)
		{
			return Find(root, value).Value;
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
	}


}

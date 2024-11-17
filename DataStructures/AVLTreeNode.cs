using System;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.DataStructures
{
	/// <summary>
	/// Represents a single node in the AVL tree.
	/// </summary>
	/// <typeparam name="T">Type of value stored in the node.</typeparam>
	public class AVLTreeNode<T> where T : IComparable<T>
	{
		public T Value { get; set; }
		public AVLTreeNode<T> Left { get; set; }
		public AVLTreeNode<T> Right { get; set; }
		public int Height { get; set; }
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

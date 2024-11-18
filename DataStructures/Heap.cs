using System;
using System.Collections.Generic;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.DataStructures
{
	public enum HeapType { MinHeap, MaxHeap }

	public class Heap<T> where T : IComparable<T>
	{
		private readonly List<T> _elements = new List<T>();
		private readonly HeapType _type;

		public Heap(HeapType type)
		{
			_type = type;
		}

		public int Count => _elements.Count;

		public void Insert(T value)
		{
			_elements.Add(value);
			HeapifyUp(_elements.Count - 1);
		}

		public T Remove()
		{
			if (_elements.Count == 0) throw new InvalidOperationException("Heap is empty.");
			T root = _elements[0];
			_elements[0] = _elements[_elements.Count - 1];
			_elements.RemoveAt(_elements.Count - 1);
			HeapifyDown(0);
			return root;
		}

		public T Peek()
		{
			if (_elements.Count == 0) throw new InvalidOperationException("Heap is empty.");
			return _elements[0];
		}

		private void HeapifyUp(int index)
		{
			while (index > 0)
			{
				int parentIndex = (index - 1) / 2;
				if ((_type == HeapType.MinHeap && _elements[index].CompareTo(_elements[parentIndex]) < 0) ||
					(_type == HeapType.MaxHeap && _elements[index].CompareTo(_elements[parentIndex]) > 0))
				{
					Swap(index, parentIndex);
					index = parentIndex;
				}
				else
				{
					break;
				}
			}
		}

		private void HeapifyDown(int index)
		{
			while (index < _elements.Count)
			{
				int leftChild = 2 * index + 1;
				int rightChild = 2 * index + 2;
				int swapIndex = index;

				if (leftChild < _elements.Count &&
					((_type == HeapType.MinHeap && _elements[leftChild].CompareTo(_elements[swapIndex]) < 0) ||
					 (_type == HeapType.MaxHeap && _elements[leftChild].CompareTo(_elements[swapIndex]) > 0)))
				{
					swapIndex = leftChild;
				}

				if (rightChild < _elements.Count &&
					((_type == HeapType.MinHeap && _elements[rightChild].CompareTo(_elements[swapIndex]) < 0) ||
					 (_type == HeapType.MaxHeap && _elements[rightChild].CompareTo(_elements[swapIndex]) > 0)))
				{
					swapIndex = rightChild;
				}

				if (swapIndex == index) break;

				Swap(index, swapIndex);
				index = swapIndex;
			}
		}

		private int Compare(T a, T b)
		{
			if (_type == HeapType.MinHeap)
			{
				return a.CompareTo(b);  // Min-heap: smaller comes first
			}
			else
			{
				return b.CompareTo(a);  // Max-heap: larger comes first
			}
		}

		private void Swap(int index1, int index2)
		{
			(_elements[index1], _elements[index2]) = (_elements[index2], _elements[index1]);
		}
	}

}

using System;
using System.Collections.Generic;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.DataStructures
{
	// This is a custom Priority Queue implementation using a Heap structure.
	public class PriorityQueue<T> where T : IComparable<T>
	{
		private readonly List<T> _heap;  // A simple list-based heap (min-heap)

		// Constructor initializes the heap list
		public PriorityQueue()
		{
			_heap = new List<T>();
		}

		// Returns the number of elements in the priority queue
		public int Count => _heap.Count;

		// Add an item to the priority queue (heap)
		public void Enqueue(T item)
		{
			_heap.Add(item);  // Add the item at the end of the list
			HeapifyUp(_heap.Count - 1);  // Reorganize to maintain heap property
		}

		// Remove and return the item with the highest priority (lowest value in a min-heap)
		public T Dequeue()
		{
			if (_heap.Count == 0)
				throw new InvalidOperationException("Queue is empty.");

			// Move the last element to the root and heapify down
			T result = _heap[0];
			_heap[0] = _heap[_heap.Count - 1];
			_heap.RemoveAt(_heap.Count - 1);
			HeapifyDown(0);
			return result;
		}

		// Peek at the item with the highest priority (without removing it)
		public T Peek()
		{
			if (_heap.Count == 0)
				throw new InvalidOperationException("Queue is empty.");

			return _heap[0];  // The root contains the highest priority element
		}

		// Check if the queue is empty
		public bool IsEmpty()
		{
			return _heap.Count == 0;
		}

		// Helper method to maintain heap property when inserting an element
		private void HeapifyUp(int index)
		{
			int parentIndex = (index - 1) / 2;
			if (index > 0 && _heap[index].CompareTo(_heap[parentIndex]) < 0)
			{
				Swap(index, parentIndex);
				HeapifyUp(parentIndex);
			}
		}

		// Helper method to maintain heap property when removing the root element
		private void HeapifyDown(int index)
		{
			int leftChildIndex = 2 * index + 1;
			int rightChildIndex = 2 * index + 2;
			int smallest = index;

			if (leftChildIndex < _heap.Count && _heap[leftChildIndex].CompareTo(_heap[smallest]) < 0)
			{
				smallest = leftChildIndex;
			}

			if (rightChildIndex < _heap.Count && _heap[rightChildIndex].CompareTo(_heap[smallest]) < 0)
			{
				smallest = rightChildIndex;
			}

			if (smallest != index)
			{
				Swap(index, smallest);
				HeapifyDown(smallest);
			}
		}

		// Swap two elements in the heap
		private void Swap(int index1, int index2)
		{
			T temp = _heap[index1];
			_heap[index1] = _heap[index2];
			_heap[index2] = temp;
		}
	}


}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
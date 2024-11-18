using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.DataStructures
{
	public class DisjointSet
	{
		private int[] parent;
		private int[] rank;

		public DisjointSet(int size)
		{
			parent = new int[size];
			rank = new int[size];

			// Initially, each element is its own parent (i.e., in its own set)
			for (int i = 0; i < size; i++)
			{
				parent[i] = i;
				rank[i] = 0;
			}
		}

		// Find the root of the set containing `x`
		public int Find(int x)
		{
			if (parent[x] != x)
			{
				parent[x] = Find(parent[x]); // Path compression
			}
			return parent[x];
		}

		// Union the sets containing `x` and `y`
		public void Union(int x, int y)
		{
			int rootX = Find(x);
			int rootY = Find(y);

			if (rootX != rootY)
			{
				// Union by rank (attach the smaller tree under the larger one)
				if (rank[rootX] > rank[rootY])
				{
					parent[rootY] = rootX;
				}
				else if (rank[rootX] < rank[rootY])
				{
					parent[rootX] = rootY;
				}
				else
				{
					parent[rootY] = rootX;
					rank[rootX]++;
				}
			}
		}
	}
}

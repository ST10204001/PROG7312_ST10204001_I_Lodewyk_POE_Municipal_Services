using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.DataStructures
{
	public class Edge
	{
		public ServiceRequest Start { get; set; }
		public ServiceRequest End { get; set; }
		public double Weight { get; set; }

		public Edge(ServiceRequest start, ServiceRequest end, double weight)
		{
			Start = start;
			End = end;
			Weight = weight;
		}
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
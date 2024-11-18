using System;
using System.Collections.Generic;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model
{
	public class ServiceRequest : IComparable<ServiceRequest>
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string Status { get; set; }  // e.g., Pending, In-Progress, Completed
		public DateTime RequestDate { get; set; }
		public string Priority { get; set; }  // e.g., High, Medium, Low

		// Implement IComparable to compare by Priority, RequestDate, or Id
		public int CompareTo(ServiceRequest other)
		{
			if (other == null) return 1;

			// Define priority levels to be compared
			var priorityOrder = new Dictionary<string, int>
		{
			{ "High", 1 },
			{ "Medium", 2 },
			{ "Low", 3 }
		};

			// First, compare by Priority
			int priorityComparison = priorityOrder[this.Priority].CompareTo(priorityOrder[other.Priority]);
			if (priorityComparison != 0)
			{
				return priorityComparison;  // If priorities are different, return the result
			}

			// If priorities are the same, compare by RequestDate
			int dateComparison = this.RequestDate.CompareTo(other.RequestDate);
			if (dateComparison != 0)
			{
				return dateComparison;  // If request dates are different, return the result
			}

			// If both Priority and RequestDate are the same, compare by Id
			return this.Id.CompareTo(other.Id);
		}
	}

}

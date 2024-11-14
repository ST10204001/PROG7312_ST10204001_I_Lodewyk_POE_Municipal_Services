using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model
{
	public class ServiceRequest : IComparable<ServiceRequest>
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string Status { get; set; }  // e.g., Pending, In-Progress, Completed
		public DateTime SubmissionDate { get; set; }
		public string Priority { get; set; }  // e.g., High, Medium, Low

		// Implement IComparable to compare by ID, SubmissionDate, or any other criteria.
		public int CompareTo(ServiceRequest other)
		{
			if (other == null) return 1;

			// Compare by ID (or change to SubmissionDate or another field if preferred)
			return this.Id.CompareTo(other.Id);
		}
	}
}

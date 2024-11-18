using System;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model
{
	public class Issue : IComparable<Issue>
	{
		//Fields
		public string Location { get; set; }
		public string Category { get; set; }
		public string Description { get; set; }
		public string MediaUrl { get; set; }

		// Properties to determine media type
		public bool IsImage => MediaUrl.EndsWith(".jpg") || MediaUrl.EndsWith(".png");
		public bool IsDocumentOrVideo => MediaUrl.EndsWith(".pdf") || MediaUrl.EndsWith(".mp4");

		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Default Constructor
		/// </summary>
		public Issue()
		{
		}
		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="location"></param>
		/// <param name="category"></param>
		/// <param name="description"></param>
		/// <param name="mediaUrl"></param>
		public Issue(string location, string category, string description, string mediaUrl)
		{
			Location = location;
			Category = category;
			Description = description;
			MediaUrl = mediaUrl;
		}
		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		// Implement IComparable<Issue>
		public int CompareTo(Issue other)
		{
			if (other == null) return 1; // Nulls are considered less than any instance

			// Compare based on media type first (image vs document/video)
			if (IsImage && !other.IsImage) return -1;  // Current issue is an image, consider it "less"
			if (!IsImage && other.IsImage) return 1;   // Current issue is not an image, consider it "greater"

			// If media types are the same, compare by Location
			int locationComparison = string.Compare(Location, other.Location, StringComparison.Ordinal);
			if (locationComparison != 0) return locationComparison;

			// Compare by Category and Description
			int categoryComparison = string.Compare(Category, other.Category, StringComparison.Ordinal);
			if (categoryComparison != 0) return categoryComparison;

			return string.Compare(Description, other.Description, StringComparison.Ordinal);
		}
		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Override ToString (optional) for better debugging and display
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return $"Location: {Location}, Category: {Category}, Description: {Description}, MediaUrl: {MediaUrl}";
		}


	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
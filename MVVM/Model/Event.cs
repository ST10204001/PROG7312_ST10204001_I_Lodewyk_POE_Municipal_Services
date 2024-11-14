using System;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model
{
	public class Event
	{
		public string Category { get; }
		public DateTime Date { get; }
		public string Title { get; }
		public string Location { get; }
		public string Type { get; } // "Event" or "Announcement"

		public Event(string category, DateTime date, string title, string location, string type)
		{
			Category = category;
			Date = date;
			Title = title;
			Location = location;
			Type = type;
		}
	}
}

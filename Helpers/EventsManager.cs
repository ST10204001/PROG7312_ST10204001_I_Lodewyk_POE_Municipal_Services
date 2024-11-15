using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.Helpers
{
	public class EventsManager
	{
		private Queue<Event> _eventQueue;
		private Dictionary<string, HashSet<Event>> _eventsByCategory; // Dictionary to store unique events by category

		public EventsManager()
		{
			_eventQueue = new Queue<Event>();
			_eventsByCategory = new Dictionary<string, HashSet<Event>>();
			InitializeEvents();
		}

		private void InitializeEvents()
		{
			var events = GetAllEvents();
			foreach (var ev in events)
			{
				EnqueueEvent(ev);
			}
		}

		public void EnqueueEvent(Event ev)
		{
			_eventQueue.Enqueue(ev);

			if (!_eventsByCategory.ContainsKey(ev.Category))
			{
				_eventsByCategory[ev.Category] = new HashSet<Event>();
			}
			_eventsByCategory[ev.Category].Add(ev); // Use HashSet to ensure uniqueness
		}

		public List<Event> Search(string category, DateTime? date, string title)
		{
			// Filter events from the queue based on category, date, and title
			var filteredEvents = _eventQueue.Where(ev =>
				(string.IsNullOrEmpty(category) || ev.Category == category) &&
				(!date.HasValue || ev.Date.Date == date.Value.Date) &&
				(string.IsNullOrEmpty(title) || ev.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0)
			).ToList();

			return filteredEvents;
		}

		private List<Event> GetAllEvents()
		{
			return new List<Event>
			{
		new Event("Sanitation", DateTime.Now.AddDays(1), "Waste Collection - Morningside", "Morningside, Durban", "Event"),
		new Event("Parks and Recreation", DateTime.Now.AddDays(5), "Community Clean Up - Botanical Gardens", "Botanical Gardens, Durban", "Event"),
		new Event("Utilities", DateTime.Now.AddDays(2), "Water Supply Maintenance - Umhlanga", "Umhlanga, Durban", "Event"),
		new Event("Public Safety", DateTime.Now.AddDays(3), "Fire Drill - Local School", "Durban North High School", "Event"),
		new Event("Traffic", DateTime.Now.AddDays(4), "Road Closure Notification - N2 Highway", "N2 Highway, Durban", "Announcement"),
		new Event("Street Lighting", DateTime.Now.AddDays(7), "Light Installation - City Centre", "City Centre, Durban", "Event"),
		new Event("Maintenance", DateTime.Now.AddDays(6), "Pothole Repairs - Various Areas", "Various Locations in Durban", "Event"),
		new Event("Health Services", DateTime.Now.AddDays(8), "Free Health Check-up Campaign", "Community Hall, Durban", "Announcement"),
		new Event("Cultural Event", DateTime.Now.AddDays(10), "Heritage Day Celebrations", "Durban beachfront", "Event"),
		new Event("Public Consultation", DateTime.Now.AddDays(9), "Town Hall Meeting - Local Issues", "Community Centre, Durban", "Announcement")
			};
		}


		public Dictionary<string, HashSet<Event>> GetEventsByCategory()
		{
			return _eventsByCategory; // Returns the dictionary for access by category
		}
	}
}

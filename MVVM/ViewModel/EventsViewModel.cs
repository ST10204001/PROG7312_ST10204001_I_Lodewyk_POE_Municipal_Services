using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.Helpers;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.ViewModel
{
	public class EventsViewModel : ViewModelBase
	{
		private ObservableCollection<Event> _issues;
		private HashSet<string> _searchHistory; // To track user search patterns

		// Properties for filtering
		private string _selectedCategory;
		private DateTime? _selectedDate;
		private string _searchTitle;

		public ObservableCollection<Event> Issues
		{
			get => _issues;
			set
			{
				_issues = value;
				OnPropertyChanged(nameof(Issues));
			}
		}

		public string SelectedCategory
		{
			get => _selectedCategory;
			set
			{
				_selectedCategory = value;
				OnPropertyChanged(nameof(SelectedCategory));
			}
		}

		public DateTime? SelectedDate
		{
			get => _selectedDate;
			set
			{
				_selectedDate = value;
				OnPropertyChanged(nameof(SelectedDate));
			}
		}

		public string SearchTitle
		{
			get => _searchTitle;
			set
			{
				_searchTitle = value;
				OnPropertyChanged(nameof(SearchTitle));
			}
		}

		public ObservableCollection<string> Categories { get; set; }

		private EventsManager _eventsManager;

		public EventsViewModel()
		{
			Issues = new ObservableCollection<Event>();
			Categories = new ObservableCollection<string>
			{
				"Sanitation",
				"Parks and Recreation",
				"Utilities",
				"Public Safety",
				"Traffic",
				"Street Lighting",
				"Maintenance"
			};

			_eventsManager = new EventsManager();
			_searchHistory = new HashSet<string>(); // Initialize search history
		}

		public void Search(string category, DateTime? date, string title)
		{
			// Track search patterns
			if (!string.IsNullOrEmpty(title))
			{
				_searchHistory.Add(title);
			}

			Issues.Clear();
			var results = _eventsManager.Search(category, date, title);
			foreach (var ev in results)
			{
				Issues.Add(ev);
			}
		}

		public List<Event> GetRecommendations()
		{
			// Basic recommendation based on search history
			var recommendations = new List<Event>();

			foreach (var search in _searchHistory)
			{
				var matchedEvents = _eventsManager.Search(null, null, search);
				recommendations.AddRange(matchedEvents);
			}

			return recommendations.Distinct().ToList(); // Return unique recommendations
		}

		public void UpdateProperty(string propertyName, string value)
		{
			switch (propertyName)
			{
				case nameof(SelectedCategory):
					SelectedCategory = value; // Update SelectedCategory and trigger OnPropertyChanged
					break;
				case nameof(SearchTitle):
					SearchTitle = value; // Update SearchTitle and trigger OnPropertyChanged
					break;
				// Add more cases if you have more properties to update.
				default:
					throw new ArgumentException("Invalid property name", nameof(propertyName));
			}
		}
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
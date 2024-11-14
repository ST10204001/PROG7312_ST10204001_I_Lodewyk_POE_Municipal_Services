using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.View.Pages
{
	public partial class EventsPage : UserControl
	{
		private EventsViewModel _viewModel;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public EventsPage()
		{
			InitializeComponent();
			_viewModel = new EventsViewModel();
			DataContext = _viewModel;
			PopulateCategories();
		}

		private void PopulateCategories()
		{
			// Set the ComboBox source to the categories from the ViewModel
			CategoryComboBox.ItemsSource = _viewModel.Categories;
		}

		private void OnCategorySelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Update SelectedCategory property in the ViewModel when selection changes
			if (CategoryComboBox.SelectedItem is string newCategory)
			{
				_viewModel.UpdateProperty(nameof(_viewModel.SelectedCategory), newCategory);
			}
		}

		private void OnSearchButtonClick(object sender, RoutedEventArgs e)
		{
			string title = SearchTxtBlock.Text; // Get title from search TextBox
			string category = CategoryComboBox.SelectedItem?.ToString(); // Get selected category
			DateTime? selectedDate = DatePicker.SelectedDate; // Get selected date

			// Perform search with title, category, and date
			_viewModel.Search(category, selectedDate, title);

			// Get recommendations and display them (implement a method to show these)
			var recommendations = _viewModel.GetRecommendations();
			// Display recommendations as needed
			DisplayRecommendations(recommendations); // Implement this method to handle display
		}

		private void clearButton_Click(object sender, RoutedEventArgs e)
		{
			// Clear the ListView (ObservableCollection in ViewModel)
			_viewModel.Issues.Clear();

			// Reset input fields (ComboBox and DatePicker)
			CategoryComboBox.SelectedIndex = -1; // Clear selected category
			DatePicker.SelectedDate = null;      // Clear selected date
			SearchTxtBlock.Text = string.Empty;  // Clear search text
		}

		// Method to display recommendations
		private void DisplayRecommendations(List<Event> recommendations)
		{
		
		}
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.ViewModel;
using System;
using System.Windows;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.View.Windows
{
	/// <summary>
	/// Interaction logic for NewRequestWindow.xaml
	/// </summary>
	public partial class NewRequestWindow : Window
	{
		public NewRequestWindow()
		{
			InitializeComponent();
			DataContext = new ServiceRequestViewModel();
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void SubmitButton_Click(object sender, RoutedEventArgs e)
		{		
			try
			{
				ServiceRequestViewModel viewModel = new ServiceRequestViewModel();

				// Get values from your input fields (for example, textboxes)
				string description = DescriptionTextBox.Text; // Example TextBox for description
				string status = StatusComboBox.SelectedItem.ToString();
				string priority = PriorityComboBox.SelectedItem.ToString();
				DateTime date = DateTime.Now;

				// Call AddNewRequest with parameters
				viewModel.AddNewRequest(description, status, priority, date);

				MessageBox.Show("Request saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
				Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An error occurred while saving the request: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}

		}
	}
}

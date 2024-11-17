using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.View.Windows;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.View.Pages
{
	/// <summary>
	/// Interaction logic for RequestStatusPage.xaml
	/// </summary>
	public partial class RequestStatusPage : UserControl
	{
		public RequestStatusPage()
		{
			InitializeComponent();
			DataContext = new ServiceRequestViewModel();
		}

		// This method is triggered when the search button is clicked
		private void OnSearchButtonClick(object sender, RoutedEventArgs e)
		{
			var viewModel = (ServiceRequestViewModel)DataContext;
			viewModel.FilterRequests();  // Call the method to filter the requests based on SearchText
		}

		private void clearButton_Click(object sender, RoutedEventArgs e)
		{
			var viewModel = (ServiceRequestViewModel)DataContext;
			viewModel.SearchText = string.Empty;  // Clear the search text
			viewModel.SelectedStatusFilter = string.Empty;
			viewModel.SelectedPriorityFilter = string.Empty;
			viewModel.FilterRequests();  // Re-filter to show all results
		}

		private void OnNewRequestButtonClick(object sender, RoutedEventArgs e)
		{
			var newRequestWindow = new NewRequestWindow();
			newRequestWindow.ShowDialog();
		}
    }
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.View.UserControls;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.View.Pages
{
	/// <summary>
	/// Interaction logic for ReportIssuesPage.xaml
	/// This page allows users to report issues by entering location, category, description, and attaching media.
	/// </summary>
	public partial class ReportIssuesPage : UserControl
	{
		private readonly ReportIssuesViewModel _viewModel;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public ReportIssuesPage()
		{
			InitializeComponent();
			_viewModel = new ReportIssuesViewModel();
			DataContext = _viewModel;
			InitializeTextBoxEvents();
		}

		/// <summary>
		/// Initializes event handlers for text boxes to update the completion percentage
		/// as users fill out the required fields.
		/// </summary>
		private void InitializeTextBoxEvents()
		{
			var textBoxes = new List<MyTextBox>
			{
				locationTextBox,
				descriptionTextBox
			};

			foreach (var textBox in textBoxes)
			{
				textBox.TextChanged += (s, e) =>
				{
					// Update the completion percentage based on filled fields
					int filledFields = textBoxes.Count(t => !string.IsNullOrWhiteSpace(t.Text));
					_viewModel.UpdateCompletionPercentage(textBoxes.Count, filledFields);
				};
			}
		}

		/// <summary>
		/// Handles the click event for the media attachment button.
		/// Calls the method to handle media attachment.
		/// </summary>
		private void mediaButton_ButtonClick(object sender, RoutedEventArgs e)
		{
			// Call the method to attach media
			AttachMedia();
		}

		/// <summary>
		/// Opens a file dialog for users to select a media file.
		/// Updates the view model with the selected file path or displays a message if no file is selected.
		/// </summary>
		private void AttachMedia()
		{
			Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
			{
				Filter = "Images|*.jpg;*.jpeg;*.png|Documents|*.pdf;*.docx;*.txt|Videos|*.mp4;*.avi|All files|*.*",
				Title = "Select Media File"
			};

			if (openFileDialog.ShowDialog() == true)
			{
				// Get the selected file path
				string filePath = openFileDialog.FileName;

				// Call the ViewModel method to update the attached media
				_viewModel.AttachMedia(filePath);

				// Update the TextBlock to display the selected file path
				url.Text = $"File selected: {filePath}";
			}
			else
			{
				// Handle the case where the file dialog was canceled
				url.Text = "No file selected.";
			}
		}
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
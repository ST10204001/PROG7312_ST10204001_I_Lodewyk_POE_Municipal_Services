using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.Helpers;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.ViewModel
{
	/// <summary>
	/// ViewModel for managing the state of the Report Issues page.
	/// Contains properties for location, category, description, media URL, and the completion percentage.
	/// </summary>
	public class ReportIssuesViewModel : ViewModelBase
	{
		public ObservableCollection<Issue> Issues { get; set; }

		private string _locationText;
		public string LocationText
		{
			get => _locationText;
			set
			{
				_locationText = value;
				OnPropertyChanged(nameof(LocationText));
				UpdateFormCompletion(); // Update completion percentage when the location changes
			}
		}

		private string _categoryText;
		public string CategoryText
		{
			get => _categoryText;
			set
			{
				_categoryText = value;
				OnPropertyChanged(nameof(CategoryText));
				UpdateFormCompletion(); // Update completion percentage when the category changes
			}
		}

		private string _descriptionText;
		public string DescriptionText
		{
			get => _descriptionText;
			set
			{
				_descriptionText = value;
				OnPropertyChanged(nameof(DescriptionText));
				UpdateFormCompletion(); // Update completion percentage when the description changes
			}
		}

		private string _mediaUrl;
		public string MediaUrl
		{
			get => _mediaUrl;
			set
			{
				_mediaUrl = value;
				OnPropertyChanged(nameof(MediaUrl));
			}
		}

		public ICommand SubmitIssueCommand { get; }

		private int _completionPercentage;
		public int CompletionPercentage
		{
			get => _completionPercentage;
			set
			{
				_completionPercentage = value;
				OnPropertyChanged(nameof(CompletionPercentage)); // Notify property change
			}
		}

		/// <summary>
		/// Default Constructor
		/// </summary>
		public ReportIssuesViewModel()
		{
			Issues = new ObservableCollection<Issue>();

			// Initialize commands
			SubmitIssueCommand = new RelayCommand(SubmitIssue, CanSubmitIssue);

			// Initialize default values
			ClearInputs();
		}

		/// <summary>
		/// Submits a new issue by creating an Issue object and adding it to the collection.
		/// Clears the input fields after submission.
		/// </summary>
		private void SubmitIssue()
		{
			var newIssue = new Issue(LocationText, CategoryText, DescriptionText, MediaUrl);
			Issues.Add(newIssue);
			ClearInputs();
		}

		/// <summary>
		/// Checks if the form can be submitted based on the presence of required fields.
		/// Returns true if all required fields are filled; otherwise, false.
		/// </summary>
		private bool CanSubmitIssue()
		{
			return !string.IsNullOrWhiteSpace(LocationText) &&
				   !string.IsNullOrWhiteSpace(CategoryText) &&
				   !string.IsNullOrWhiteSpace(DescriptionText);
		}

		/// <summary>
		/// Clears input fields after submission to prepare for new entries.
		/// </summary>
		private void ClearInputs()
		{
			LocationText = string.Empty;
			CategoryText = string.Empty;
			DescriptionText = string.Empty;
			MediaUrl = string.Empty;

			// Update completion percentage
			UpdateFormCompletion();
		}

		/// <summary>
		/// Updates the completion percentage based on the number of filled fields.
		/// </summary>
		public void UpdateCompletionPercentage(int totalFields, int filledFields)
		{
			if (totalFields > 0)
			{
				CompletionPercentage = (filledFields * 100) / totalFields;
			}
		}

		/// <summary>
		/// Updates the completion status of the form based on the filled fields.
		/// Calculates the number of filled fields and updates the completion percentage.
		/// </summary>
		private void UpdateFormCompletion()
		{
			int filledFields = 0;
			int totalFields = 3; // LocationText, CategoryText, and DescriptionText

			if (!string.IsNullOrWhiteSpace(LocationText)) filledFields++;
			if (!string.IsNullOrWhiteSpace(CategoryText)) filledFields++;
			if (!string.IsNullOrWhiteSpace(DescriptionText)) filledFields++;

			UpdateCompletionPercentage(totalFields, filledFields);
		}

		/// <summary>
		/// Attaches media by storing the file path.
		/// Updates the MediaUrl property and any relevant flags based on the media type.
		/// </summary>
		public void AttachMedia(string filePath)
		{
			// Logic to handle the media attachment
			if (!string.IsNullOrWhiteSpace(filePath))
			{
				MediaUrl = filePath; // Store the file path
				OnPropertyChanged(nameof(MediaUrl));
			}
		}
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
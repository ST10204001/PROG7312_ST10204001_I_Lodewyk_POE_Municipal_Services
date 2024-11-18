using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.DataStructures;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.Helpers;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.View.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.ViewModel
{
	/// <summary>
	/// ViewModel for managing the state of the Report Issues page.
	/// Contains properties for location, category, description, media URL, and the completion percentage.
	/// </summary>
	public class ReportIssuesViewModel : ViewModelBase
	{
		private readonly RedBlackTree<Issue> _issuesTree = new RedBlackTree<Issue>();

		// Static file path for persistence
		private static readonly string FILEPATH = GetFilePath();
		public IEnumerable<Issue> Issues => _issuesTree.ToList();


		private Issue _selectedIssue;
		public Issue SelectedIssue
		{
			get => _selectedIssue;
			set
			{
				_selectedIssue = value;
				OnPropertyChanged(nameof(SelectedIssue));
				if (_selectedIssue != null)
				{
					ShowIssueDetails(); // Display details on selection
				}
			}
		}

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

		private void ShowIssueDetails()
		{
			var detailWindow = new ReportIssuesWindow(SelectedIssue);
			detailWindow.ShowDialog();
		}
		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ReportIssuesViewModel()
		{
			// Load issues from the file when the ViewModel is initialized
			_issuesTree.LoadFromFile(FILEPATH);

			// Initialize commands
			SubmitIssueCommand = new RelayCommand(SubmitIssue, CanSubmitIssue);

			// Initialize default values
			ClearInputs();
		}
		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Submits a new issue by creating an Issue object and adding it to the collection.
		/// Clears the input fields after submission.
		/// </summary>
		private void SubmitIssue()
		{
			var newIssue = new Issue(LocationText, CategoryText, DescriptionText, MediaUrl);
			_issuesTree.Insert(newIssue);
			SaveIssuesToJson(FILEPATH);
			MessageBox.Show($"Issues saved to {FILEPATH}");
			ClearInputs();
		}
		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
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
		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
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
		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
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
		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Updates the completion status of the form based on the filled fields.
		/// Calculates the number of filled fields and updates the completion percentage.
		/// </summary>
		private void UpdateFormCompletion()
		{
			var fields = new[] { LocationText, CategoryText, DescriptionText };
			int filledFields = fields.Count(field => !string.IsNullOrWhiteSpace(field));
			UpdateCompletionPercentage(fields.Length, filledFields);
		}
		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
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
		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		public void SaveIssuesToJson(string filePath)
		{
			try
			{
				var issuesList = _issuesTree.ToList();
				var json = JsonSerializer.Serialize(issuesList, new JsonSerializerOptions { WriteIndented = true });
				File.WriteAllText(filePath, json);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error saving issues: {ex.Message}");
			}
		}
		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Gets the file path for storing issues
		/// </summary>
		private static string GetFilePath()
		{
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			string filePath = Path.Combine(baseDirectory, "File", "issues.json");
			EnsureDirectoryExists(filePath);
			return filePath;
		}
		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Ensures the directory for the JSON file exists.
		/// </summary>
		private static void EnsureDirectoryExists(string filePath)
		{
			string directoryPath = Path.GetDirectoryName(filePath);
			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}
		}
		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
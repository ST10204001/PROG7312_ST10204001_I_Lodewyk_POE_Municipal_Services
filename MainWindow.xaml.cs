using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services
{
	public enum AppPages
	{
		Main_Menu, Report_Issues, Events, Request_Status
	}

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		/// <summary>
		/// init all pages just one time 
		/// </summary>
		private MainMenu mainMenuPage = new MainMenu();
		private ReportIssuesPage reportIssuesPage = new ReportIssuesPage();
		private EventsPage eventsPage = new EventsPage();
		private RequestStatusPage requestStatusPage = new RequestStatusPage();

		private DispatcherTimer _timer;

		public MainWindow()
		{
			InitializeComponent();
			InitializeStatusBar();
		}

		private void InitializeStatusBar()
		{
			// Initialize the timer
			_timer = new DispatcherTimer();
			_timer.Interval = TimeSpan.FromSeconds(1); // Update every second
			_timer.Tick += UpdateStatusBar; // Attach event handler
			_timer.Start(); // Start the timer
		}

		private void UpdateStatusBar(object sender, EventArgs e)
		{
			// Update the current time and date
			TimeTextBlock.Text = DateTime.Now.ToString("HH:mm"); // 24-hour format
			DateTextBlock.Text = DateTime.Now.ToString("MMM, dd");
			DayTextBlock.Text = DateTime.Now.ToString("dddd");
		}

		public void ExecutePage(AppPages page)
		{
			backButton.Visibility = Visibility.Visible;

			switch (page)
			{
				case AppPages.Main_Menu:
					container.Content = mainMenuPage;
					titleText.Text = "Main Menu";
					break;
				case AppPages.Report_Issues:
					container.Content = reportIssuesPage;
					titleText.Text = "Report Issues";
					break;
				case AppPages.Events:
					container.Content = eventsPage;
					titleText.Text = "Local Events";
					break;
				case AppPages.Request_Status:
					container.Content = requestStatusPage;
					titleText.Text = "Request Status";
					break;
			}
		}

		private void backButton_Click(object sender, RoutedEventArgs e)
		{
			container.Content = mainMenuPage;
			backButton.Visibility = Visibility.Collapsed;
			titleText.Text = "Main Menu";
		}

		private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				this.DragMove();
			}
		}

		private bool IsMaximize = false;

		private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount == 2)
			{
				if (IsMaximize)
				{
					this.WindowState = WindowState.Normal;
					this.Width = 1280;
					this.Height = 780;

					IsMaximize = false;
				}
				else
				{
					this.WindowState = WindowState.Maximized;

					IsMaximize = true;
				}
			}
		}

		private void MinButton_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void MaxButton_Click(object sender, RoutedEventArgs e)
		{
			if (this.WindowState == WindowState.Normal)
				this.WindowState = WindowState.Maximized;
			else
				this.WindowState = WindowState.Normal;
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

	}
}

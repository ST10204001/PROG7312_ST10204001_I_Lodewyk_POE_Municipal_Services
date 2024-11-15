using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.View.Pages
{
	/// <summary>
	/// Interaction logic for MainMenu.xaml
	/// </summary>
	public partial class MainMenu : UserControl
	{
		private MainWindow window;

		public MainMenu()
		{
			InitializeComponent();
		}

		private void LoadWindow()
		{
			if (window == null)
			{
				window = Window.GetWindow(App.Current.MainWindow) as MainWindow;
			}
		}

		private void ReportIssues_ElementClick(object sender, RoutedEventArgs e)
		{
			LoadWindow();
			window.ExecutePage(AppPages.Report_Issues);
		}

		private void LocalEvents_ElementClick(object sender, RoutedEventArgs e)
		{
			LoadWindow();
			window.ExecutePage(AppPages.Events);
		}

		private void RequestStatus_ElementClick(object sender, RoutedEventArgs e)
		{
			LoadWindow();
			window.ExecutePage(AppPages.Request_Status);
		}
    }
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
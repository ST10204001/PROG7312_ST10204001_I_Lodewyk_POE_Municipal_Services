using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model;
using System.Windows;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.View.Windows
{
	/// <summary>
	/// Interaction logic for ReportIssuesWindow.xaml
	/// </summary>
	public partial class ReportIssuesWindow : Window
	{
		public ReportIssuesWindow(Issue issue)
		{
			InitializeComponent();
			DataContext = issue;
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}

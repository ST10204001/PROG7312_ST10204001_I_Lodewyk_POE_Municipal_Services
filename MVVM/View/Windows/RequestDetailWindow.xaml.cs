using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model;
using System.Windows;


namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.View.Windows
{
	/// <summary>
	/// Interaction logic for RequestDetailWindow.xaml
	/// </summary>
	public partial class RequestDetailWindow : Window
	{
		public RequestDetailWindow(ServiceRequest request)
		{
			InitializeComponent();
			DataContext = request;
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
    }
}

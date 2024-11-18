using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.ViewModel;
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
			this.DataContext = new ServiceRequestViewModel();
		}
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
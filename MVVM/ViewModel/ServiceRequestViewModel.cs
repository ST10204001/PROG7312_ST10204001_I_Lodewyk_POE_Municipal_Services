using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.Classes;
using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.ViewModel
{
	public class ServiceRequestViewModel : ViewModelBase
	{
		private AVLTree<ServiceRequest> _serviceRequests;

		public ObservableCollection<ServiceRequest> ServiceRequests { get; set; }

		public ServiceRequestViewModel()
		{
			_serviceRequests = new AVLTree<ServiceRequest>();
			ServiceRequests = new ObservableCollection<ServiceRequest>();
		}

		public void AddRequest(ServiceRequest request)
		{
			_serviceRequests.Insert(request);
			ServiceRequests.Add(request);  // Update observable collection for UI binding
		}

		public ServiceRequest SearchRequest(int id)
		{
			ServiceRequest searchRequest = new ServiceRequest { Id = id };
			return _serviceRequests.Find(searchRequest);  // Pass ServiceRequest object
		}

		public void UpdateRequestStatus(int id, string status)
		{
			var request = SearchRequest(id);
			if (request != null)
			{
				request.Status = status;
				OnPropertyChanged(nameof(ServiceRequests));
			}
		}

	}
}

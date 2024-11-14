using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.ViewModel
{
	public class CircularProgressBarViewModel : ViewModelBase
	{
		private double _completionPercentage;

		public double CompletionPercentage
		{
			get { return _completionPercentage; }
			set
			{
				if (_completionPercentage != value)
				{
					_completionPercentage = value;
					OnPropertyChanged(nameof(CompletionPercentage));
				}
			}
		}
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
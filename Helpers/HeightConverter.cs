using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.Classes
{
	public class HeightConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// Return a specific height for multiline, else return a single line height
			return (bool)value ? 100 : 35; // You can adjust these heights as needed
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

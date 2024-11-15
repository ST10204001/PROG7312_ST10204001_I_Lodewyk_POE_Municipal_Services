using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.Helpers
{
	public class Validations
	{
		/// <summary>
		/// Default Constructor
		/// </summary>
		public Validations() 
		{ 
		}
		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// 
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public bool IsValidPath(string path)
		{
			// Simple validation: Check if the path is not null or empty
			return !string.IsNullOrEmpty(path) && System.IO.File.Exists(path);
		}
		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public bool IsValidLocation(string location)
		{
			return !string.IsNullOrEmpty(location);
		}
		//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// 
		/// </summary>
		/// <param name="comboBox"></param>
		/// <returns></returns>
		public bool IsComboBoxValid(ComboBox comboBox)
		{

			if (comboBox.SelectedValue == null || string.IsNullOrEmpty(comboBox.SelectedValue.ToString()))
			{
				return false;
			}

			return true;
		}
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
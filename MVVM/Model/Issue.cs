using PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.Model
{
	public class Issue : ViewModelBase
	{
		//Fields
		public string Location { get; set; }
		public string Category { get; set; }
		public string Description { get; set; }
		public string MediaUrl { get; set; }

		// Properties to determine media type
		public bool IsImage => MediaUrl.EndsWith(".jpg") || MediaUrl.EndsWith(".png");
		public bool IsDocumentOrVideo => MediaUrl.EndsWith(".pdf") || MediaUrl.EndsWith(".mp4");


		/// <summary>
		/// Default Constructor
		/// </summary>
		public Issue()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="location"></param>
		/// <param name="category"></param>
		/// <param name="description"></param>
		/// <param name="mediaUrl"></param>
		public Issue(string location, string category, string description, string mediaUrl)
		{
			Location = location;
			Category = category;
			Description = description;
			MediaUrl = mediaUrl;
		}

/*		//Properties
		public string Location
		{
			get
			{
				return location;
			}
			set 
			{ 
				location = value;
				OnPropertyChanged(nameof(location));
			}
		}

		public string Category
		{
			get
			{
				return category;
			}
			set
			{
				category = value;
				OnPropertyChanged(nameof(category));
			}
		}

		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				description = value;
				OnPropertyChanged(nameof(description));
			}
		}

		public string MediaAttachment
		{
			get
			{
				return mediaAttachment;
			}
			set
			{
				mediaAttachment = value;
				OnPropertyChanged(nameof(mediaAttachment));
			}
		}*/


	}
}

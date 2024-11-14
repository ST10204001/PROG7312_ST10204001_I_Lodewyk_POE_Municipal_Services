using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.View.UserControls
{

	/// <summary>
	/// Interaction logic for Element.xaml
	/// </summary>
	public partial class Element : UserControl
	{
		private IconChar _icon;

		public static readonly RoutedEvent ElementClickEvent =
			EventManager.RegisterRoutedEvent("ElementClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Element));

		public event RoutedEventHandler ElementClick
		{
			add { AddHandler(ElementClickEvent, value); }
			remove { RemoveHandler(ElementClickEvent, value); }
		}

		void Button_Click(object sender, RoutedEventArgs e) => RaiseEvent(new RoutedEventArgs(ElementClickEvent));


		public Element()
		{
			InitializeComponent();
		}

		public string Title
		{
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}
		public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Element));


		public bool IsActive
		{
			get { return (bool)GetValue(IsActiveProperty); }
			set { SetValue(IsActiveProperty, value); }
		}
		public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(Element));


		public IconChar Icon
		{
			get { return (IconChar)GetValue(IconProperty); }
			set { SetValue(IconProperty, value); }
		}
		public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(IconChar), typeof(Element));

	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.View.UserControls
{
	/// <summary>
	/// Interaction logic for MyButton.xaml
	/// </summary>
	public partial class MyButton : UserControl
	{
		public MyButton()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty CaptionProperty =
			DependencyProperty.Register(nameof(Caption), typeof(string), typeof(MyButton), new PropertyMetadata(string.Empty));

		public string Caption
		{
			get => (string)GetValue(CaptionProperty);
			set => SetValue(CaptionProperty, value);
		}

		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register(nameof(Text), typeof(string), typeof(MyButton), new PropertyMetadata(string.Empty));

		public string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

		public event RoutedEventHandler ButtonClick;

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			ButtonClick?.Invoke(this, e);
		}

		public static readonly DependencyProperty CommandProperty =
		DependencyProperty.Register("Command", typeof(ICommand), typeof(MyButton), new PropertyMetadata(null));

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
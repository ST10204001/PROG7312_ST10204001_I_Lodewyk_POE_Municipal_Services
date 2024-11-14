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
	/// Interaction logic for MyTextBox.xaml
	/// </summary>
	public partial class MyTextBox : UserControl
	{
		public MyTextBox()
		{
			InitializeComponent();
		}

		public string Hint
		{
			get { return (string)GetValue(HintProperty); }
			set { SetValue(HintProperty, value); }
		}

		public static readonly DependencyProperty HintProperty = DependencyProperty.Register("Hint", typeof(string), typeof(MyTextBox));

		public string Caption
		{
			get { return (string)GetValue(CaptionProperty); }
			set { SetValue(CaptionProperty, value); }
		}

		public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(string), typeof(MyTextBox));

		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register("Text", typeof(string), typeof(MyTextBox), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public event EventHandler TextChanged;

		private void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			TextChanged?.Invoke(this, EventArgs.Empty);
		}

		public static readonly DependencyProperty IsMultilineProperty =
			DependencyProperty.Register(nameof(IsMultiline), typeof(bool), typeof(MyTextBox), new PropertyMetadata(false));

		public bool IsMultiline
		{
			get => (bool)GetValue(IsMultilineProperty);
			set => SetValue(IsMultilineProperty, value);
		}

	}
}

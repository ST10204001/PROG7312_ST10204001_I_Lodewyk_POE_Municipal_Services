using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.MVVM.View.UserControls
{
	public partial class CircularProgressBar : UserControl
	{
		public static readonly DependencyProperty PercentageProperty =
			DependencyProperty.Register("Percentage", typeof(double), typeof(CircularProgressBar),
			new PropertyMetadata(0.0, OnPercentageChanged));

		public double Percentage
		{
			get { return (double)GetValue(PercentageProperty); }
			set { SetValue(PercentageProperty, value); }
		}

		private static void OnPercentageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var control = d as CircularProgressBar;
			if (control != null)
			{
				control.UpdateArc((double)e.NewValue);
			}
		}

		public CircularProgressBar()
		{
			InitializeComponent();
		}

		private void UpdateArc(double percentage)
		{
			// Ensure percentage is within 0-100
			percentage = Math.Max(0, Math.Min(100, percentage));

			// Calculate the angle based on percentage
			double angle = 360 * (percentage / 100);
			bool isLargeArc = angle > 180;

			// Update ArcSegment
			ProgressSegment.IsLargeArc = isLargeArc;

			// Calculate endpoint based on angle
			double radians = (angle - 90) * (Math.PI / 180); // Convert angle to radians
			double x = 50 + 50 * Math.Cos(radians);
			double y = 50 + 50 * Math.Sin(radians);

			ProgressSegment.Point = new Point(x, y);
		}
	}
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
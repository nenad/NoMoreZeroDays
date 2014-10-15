using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace NoMoreZeroDays.Helpers
{
    public class ProgressBarSmoother
    {
        public static double GetSmoothValue(DependencyObject obj)
        {
            return (double)obj.GetValue(SmoothValueProperty);
        }

        public static void SetSmoothValue(DependencyObject obj, double value)
        {
            obj.SetValue(SmoothValueProperty, value);
        }

        public static readonly DependencyProperty SmoothValueProperty =
            DependencyProperty.RegisterAttached("SmoothValue", typeof(double), typeof(ProgressBarSmoother), new PropertyMetadata(0.0, changing));

        private static void changing(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation anim = new DoubleAnimation()
            {
                From = (double)e.OldValue,
                To = (double)e.NewValue,
                Duration = new Duration(new TimeSpan(0,0,0,0,500)),
            };
            storyboard.Children.Add(anim);
            ProgressBar progressBar = (ProgressBar)d;
            Storyboard.SetTarget(anim, d);
            Storyboard.SetTargetProperty(anim, "Value");
            storyboard.Begin();
            //var anim = new DoubleAnimation((double)e.OldValue, (double)e.NewValue, new TimeSpan(0, 0, 0, 0, 250));
            //(d as ProgressBar).BeginAnimation(ProgressBar.ValueProperty, anim, HandoffBehavior.Compose);
        }
    }
}

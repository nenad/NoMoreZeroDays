using Rehabitation.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Rehabitation.Custom_Controls
{
    public sealed partial class HabitControl : UserControl
    {
        public CompositeTransform Translater;
        #region IsActive DependencyProperty
        public bool IsActive
        {
            get
            {
                return (Boolean)GetValue(IsActiveDP);
            }
            set
            {
                SetValue(IsActiveDP, value);
            }
        }

        public static DependencyProperty IsActiveDP = DependencyProperty.RegisterAttached(
            "IsActive",
            typeof(Boolean),
            typeof(HabitControl),
            new PropertyMetadata(false, OnIsActiveChanged)
        );
        static void OnIsActiveChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            HabitControl habitControl = (HabitControl)obj;
            if ((bool)args.NewValue == true)
            {
                habitControl.PanelDetails.Opacity = 0.20;
                habitControl.PanelEdit.Visibility = Visibility.Visible;
            }
            else
            {
                habitControl.PanelDetails.Opacity = 1;
                habitControl.PanelEdit.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        public HabitControl()
        {
            this.InitializeComponent();
            Translater = panelTranslator;
        }

        #region Dragging the element
        double start = 0;
        double end = 0;

        CoreDispatcher dispatcher;
        
        private void DragStart(object sender, ManipulationStartedRoutedEventArgs e)
        {
            Debug.WriteLine("Started");

            e.Handled = true;
            start = panelTranslator.TranslateY;
            dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
        }

        private async void DragDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            e.Handled = true;
            Point deltaPoint = e.Delta.Translation;
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                panelTranslator.TranslateY += deltaPoint.Y;
            });
        }

        private void DragEnd(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            e.Handled = true;
            end = e.Cumulative.Translation.Y;
            var total = (end - start);

            if (total <= this.Height / 2)
            {
                var time = new Duration(new TimeSpan(0, 0, 0, 0, 400));
                var translation = new TranslateTransform()
                {
                    X = 0,
                    Y = start
                };
            }
            else
            {
                HabitManager.HabitManager.MoveHabit(this, total);
            }
        }
        #endregion

        private void StopPropagationTap(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        #region Add/Remove

        public void Add()
        {
            HabitManager.HabitManager.AddHabit(this);
        }

        public void Remove()
        {
            HabitManager.HabitManager.RemoveHabit(this);
        }

        #endregion

        private void DeleteHabit(object sender, TappedRoutedEventArgs e)
        {
            Remove();
        }

    }

}

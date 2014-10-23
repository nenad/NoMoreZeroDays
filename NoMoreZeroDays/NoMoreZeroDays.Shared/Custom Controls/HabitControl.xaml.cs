using NoMoreZeroDays.Helpers;
using NoMoreZeroDays.Models;
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

namespace NoMoreZeroDays.Custom_Controls
{
    public sealed partial class HabitControl : UserControl
    {
        public static HabitControl ActiveControl = null;

        private Habit Habit
        {
            get
            {
                return this.DataContext as Habit;
            }
        }
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
            Habit h = habitControl.DataContext as Habit;
            
            var newvalue = (bool)args.NewValue;
            Debug.WriteLine(h.Name + " " + newvalue);
            if (newvalue)
            {
                habitControl.SlidePanelRightToLeft.AutoReverse = false;
                habitControl.SlidePanelRightToLeft.Begin();
                /*
                habitControl.StorySlideFromLeft.AutoReverse = false;
                habitControl.StorySlideFromRight.AutoReverse = false;
                habitControl.FadeOpacity.AutoReverse = false;

                habitControl.StorySlideFromLeft.Begin();
                habitControl.StorySlideFromRight.Begin();
                habitControl.FadeOpacity.Begin();
                 * */
            }
            else
            {
                var panel = habitControl.SlidePanelRightToLeft;
                panel.AutoReverse = true;
                //panel.Begin();
                //panel.Pause();
                panel.Seek(new TimeSpan(0, 0, 0, 0, 300));
                panel.Resume();

                /*
                TimeSpan getTime;

                habitControl.StorySlideFromLeft.SkipToFill();
                getTime = habitControl.StorySlideFromLeft.GetCurrentTime();
                habitControl.StorySlideFromLeft.Seek(getTime);

                habitControl.StorySlideFromRight.SkipToFill();
                getTime = habitControl.StorySlideFromRight.GetCurrentTime();
                habitControl.StorySlideFromRight.Seek(getTime);

                habitControl.FadeOpacity.SkipToFill();
                getTime = habitControl.FadeOpacity.GetCurrentTime();
                habitControl.FadeOpacity.Seek(getTime);
                //habitControl.StorySlideFromLeft.Seek(new TimeSpan(0, 0, 1));


                habitControl.StorySlideFromLeft.AutoReverse = true;
                habitControl.StorySlideFromRight.AutoReverse = true;
                habitControl.FadeOpacity.AutoReverse = true;

                habitControl.StorySlideFromLeft.Resume();
                habitControl.StorySlideFromRight.Resume();
                habitControl.FadeOpacity.Resume();
                 */
            }
        }

        #endregion

        public HabitControl()
        {
            this.InitializeComponent();
            Translater = panelTranslator;
            HabitManager.HabitManager.AddHabit(this);
        }

        #region Dragging the element
        double start = 0;
        double end = 0;
        double totalAbs;
        double height;
        double total;
        CoreDispatcher dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
        
        private void DragStart(object sender, ManipulationStartedRoutedEventArgs e)
        {
            e.Handled = true;
            start = panelTranslator.TranslateY;
            height = this.ActualHeight;
        }

        private async void DragDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            e.Handled = true;
            Point deltaPoint = e.Delta.Translation;
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
               // panelTranslator.TranslateY += deltaPoint.Y;
            });
            
            end = e.Cumulative.Translation.Y;
            totalAbs = Math.Abs(end - start);
            total = (end - start);

            if (totalAbs >= height / 2)
            {
               // HabitManager.HabitManager.MoveHabit(this, total);
            }
        }

        private void DragEnd(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            e.Handled = true;
            if (totalAbs <= this.ActualHeight)
            {
                var time = new Duration(new TimeSpan(0, 0, 0, 0, 400));
                Storyboard slideBack = new Storyboard();
                DoubleAnimation doubleAnim = new DoubleAnimation();
                doubleAnim.From = total;
                doubleAnim.To = 0;
                doubleAnim.Duration = time;
                slideBack.Children.Add(doubleAnim);
                Storyboard.SetTarget(doubleAnim, this.panelTranslator);
                Storyboard.SetTargetProperty(doubleAnim, "TranslateY");
                slideBack.Begin();
            }
            
        }
        #endregion

        private void StopPropagationTap(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        #region Add/Remove

        public void Remove()
        {
            HabitManager.HabitManager.RemoveHabit(this);
        }

        #endregion

        private void DeleteHabit(object sender, TappedRoutedEventArgs e)
        {
            Remove();
        }


        private void DoneHabit(object sender, TappedRoutedEventArgs e)
        {
            Habit.CurrentDay++;
            progressDaysLeft.Value += 1;
        }

        private void PanelMain_Holding(object sender, HoldingRoutedEventArgs e)
        {
            if (e.HoldingState != Windows.UI.Input.HoldingState.Started)
                return;


            if (ActiveControl != this && ActiveControl != null)
            {
                ActiveControl.IsActive = false;
            }

            ActiveControl = this;
            IsActive = true;
            e.Handled = true;

        }
    }

}

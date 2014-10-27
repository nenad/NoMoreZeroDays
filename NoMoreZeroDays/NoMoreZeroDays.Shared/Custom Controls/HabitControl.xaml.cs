using NoMoreZeroDays.Helpers;
using NoMoreZeroDays.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
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

    enum Direction
    {
        Up,
        Down
    }

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

        public HabitControl()
        {
            this.InitializeComponent();
            HabitManager.HabitManager.AddHabit(this);
        }

        #region Add/Remove

        public void Remove()
        {
            HabitManager.HabitManager.RemoveHabit(this);
        }

        #endregion



        private void PanelMain_Holding(object sender, HoldingRoutedEventArgs e)
        {
            if (e.HoldingState != Windows.UI.Input.HoldingState.Started)
                return;

            MenuFlyout menu = new MenuFlyout();
            Style menuStyle = new Windows.UI.Xaml.Style()
            {
                TargetType = typeof(MenuFlyoutPresenter)
            };
            //menuStyle.Setters.Add(new Setter(BackgroundProperty, new SolidColorBrush(Colors.Blue)));
            menu.MenuFlyoutPresenterStyle = menuStyle;

            #region Menu Items 
            var moveUpItem = new MenuFlyoutItem();
            moveUpItem.Text = "Move Up";
            moveUpItem.Click += moveUpItem_Click;

            var moveDownItem = new MenuFlyoutItem();
            moveDownItem.Text = "Move Down";
            moveDownItem.Click += moveDownItem_Click;

            var deleteItem = new MenuFlyoutItem();
            deleteItem.Text = "Delete";
            deleteItem.Click += deleteItem_Click;

            var editItem = new MenuFlyoutItem();
            editItem.Text = "Edit";
            editItem.Click += editItem_Click;

            var moreInfo = new MenuFlyoutItem();
            moreInfo.Text = "More info";
            moreInfo.Click += moreInfo_Click;
            menu.Items.Add(moveUpItem);
            menu.Items.Add(moveDownItem);
            menu.Items.Add(moreInfo);

            menu.Items.Add(new MenuFlyoutSeparator());

            menu.Items.Add(editItem);
            menu.Items.Add(deleteItem);
            #endregion

            menu.ShowAt(sender as FrameworkElement);
            e.Handled = true;
        }

        private void ToggleHabit(object sender, TappedRoutedEventArgs e)
        {
            Habit.IsDoneForToday = !Habit.IsDoneForToday;

            if (Habit.IsDoneForToday)
            {
                Habit.CurrentDay++;

                AnimationHelper.Play(ResizeCheckmark);
                //progressDaysLeft.Value += 1;
            }
            else
            {
                Habit.CurrentDay--;
                AnimationHelper.Reverse(ResizeCheckmark);
                //progressDaysLeft.Value -= 1;
            }

        }
        
        void moreInfo_Click(object sender, RoutedEventArgs e)
        {
            
        }

        async void editItem_Click(object sender, RoutedEventArgs e)
        {
            var habit = (sender as HabitControl).DataContext as Habit;
            await new MessageDialog("Edit " + habit).ShowAsync();
        }

        void deleteItem_Click(object sender, RoutedEventArgs e)
        {
            Remove();
        }

        void moveDownItem_Click(object sender, RoutedEventArgs e)
        {
            MovePosition(Direction.Down);
        }

        void moveUpItem_Click(object sender, RoutedEventArgs e)
        {
            MovePosition(Direction.Up);
        }

        void MovePosition(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    HabitManager.HabitManager.MoveHabit(this, -1);
                    break;
                case Direction.Down:
                    HabitManager.HabitManager.MoveHabit(this, 1);
                    break;
            }
        }
    }

}

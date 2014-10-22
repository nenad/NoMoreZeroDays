using NoMoreZeroDays.HabitManager;
using NoMoreZeroDays.Helpers;
using NoMoreZeroDays.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NoMoreZeroDays.Custom_Controls
{
    public sealed partial class AddHabit : UserControl, IStackable
    {
        public void Show()
        {
            if (SlideFromTop.GetCurrentTime() == TimeSpan.Zero)
            {
                txtHabitName.Focus(Windows.UI.Xaml.FocusState.Programmatic);
                SlideFromTop.AutoReverse = false;
                SlideFromTop.Begin();
                Visibility = Windows.UI.Xaml.Visibility.Visible;
                BackHistoryStack.History.Push(this);
                Debug.WriteLine("Added AddHabit");
            }
        }

        public void Hide()
        {
            SlideFromTop.SkipToFill();
            var lastSeconds = SlideFromTop.GetCurrentTime();

            txtHabitDesc.Text = String.Empty;
            txtHabitName.Text = String.Empty;
            defaultCheckbox.IsChecked = true;
            
            SlideFromTop.Seek(lastSeconds);
            SlideFromTop.AutoReverse = true;
            SlideFromTop.Resume();
            BackHistoryStack.History.Pop();
        }

        public AddHabit()
        {
            this.InitializeComponent();
            this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void AddHabitClick(object sender, RoutedEventArgs e)
        {
            String HabitName = txtHabitName.Text;
            String HabitDesc = txtHabitDesc.Text;
            int Days = 0;

            foreach (var checkbox in Checkboxes.Children)
            {
                RadioButton chk = checkbox as RadioButton;
                if (chk.IsChecked == true)
                {
                    int value = int.Parse(((string)(chk.Content)).Substring(0, 3).Trim());
                    Debug.WriteLine(value + " days");
                    Days = value;
                    break;
                }
            }

            if (HabitName == String.Empty)
            {
                txtHabitName.Focus(Windows.UI.Xaml.FocusState.Programmatic);
                return;
            }

            Habit newHabit = new Habit()
            {
                Name = HabitName,
                Description = HabitDesc,
                Days = Days,
                CurrentDay = 0
            };
            HabitList.Instance.Add(newHabit);
            Debug.WriteLine("added: " + newHabit);
            //HabitSerializer.Save();
            Hide();
        }
    }
}

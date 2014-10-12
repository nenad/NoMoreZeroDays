using Rehabitation.Custom_Controls;
using Rehabitation.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Rehabitation.HabitManager
{
    public class HabitManager
    {
        public static void SetHabits(List<HabitControl> NewHabitControls)
        {
            HabitControls = NewHabitControls;
        }
        static List<HabitControl> HabitControls = new List<HabitControl>();

        public static void AddHabit(HabitControl HabitControl)
        {
            HabitControls.Add(HabitControl);
        }
        public static void RemoveHabit(HabitControl habitControl)
        {
            Habit habit = habitControl.DataContext as Habit;
            Debug.WriteLine(habit.Name);
            HabitControls.Remove(habitControl);
            HabitItems.Instance.Remove(habit);
        }

        public void ListHeights()
        {
            for (int i = 0; i < HabitControls.Count; i++)
            {
                Debug.WriteLine(HabitControls[i].Height + " - " + HabitControls[i].Translater.TranslateY);
            }
        }

        internal static void MoveHabit(HabitControl habitControl, double total)
        {
            int index = HabitControls.IndexOf(habitControl);
            int movedPositions = (int)Math.Ceiling(total / habitControl.ActualHeight);
            int startFromIndex = Math.Max(0, Math.Min(index - movedPositions, HabitControls.Count));

            Debug.WriteLine(index + " " + movedPositions + " " + startFromIndex);

            HabitControl swappingElement = HabitControls[startFromIndex];
            HabitControls[index] = swappingElement;
            HabitControls[startFromIndex] = habitControl;

            Debug.WriteLine("Swapped " + swappingElement.txtHabitName.Text + " with " + habitControl.txtHabitName.Text);

            //HabitItems.Instance.Move(index, startFromIndex);
            
        }
    }
}

using NoMoreZeroDays.Custom_Controls;
using NoMoreZeroDays.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NoMoreZeroDays.HabitManager
{
    public class HabitManager
    {
        //Static help variables
        static List<HabitControl> HabitControls = new List<HabitControl>();

        public static int ResetTime = 6;
        public static int ReminderTime = 18;


        public static void SetHabits(List<HabitControl> NewHabitControls)
        {
            HabitControls = NewHabitControls;
        }

        public static void AddHabit(HabitControl HabitControl)
        {
            HabitControls.Add(HabitControl);
        }
        public static void RemoveHabit(HabitControl habitControl)
        {
            Habit habit = habitControl.DataContext as Habit;
            HabitList.Instance.RemoveHabit(habit);
        }

        public static void MoveHabit(HabitControl habitControl, int distance)
        {
            int oldPosition = HabitControls.IndexOf(habitControl);
            int newPosition = Clamp(oldPosition + distance, 0, HabitControls.Count - 1);
            
            var oldH = HabitList.Instance[oldPosition];
            HabitList.Instance[oldPosition] = HabitList.Instance[newPosition];
            HabitList.Instance[newPosition] = oldH;

            Debug.WriteLine(String.Format("Index: {0}/{1}", oldPosition, HabitControls.Count - 1));
        }

        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }



    }
}

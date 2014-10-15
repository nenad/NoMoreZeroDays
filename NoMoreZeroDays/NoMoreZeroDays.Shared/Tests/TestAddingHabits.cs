using NoMoreZeroDays.HabitManager;
using NoMoreZeroDays.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NoMoreZeroDays.Tests
{
    public class TestAddingHabits
    {
        static Random rnd = new Random();
        public TestAddingHabits()
        {
            int randomNumber = new Random().Next(5) + 3;
            for (int i = 0; i < randomNumber; i++)
                HabitList.Instance.Add(CreateRandomHabit());
        }

        public Habit CreateRandomHabit(String Name = "", String Description = "", String ImageLocation = "", int Days = 0, int CurrentDay = 0)
        {
            if (Name == String.Empty)
            {
                Name = "Habit " + rnd.Next(100);
            }
            if (Description == String.Empty)
            {
                Description = "Habit description" + rnd.Next(100);
            }
            if (ImageLocation == String.Empty)
            {
                ImageLocation = "/Resources/Icons/trophy_32.png";
            }
            if (Days == 0)
            {
                Days = rnd.Next(15) + 15;
            }
            if (CurrentDay == 0)
            {
                CurrentDay = rnd.Next(Days);
            }
            Habit habit = new Habit();
            habit.Name = Name;
            habit.Description = Description;
            habit.CurrentDay = CurrentDay;
            habit.Days = Days;
            habit.LastDateDone = new DateTime(2014, 10, 11 + new Random().Next(5));
            Debug.WriteLine(habit);
            return habit;
        }
    }
}

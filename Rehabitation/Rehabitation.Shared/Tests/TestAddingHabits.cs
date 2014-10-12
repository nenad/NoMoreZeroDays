using Rehabitation.HabitManager;
using Rehabitation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rehabitation.Tests
{
    public class TestAddingHabits
    {
        static Random rnd = new Random();
        public TestAddingHabits()
        {
            int randomNumber = new Random().Next(5) + 3;
            for (int i = 0; i < randomNumber; i++)
                HabitItems.Instance.Add(CreateRandomHabit());
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
            habit.ImageLocation = ImageLocation;
            habit.Description = Description;
            habit.CurrentDay = CurrentDay;
            habit.Days = Days;
            return habit;
        }
    }
}

using NoMoreZeroDays.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NoMoreZeroDays.HabitManager
{
    public class HabitList : ObservableCollection<Habit>
    {
        private static HabitList _instance;
        public static HabitList Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HabitList();
                }
                return _instance;
            }
        }
        public HabitList()
        {
            _instance = this;
        }

        public void Add(Habit[] habits)
        {
            foreach (var habit in habits)
            {
                this.Add(habit);
            }
        }

        public Habit[] GetHabits()
        {
            Habit[] habits = new Habit[this.Items.Count];
            this.Items.CopyTo(habits, 0);
            return habits;
        }
    }
}

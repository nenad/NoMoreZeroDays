using NoMoreZeroDays.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace NoMoreZeroDays.HabitManager
{
    public class HabitList : ObservableCollection<Habit>
    {
        static NoMoreZeroDays.App app = App.Current as NoMoreZeroDays.App;

        #region Singleton
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
        #endregion

        /// <summary>
        /// Adds a Habit to the list, and writes it to the database
        /// </summary>
        /// <param name="habit">The habit to add</param>
        public async void AddHabit(Habit habit)
        {
            habit.ListPosition = this.Count;
            this.Add(habit);

            var db = new SQLite.SQLiteAsyncConnection(app.DBPath);
            await db.InsertAsync(habit);
        }

        public void Add(Habit[] habits)
        {
            foreach (var habit in habits)
            {
                //Do some index work
                this.Add(habit);
            }
        }

        public async void RemoveHabit(Habit habit)
        {
            var db = new SQLite.SQLiteAsyncConnection(app.DBPath);
            await db.DeleteAsync(habit);
            this.Remove(habit);
        }

        public Habit[] GetHabits()
        {
            Habit[] habits = new Habit[this.Items.Count];
            this.Items.CopyTo(habits, 0);
            return habits;
        }
    }
}

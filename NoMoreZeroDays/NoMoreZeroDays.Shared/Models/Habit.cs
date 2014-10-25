using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NoMoreZeroDays.Models
{
    public class Habit
    {

        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id
        {
            get;
            set;
        }

        public int ListPosition
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public int Days
        {
            get;
            set;
        }

        public int CurrentDay
        {
            get;
            set;
        }

        public bool IsDoneForToday
        {
            get;
            set;
        }

        public int MissedDays
        {
            get;
            set;
        }

        public override string ToString()
        {
            return String.Format("{0}, {1} [{2}/{3}]", Name, Description, CurrentDay, Days);
        }

    }
}

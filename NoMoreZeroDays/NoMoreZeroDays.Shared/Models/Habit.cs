using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NoMoreZeroDays.Models
{
    public class Habit
    {
        public Habit()
        {

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

        public String ImageLocation
        {
            get;
            set;
        }

        public DateTime LastDateDone
        {
            get;
            set;
        }

        public int MissedDays
        {
            get
            {
                var now = DateTime.Now;
                var diff = now - LastDateDone;
                return diff.Days;
            }
        }

        public bool IsDoneForToday
        {
            get
            {
                var day = DateTime.Now.Day;
                var month = DateTime.Now.Month;
                return LastDateDone.Day == day && LastDateDone.Month == month;
            }
        }

    }
}

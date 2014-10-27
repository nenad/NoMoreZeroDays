using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Windows.UI.Xaml;

namespace NoMoreZeroDays.Models
{
    public class Habit : INotifyPropertyChanged
    {

        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id
        {
            get;
            set;
        }

        private int _listPosition;
        public int ListPosition
        {
            get
            {
                return _listPosition;
            }
            set
            {
                _listPosition = value;
                NotifyPropertyChanged("ListPosition");
            }
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

        private int _currentDay;
        public int CurrentDay
        {
            get
            {
                return _currentDay;
            }
            set
            {
                _currentDay = value;
                NotifyPropertyChanged("CurrentDay");
            }
        }

        private bool _isDoneForToday;
        public bool IsDoneForToday
        {
            get
            {
                return _isDoneForToday;
            }
            set
            {
                _isDoneForToday = value;
                NotifyPropertyChanged("IsDoneForToday");
            }
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


        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        
    }
}

using Rehabitation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Rehabitation.HabitManager;
using System.Diagnostics;

namespace Rehabitation.ViewModels
{
    public class HabitViewModel : INotifyPropertyChanged
    {

        private HabitItems _habits;
        public HabitItems Habits
        {
            get
            {
                return _habits;
            }
            set {
                NotifyPropertyChanged("Habits");
                _habits = value;
            }
        }

        public HabitViewModel()
        {
            Habits = new HabitItems();
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

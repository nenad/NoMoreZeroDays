using Rehabitation.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Rehabitation.HabitManager
{
    public class HabitItems : ObservableCollection<Habit>
    {
        private static HabitItems _instance;
        public static HabitItems Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HabitItems();
                }
                return _instance;
            }
        }
        public HabitItems()
        {
            _instance = this;
        }
    }
}

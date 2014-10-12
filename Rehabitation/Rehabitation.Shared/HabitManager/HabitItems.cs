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
            for (int i = 0; i < 10; i++)
            {
                Add(new Habit()
                {
                    Name = "Habit " + i,
                    ImageLocation = "/Resources/Icons/trophy_32.png",
                    Description = "Habit " + i + " description",
                    Days = new Random().Next(35) + 10,
                    CurrentDay = new Random().Next(45)
                });
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Rehabitation.Models
{
    public class Habit
    {

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

    }
}

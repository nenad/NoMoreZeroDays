using Newtonsoft.Json;
using NoMoreZeroDays.Helpers;
using NoMoreZeroDays.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;

namespace NoMoreZeroDays.HabitManager
{
    class HabitSerializer
    {
        static NoMoreZeroDays.App app = App.Current as NoMoreZeroDays.App;

        public static async Task LoadFromDB()
        {
            var db = new SQLite.SQLiteAsyncConnection(app.DBPath);
            var query = db.Table<Habit>().OrderBy(h => h.ListPosition);
            var result = await query.ToListAsync();
            HabitList.Instance.Add(result.ToArray());
        }

        public static async Task SaveToDB()
        {
            var db = new SQLite.SQLiteAsyncConnection(app.DBPath);
            var query = await db.UpdateAllAsync(HabitList.Instance.GetHabits());
        }
    }
}

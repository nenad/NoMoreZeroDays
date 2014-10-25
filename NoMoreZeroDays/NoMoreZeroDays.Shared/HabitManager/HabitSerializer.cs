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
        static string fileName = "habitData.json";
        static StorageFolder folder = Windows.Storage.ApplicationData.Current.RoamingFolder;
        /// <summary>
        /// Saves the habits as serialized JSON to Roaming Folder
        /// </summary>
        /// <returns></returns>
        public async static Task Save()
        {
            // Get the habits from the list and serialize as json
            var json = JsonConvert.SerializeObject(HabitList.Instance.GetHabits());
            // Create file
            StorageFile saveFile = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            // Write to file async
            await Windows.Storage.FileIO.WriteTextAsync(saveFile, json);
            await new MessageDialog(json).ShowAsync();
        }

        /// <summary>
        /// Load the habits in the list from serialized JSON file
        /// </summary>
        public async static Task Load()
        {

#if WINDOWS_PHONE_APP
            StatusBarManager.ShowStatusText("Loading habits...");
#endif
            if (await DoesFileExist(fileName))
            {
                var file = await folder.GetFileAsync(HabitSerializer.fileName);
                if (file != null)
                {
                    string jsonData = await Windows.Storage.FileIO.ReadTextAsync(file);
                    Habit[] habits = JsonConvert.DeserializeObject<Habit[]>(jsonData);
                    HabitList.Instance.Add(habits);
                    await new MessageDialog("Loaded!" + jsonData).ShowAsync();

#if WINDOWS_PHONE_APP
                    StatusBarManager.Hide();
#endif
                }
            }
            else
            {

#if WINDOWS_PHONE_APP
                StatusBarManager.Hide();
#endif
                await new MessageDialog("Not loaded!").ShowAsync();
                await Save();
            }
        }

        static async Task<bool> DoesFileExist(string fileName)
        {
            try
            {
                await folder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                new MessageDialog("File doesn't exist!").ShowAsync();
                return false;
            }
        }

        public static async Task LoadFromDB()
        {
            var db = new SQLite.SQLiteAsyncConnection(app.DBPath);
            var query = db.Table<Habit>();
            var result = await query.ToListAsync();
            HabitList.Instance.Add(result.ToArray());
        }
    }
}

using Newtonsoft.Json;
using NoMoreZeroDays.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NoMoreZeroDays.HabitManager
{
    class HabitSerializer
    {
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
        }

        /// <summary>
        /// Load the habits in the list from serialized JSON file
        /// </summary>
        public async static Task Load()
        {
            if (await DoesFileExist(fileName))
            {
                var file = await folder.GetFileAsync(HabitSerializer.fileName);
                if (file != null)
                {
                    string jsonData = await Windows.Storage.FileIO.ReadTextAsync(file);
                    Habit[] habits = JsonConvert.DeserializeObject<Habit[]>(jsonData);
                    HabitList.Instance.Add(habits);
                }
            }
            else
            {
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
                return false;
            }
        }
    }
}

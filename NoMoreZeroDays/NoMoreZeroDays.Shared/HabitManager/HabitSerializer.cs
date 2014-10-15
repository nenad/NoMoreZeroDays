using Newtonsoft.Json;
using NoMoreZeroDays.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.Storage;

namespace NoMoreZeroDays.HabitManager
{
    class HabitSerializer
    {
        static string fileName = "habitData.json";
        static StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
        public async static void Save()
        {
            Debug.WriteLine("SAVED");
            var json = JsonConvert.SerializeObject(HabitList.Instance.GetHabits());

            StorageFile saveFile = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(saveFile, json);
            
        }

        public async static void Load()
        {
            Debug.WriteLine("LOADED");
            try
            {
                StorageFile loadFile = await folder.GetFileAsync(fileName);
                string jsonData = await Windows.Storage.FileIO.ReadTextAsync(loadFile);
                Habit[] habits = JsonConvert.DeserializeObject<Habit[]>(jsonData);
                HabitList.Instance.Add(habits);
            }
            catch (Exception ex)
            {
                Save();
            }
        }
    }
}

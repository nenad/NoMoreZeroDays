using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace HabitUpdater
{
    public struct HabitResult
    {
        public int TotalHabits;
        public int DoneHabits;
    }
    public sealed class HabitUpdater : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();

           // await UpdateData();
            //await UpdateTile();

            deferral.Complete();
        }

       

      //  public static async Task<HabitResult> UpdateData() {
        //    var storageFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;
          //  return new HabitResult();
        //}


    }
}

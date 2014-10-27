using NoMoreZeroDays.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace HabitUpdater
{
    public struct HabitResult
    {
        public int TotalHabits;
        public int DoneHabits;
    }
    public sealed class HabitUpdater : IBackgroundTask
    {

        static ToastNotifier notificationManager;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            notificationManager = ToastNotificationManager.CreateToastNotifier();

            var result = await UpdateData();

            UpdateTile(result);

            deferral.Complete();
        }



        public static async Task<HabitResult> UpdateData()
        {
            var DBPath = Path.Combine(Windows.Storage.ApplicationData.Current.RoamingFolder.Path, "Habits.sqlite");
            var db = new SQLite.SQLiteAsyncConnection(DBPath);
            var table = db.Table<Habit>();
            var list = await table.ToListAsync();

            var done = list.Count(h => h.IsDoneForToday == true);
            var all = list.Count;

            return new HabitResult()
            {
                DoneHabits = done,
                TotalHabits = all
            };

        }

        static void SetUpNotification(HabitResult result)
        {
            var scheduled = notificationManager.GetScheduledToastNotifications();
            if (scheduled.Count == 0)
            {
                ToastNotification(result);
            }
        }

        private static void ToastNotification(HabitResult result)
        {

            var xml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            var textNodes = xml.GetElementsByTagName("text");

            for (int i = 0; i < textNodes.Count; i++)
            {
                textNodes[i].InnerText = "Test line " + i;
            }

            var now = new DateTime();

            var atTime = 20;
            if (now.Hour > 20)
            {
                return;
            }

            var requiredTime = new DateTime(now.Year, now.Month, now.Day, atTime, 0, 0);

            var scheduledNotification = new ScheduledToastNotification(xml, new DateTimeOffset(requiredTime));


            notificationManager.AddToSchedule(scheduledNotification);
        }

        private static void UpdateTile(HabitResult result)
        {
            // Create a tile update manager for the specified syndication feed.
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueue(true);
            updater.Clear();

            /*
            // Create a tile notification for each feed item.
            foreach (var item in result.Items)
            {
                XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWideText03);

                var title = item.Title;
                string titleText = title.Text == null ? String.Empty : title.Text;
                tileXml.GetElementsByTagName(textElementName)[0].InnerText = titleText;

                // Create a new tile notification. 
                updater.Update(new TileNotification(tileXml));

                // Don't create more than 5 notifications.
                if (itemCount++ > 5)
                    break;
            }
             * */
        }


    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace NoMoreZeroDays.Helpers
{
    public class StatusBarManager
    {
        static StatusBar statusBar;
        public StatusBarManager()
        {
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
        }

        public static void ShowStatusText(string text)
        {
            statusBar.ProgressIndicator.Text = text;
            statusBar.ProgressIndicator.ProgressValue = 0;
        }

        public static async void Hide()
        {
            await statusBar.HideAsync();
        }

    }
}

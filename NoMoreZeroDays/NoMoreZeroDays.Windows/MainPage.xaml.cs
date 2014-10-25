using NoMoreZeroDays.Custom_Controls;
using NoMoreZeroDays.Helpers;
using NoMoreZeroDays.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NoMoreZeroDays
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Disabled;


            KeyDown += MainPage_KeyDown;
        }

        void MainPage_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Escape)
            {
                HideAddPanel();
            }
        }

        void HideAddPanel()
        {
            if (BackHistoryStack.History.Count != 0)
            {
                var item = BackHistoryStack.History.Peek();
                item.Hide();
                return;
            }

            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.
            this.DataContext = new HabitViewModel();
            //new Tests.TestAddingHabits();

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            var oneHabit = HabitManager.HabitList.Instance[0];
            var twoHabit = HabitManager.HabitList.Instance[1];
            HabitManager.HabitList.Instance[0] = twoHabit;
            HabitManager.HabitList.Instance[1] = oneHabit;

            /*
            foreach (var item in HabitList.Items)
            {
                var container = HabitList.ContainerFromItem(item) as FrameworkElement;
                var habitControl = VisualTreeHelper.GetChild(container, 0) as HabitControl;
                habitControl.IsActive = !habitControl.IsActive;
            }
             * */
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            AddHabitPanel.Show();
        }
    }
}

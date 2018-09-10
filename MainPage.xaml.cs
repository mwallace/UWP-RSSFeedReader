using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPTestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer dispatcherTimer;
        RSSFeedReader rss_0;

        public ObservableCollection<Customer> Customers { get; }
            = new ObservableCollection<Customer>();

        public MainPage()
        {
            this.InitializeComponent();
            rss_0 = new RSSFeedReader();
            string rssFeed = this.RSSFeedURL.Text;
            GetFeedFromTextBox();
            DispatcherTimerSetup();
        }


        public void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            // 30 Second Timer
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Start();
        }

        void dispatcherTimer_Tick(object sender, object e)
        {
            GetFeedFromTextBox();
        }

        private void RSSFeedURL_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetFeedFromTextBox();
        }

        private void GetFeedFromTextBox()
        {
            // At this point in this experiment, it is becoming clear that a Collection
            // is the wrong data structure. #TODO get rid of Customers
            // For now, clear it
            Customers.Clear();
            string rssFeed = this.RSSFeedURL.Text;
            rss_0.downloadRSSFeedAsync(rssFeed, Customers);
        }
    }
}

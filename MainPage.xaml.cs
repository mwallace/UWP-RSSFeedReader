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
        RSSFeedReader rss_1;
        RSSFeedReader rss_2;

        public ObservableCollection<Customer> Customers { get; }
            = new ObservableCollection<Customer>();

        public MainPage()
        {
            this.InitializeComponent();
            rss_0 = new RSSFeedReader();
            rss_1 = new RSSFeedReader();
            rss_2 = new RSSFeedReader();
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
            // Read RSS Feeds every tick of the timer
            rss_0.downloadRSSFeedAsync("https://news.xbox.com/en-us/feed/stories/", Customers);
            rss_1.downloadRSSFeedAsync("http://feeds.reuters.com/reuters/scienceNews", Customers);
            rss_2.downloadRSSFeedAsync("https://www.npr.org/rss/podcast.php?id=510308", Customers);

        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            DispatcherTimerSetup();
        }
    }
}

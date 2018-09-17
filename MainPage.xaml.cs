using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Input;
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

namespace RSSFeedReader
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer dispatcherTimer;
        RSSFeedReader rss_0;

        public ObservableCollection<RSSLink> RSSLinks { get; }
            = new ObservableCollection<RSSLink>();

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
            // is the wrong data structure. #TODO get rid of RSSLinks
            // For now, clear it
            RSSLinks.Clear();
            string rssFeed = this.RSSFeedURL.Text;
            rss_0.downloadRSSFeedAsync(rssFeed, RSSLinks);
        }

        public void ClickItemList(object sender, ItemClickEventArgs e)
        {
            RSSLink clickedItem = null;
            try
            {
                clickedItem = (RSSLink)e.ClickedItem;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            string uriString = clickedItem.Link;
            Uri uri;
            if (Uri.IsWellFormedUriString(uriString, UriKind.Absolute))
            {
                uri = new Uri(uriString);
                this.WebView_0.Navigate(uri);
            }
            else
            {
                Debug.WriteLine("invalid uriString: " + uriString);
            }           
        }


    }
}

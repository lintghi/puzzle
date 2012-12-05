using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace Puzzle.Pages {
    public partial class SettingAndAboutPage : PhoneApplicationPage {
        const string MY_EMAIL_ADDRESS = "lintghi@qq.com";
        const string EMAIL_SUBJECT = "给lintghi的邮件";
        const string URI_TO_SHARE = "http://msdn.microsoft.com/en-us/library/ff431744(v=VS.92).aspx";
   
        private MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
        private ShareLinkTask shareLinkTask = new ShareLinkTask();

        public SettingAndAboutPage() {
            InitializeComponent();

            DataContext = App.AppSettingDao.AppSettingModel;
            Loaded += new RoutedEventHandler(OnSettingAndAboutPageLoaded);
        }

        void OnSettingAndAboutPageLoaded(object sender, RoutedEventArgs e) {
            if (!App.AppSettingDao.IsDataLoaded) {
                App.AppSettingDao.LoadData();
            }
        }

        private void OnPraiseAppBarBtnClick(object sender, EventArgs e) {
            praise();
        }
        private void OnPraiseBtnClick(object sender, RoutedEventArgs e) {
            praise();
        }
        private void praise() {
            marketplaceReviewTask.Show();
        }

        private void OnAdviceAppBarBtnClick(object sender, EventArgs e) {
            advice();
        }
        private void OnAdviceBtnClick(object sender, RoutedEventArgs e) {
            advice();
        }
        private void advice() {
            EmailComposeTask task = new EmailComposeTask();
            task.To = MY_EMAIL_ADDRESS;
            task.Subject = EMAIL_SUBJECT;

            task.Show();
        }

        private void OnShareBtnClick(object sender, RoutedEventArgs e) {
            shareLinkTask.Title = "有好软件";
            shareLinkTask.LinkUri = new Uri(URI_TO_SHARE, UriKind.Absolute);
            shareLinkTask.Message = "x";

            shareLinkTask.Show();
        }
    }
}
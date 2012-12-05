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
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Navigation;
using Puzzle.Utils;
using Puzzle.Vo;
using System.Windows.Resources;

namespace Puzzle {
    public partial class MainPage : PhoneApplicationPage {
        const string TILE_PAGE_URI = "/Pages/TilePage.xaml";
        const string SETTING_AND_ABOUT_PAGE_URI = "/Pages/SettingAndAboutPage.xaml";
        const string DIY_GAME_PAGE_URI = "/Pages/DiyGamePage.xaml";
        const string HIGH_SCORE_PAGE_URI = "/Pages/HighScorePage.xaml";

        const int IMAGE_COUNT = 5;
        const string IMAGE_URI = "Images/ForTile/";
        const string IMAGE_EXTENSION = ".jpg";

        private Random rand = new Random();
        private MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();

        public MainPage() {
            InitializeComponent();

            DataContext = App.AppSettingDao.AppSettingModel;
            Loaded += new RoutedEventHandler(OnMainPageLoaded);
        }

        void OnMainPageLoaded(object sender, RoutedEventArgs e) {
            if (!App.AppSettingDao.IsDataLoaded) {
                App.AppSettingDao.LoadData();
            }
        }

        private void OnQuickStartBtnClick(object sender, RoutedEventArgs e) {
            StreamResourceInfo resource = Application.GetResourceStream(new Uri(IMAGE_URI + rand.Next(1, IMAGE_COUNT) + IMAGE_EXTENSION, UriKind.Relative));
            if (resource != null) {
                WriteableBitmap bitmap = ImageUtils.toWriteableBitmap(resource.Stream);
                MemoryStream stream = new MemoryStream();
                bitmap.SaveJpeg(stream, 416, 416, 0, 100);
                bitmap.SetSource(stream);
                (Application.Current as App).CurrentPhoto = bitmap;
            }
            this.NavigationService.Navigate(new Uri(TILE_PAGE_URI, UriKind.Relative));
        }

        private void OnDiyBtnClick(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new Uri(DIY_GAME_PAGE_URI, UriKind.Relative));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e) {
            if (App.AppSettingDao.AppSettingModel.IsMainPageExitTip) {
                closeTipDialog.IsOpen = true;
                e.Cancel = true;
            }

            base.OnBackKeyPress(e);
        }

        private void OnCloseTipDialogOpened(object sender, EventArgs e) {
            this.IsEnabled = false;
            this.ApplicationBar.IsMenuEnabled = false;
            foreach (ApplicationBarIconButton button in this.ApplicationBar.Buttons) {
                button.IsEnabled = false;
            }
        }

        private void OnCancelBackClick(object sender, RoutedEventArgs e) {
            closeTipDialog.IsOpen = false;
            this.IsEnabled = true;
            this.ApplicationBar.IsMenuEnabled = true;
            foreach (ApplicationBarIconButton button in this.ApplicationBar.Buttons) {
                button.IsEnabled = true;
            }
        }

        private void OnConfirmBackClick(object sender, RoutedEventArgs e) {
            App.Quit();
        }

        private void OnSettingBtnClick(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new Uri(SETTING_AND_ABOUT_PAGE_URI, UriKind.Relative));
        }

        private void OnHighScoreClick(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new Uri(HIGH_SCORE_PAGE_URI, UriKind.Relative));
        }

        private void OnPraiseBtnClick(object sender, RoutedEventArgs e) {
            marketplaceReviewTask.Show();
        }

        private void OnGoToHomeAppBarBtnClick(object sender, EventArgs e) {
            homePanorama.DefaultItem = homePanorama.Items[0];
        }

        private void OnPraiseAppBarBtnClick(object sender, EventArgs e) {
            marketplaceReviewTask.Show();
        }
    }
}

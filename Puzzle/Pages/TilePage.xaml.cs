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

namespace Puzzle.Pages {
    public partial class TilePage : PhoneApplicationPage {
        private bool isGameing = false;
        const int MARGIN = 2;
        const string IS_GAMEING = "isGameing";
        const string IS_TILE_PAGE_BACK_KEY_PRESS_TIP = "isTilePageBackKeyPressTip";

        private PhoneApplicationService appService = PhoneApplicationService.Current;

        private TileGridProxy tileGridProxy;
        private StopWatchTextBlockProxy stopWatchTextBlockProxy;

        private int moveCount = 0;

        public TilePage() {
            InitializeComponent();

            DataContext = App.AppSettingDao.AppSettingModel;
            Loaded += new RoutedEventHandler(OnTilePageLoaded);
        }

        void OnTilePageLoaded(object sender, RoutedEventArgs e) {
            if (!App.AppSettingDao.IsDataLoaded) {
                App.AppSettingDao.LoadData();
            }
        }

        private void DoGameStart() {
            tileGridProxy = new TileGridProxy(playGrid, (Application.Current as App).CurrentPhoto, 
                    (Application.Current as App).TileSizeVo, MARGIN);
            tileGridProxy.SrambleTiles();

            moveCount = 0;
            moveCountTextBlock.Text = moveCount + "";

            stopWatchTextBlockProxy = new StopWatchTextBlockProxy(stopWatchTextBlock);
            stopWatchTextBlockProxy.StartStopWatch();

            originalImage.Source = (Application.Current as App).CurrentPhoto;
            isGameing = true;
        }

        protected override void OnManipulationStarted(ManipulationStartedEventArgs e) {
            if (e.OriginalSource is Image && isGameing) {
                Image img = e.OriginalSource as Image;
                this.MoveTileAndCheckGameOver(img);
                e.Complete();
                e.Handled = true;
            }
            base.OnManipulationStarted(e);
        }

        private void MoveTileAndCheckGameOver(Image img) {
            moveCount++;
            moveCountTextBlock.Text = moveCount + "";

            if (this.tileGridProxy.MoveTileAndCheckGameOver(img)) {
                DoGameOver();
            }
        }

        private void DoGameOver() {
            isGameing = false;
            stopWatchTextBlockProxy.StopStopWatch();

            AchievementModel achievement = new AchievementModel(moveCount, (int)stopWatchTextBlockProxy.TotalSeconds());
            App.AchievementDao.TrySaveAchievement(achievement);
            
            DoClear();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e) {
            if (this.tileGridProxy != null) {
                this.tileGridProxy.SaveState(appService.State);
            }

            if (this.stopWatchTextBlockProxy != null) {
                this.stopWatchTextBlockProxy.SaveState(appService.State);
            }

            appService.State[IS_GAMEING] = isGameing;
            (Application.Current as App).CurrentPhoto = null;
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            if (this.tileGridProxy != null) {
                this.tileGridProxy.LoadState(appService.State);
            }

            if (this.stopWatchTextBlockProxy != null) {
                this.stopWatchTextBlockProxy.LoadState(appService.State);
            }

            object objectIsGameing = null;
            if (appService.State.ContainsKey(IS_GAMEING)) {
                isGameing = appService.State.TryGetValue(IS_GAMEING, out objectIsGameing) && (bool)objectIsGameing;
            }

            if ((Application.Current as App).CurrentPhoto != null && !isGameing) {
                DoGameStart();
            }
            base.OnNavigatedTo(e);
        }

        public void DoClear() {
            appService.State.Clear();
            (Application.Current as App).CurrentPhoto = null;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e) {
            if (App.AppSettingDao.AppSettingModel.IsTilePageExitTip) {
                closeTipDialog.IsOpen = true;
                e.Cancel = true;
            }
            base.OnBackKeyPress(e);
        }

        private void OnConfirmBackClick(object sender, RoutedEventArgs e) {
            if (this.NavigationService.CanGoBack) {
                this.DoGameOver();
                this.NavigationService.GoBack();
            }
        }

        private void OnCancelBackClick(object sender, RoutedEventArgs e) {
            this.IsEnabled = true;
            closeTipDialog.IsOpen = false;
        }

        private void OnCloseTipDialogOpened(object sender, EventArgs e) {
            this.IsEnabled = false;
        }

    }

}
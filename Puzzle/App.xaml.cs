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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using System.Windows.Media.Imaging;
using Puzzle.Utils;
using Puzzle.Vo;
using System.IO.IsolatedStorage;

namespace Puzzle {
    public partial class App : Application {
        public PhoneApplicationFrame RootFrame { get; private set; }

        public WriteableBitmap CurrentPhoto { get; set;}

        private TileSizeVo size = new TileSizeVo(3, 3);
        public TileSizeVo TileSizeVo{
            get {
                return this.size;
            }
            set {
                this.size = value;
            }
        }

        private static IsolatedStorageSettings isSetting = null;
        public static IsolatedStorageSettings IsolatedStorageSettings {
            get {
                if (isSetting == null)
                    isSetting = IsolatedStorageSettings.ApplicationSettings;
                return isSetting;
            }
        }

        private static AppSettingDao appSettingDao = null;
        public static AppSettingDao AppSettingDao{
            get {
                if (appSettingDao == null) {
                    appSettingDao = new AppSettingDao(IsolatedStorageSettings);
                }

                return appSettingDao;
            }
        }

        private static AchievementDao achievementDao = null;
        public static AchievementDao AchievementDao { 
            get {
                if (achievementDao == null)
                    achievementDao = new AchievementDao(IsolatedStorageSettings);
                return achievementDao;
            }
        }

        public App() {
            UnhandledException += Application_UnhandledException;
            InitializeComponent();
            InitializePhoneApplication();
            if (System.Diagnostics.Debugger.IsAttached) {
                Application.Current.Host.Settings.EnableFrameRateCounter = true;
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }
        }

        private void Application_Launching(object sender, LaunchingEventArgs e) {
            
        }

        private void Application_Activated(object sender, ActivatedEventArgs e) {
        }
        private void Application_Deactivated(object sender, DeactivatedEventArgs e) {
        }

        private void Application_Closing(object sender, ClosingEventArgs e) {
            IsolatedStorageSettings.Save();
        }

        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e) {
            if (System.Diagnostics.Debugger.IsAttached) {
                System.Diagnostics.Debugger.Break();
            }
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e) {
            if (e.ExceptionObject is QuitException) {
                return;
            }

            if (System.Diagnostics.Debugger.IsAttached) {
                System.Diagnostics.Debugger.Break();
            }
        }

        //for exit
        private class QuitException : Exception { }
        public static void Quit() {
            throw new QuitException();
        }
        #region 电话应用程序初始化

        // 避免双重初始化
        private bool phoneApplicationInitialized = false;

        // 请勿向此方法中添加任何其他代码
        private void InitializePhoneApplication() {
            if (phoneApplicationInitialized)
                return;

            // 创建框架但先不将它设置为 RootVisual；这允许初始
            // 屏幕保持活动状态，直到准备呈现应用程序时。
            RootFrame = new TransitionFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // 处理导航故障
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // 确保我们未再次初始化
            phoneApplicationInitialized = true;
        }

        // 请勿向此方法中添加任何其他代码
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e) {
            // 设置根视觉效果以允许应用程序呈现
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // 删除此处理程序，因为不再需要它
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}
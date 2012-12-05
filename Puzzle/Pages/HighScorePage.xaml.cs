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
using System.IO.IsolatedStorage;
using Puzzle.Vo;

namespace Puzzle.Pages {
    public partial class HighScorePage : PhoneApplicationPage {
      

        public HighScorePage() {
            InitializeComponent();

            DataContext = App.AchievementDao.Achievement;
            this.Loaded += new RoutedEventHandler(OnHighScorePageLoaded);
        }
        
        void OnHighScorePageLoaded(object sender, RoutedEventArgs e) {
            if (!App.AchievementDao.IsDataLoaded) {
                App.AchievementDao.LoadData();
            }
        }

        private void OnGoToHomeAppBarBtnClick(object sender, EventArgs e) {
            if (NavigationService.CanGoBack) {
                NavigationService.GoBack();
            }
        }
    }
}
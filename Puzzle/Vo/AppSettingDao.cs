using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;

namespace Puzzle.Vo {
    public class AppSettingDao {
        public AppSettingModel AppSettingModel {
            set; get;
        }

        public bool IsDataLoaded {
            private set; get;
        }

        private IsolatedStorageSettings isSetting;
        public AppSettingDao(IsolatedStorageSettings isSetting) {
            this.isSetting = isSetting;

            AppSettingModel = new AppSettingModel();
        }

        public void LoadData() {
            if (IsDataLoaded) {
                if (isSetting.Contains("IsMainPageExitTip")) {
                    object objectIsMainPageExitTip = null;
                    AppSettingModel.IsMainPageExitTip = isSetting.TryGetValue("IsMainPageExitTip", out objectIsMainPageExitTip) && (bool)objectIsMainPageExitTip;
                }
                
                if (isSetting.Contains("IsTilePageExitTip")) {
                    object objectIsTilePageExitTip = null;
                    AppSettingModel.IsTilePageExitTip = isSetting.TryGetValue("IsTilePageExitTip", out objectIsTilePageExitTip) && (bool)objectIsTilePageExitTip;
                }

                IsDataLoaded = true;
            }
        }
    }
}

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
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Puzzle.Vo {
    public class AppSettingModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isTilePageExitTip = true;
        public bool IsTilePageExitTip {
            set {
                if (value != isTilePageExitTip) {
                    isTilePageExitTip = value;
                    OnPropertyChanged("IsTilePageExitTip");
                }
            }

            get {
                return isTilePageExitTip;
            }
        }

        private bool isMainPageExitTip = true;
        public bool IsMainPageExitTip {
            set {
                if (value != isMainPageExitTip) {
                    isMainPageExitTip = value;
                    OnPropertyChanged("IsMainPageExitTip");
                }
            }
            get {
                return isMainPageExitTip;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}

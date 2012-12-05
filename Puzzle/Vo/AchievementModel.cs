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

namespace Puzzle.Vo {
    public class AchievementModel : INotifyPropertyChanged{
        const string MOVE_COUNT = "MoveCount";
        const string CONSUME_SECONDS = "ConsumeSeconds";

        public AchievementModel(int moveCount, int consumeSeconds) {
            this.ConsumeSeconds = consumeSeconds;
            this.moveCount = moveCount;
        }

        public AchievementModel() {
        }

        private int moveCount = 0;
        public int MoveCount{
            get {
                return moveCount;
            }

            set{
                if (value != moveCount) {
                    moveCount = value;
                    NotifyPropertyChanged(MOVE_COUNT);
                }
            }
        }

        private int consumeSeconds = 0;
        public int ConsumeSeconds {
            get {
                return this.consumeSeconds;
            }

            set {
                if (value != consumeSeconds) {
                    this.consumeSeconds = value;
                    NotifyPropertyChanged(CONSUME_SECONDS);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

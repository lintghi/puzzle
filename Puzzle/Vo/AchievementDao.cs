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
    public class AchievementDao {
        private const string ACHIEVEMENT = "Achievement";
        private const string MOVE_COUNT = "moveCount";
        private const string CONSUME_SECOND = "consumeSecond";

        public bool IsDataLoaded { private set; get; }

        private AchievementModel achievement;
        public AchievementModel Achievement {
            get {
                return this.achievement;
            }
            set {
                this.achievement = value;
            }
        }

        private IsolatedStorageSettings isSetting;
        public AchievementDao(IsolatedStorageSettings isSetting) {
            this.isSetting = isSetting;
            achievement = new AchievementModel();
        }

        public void LoadData() {
            if (isSetting.Contains(CONSUME_SECOND)) {
                object objectConsumeSecond = null;
                achievement.ConsumeSeconds = isSetting.TryGetValue(CONSUME_SECOND, out objectConsumeSecond) ? 
                    (int)objectConsumeSecond : 0;
            }

            if (isSetting.Contains(MOVE_COUNT)) {
                object objectMoveCount = null;
                achievement.MoveCount = isSetting.TryGetValue(MOVE_COUNT, out objectMoveCount) ? (int)objectMoveCount : 0;
            }

            IsDataLoaded = true;
        }

        public void TrySaveAchievement(AchievementModel achievement) {
            if (this.Achievement.MoveCount == 0
                    || (achievement.MoveCount != 0 && achievement.MoveCount < this.Achievement.MoveCount)) {
                if (isSetting.Contains(MOVE_COUNT)) {
                    isSetting.Remove(MOVE_COUNT);
                }
                isSetting.Add(MOVE_COUNT, achievement.MoveCount);
                this.Achievement.MoveCount = achievement.MoveCount;
            }

            if (this.Achievement.ConsumeSeconds == 0
                    || (achievement.ConsumeSeconds != 0 && achievement.ConsumeSeconds < this.Achievement.ConsumeSeconds)) {
                if (isSetting.Contains(CONSUME_SECOND)) {
                    isSetting.Remove(CONSUME_SECOND);
                }
                isSetting.Add(CONSUME_SECOND, achievement.ConsumeSeconds);
                this.Achievement.ConsumeSeconds = achievement.ConsumeSeconds;
            }
        }

    }
}

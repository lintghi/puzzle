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
using System.Globalization;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Puzzle.Utils {
    public class StopWatchTextBlockProxy {
        const string STOP_WATCH_RUNNING = "stopWatchRunning";
        const string SUSPENSION_ADJUSTMENT = "suspensionAdjustment";
        const string TOMBSTONE_BEGIN_TIME = "tombstoneBeginTime";
        private TextBlock textBlock;

        private Stopwatch stopWatch = new Stopwatch();
        TimeSpan suspensionAdjustment = new TimeSpan();
       // string decimalSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;

        public StopWatchTextBlockProxy(TextBlock textBlock) {
            this.textBlock = textBlock;
        }

        void DisplayTime() {
            TimeSpan elapsedTime = stopWatch.Elapsed + suspensionAdjustment;
            
            this.textBlock.Text = String.Format("{0:D2}:{1:D2}:{2:D2}",
                        elapsedTime.Hours,
                        elapsedTime.Minutes,
                        elapsedTime.Seconds,
                        //decimalSeparator,
                        elapsedTime.Milliseconds / 10);
        }

        public void SaveState(IDictionary<string, object> state) {
            state[SUSPENSION_ADJUSTMENT] = suspensionAdjustment + stopWatch.Elapsed;
            state[TOMBSTONE_BEGIN_TIME] = DateTime.Now;
        }

        public void LoadState(IDictionary<string, object> state) {
            if (state.ContainsKey(SUSPENSION_ADJUSTMENT)) {
                suspensionAdjustment = (TimeSpan)state[SUSPENSION_ADJUSTMENT];
                DisplayTime();
            }
        }

        public void StartStopWatch() {
           
            stopWatch.Start();
            CompositionTarget.Rendering += OnCompositionTargetRendering;
        }

        public void StopStopWatch() {
            stopWatch.Stop();
            CompositionTarget.Rendering -= OnCompositionTargetRendering;
        }

        public void ResetStopWatch() {
            stopWatch.Reset();
            suspensionAdjustment = new TimeSpan();
            DisplayTime();
        }

        void OnCompositionTargetRendering(object sender, EventArgs e) {
            DisplayTime();
        }

        public double TotalSeconds() {
             TimeSpan elapsedTime = stopWatch.Elapsed + suspensionAdjustment;
             return elapsedTime.TotalSeconds;
        }
    }
}

﻿#pragma checksum "C:\Users\Lintghi\documents\visual studio 2010\Projects\Puzzle\Puzzle\StartPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BAF3752F2649575A7866FB9C0CA2AA50"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.225
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Puzzle {
    
    
    public partial class StartPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Grid playGrid;
        
        internal System.Windows.Controls.Button loadButton;
        
        internal System.Windows.Controls.Button scrambleButton;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton appbarLoadButton;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton appbarScrambleButton;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Puzzle;component/StartPage.xaml", System.UriKind.Relative));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.playGrid = ((System.Windows.Controls.Grid)(this.FindName("playGrid")));
            this.loadButton = ((System.Windows.Controls.Button)(this.FindName("loadButton")));
            this.scrambleButton = ((System.Windows.Controls.Button)(this.FindName("scrambleButton")));
            this.appbarLoadButton = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("appbarLoadButton")));
            this.appbarScrambleButton = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("appbarScrambleButton")));
        }
    }
}


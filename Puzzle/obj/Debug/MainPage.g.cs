﻿#pragma checksum "C:\Users\Lintghi\documents\visual studio 2010\Projects\Puzzle\Puzzle\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4BFC72939D3E006E7668BD52E896705E"
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
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Primitives.Popup closeTipDialog;
        
        internal Microsoft.Phone.Controls.Panorama homePanorama;
        
        internal System.Windows.Controls.Button diyBtn;
        
        internal System.Windows.Controls.Button settingBtn;
        
        internal System.Windows.Controls.Button praiseBtn;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Puzzle;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.closeTipDialog = ((System.Windows.Controls.Primitives.Popup)(this.FindName("closeTipDialog")));
            this.homePanorama = ((Microsoft.Phone.Controls.Panorama)(this.FindName("homePanorama")));
            this.diyBtn = ((System.Windows.Controls.Button)(this.FindName("diyBtn")));
            this.settingBtn = ((System.Windows.Controls.Button)(this.FindName("settingBtn")));
            this.praiseBtn = ((System.Windows.Controls.Button)(this.FindName("praiseBtn")));
        }
    }
}

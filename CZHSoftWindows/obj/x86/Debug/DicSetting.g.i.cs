﻿#pragma checksum "..\..\..\DicSetting.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "72E4FA9D861602D89AD868A61DD26C7D"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1008
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CZHSoftWindows {
    
    
    /// <summary>
    /// DicSetting
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class DicSetting : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\DicSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgSetting;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\DicSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
        #line default
        #line hidden
        
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
            System.Uri resourceLocater = new System.Uri("/CZHSoftWindows;component/dicsetting.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\DicSetting.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.dgSetting = ((System.Windows.Controls.DataGrid)(target));
            
            #line 6 "..\..\..\DicSetting.xaml"
            this.dgSetting.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.dgSetting_PreviewKeyDown);
            
            #line default
            #line hidden
            
            #line 6 "..\..\..\DicSetting.xaml"
            this.dgSetting.PreviewKeyUp += new System.Windows.Input.KeyEventHandler(this.dgSetting_PreviewKeyUp);
            
            #line default
            #line hidden
            
            #line 6 "..\..\..\DicSetting.xaml"
            this.dgSetting.Loaded += new System.Windows.RoutedEventHandler(this.dgSetting_Loaded);
            
            #line default
            #line hidden
            
            #line 6 "..\..\..\DicSetting.xaml"
            this.dgSetting.InitializingNewItem += new System.Windows.Controls.InitializingNewItemEventHandler(this.DataGrid_InitializingNewItem);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\DicSetting.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


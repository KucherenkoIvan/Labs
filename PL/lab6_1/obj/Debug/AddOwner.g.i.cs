﻿#pragma checksum "..\..\AddOwner.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F94E678620962C97F1C63407F6C599744E40C2B0E63E9A8EEBD8E36EC87404D0"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using lab6_1;


namespace lab6_1 {
    
    
    /// <summary>
    /// AddOwner
    /// </summary>
    public partial class AddOwner : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\AddOwner.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Name;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\AddOwner.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox pSeries;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\AddOwner.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox pNum;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\AddOwner.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox pInfo;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/lab6_1;component/addowner.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddOwner.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.pSeries = ((System.Windows.Controls.TextBox)(target));
            
            #line 15 "..\..\AddOwner.xaml"
            this.pSeries.KeyDown += new System.Windows.Input.KeyEventHandler(this.NumFilter);
            
            #line default
            #line hidden
            return;
            case 3:
            this.pNum = ((System.Windows.Controls.TextBox)(target));
            
            #line 20 "..\..\AddOwner.xaml"
            this.pNum.KeyDown += new System.Windows.Input.KeyEventHandler(this.NumFilter);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 21 "..\..\AddOwner.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.refresh);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 22 "..\..\AddOwner.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 23 "..\..\AddOwner.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.pInfo = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


﻿#pragma checksum "..\..\..\UI\NewAudienceWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1A8B0688C9DA121A719BE6A071D797494807E6A2B98DE6C4B1168FBE7860E315"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ScheduX;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace ScheduX.UI.Audiences {
    
    
    /// <summary>
    /// NewAudienceWindow
    /// </summary>
    public partial class NewAudienceWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\UI\NewAudienceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameTextBox;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\UI\NewAudienceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AudienceTypeTextBox;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\UI\NewAudienceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CapacityTextBox;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\UI\NewAudienceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Add;
        
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
            System.Uri resourceLocater = new System.Uri("/ScheduX;component/ui/newaudiencewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UI\NewAudienceWindow.xaml"
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
            
            #line 8 "..\..\..\UI\NewAudienceWindow.xaml"
            ((ScheduX.UI.Audiences.NewAudienceWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.OnClosed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.NameTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 28 "..\..\..\UI\NewAudienceWindow.xaml"
            this.NameTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBoxTextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.AudienceTypeTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 33 "..\..\..\UI\NewAudienceWindow.xaml"
            this.AudienceTypeTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBoxTextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.CapacityTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 38 "..\..\..\UI\NewAudienceWindow.xaml"
            this.CapacityTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBoxTextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Add = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


﻿#pragma checksum "..\..\..\UI\SubjectsWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "63705E9C67F6D0461827EFD99931C9F24F2B4CD4596C0869C4EA7B98203C380A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ScheduX.UI.Classes;
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


namespace ScheduX.UI.Subjects {
    
    
    /// <summary>
    /// SubjectsWindow
    /// </summary>
    public partial class SubjectsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\UI\SubjectsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Header;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\UI\SubjectsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Toolbar;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\UI\SubjectsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button New;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\UI\SubjectsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ImportExcel;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\UI\SubjectsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button TrashBin;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\UI\SubjectsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView SubjectsList;
        
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
            System.Uri resourceLocater = new System.Uri("/ScheduX;component/ui/subjectswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UI\SubjectsWindow.xaml"
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
            
            #line 8 "..\..\..\UI\SubjectsWindow.xaml"
            ((ScheduX.UI.Subjects.SubjectsWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.OnClosed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Header = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.Toolbar = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.New = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\UI\SubjectsWindow.xaml"
            this.New.Click += new System.Windows.RoutedEventHandler(this.New_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ImportExcel = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\UI\SubjectsWindow.xaml"
            this.ImportExcel.Click += new System.Windows.RoutedEventHandler(this.ImportExcel_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TrashBin = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\UI\SubjectsWindow.xaml"
            this.TrashBin.Click += new System.Windows.RoutedEventHandler(this.TrashBin_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.SubjectsList = ((System.Windows.Controls.ListView)(target));
            return;
            case 8:
            
            #line 30 "..\..\..\UI\SubjectsWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ContextMenuEditButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 35 "..\..\..\UI\SubjectsWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ContextMenuCopyButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 40 "..\..\..\UI\SubjectsWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ContextMenuDeleteButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 60 "..\..\..\UI\SubjectsWindow.xaml"
            ((System.Windows.Controls.GridViewColumnHeader)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.ColumnSizeHandler);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 63 "..\..\..\UI\SubjectsWindow.xaml"
            ((System.Windows.Controls.GridViewColumnHeader)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.ColumnSizeHandler);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


﻿#pragma checksum "..\..\..\..\UI\Pages\Home.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "112964E9463D6118534D2148714AB1F31B6759626F54EB328CA2E736D2B7780E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ScheduX.UI.Pages;
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


namespace ScheduX.UI.Pages {
    
    
    /// <summary>
    /// Home
    /// </summary>
    public partial class Home : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\..\..\UI\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu ConfigurateMenu;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\UI\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse PeriodIndicator;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\UI\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu DictionaryButtonList;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\..\UI\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse GroupsIndicator;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\..\UI\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse TeachersIndicator;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\..\UI\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse AudiencesIndicator;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\..\UI\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse SubjectsIndicator;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\..\..\UI\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu LoadButtonList;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\..\..\UI\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse LessonsIndicator;
        
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
            System.Uri resourceLocater = new System.Uri("/ScheduX;component/ui/pages/home.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UI\Pages\Home.xaml"
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
            
            #line 23 "..\..\..\..\UI\Pages\Home.xaml"
            ((System.Windows.Controls.Button)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.ButtonMouseEnterHandler);
            
            #line default
            #line hidden
            
            #line 23 "..\..\..\..\UI\Pages\Home.xaml"
            ((System.Windows.Controls.Button)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.ButtonMouseLeaveHandler);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ConfigurateMenu = ((System.Windows.Controls.Menu)(target));
            
            #line 33 "..\..\..\..\UI\Pages\Home.xaml"
            this.ConfigurateMenu.MouseEnter += new System.Windows.Input.MouseEventHandler(this.MenuMouseEnterHandler);
            
            #line default
            #line hidden
            
            #line 33 "..\..\..\..\UI\Pages\Home.xaml"
            this.ConfigurateMenu.MouseLeave += new System.Windows.Input.MouseEventHandler(this.MenuMouseLeaveHandler);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 34 "..\..\..\..\UI\Pages\Home.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PeriodOfStudyItemHandler);
            
            #line default
            #line hidden
            return;
            case 4:
            this.PeriodIndicator = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 5:
            
            #line 58 "..\..\..\..\UI\Pages\Home.xaml"
            ((System.Windows.Controls.Button)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.ButtonMouseEnterHandler1);
            
            #line default
            #line hidden
            
            #line 58 "..\..\..\..\UI\Pages\Home.xaml"
            ((System.Windows.Controls.Button)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.ButtonMouseLeaveHandler1);
            
            #line default
            #line hidden
            return;
            case 6:
            this.DictionaryButtonList = ((System.Windows.Controls.Menu)(target));
            
            #line 68 "..\..\..\..\UI\Pages\Home.xaml"
            this.DictionaryButtonList.MouseEnter += new System.Windows.Input.MouseEventHandler(this.MenuMouseEnterHandler);
            
            #line default
            #line hidden
            
            #line 68 "..\..\..\..\UI\Pages\Home.xaml"
            this.DictionaryButtonList.MouseLeave += new System.Windows.Input.MouseEventHandler(this.MenuMouseLeaveHandler);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 69 "..\..\..\..\UI\Pages\Home.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ClassesItemHandler);
            
            #line default
            #line hidden
            return;
            case 8:
            this.GroupsIndicator = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 9:
            
            #line 82 "..\..\..\..\UI\Pages\Home.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.TeachersItemHandler);
            
            #line default
            #line hidden
            return;
            case 10:
            this.TeachersIndicator = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 11:
            
            #line 95 "..\..\..\..\UI\Pages\Home.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AudiencesItemHandler);
            
            #line default
            #line hidden
            return;
            case 12:
            this.AudiencesIndicator = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 13:
            
            #line 108 "..\..\..\..\UI\Pages\Home.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SubjectsItemHandler);
            
            #line default
            #line hidden
            return;
            case 14:
            this.SubjectsIndicator = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 15:
            
            #line 132 "..\..\..\..\UI\Pages\Home.xaml"
            ((System.Windows.Controls.Button)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.ButtonMouseEnterHandler2);
            
            #line default
            #line hidden
            
            #line 132 "..\..\..\..\UI\Pages\Home.xaml"
            ((System.Windows.Controls.Button)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.ButtonMouseLeaveHandler2);
            
            #line default
            #line hidden
            return;
            case 16:
            this.LoadButtonList = ((System.Windows.Controls.Menu)(target));
            
            #line 142 "..\..\..\..\UI\Pages\Home.xaml"
            this.LoadButtonList.MouseEnter += new System.Windows.Input.MouseEventHandler(this.MenuMouseEnterHandler);
            
            #line default
            #line hidden
            
            #line 142 "..\..\..\..\UI\Pages\Home.xaml"
            this.LoadButtonList.MouseLeave += new System.Windows.Input.MouseEventHandler(this.MenuMouseLeaveHandler);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 143 "..\..\..\..\UI\Pages\Home.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.LessonsItemHandler);
            
            #line default
            #line hidden
            return;
            case 18:
            this.LessonsIndicator = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 19:
            
            #line 167 "..\..\..\..\UI\Pages\Home.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.GenerateButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


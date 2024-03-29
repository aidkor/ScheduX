﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScheduX.UI.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public EditorWindow ParentWindowInstance { get; set; }
        public Settings()
        {
            InitializeComponent();
        }
        public Settings(EditorWindow instance)
        {
            InitializeComponent();
            ParentWindowInstance = instance;
        }
    }
}

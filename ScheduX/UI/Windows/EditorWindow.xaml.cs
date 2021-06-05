﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ScheduX.UI.Pages;
using ScheduX.App_Logic;

namespace ScheduX.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        public Home HomePage { get; set; }
        public Help HelpPage { get; set; }
        public Settings SettingsPage { get; set; }
        public Schedule SchedulePage { get; set; }
        private string shxFilePath { get; set; }    
        public EditorWindow()
        {
            InitializeComponent();
            HomePage = new Home(this);
            HelpPage = new Help(this);
            SettingsPage = new Settings(this);
            SetWindowSize();         
            SetStartPage();
        }
        public EditorWindow(string filePath)
        {
            InitializeComponent();
            HomePage = new Home(this);
            HelpPage = new Help(this);
            SetWindowSize();
            SetStartPage();
            // Shx Parser Work
            shxFilePath = filePath;            
        }
        private void SetStartPage()
        {
            WorkingSpace.Content = HomePage;
        }
        private void SetWindowSize()
        {
            Height = SystemParameters.PrimaryScreenHeight / 1.9;
            Width = SystemParameters.PrimaryScreenWidth / 2;
        }
        private void OpenCloseLeftSideBarButton(object sender, RoutedEventArgs e)
        {
            if (LeftSideBarContent.IsVisible)
            {
                LeftSideBarContent.Visibility = Visibility.Collapsed;
                LeftSideBarOpenCloseButtonImg.Source = new BitmapImage(new Uri($@"{Properties.Settings.Default.ImagesPath}/menu.png", UriKind.Relative));
                LeftSideBarOpenCloseButtonImg.Margin = new Thickness(17, 0, 17, 0);
                LeftSideBarOpenCloseButtonImg.Height = 24;
                LeftSideBarOpenCloseButtonImg.Width = 24;
            }
            else
            {
                LeftSideBarContent.Visibility = Visibility.Visible;
                LeftSideBarOpenCloseButtonImg.Source = new BitmapImage(new Uri($@"{Properties.Settings.Default.ImagesPath}/back.png", UriKind.Relative));
                LeftSideBarOpenCloseButtonImg.Height = 24;
                LeftSideBarOpenCloseButtonImg.Width = 24;
            }
        }      
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {         
            WorkingSpace.Content = HomePage;
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {           
            WorkingSpace.Content = SettingsPage;
        }
        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {           
            WorkingSpace.Content = HelpPage;
        }
        private void ScheduleButton_Click(object sender, RoutedEventArgs e)
        {          
            WorkingSpace.Content = SchedulePage;
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            Grid content = button.Content as Grid;
            string imgName = (content.Children[1] as Image).Source.ToString().Split('/').Last().Split('_').First();
            
            button.Foreground = Brushes.White;
            (content.Children[0] as Grid).Background = ColorPalette.GetPredefinedColor(PredefinedColors.Sky);
            (content.Children[1] as Image).Source = new BitmapImage(new Uri($@"{Properties.Settings.Default.ImagesPath}\{imgName}_white.png", UriKind.Relative));            
        }
        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            Grid content = button.Content as Grid;
            string imgName = (content.Children[1] as Image).Source.ToString().Split('/').Last().Split('_').First();

            button.Foreground = ColorPalette.GetPredefinedColor(PredefinedColors.Sky);
            (content.Children[0] as Grid).Background = Brushes.Transparent;
            (content.Children[1] as Image).Source = new BitmapImage(new Uri($@"{Properties.Settings.Default.ImagesPath}\{imgName}_blue.png", UriKind.Relative));

        }     
    
    }
}

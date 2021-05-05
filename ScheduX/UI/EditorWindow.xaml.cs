using System;
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
using ScheduX.UI.Pages;
using ScheduX.UI.PeriodOfStudy;
using ScheduX.UI.Classes;
using ScheduX.Resourses.AppLogic;
using ScheduX.Resourses.UILogic;

namespace ScheduX.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        public Home HomePage { get; set; }      
        public Schedule SchedulePage { get; set; }
        private string shxFilePath { get; set; }    
        public EditorWindow()
        {
            InitializeComponent();
            HomePage = new Home(this);
            SetWindowSize();         
            SetStartPage();
        }
        public EditorWindow(string filePath)
        {
            InitializeComponent();
            HomePage = new Home(this);
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
                LeftSideBarOpenCloseButtonImg.Source = new BitmapImage(new Uri(@"..\Resourses\Images\EditorWindowImg\menu.png", UriKind.Relative));
                LeftSideBarOpenCloseButtonImg.Margin = new Thickness(17, 0, 17, 0);
                LeftSideBarOpenCloseButtonImg.Height = 24;
                LeftSideBarOpenCloseButtonImg.Width = 24;
            }
            else
            {
                LeftSideBarContent.Visibility = Visibility.Visible;
                LeftSideBarOpenCloseButtonImg.Source = new BitmapImage(new Uri(@"..\Resourses\Images\EditorWindowImg\back.png", UriKind.Relative));
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
        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            WorkingSpace.Content = null;
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {           
            WorkingSpace.Content = null;
        }
        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {           
            WorkingSpace.Content = null;
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
            (content.Children[0] as Grid).Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#7DABE0");
            (content.Children[1] as Image).Source = new BitmapImage(new Uri($@"..\Resourses\Images\EditorWindowImg\{imgName}_white.png", UriKind.Relative));            
        }
        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            Grid content = button.Content as Grid;
            string imgName = (content.Children[1] as Image).Source.ToString().Split('/').Last().Split('_').First();

            button.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#7DABE0");
            (content.Children[0] as Grid).Background = Brushes.Transparent;
            (content.Children[1] as Image).Source = new BitmapImage(new Uri($@"..\Resourses\Images\EditorWindowImg\{imgName}_blue.png", UriKind.Relative));

        }     
    
    }
}

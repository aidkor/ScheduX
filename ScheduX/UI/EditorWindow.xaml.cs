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
using ScheduX.Resourses;

namespace ScheduX.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        private object shxProjectFile { get; set; }
        public EditorWindow()
        {
            InitializeComponent();
            // HACK: Change this in XAML code with data binding
            Height = SystemParameters.PrimaryScreenHeight / 1.9;
            Width = SystemParameters.PrimaryScreenWidth / 2;
        }
        public EditorWindow(object filePath)
        {
            InitializeComponent();
            shxProjectFile = filePath;
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
            LeftSideBarContent.Visibility = Visibility.Collapsed;
            LeftSideBarOpenCloseButtonImg.Source = new BitmapImage(new Uri(@"..\Resourses\Images\EditorWindowImg\menu.png", UriKind.Relative));
            LeftSideBarOpenCloseButtonImg.Margin = new Thickness(17, 0, 17, 0);
            LeftSideBarOpenCloseButtonImg.Height = 24;
            LeftSideBarOpenCloseButtonImg.Width = 24;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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


        private void ButtonMouseEnterHandler(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = (Brush)new BrushConverter().ConvertFrom("#5092FF");
        }

        private void ButtonMouseLeaveHandler(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = (Brush)new BrushConverter().ConvertFrom("#354052");
        }
    }
}

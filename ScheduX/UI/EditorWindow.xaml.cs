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
using ScheduX.UI.PeriodOfStudy;

namespace ScheduX.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        PeriodOfStudyWindow PeriodOfStudyWindow { get; set; }
        private object shxProjectFile { get; set; }
        public EditorWindow()
        {
            InitializeComponent();          
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
        private void PeriodOfStudyItemHandler(object sender, RoutedEventArgs e)
        {
            if (PeriodOfStudyWindow == null)
            {
                PeriodOfStudyWindow = new PeriodOfStudyWindow();
                PeriodOfStudyWindow.Owner = this;
                PeriodOfStudyWindow.Show();
            }
            else
            {
                PeriodOfStudyWindow.Show();
            }
        }

        // HACK: Rewrite in XAML code
        #region
        private void ButtonMouseEnterHandler(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = (Brush)new BrushConverter().ConvertFrom("#5092FF");
            ConfigurateMenu.Visibility = Visibility.Visible;
        }

        private void ButtonMouseLeaveHandler(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = (Brush)new BrushConverter().ConvertFrom("#354052");
            ConfigurateMenu.Visibility = Visibility.Collapsed;
        }
        private void ButtonMouseEnterHandler1(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = (Brush)new BrushConverter().ConvertFrom("#5092FF");
            DictionaryButtonList.Visibility = Visibility.Visible;
        }

        private void ButtonMouseLeaveHandler1(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = (Brush)new BrushConverter().ConvertFrom("#354052");
            DictionaryButtonList.Visibility = Visibility.Collapsed;
        }
        private void ButtonMouseEnterHandler2(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = (Brush)new BrushConverter().ConvertFrom("#5092FF");
            LoadButtonList.Visibility = Visibility.Visible;
        }

        private void ButtonMouseLeaveHandler2(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = (Brush)new BrushConverter().ConvertFrom("#354052");
            LoadButtonList.Visibility = Visibility.Collapsed;
        }
        #endregion
        // =============================================================================================

        private void MenuMouseEnterHandler(object sender, MouseEventArgs e)
        {
            ((Menu)sender).Visibility = Visibility.Visible;
        }

        private void MenuMouseLeaveHandler(object sender, MouseEventArgs e)
        {
            ((Menu)sender).Visibility = Visibility.Collapsed;
        }
    }
}

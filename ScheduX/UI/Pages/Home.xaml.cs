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
using ScheduX.UI.Teachers;
using ScheduX.Resourses.AppLogic;

namespace ScheduX.UI.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public PeriodOfStudyWindow PeriodOfStudyWindowInstance { get; set; }
        public ClassesWindow ClassesWindowInstance { get; set; }
        public TeachersWindow TeachersWindowInstance { get; set; }
        public EditorWindow EditorWindowInstance { get; set; }
        public Home(EditorWindow instance)
        {
            InitializeComponent();
            EditorWindowInstance = instance;
        }
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
        private void MenuMouseEnterHandler(object sender, MouseEventArgs e)
        {
            ((Menu)sender).Visibility = Visibility.Visible;
        }
        private void MenuMouseLeaveHandler(object sender, MouseEventArgs e)
        {
            ((Menu)sender).Visibility = Visibility.Collapsed;
        }
        #endregion
        private void PeriodOfStudyItemHandler(object sender, RoutedEventArgs e)
        {
            PeriodOfStudyWindowInstance = PeriodOfStudyWindowInstance ?? new PeriodOfStudyWindow();
            PeriodOfStudyWindowInstance.Owner = EditorWindowInstance;
            PeriodOfStudyWindowInstance.Show();
        }
        private void ClassesItemHandler(object sender, RoutedEventArgs e)
        {
            ClassesWindowInstance = ClassesWindowInstance ?? new ClassesWindow();
            ClassesWindowInstance.Owner = EditorWindowInstance;
            ClassesWindowInstance.Show();
        }

        private void TeachersItemHandler(object sender, RoutedEventArgs e)
        {
            TeachersWindowInstance = TeachersWindowInstance ?? new TeachersWindow();
            TeachersWindowInstance.Owner = EditorWindowInstance;
            TeachersWindowInstance.Show();
        }

        private void AudiencesItemHandler(object sender, RoutedEventArgs e)
        {

        }

        private void SubjectsItemHandler(object sender, RoutedEventArgs e)
        {

        }
    }
}

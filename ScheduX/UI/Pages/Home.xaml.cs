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
using ScheduX.UI.Audiences;
using ScheduX.UI.Subjects;
using ScheduX.UI.Lessons;
using ScheduX.Resourses.AppLogic;

namespace ScheduX.UI.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public PeriodOfStudyWindow PeriodOfStudyWindowInstance { get; set; }    
        public GroupsWindow ClassesWindowInstance { get; set; }
        public TeachersWindow TeachersWindowInstance { get; set; }
        public AudiencesWindow AudiencesWindowInstance { get; set; }
        public SubjectsWindow SubjectsWindowInstance { get; set; }
        public LessonsWindow LessonsWindowInstance { get; set; }
        public EditorWindow EditorWindowInstance { get; set; }
        public Home(EditorWindow instance)
        {
            InitializeComponent();
            EditorWindowInstance = instance;
        }
        #region
        private void ButtonMouseEnterHandler(object sender, MouseEventArgs e)
        {            
            ConfigurateMenu.Visibility = Visibility.Visible;
        }

        private void ButtonMouseLeaveHandler(object sender, MouseEventArgs e)
        {            
            ConfigurateMenu.Visibility = Visibility.Collapsed;
        }
        private void ButtonMouseEnterHandler1(object sender, MouseEventArgs e)
        {            
            DictionaryButtonList.Visibility = Visibility.Visible;
        }

        private void ButtonMouseLeaveHandler1(object sender, MouseEventArgs e)
        {            
            DictionaryButtonList.Visibility = Visibility.Collapsed;
        }
        private void ButtonMouseEnterHandler2(object sender, MouseEventArgs e)
        {            
            LoadButtonList.Visibility = Visibility.Visible;
        }

        private void ButtonMouseLeaveHandler2(object sender, MouseEventArgs e)
        {            
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
            ClassesWindowInstance = ClassesWindowInstance ?? new GroupsWindow();
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
            AudiencesWindowInstance = AudiencesWindowInstance ?? new AudiencesWindow();
            AudiencesWindowInstance.Owner = EditorWindowInstance;
            AudiencesWindowInstance.Show();
        }
        private void SubjectsItemHandler(object sender, RoutedEventArgs e)
        {
            SubjectsWindowInstance = SubjectsWindowInstance ?? new SubjectsWindow();
            SubjectsWindowInstance.Owner = EditorWindowInstance;
            SubjectsWindowInstance.Show();
        }
        private void LessonsItemHandler(object sender, RoutedEventArgs e)
        {
            LessonsWindowInstance = LessonsWindowInstance ?? new LessonsWindow();
            LessonsWindowInstance.Owner = EditorWindowInstance;
            LessonsWindowInstance.Show();
        }
    }
}

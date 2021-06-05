using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ScheduX.UI.PeriodOfStudy;
using ScheduX.UI.Classes;
using ScheduX.UI.Teachers;
using ScheduX.UI.Audiences;
using ScheduX.UI.Subjects;
using ScheduX.UI.Lessons;
using ScheduX.App_Logic.OptimAlg;

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
        public EditorWindow ParentWindowInstance { get; set; }
        public Home(EditorWindow instance)
        {
            InitializeComponent();
            ParentWindowInstance = instance;
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
            PeriodOfStudyWindowInstance.Owner = ParentWindowInstance;
            PeriodOfStudyWindowInstance.Show();
        }    
        private void ClassesItemHandler(object sender, RoutedEventArgs e)
        {
            ClassesWindowInstance = ClassesWindowInstance ?? new GroupsWindow();
            ClassesWindowInstance.Owner = ParentWindowInstance;
            ClassesWindowInstance.Show();
        }
        private void TeachersItemHandler(object sender, RoutedEventArgs e)
        {
            TeachersWindowInstance = TeachersWindowInstance ?? new TeachersWindow();
            TeachersWindowInstance.Owner = ParentWindowInstance;
            TeachersWindowInstance.Show();
        }
        private void AudiencesItemHandler(object sender, RoutedEventArgs e)
        {
            AudiencesWindowInstance = AudiencesWindowInstance ?? new AudiencesWindow();
            AudiencesWindowInstance.Owner = ParentWindowInstance;
            AudiencesWindowInstance.Show();
        }
        private void SubjectsItemHandler(object sender, RoutedEventArgs e)
        {
            SubjectsWindowInstance = SubjectsWindowInstance ?? new SubjectsWindow();
            SubjectsWindowInstance.Owner = ParentWindowInstance;
            SubjectsWindowInstance.Show();
        }
        private void LessonsItemHandler(object sender, RoutedEventArgs e)
        {
            LessonsWindowInstance = LessonsWindowInstance ?? new LessonsWindow();
            LessonsWindowInstance.Owner = ParentWindowInstance;
            LessonsWindowInstance.Show();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            SetLoadingCursor();

            var list = LessonsWindowInstance.SchoolLessonDict.dictionaryList;           
            var solver = new Solver();//создаем решатель

            Plan.DaysPerWeek = 5;
            Plan.HoursPerDay = 6;

            solver.FitnessFunctions.Add(FitnessFunctions.Windows);//будем штрафовать за окна            
            solver.FitnessFunctions.Add(FitnessFunctions.LateLesson);//будем штрафовать за поздние пары

            var res = solver.Solve(list);//находим лучший план

            Plan.WriteInFile(@"C:\Users\Asus\Desktop\Plan.txt", res.ToString());
        }
        private async void SetLoadingCursor()
        {
            this.Cursor = Cursors.Wait;
            await Task.Delay(1500);
            this.Cursor = Cursors.Arrow;
        }
    }
}

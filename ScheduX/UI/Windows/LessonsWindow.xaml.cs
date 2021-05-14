using System;
using System.Windows;
using ScheduX.App_Logic;

namespace ScheduX.UI.Lessons
{
    /// <summary>
    /// Interaction logic for LessonsWindow.xaml
    /// </summary>
    public partial class LessonsWindow : Window
    {
        public NewLessonWindow NewLessonWindowInstance { get; set; }
        public SchoolLessonDictionary SchoolLessonDict { get; set; }
        public LessonsWindow()
        {
            InitializeComponent();
            SchoolLessonDict = new SchoolLessonDictionary();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }
        private void OnClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ElementsModification.HideChildWindow(this);
        }
        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewLessonWindowInstance = NewLessonWindowInstance ?? new NewLessonWindow();
            NewLessonWindowInstance.Owner = this;
            NewLessonWindowInstance.Add.Click -= NewLessonWindowInstance.Done_Click;
            NewLessonWindowInstance.Add.Click += NewLessonWindowInstance.Add_Click;
            NewLessonWindowInstance.Show();


        }
        private void EmptyDictionaryChecker()
        {
            if (SchoolLessonDict.dictionaryList.Count == 0)
            {
                (Owner as EditorWindow).HomePage.PeriodIndicator.Fill = ColorPalette.GetPredefinedColor(PredefinedColors.Grey);
                //TrashBin.IsEnabled = false;

            }
        }        
        private void ContextMenuEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (LessonsList.SelectedItem != null & LessonsList.SelectedItems.Count == 1)
            {
                NewLessonWindowInstance.Add.Click -= NewLessonWindowInstance.Add_Click;
                NewLessonWindowInstance.Add.Click += NewLessonWindowInstance.Done_Click;
                NewLessonWindowInstance.Add.Content = "DONE";

                var currentElement = LessonsList.SelectedItem as SchoolLesson;
                NewLessonWindowInstance.NameTextBox.Text = currentElement.Name;
                NewLessonWindowInstance.SubjectComboBox.SelectedItem = currentElement.Subject;
                NewLessonWindowInstance.AudienceComboBox.SelectedItem = currentElement.Audience;
                NewLessonWindowInstance.TeacherComboBox.SelectedItem = currentElement.Teacher;
                NewLessonWindowInstance.GroupComboBox.SelectedItem = currentElement.Group;

                NewLessonWindowInstance.Show();
            }
            else
            {
                MessageBox.Show("Choose a lesson");
            }
        }
        private void ContextMenuCopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (LessonsList.SelectedItems != null)
            {
                foreach (SchoolLesson item in LessonsList.SelectedItems)
                {
                    var lesson = new SchoolLesson(item.Name, item.Subject, item.Teacher, item.Audience, item.Group);
                    SchoolLessonDict.dictionaryList.Add(lesson);
                    LessonsList.Items.Add(lesson);
                }
            }
        }
        private void ContextMenuDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (LessonsList.SelectedItems != null)
            {
                MessageBoxResult result = MessageBox.Show("Sure want to delete ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                    {
                        while (LessonsList.SelectedItems.Count != 0)
                        {
                            SchoolLessonDict.dictionaryList.RemoveAll(group => group.GetHashCode() == LessonsList.SelectedItems[0].GetHashCode());
                            LessonsList.Items.RemoveAt(LessonsList.Items.IndexOf(LessonsList.SelectedItems[0]));
                        }
                        EmptyDictionaryChecker();
                        break;
                    }

                    case MessageBoxResult.No:
                        break;
                }
            }
        }
        private void TrashBin_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Sure want to delete ALL ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            switch (result)
            {
                case MessageBoxResult.Yes:
                {
                    SchoolLessonDict.dictionaryList.Clear();
                    LessonsList.Items.Clear();
                    EmptyDictionaryChecker();
                    break;
                }
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}

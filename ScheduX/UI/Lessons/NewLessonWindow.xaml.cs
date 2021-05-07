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
using System.Windows.Shapes;
using ScheduX.Resourses.AppLogic;
using ScheduX.Resourses.UILogic;
using ScheduX.UI.Pages;

namespace ScheduX.UI.Lessons
{
    /// <summary>
    /// Interaction logic for NewLessonWindow.xaml
    /// </summary>
    public partial class NewLessonWindow : Window
    {
        public List<GroupElement> Groups { get; set; } = new List<GroupElement>();
        public List<AudienceElement> Audiences { get; set; }  = new List<AudienceElement>();
        public List<SubjectElement> Subjects { get; set; } = new List<SubjectElement>();
        public List<TeacherElement> Teachers { get; set; }  = new List<TeacherElement>();

        public NewLessonWindow()
        {           
            InitializeComponent();
            this.DataContext = this;
        }
      
        private void SubjectTextBox_DropDownOpened(object sender, EventArgs e)
        {
            (sender as ComboBox).BorderBrush = new BrushConverter().ConvertFrom("#FFFFFF") as SolidColorBrush;
            var ownerEditorWindowInstance = (this.Owner as LessonsWindow).Owner as EditorWindow;
            var data = ownerEditorWindowInstance.HomePage.SubjectsWindowInstance?.SubjectsList.Items;
            if (data != null)
            {
                foreach (var item in data)
                {
                    Subjects.Add(item as SchoolSubject);
                }
            }
        }
        private void AudienceTextBox_DropDownOpened(object sender, EventArgs e)
        {
            var ownerEditorWindowInstance = (this.Owner as LessonsWindow).Owner as EditorWindow;
            var data = ownerEditorWindowInstance.HomePage.AudiencesWindowInstance?.AudiencesList.Items;
            if (data != null)
            {
                foreach (var item in data)
                {
                    Audiences.Add(item as SchoolAudience);
                }
            }
        }
        private void TeacherTextBox_DropDownOpened(object sender, EventArgs e)
        {
            var ownerEditorWindowInstance = (this.Owner as LessonsWindow).Owner as EditorWindow;
            var data = ownerEditorWindowInstance.HomePage.TeachersWindowInstance?.TeachersList.Items;
            if (data != null)
            {
                foreach (var item in data)
                {
                    Teachers.Add(item as SchoolTeacher);
                }
            }
        }
        private void GroupTextBox_DropDownOpened(object sender, EventArgs e)
        {
            var ownerEditorWindowInstance = (this.Owner as LessonsWindow).Owner as EditorWindow;
            var data = ownerEditorWindowInstance.HomePage.ClassesWindowInstance?.GroupsList.Items;
            if (data != null)
            {
                foreach (var item in data)
                {
                    Groups.Add(item as SchoolGroup);
                }
            }
        }
        private void ResetControls()
        {
            this.Visibility = Visibility.Hidden;
            foreach (TextBox item in FindVisualChildren<TextBox>(this))
            {
                item.Text = null;
            }
            foreach (ComboBox item in FindVisualChildren<ComboBox>(this))
            {
                item.Text = null;
            }
        }
        private bool IsWrongTextBoxValue()
        {
            bool flag = false;
            foreach (TextBox item in FindVisualChildren<TextBox>(this))
            {
                if (item.Text == "")
                {
                    item.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
                    flag = true;
                }
            }
            foreach (ComboBox item in FindVisualChildren<ComboBox>(this))
            {
                if (item.Items.IsEmpty)
                {                    
                    flag = true;
                }
            }
            return flag;
        }
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).BorderBrush = new BrushConverter().ConvertFrom("#595959") as SolidColorBrush;
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }
        private void OnClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ResetControls();
        }
        public void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var OwnerWindowInstance = (LessonsWindow)this.Owner;
                SchoolLesson lesson = new SchoolLesson(NameTextBox.Text, SubjectComboBox.SelectedItem as SchoolSubject, TeacherComboBox.SelectedItem as SchoolTeacher, AudienceComboBox.SelectedItem as SchoolAudience, GroupComboBox.SelectedItem as SchoolGroup);
                OwnerWindowInstance.Dict.dictionaryList.Add(lesson);
                OwnerWindowInstance.LessonsList.Items.Add(lesson);
                ResetControls();
            }
        }
        public void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var ownerWindowInstance = this.Owner as LessonsWindow;
                var lesson = ownerWindowInstance.Dict.dictionaryList.Find(item => item.GetHashCode() == ownerWindowInstance.LessonsList.SelectedItem.GetHashCode());

                lesson.Name = NameTextBox.Text;
                lesson.Subject = SubjectComboBox.SelectedItem as SchoolSubject;
                lesson.Teacher = TeacherComboBox.SelectedItem as SchoolTeacher;
                lesson.Audience = AudienceComboBox.SelectedItem as SchoolAudience;
                lesson.Group = GroupComboBox.SelectedItem as SchoolGroup;

                int index = ownerWindowInstance.LessonsList.Items.IndexOf(ownerWindowInstance.LessonsList.SelectedItem);
                ownerWindowInstance.LessonsList.Items.Remove(ownerWindowInstance.LessonsList.SelectedItem);
                ownerWindowInstance.LessonsList.Items.Insert(index, lesson);

                ResetControls();
            }
        }


    }
}

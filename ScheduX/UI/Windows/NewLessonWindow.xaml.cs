using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ScheduX.App_Logic;

namespace ScheduX.UI.Lessons
{
    /// <summary>
    /// Interaction logic for NewLessonWindow.xaml
    /// </summary>
    public partial class NewLessonWindow : Window
    {
        public List<SchoolGroup> Groups { get; set; } = new List<SchoolGroup>();
        public List<SchoolAudience> Audiences { get; set; } = new List<SchoolAudience>();
        public List<SchoolSubject> Subjects { get; set; } = new List<SchoolSubject>();
        public List<SchoolTeacher> Teachers { get; set; } = new List<SchoolTeacher>();

        public NewLessonWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void SubjectTextBox_DropDownOpened(object sender, EventArgs e)
        {

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
        private bool IsWrongTextBoxValue()
        {
            bool flag = false;
            foreach (TextBox item in ElementsModification.FindVisualChildren<TextBox>(this))
            {
                if (item.Text == "")
                {
                    item.BorderBrush = ColorPalette.GetPredefinedColor(PredefinedColors.Blood);
                    flag = true;
                }
                if (!uint.TryParse(item.Text, out uint _) & item.Name != "NameTextBox")
                {
                    item.BorderBrush = ColorPalette.GetPredefinedColor(PredefinedColors.Blood);
                    flag = true;
                }
            }
            foreach (ComboBox item in ElementsModification.FindVisualChildren<ComboBox>(this))
            {
                if (item.Items.IsEmpty)
                {
                    flag = true;
                }
            }
            return flag;
        }
        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).BorderBrush = ColorPalette.GetPredefinedColor(PredefinedColors.DarkGrey);
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }
        private void OnClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ElementsModification.ResetControlText<ComboBox>(this);
            ElementsModification.ResetControlText<TextBox>(this);
        }
        public void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var OwnerWindowInstance = this.Owner as LessonsWindow;
                for (int i = 0; i < int.Parse(LessonsPerWeekTextBox.Text); i++)
                {
                    SchoolLesson lesson = new SchoolLesson(NameTextBox.Text, SubjectComboBox.SelectedItem as SchoolSubject, TeacherComboBox.SelectedItem as SchoolTeacher, AudienceComboBox.SelectedItem as SchoolAudience, GroupComboBox.SelectedItem as SchoolGroup);
                    OwnerWindowInstance.SchoolLessonDict.dictionaryList.Add(lesson);
                    OwnerWindowInstance.LessonsList.Items.Add(lesson);
                }
                ((Owner as LessonsWindow).Owner as EditorWindow).HomePage.LessonsIndicator.Fill = ColorPalette.GetPredefinedColor(PredefinedColors.Green);
                ElementsModification.ResetControlText<ComboBox>(this);
                ElementsModification.ResetControlText<TextBox>(this);
            }
        }
        public void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var ownerWindowInstance = this.Owner as LessonsWindow;
                var lesson = ownerWindowInstance.SchoolLessonDict.dictionaryList.Find(item => item.GetHashCode() == ownerWindowInstance.LessonsList.SelectedItem.GetHashCode());

                lesson.Name = NameTextBox.Text;
                lesson.Subject = SubjectComboBox.SelectedItem as SchoolSubject;
                lesson.Teacher = TeacherComboBox.SelectedItem as SchoolTeacher;
                lesson.Audience = AudienceComboBox.SelectedItem as SchoolAudience;
                lesson.Group = GroupComboBox.SelectedItem as SchoolGroup;

                int index = ownerWindowInstance.LessonsList.Items.IndexOf(ownerWindowInstance.LessonsList.SelectedItem);
                ownerWindowInstance.LessonsList.Items.Remove(ownerWindowInstance.LessonsList.SelectedItem);
                ownerWindowInstance.LessonsList.Items.Insert(index, lesson);

                ElementsModification.ResetControlText<ComboBox>(this);
                ElementsModification.ResetControlText<TextBox>(this);
            }
        }
    }
}

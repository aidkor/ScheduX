using System;
using System.Windows;
using System.Windows.Controls;
using ScheduX.Resourses.UILogic;
using ScheduX.Resourses.AppLogic;

namespace ScheduX.UI.Teachers
{
    /// <summary>
    /// Interaction logic for NewTeacherWindow.xaml
    /// </summary>
    public partial class NewTeacherWindow : Window
    {
        public NewTeacherWindow()
        {
            InitializeComponent();
        }     
        private bool IsWrongTextBoxValue()
        {
            bool flag = false;
            foreach (TextBox item in UITools.FindVisualChildren<TextBox>(this))
            {
                if (item.Text == "")
                {
                    item.BorderBrush = UITools.GetRedColor();
                    flag = true;
                }
                if (!uint.TryParse(item.Text, out uint _) & item.Name == "ExperienceTextBox")
                {
                    item.BorderBrush = UITools.GetRedColor();
                    flag = true;
                }
            }
            return flag;
        }
        public void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var OwnerWindowInstance = this.Owner as TeachersWindow;
                var teacher = new SchoolTeacher(NameTextBox.Text,PostTextBox.Text,int.Parse(ExperienceTextBox.Text),AddressTextBox.Text,TelephoneTextBox.Text);
                OwnerWindowInstance.SchoolTeacherDictionary.dictionaryList.Add(teacher);
                OwnerWindowInstance.TeachersList.Items.Add(teacher);
                ((Owner as TeachersWindow).Owner as EditorWindow).HomePage.TeachersIndicator.Fill = UITools.GetGreenColor();
                UITools.ResetControlText<TextBox>(this);
            }
        }
        public void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var ownerWindowInstance = this.Owner as TeachersWindow;
                var teacher = ownerWindowInstance.SchoolTeacherDictionary.dictionaryList.Find(item => item.GetHashCode() == ownerWindowInstance.TeachersList.SelectedItem.GetHashCode());

                teacher.Name = NameTextBox.Text;
                teacher.Post = PostTextBox.Text;
                teacher.Experience = int.Parse(ExperienceTextBox.Text);
                teacher.Address = AddressTextBox.Text;
                teacher.Telephone = TelephoneTextBox.Text;

                int index = ownerWindowInstance.TeachersList.Items.IndexOf(ownerWindowInstance.TeachersList.SelectedItem);
                ownerWindowInstance.TeachersList.Items.Remove(ownerWindowInstance.TeachersList.SelectedItem);
                ownerWindowInstance.TeachersList.Items.Insert(index, teacher);

                UITools.ResetControlText<TextBox>(this);
            }
        }
        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).BorderBrush = UITools.GetDarkGreyColor();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            UITools.RemoveIcon(this);
        }
        private void OnClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            UITools.ResetControlText<TextBox>(this);
        }
    }
}

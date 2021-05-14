using System;
using System.Windows;
using System.Windows.Controls;
using ScheduX.App_Logic;

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
            foreach (TextBox item in ElementsModification.FindVisualChildren<TextBox>(this))
            {
                if (item.Text == "")
                {
                    item.BorderBrush = ColorPalette.GetPredefinedColor(PredefinedColors.Blood);
                    flag = true;
                }
                if (!uint.TryParse(item.Text, out uint _) & item.Name == "ExperienceTextBox")
                {
                    item.BorderBrush = ColorPalette.GetPredefinedColor(PredefinedColors.Blood);
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
                OwnerWindowInstance.SchoolTeacherDict.dictionaryList.Add(teacher);
                OwnerWindowInstance.TeachersList.Items.Add(teacher);
                ((Owner as TeachersWindow).Owner as EditorWindow).HomePage.TeachersIndicator.Fill = ColorPalette.GetPredefinedColor(PredefinedColors.Green);
                ElementsModification.ResetControlText<TextBox>(this);
            }
        }
        public void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var ownerWindowInstance = this.Owner as TeachersWindow;
                var teacher = ownerWindowInstance.SchoolTeacherDict.dictionaryList.Find(item => item.GetHashCode() == ownerWindowInstance.TeachersList.SelectedItem.GetHashCode());

                teacher.Name = NameTextBox.Text;
                teacher.Post = PostTextBox.Text;
                teacher.Experience = int.Parse(ExperienceTextBox.Text);
                teacher.Address = AddressTextBox.Text;
                teacher.Telephone = TelephoneTextBox.Text;

                int index = ownerWindowInstance.TeachersList.Items.IndexOf(ownerWindowInstance.TeachersList.SelectedItem);
                ownerWindowInstance.TeachersList.Items.Remove(ownerWindowInstance.TeachersList.SelectedItem);
                ownerWindowInstance.TeachersList.Items.Insert(index, teacher);

                ElementsModification.ResetControlText<TextBox>(this);
            }
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
            ElementsModification.ResetControlText<TextBox>(this);
        }
    }
}

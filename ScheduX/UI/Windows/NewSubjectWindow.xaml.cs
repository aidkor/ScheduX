using System;
using System.Windows;
using System.Windows.Controls;
using ScheduX.App_Logic;

namespace ScheduX.UI.Subjects
{
    /// <summary>
    /// Interaction logic for NewSubjectWindow.xaml
    /// </summary>
    public partial class NewSubjectWindow : Window
    {
        public NewSubjectWindow()
        {
            InitializeComponent();
        }
        public void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var OwnerWindowInstance = this.Owner as SubjectsWindow;
                var subject = new SchoolSubject(NameTextBox.Text, int.Parse(ComplexityTextBox.Text));
                OwnerWindowInstance.SchoolSubjectDict.dictionaryList.Add(subject);
                OwnerWindowInstance.SubjectsList.Items.Add(subject);
                ((Owner as SubjectsWindow).Owner as EditorWindow).HomePage.SubjectsIndicator.Fill = ColorPalette.GetPredefinedColor(PredefinedColors.Green);

                ElementsModification.ResetControlText<TextBox>(this);
            }
        }
        public void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var ownerWindowInstance = this.Owner as SubjectsWindow;
                var subject = ownerWindowInstance.SchoolSubjectDict.dictionaryList.Find(item => item.GetHashCode() == ownerWindowInstance.SubjectsList.SelectedItem.GetHashCode());

                subject.Name = NameTextBox.Text;
                subject.Complexity = int.Parse(ComplexityTextBox.Text);                

                int index = ownerWindowInstance.SubjectsList.Items.IndexOf(ownerWindowInstance.SubjectsList.SelectedItem);
                ownerWindowInstance.SubjectsList.Items.Remove(ownerWindowInstance.SubjectsList.SelectedItem);
                ownerWindowInstance.SubjectsList.Items.Insert(index, subject);

                ElementsModification.ResetControlText<TextBox>(this);
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
            ElementsModification.ResetControlText<TextBox>(this);
        }
    }
}

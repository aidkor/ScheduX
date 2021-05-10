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
                OwnerWindowInstance.SchoolSubjectDictionary.dictionaryList.Add(subject);
                OwnerWindowInstance.SubjectsList.Items.Add(subject);
                ((Owner as SubjectsWindow).Owner as EditorWindow).HomePage.SubjectsIndicator.Fill = UITools.GetGreenColor();

                UITools.ResetControlText<TextBox>(this);
            }
        }
        public void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var ownerWindowInstance = this.Owner as SubjectsWindow;
                var subject = ownerWindowInstance.SchoolSubjectDictionary.dictionaryList.Find(item => item.GetHashCode() == ownerWindowInstance.SubjectsList.SelectedItem.GetHashCode());

                subject.Name = NameTextBox.Text;
                subject.Complexity = int.Parse(ComplexityTextBox.Text);                

                int index = ownerWindowInstance.SubjectsList.Items.IndexOf(ownerWindowInstance.SubjectsList.SelectedItem);
                ownerWindowInstance.SubjectsList.Items.Remove(ownerWindowInstance.SubjectsList.SelectedItem);
                ownerWindowInstance.SubjectsList.Items.Insert(index, subject);

                UITools.ResetControlText<TextBox>(this);
            }
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
                if (!uint.TryParse(item.Text, out uint _) & item.Name != "NameTextBox")
                {
                    item.BorderBrush = UITools.GetRedColor();
                    flag = true;
                }
            }
          
            return flag;
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

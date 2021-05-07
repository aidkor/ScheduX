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
                var OwnerWindowInstance = (SubjectsWindow)this.Owner;
                var subject = new SchoolSubject(NameTextBox.Text, int.Parse(ComplexityTextBox.Text));
                OwnerWindowInstance.SchoolSubjectDictionary.dictionaryList.Add(subject);
                OwnerWindowInstance.SubjectsList.Items.Add(subject);
                ((Owner as SubjectsWindow).Owner as EditorWindow).HomePage.SubjectsIndicator.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#A8D66D");
                ResetControls();
            }
        }
        public void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var ownerWindowInstance = (SubjectsWindow)this.Owner;
                var subject = (SchoolSubject)ownerWindowInstance.SchoolSubjectDictionary.dictionaryList.Find(item => item.GetHashCode() == ownerWindowInstance.SubjectsList.SelectedItem.GetHashCode());

                subject.Name = NameTextBox.Text;
                subject.Complexity = int.Parse(ComplexityTextBox.Text);                

                int index = ownerWindowInstance.SubjectsList.Items.IndexOf(ownerWindowInstance.SubjectsList.SelectedItem);
                ownerWindowInstance.SubjectsList.Items.Remove(ownerWindowInstance.SubjectsList.SelectedItem);
                ownerWindowInstance.SubjectsList.Items.Insert(index, subject);

                ResetControls();
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
                if (!uint.TryParse(item.Text, out uint _) & item.Name != "NameTextBox")
                {
                    item.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
                    flag = true;
                }
            }
          
            return flag;
        }
        private void ResetControls()
        {
            this.Visibility = Visibility.Hidden;
            foreach (TextBox item in FindVisualChildren<TextBox>(this))
            {
                item.Text = null;
            }
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
            ((TextBox)sender).BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#595959");
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
    }
}

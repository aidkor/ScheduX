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
    /// Interaction logic for SubjectsWindow.xaml
    /// </summary>
    public partial class SubjectsWindow : Window
    {
        public NewSubjectWindow NewSubjectWindowInstance { get; set; }
        public SchoolSubjectDictionary SchoolSubjectDictionary { get; set; }
        public SubjectsWindow()
        {
            InitializeComponent();
            SchoolSubjectDictionary = new SchoolSubjectDictionary();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            UITools.RemoveIcon(this);
        }
        private void OnClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            UITools.HideChildWindow(this);
        }
        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewSubjectWindowInstance = NewSubjectWindowInstance ?? new NewSubjectWindow();
            NewSubjectWindowInstance.Owner = this;
            
            NewSubjectWindowInstance.Add.Click -= NewSubjectWindowInstance.Done_Click;
            NewSubjectWindowInstance.Add.Click += NewSubjectWindowInstance.Add_Click;
            
            NewSubjectWindowInstance.Show();
        }
        private void ContextMenuEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectsList.SelectedItem != null & SubjectsList.SelectedItems.Count == 1)
            {
                NewSubjectWindowInstance.Add.Click -= NewSubjectWindowInstance.Add_Click;
                NewSubjectWindowInstance.Add.Click += NewSubjectWindowInstance.Done_Click;
                NewSubjectWindowInstance.Add.Content = "DONE";

                var currentElement = SubjectsList.SelectedItem as SchoolSubject;
                NewSubjectWindowInstance.NameTextBox.Text = currentElement.Name;
                NewSubjectWindowInstance.ComplexityTextBox.Text = currentElement.Complexity.ToString();

                NewSubjectWindowInstance.Show();
            }
            else
            {
                MessageBox.Show("Choose a subject");
            }
        }
        private void ContextMenuCopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectsList.SelectedItems != null)
            {
                foreach (SchoolSubject item in SubjectsList.SelectedItems)
                {
                    var subject = new SchoolSubject(item.Name, item.Complexity);
                    SchoolSubjectDictionary.dictionaryList.Add(subject);
                    SubjectsList.Items.Add(subject);
                }
            }
        }
        private void ContextMenuDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectsList.SelectedItems != null)
            {
                MessageBoxResult result = MessageBox.Show("Sure want to delete ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                    {
                        while (SubjectsList.SelectedItems.Count != 0)
                        {
                            SchoolSubjectDictionary.dictionaryList.RemoveAll(subject => subject.GetHashCode() == SubjectsList.SelectedItems[0].GetHashCode());
                            SubjectsList.Items.RemoveAt(SubjectsList.Items.IndexOf(SubjectsList.SelectedItems[0]));
                        }
                        EmptyDictionaryChecker();
                        break;
                    }

                    case MessageBoxResult.No:
                        break;
                }
            }
        }
        private void ColumnSizeHandler(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width <= 100)
            {
                e.Handled = true;
                (sender as GridViewColumnHeader).Column.Width = 100;
            }
        }
        private void ImportExcel_Click(object sender, RoutedEventArgs e)
        {
            string[,] data = ExcelFileTools.UploadExcelData();
            if (data?.GetLength(1) >= UITools.FindVisualChildren<GridViewColumnHeader>(this).Count() - 2)
            {
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    int.TryParse(data[i, 1], out int complexity);

                    var subject = new SchoolSubject(data[i, 0], complexity);
                    SchoolSubjectDictionary.dictionaryList.Add(subject);
                    SubjectsList.Items.Add(subject);
                }
                 (Owner as EditorWindow).HomePage.SubjectsIndicator.Fill = UITools.GetGreenColor();
            }
            else
            {
                MessageBox.Show("Wrong Column Data");
            }
        }             
        private void EmptyDictionaryChecker()
        {
            if (SchoolSubjectDictionary.dictionaryList.Count == 0)
            {
                (Owner as EditorWindow).HomePage.SubjectsIndicator.Fill = UITools.GetGreyColor();
            }
        }       
    }
}

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ScheduX.App_Logic;


namespace ScheduX.UI.Subjects
{
    /// <summary>
    /// Interaction logic for SubjectsWindow.xaml
    /// </summary>
    public partial class SubjectsWindow : Window
    {
        public NewSubjectWindow NewSubjectWindowInstance { get; set; }
        public SchoolSubjectDictionary SchoolSubjectDict { get; set; }
        public SubjectsWindow()
        {
            InitializeComponent();
            SchoolSubjectDict = new SchoolSubjectDictionary();
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
                    SchoolSubjectDict.dictionaryList.Add(subject);
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
                            SchoolSubjectDict.dictionaryList.RemoveAll(subject => subject.GetHashCode() == SubjectsList.SelectedItems[0].GetHashCode());
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
            if (data?.GetLength(1) >= ElementsModification.FindVisualChildren<GridViewColumnHeader>(this).Count() - 2)
            {
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    int.TryParse(data[i, 1], out int complexity);

                    var subject = new SchoolSubject(data[i, 0], complexity);
                    SchoolSubjectDict.dictionaryList.Add(subject);
                    SubjectsList.Items.Add(subject);
                }
                (Owner as EditorWindow).HomePage.SubjectsIndicator.Fill = ColorPalette.GetPredefinedColor(PredefinedColors.Green);
            }
            else if (data?.GetLength(1) < ElementsModification.FindVisualChildren<GridViewColumnHeader>(this).Count() - 2)
            {
                MessageBox.Show("Wrong Columns Format");
            }
        }             
        private void EmptyDictionaryChecker()
        {
            if (SchoolSubjectDict.dictionaryList.Count == 0)
            {
                (Owner as EditorWindow).HomePage.SubjectsIndicator.Fill = ColorPalette.GetPredefinedColor(PredefinedColors.Grey);
            }
        }
        private void TrashBin_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Sure want to delete ALL ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            switch (result)
            {
                case MessageBoxResult.Yes:
                {
                    SchoolSubjectDict.dictionaryList.Clear();
                    SubjectsList.Items.Clear();
                    EmptyDictionaryChecker();
                    break;
                }
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ScheduX.Resourses.UILogic;
using ScheduX.Resourses.AppLogic;

namespace ScheduX.UI.PeriodOfStudy
{
    /// <summary>
    /// Interaction logic for PeriodOfStudy.xaml
    /// </summary>
    public partial class PeriodOfStudyWindow : Window
    {
        public NewPeriodWindow NewPeriodWindowInstance { get; set; }
        public SchoolStudyPeriodDictionary StudyPeriodDict { get; set; }        
        public PeriodOfStudyWindow()
        {
            InitializeComponent();
            StudyPeriodDict = new SchoolStudyPeriodDictionary();
        }
        private void Select_Click(object sender, RoutedEventArgs e)
        {
            if (PeriodsList.SelectedItem != null & PeriodsList.SelectedItems.Count == 1)
            {
                (Owner as EditorWindow).HomePage.PeriodIndicator.Fill = UITools.GetGreenColor();
                this.Close();
            }
            else
            {
                MessageBox.Show("Choose a period");
            }
        }
        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewPeriodWindowInstance = NewPeriodWindowInstance ?? new NewPeriodWindow();
            NewPeriodWindowInstance.Owner = this;
            
            NewPeriodWindowInstance.Add.Click -= NewPeriodWindowInstance.Done_Click;
            NewPeriodWindowInstance.Add.Click += NewPeriodWindowInstance.Add_Click;
            NewPeriodWindowInstance.Add.Content = "ADD";
            
            NewPeriodWindowInstance.Show();
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
        private void ColumnSizeHandler(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width <= 100)
            {
                e.Handled = true;
                (sender as GridViewColumnHeader).Column.Width = 100;
            }
        }
        private void ContextMenuSelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (PeriodsList.SelectedItem != null & PeriodsList.SelectedItems.Count == 1)
            {
                (Owner as EditorWindow).HomePage.PeriodIndicator.Fill = UITools.GetGreenColor();
                this.Close();
            }
            else
            {
                MessageBox.Show("Choose a period");
            }
        }
        private void ContextMenuEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (PeriodsList.SelectedItem != null & PeriodsList.SelectedItems.Count == 1)
            {
                NewPeriodWindowInstance.Add.Click -= NewPeriodWindowInstance.Add_Click;
                NewPeriodWindowInstance.Add.Click += NewPeriodWindowInstance.Done_Click;
                NewPeriodWindowInstance.Add.Content = "DONE";

                var currentElement = PeriodsList.SelectedItem as SchoolPeriod;
                NewPeriodWindowInstance.NameTextBox.Text = currentElement.Name;                
                NewPeriodWindowInstance.DatePicker_1.Text = currentElement.Start.ToString();
                NewPeriodWindowInstance.DatePicker_2.Text = currentElement.End.ToString();

                NewPeriodWindowInstance.Show();
            }
            else
            {
                MessageBox.Show("Choose one period");
            }
        }
        private void ContextMenuCopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (PeriodsList.SelectedItems != null)
            {
                foreach (SchoolPeriod item in PeriodsList.SelectedItems)
                {
                    var period = new SchoolPeriod(item.Name, item.Start, item.End);
                    StudyPeriodDict.dictionaryList.Add(period);
                    PeriodsList.Items.Add(period);
                }
            }
        }
        private void ContextMenuDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (PeriodsList.SelectedItems != null)
            {
                MessageBoxResult result = MessageBox.Show("Sure want to delete ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                    {
                        while (PeriodsList.SelectedItems.Count != 0)
                        {
                            StudyPeriodDict.dictionaryList.RemoveAll(period => period.GetHashCode() == PeriodsList.SelectedItems[0].GetHashCode());
                            PeriodsList.Items.RemoveAt(PeriodsList.Items.IndexOf(PeriodsList.SelectedItems[0]));
                        }
                        EmptyDictionaryChecker();
                        break;
                    }

                    case MessageBoxResult.No:
                        break;
                }
            }
        }       
        private void EmptyDictionaryChecker()
        {
            if (StudyPeriodDict.dictionaryList.Count.Equals(0))
            {
                (Owner as EditorWindow).HomePage.PeriodIndicator.Fill = UITools.GetGreyColor();
            }
        }       
        private void ImportExcel_Click(object sender, RoutedEventArgs e)
        {            
            string[,] data = ExcelFileTools.UploadExcelData();
            // HACK: Change Length Checker
            if (data?.GetLength(1) > UITools.FindVisualChildren<GridViewColumnHeader>(this).Count() - 2)
            {
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    DateTime.TryParse(data[i, 1], out DateTime start);
                    DateTime.TryParse(data[i, 2], out DateTime end);

                    var period = new SchoolPeriod(data[i, 0], start, end);
                    StudyPeriodDict.dictionaryList.Add(period);
                    PeriodsList.Items.Add(period);
                }
            }
            else
            {
                MessageBox.Show("Wrong Column Data");
            }
        }
        private void TrashBin_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Sure want to delete ALL ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            switch (result)
            {
                case MessageBoxResult.Yes:
                {
                    StudyPeriodDict.dictionaryList.Clear();
                    PeriodsList.Items.Clear();
                    EmptyDictionaryChecker();
                    break;
                }
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}

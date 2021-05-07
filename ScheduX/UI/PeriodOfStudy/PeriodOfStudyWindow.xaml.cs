using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ScheduX.Resourses.UILogic;
using ScheduX.Resourses.AppLogic;
using ScheduX.UI.PeriodOfStudy;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System.Collections;
using ScheduX.UI.Pages;

namespace ScheduX.UI.PeriodOfStudy
{
    /// <summary>
    /// Interaction logic for PeriodOfStudy.xaml
    /// </summary>
    public partial class PeriodOfStudyWindow : Window
    {
        public NewPeriodWindow NewPeriodWindowInstance { get; set; }
        public SchoolStudyPeriodDictionary Dict { get; set; }
        public PeriodOfStudyWindow()
        {
            InitializeComponent();
            Dict = new SchoolStudyPeriodDictionary();
        }
        private void Select_Click(object sender, RoutedEventArgs e)
        {
            if (PeriodsList.SelectedItem != null & PeriodsList.SelectedItems.Count == 1)
            {
                MakeIndicatorGreen();
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
            IconHelper.RemoveIcon(this);
        }
        private void OnClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            for (int i = 0; i < this.OwnedWindows.Count; i++)
            {
                this.OwnedWindows[i].Visibility = Visibility.Hidden;
            }
            this.Visibility = Visibility.Hidden;
        }
        private void ColumnSizeHandler(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width <= 100)
            {
                e.Handled = true;
                ((GridViewColumnHeader)sender).Column.Width = 100;
            }
        }
        private void ContextMenuSelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (PeriodsList.SelectedItem != null & PeriodsList.SelectedItems.Count == 1)
            {
                (Owner as EditorWindow).HomePage.PeriodIndicator.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#A8D66D");
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

                var currentElement = (SchoolPeriod)PeriodsList.SelectedItem;
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
                    Dict.dictionaryList.Add(period);
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
                            Dict.dictionaryList.RemoveAll(period => period.GetHashCode() == PeriodsList.SelectedItems[0].GetHashCode());
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
        private void ImportExcel_Click(object sender, RoutedEventArgs e)
        {
            string[,] data = UploadExcelData();  
            // HACK: Change Length Checker
            if (data?.GetLength(1) >= FindVisualChildren<GridViewColumnHeader>(this).Count() - 2)
            {
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    DateTime.TryParse(data[i, 1], out DateTime start);
                    DateTime.TryParse(data[i, 2], out DateTime end);

                    var period = new SchoolPeriod(data[i,0], start, end);
                    Dict.dictionaryList.Add(period);
                    PeriodsList.Items.Add(period);
                }
            }
            else
            {
                MessageBox.Show("Wrong Column Data");
            }
        } 
        private string[,] UploadExcelData()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel File (*.xlsx)|*.xlsx";
            if (ofd.ShowDialog() == true)
            {
                Excel.Application ObjWorkExcel = new Excel.Application();
                Excel.Workbook ObjWorkBook = ObjWorkExcel.Workbooks.Open(ofd.FileName);
                Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1]; //получить 1-й лист
                var lastCell = ObjWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);//последнюю ячейку

                string[,] data = new string[lastCell.Row, lastCell.Column];
                for (int i = 0; i < lastCell.Column; i++)
                {
                    for (int j = 0; j < lastCell.Row; j++)
                    {
                        data[i, j] = ObjWorkSheet.Cells[i + 1, j + 1].Text.ToString();
                    }
                }

                ObjWorkBook.Close(false, Type.Missing, Type.Missing);
                ObjWorkExcel.Quit(); 
                GC.Collect(); 
                return data;
            }
            return null;
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
        private void EmptyDictionaryChecker()
        {
            if (Dict.dictionaryList.Count == 0)
            {
                (Owner as EditorWindow).HomePage.PeriodIndicator.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#B5C1D3");
                TrashBin.IsEnabled = false;
                
            }
        }
        private void MakeIndicatorGreen()
        {
            (Owner as EditorWindow).HomePage.PeriodIndicator.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#A8D66D");
        }
    }
}

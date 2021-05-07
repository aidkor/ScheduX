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
using ScheduX.Resourses;
using ScheduX.Resourses.AppLogic;
using ScheduX.Resourses.UILogic;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;


namespace ScheduX.UI.Audiences
{
    /// <summary>
    /// Interaction logic for AudiencesWindow.xaml
    /// </summary>
    public partial class AudiencesWindow : Window
    {
        public NewAudienceWindow NewAudienceWindowInstance { get; set; }
        public SchoolAudienceDictionary Dict { get; set; }
        public AudiencesWindow()
        {
            InitializeComponent();
            Dict = new SchoolAudienceDictionary();
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
        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewAudienceWindowInstance = NewAudienceWindowInstance ?? new NewAudienceWindow();
            NewAudienceWindowInstance.Owner = this;
            NewAudienceWindowInstance.Add.Click -= NewAudienceWindowInstance.Done_Click;
            NewAudienceWindowInstance.Add.Click += NewAudienceWindowInstance.Add_Click;
            NewAudienceWindowInstance.Show();
        }
        private void ContextMenuEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (AudiencesList.SelectedItem != null & AudiencesList.SelectedItems.Count == 1)
            {
                NewAudienceWindowInstance.Add.Click -= NewAudienceWindowInstance.Add_Click;
                NewAudienceWindowInstance.Add.Click += NewAudienceWindowInstance.Done_Click;
                NewAudienceWindowInstance.Add.Content = "DONE";

                var currentElement = (SchoolAudience)AudiencesList.SelectedItem;
                NewAudienceWindowInstance.NameTextBox.Text = currentElement.Name;
                NewAudienceWindowInstance.AudienceTypeTextBox.Text = currentElement.AudienceType.ToString();
                NewAudienceWindowInstance.CapacityTextBox.Text = currentElement.Capacity.ToString();

                NewAudienceWindowInstance.Show();
            }
            else
            {
                MessageBox.Show("Choose an audience");
            }
        }
        private void ContextMenuCopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (AudiencesList.SelectedItems != null)
            {
                foreach (SchoolAudience item in AudiencesList.SelectedItems)
                {
                    var audience = new SchoolAudience(item.Name, item.AudienceType, item.Capacity);
                    Dict.dictionaryList.Add(audience);
                    AudiencesList.Items.Add(audience);
                }
            }
        }
        private void ContextMenuDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (AudiencesList.SelectedItems != null)
            {
                MessageBoxResult result = MessageBox.Show("Sure want to delete ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                    {
                        while (AudiencesList.SelectedItems.Count != 0)
                        {
                            Dict.dictionaryList.RemoveAll(period => period.GetHashCode() == AudiencesList.SelectedItems[0].GetHashCode());
                            AudiencesList.Items.RemoveAt(AudiencesList.Items.IndexOf(AudiencesList.SelectedItems[0]));
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
                ((GridViewColumnHeader)sender).Column.Width = 100;
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
                    int.TryParse(data[i, 2], out int capacity);

                    var audience = new SchoolAudience(data[i, 0], data[i, 1], capacity);
                    Dict.dictionaryList.Add(audience);
                    AudiencesList.Items.Add(audience);
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
                for (int i = 0; i < lastCell.Row; i++)
                {
                    for (int j = 0; j < lastCell.Column; j++)
                    {
                        data[i, j] = ObjWorkSheet.Cells[i + 1, j + 1].Text.ToString();
                    }
                }

                MakeIndicatorGreen();
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
                (Owner as EditorWindow).HomePage.AudiencesIndicator.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#B5C1D3");
            }
        }
        private void MakeIndicatorGreen()
        {
            (Owner as EditorWindow).HomePage.AudiencesIndicator.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#A8D66D");
        }
    }
}

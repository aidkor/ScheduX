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
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;

namespace ScheduX.UI.Classes
{
    /// <summary>
    /// Interaction logic for ClassesWindow.xaml
    /// </summary>
    public partial class GroupsWindow : Window
    {
        public NewGroupWindow NewGroupWindowInstance { get; set; }
        public GroupDictionary SchoolGroupDictionary { get; set; }
        public GroupsWindow()
        {
            InitializeComponent();
            SchoolGroupDictionary = new SchoolGroupDictionary();
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
            NewGroupWindowInstance = NewGroupWindowInstance ?? new NewGroupWindow();
            NewGroupWindowInstance.Owner = this;
            NewGroupWindowInstance.Add.Click -= NewGroupWindowInstance.Done_Click;
            NewGroupWindowInstance.Add.Click += NewGroupWindowInstance.Add_Click;
            NewGroupWindowInstance.Show();
        }
        private void ContextMenuEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsList.SelectedItem != null & GroupsList.SelectedItems.Count == 1)
            {
                NewGroupWindowInstance.Add.Click -= NewGroupWindowInstance.Add_Click;
                NewGroupWindowInstance.Add.Click += NewGroupWindowInstance.Done_Click;
                NewGroupWindowInstance.Add.Content = "DONE";

                var currentElement = (SchoolGroup)GroupsList.SelectedItem;
                NewGroupWindowInstance.NameTextBox.Text = currentElement.Name;
                NewGroupWindowInstance.StudentsQuantityTextBox.Text = currentElement.StudentQuantity.ToString();
                NewGroupWindowInstance.Show();
            }
            else
            {
                MessageBox.Show("Choose a group");
            }
        }
        private void ContextMenuCopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsList.SelectedItems != null)
            {
                foreach (SchoolGroup item in GroupsList.SelectedItems)
                {
                    var group = new SchoolGroup(item.Name, item.StudentQuantity);
                    SchoolGroupDictionary.dictionaryList.Add(group);
                    GroupsList.Items.Add(group);
                }
            }
        }
        private void ContextMenuDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsList.SelectedItems != null)
            {
                MessageBoxResult result = MessageBox.Show("Sure want to delete ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                    {
                        while (GroupsList.SelectedItems.Count != 0)
                        {
                            SchoolGroupDictionary.dictionaryList.RemoveAll(group => group.GetHashCode() == GroupsList.SelectedItems[0].GetHashCode());
                            GroupsList.Items.RemoveAt(GroupsList.Items.IndexOf(GroupsList.SelectedItems[0]));
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
                    int.TryParse(data[i, 1], out int studQuantity);

                    var group = new SchoolGroup(data[i, 0], studQuantity);
                    SchoolGroupDictionary.dictionaryList.Add(group);
                    GroupsList.Items.Add(group);
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
                Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1]; 
                var lastCell = ObjWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);

                string[,] data = new string[lastCell.Row, lastCell.Column];
                for (int i = 0; i < lastCell.Column; i++)
                {
                    for (int j = 0; j < lastCell.Row; j++)
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
            if (SchoolGroupDictionary.dictionaryList.Count == 0)
            {
                (Owner as EditorWindow).HomePage.GroupsIndicator.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#B5C1D3");
            }
        }
        private void MakeIndicatorGreen()
        {
            (Owner as EditorWindow).HomePage.GroupsIndicator.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#A8D66D");
        }
    }
}

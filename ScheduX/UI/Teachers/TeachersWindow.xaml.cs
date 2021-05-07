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

namespace ScheduX.UI.Teachers
{
    /// <summary>
    /// Interaction logic for TeachersWindow.xaml
    /// </summary>
    public partial class TeachersWindow : Window
    {
        public NewTeacherWindow NewTeacherWindowInstance { get; set; }
        public SchoolTeacherDictionary SchoolTeacherDictionary { get; set; }
        public TeachersWindow()
        {
            InitializeComponent();
            SchoolTeacherDictionary = new SchoolTeacherDictionary();
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
            NewTeacherWindowInstance = NewTeacherWindowInstance ?? new NewTeacherWindow();
            NewTeacherWindowInstance.Owner = this;
            NewTeacherWindowInstance.Add.Click -= NewTeacherWindowInstance.Done_Click;
            NewTeacherWindowInstance.Add.Click += NewTeacherWindowInstance.Add_Click;
            NewTeacherWindowInstance.Show();
        }
        private void ContextMenuEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeachersList.SelectedItem != null & TeachersList.SelectedItems.Count == 1)
            {
                NewTeacherWindowInstance.Add.Click -= NewTeacherWindowInstance.Add_Click;
                NewTeacherWindowInstance.Add.Click += NewTeacherWindowInstance.Done_Click;
                NewTeacherWindowInstance.Add.Content = "DONE";

                var currentElement = (SchoolTeacher)TeachersList.SelectedItem;
                NewTeacherWindowInstance.NameTextBox.Text = currentElement.Name;
                NewTeacherWindowInstance.PostTextBox.Text = currentElement.Post;
                NewTeacherWindowInstance.ExperienceTextBox.Text = currentElement.Experience.ToString();
                NewTeacherWindowInstance.AddressTextBox.Text = currentElement.Address;
                NewTeacherWindowInstance.TelephoneTextBox.Text = currentElement.Telephone;
                NewTeacherWindowInstance.Show();
            }
            else
            {
                MessageBox.Show("Choose a teacher");
            }
        }
        private void ContextMenuCopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeachersList.SelectedItems != null)
            {
                foreach (SchoolTeacher item in TeachersList.SelectedItems)
                {
                    var teacher = new SchoolTeacher(item.Name, item.Post, item.Experience, item.Address, item.Telephone);
                    SchoolTeacherDictionary.dictionaryList.Add(teacher);
                    TeachersList.Items.Add(teacher);
                }
            }
        }
        private void ContextMenuDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeachersList.SelectedItems != null)
            {
                MessageBoxResult result = MessageBox.Show("Sure want to delete ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                    {
                        while (TeachersList.SelectedItems.Count != 0)
                        {
                            SchoolTeacherDictionary.dictionaryList.RemoveAll(teacher => teacher.GetHashCode() == TeachersList.SelectedItems[0].GetHashCode());
                            TeachersList.Items.RemoveAt(TeachersList.Items.IndexOf(TeachersList.SelectedItems[0]));
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
                    int.TryParse(data[i, 2], out int exp);

                    var teacher = new SchoolTeacher(data[i, 0], data[i, 1], exp, data[i, 3], data[i, 4]);
                    SchoolTeacherDictionary.dictionaryList.Add(teacher);
                    TeachersList.Items.Add(teacher);
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
            if (SchoolTeacherDictionary.dictionaryList.Count == 0)
            {
                (Owner as EditorWindow).HomePage.TeachersIndicator.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#B5C1D3");
            }
        }
        private void MakeIndicatorGreen()
        {
            (Owner as EditorWindow).HomePage.TeachersIndicator.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#A8D66D");
        }
    }
}


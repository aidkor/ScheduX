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

namespace ScheduX.UI.Lessons
{
    /// <summary>
    /// Interaction logic for LessonsWindow.xaml
    /// </summary>
    public partial class LessonsWindow : Window
    {
        public NewLessonWindow NewLessonWindowInstance { get; set; }
        public SchoolLessonDictionary Dict { get; set; }
        public LessonsWindow()
        {
            InitializeComponent();
            Dict = new SchoolLessonDictionary();
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
            NewLessonWindowInstance = NewLessonWindowInstance ?? new NewLessonWindow();
            NewLessonWindowInstance.Owner = this;
            NewLessonWindowInstance.Add.Click -= NewLessonWindowInstance.Done_Click;
            NewLessonWindowInstance.Add.Click += NewLessonWindowInstance.Add_Click;
            NewLessonWindowInstance.Show();
        }
      /*  private void ImportExcel_Click(object sender, RoutedEventArgs e)
        {
            string[,] data = UploadExcelData();
            // HACK: Change Length Checker
            if (data?.GetLength(1) >= FindVisualChildren<GridViewColumnHeader>(this).Count() - 2)
            {
                for (int i = 0; i < data.GetLength(0); i++)
                {
                   

                    var lesson = new SchoolLesson();
                    Dict.dictionaryList.Add(lesson);
                    LessonsList.Items.Add(lesson);
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

                MakeIndicatorGreen();
                ObjWorkBook.Close(false, Type.Missing, Type.Missing);
                ObjWorkExcel.Quit();
                GC.Collect();
                return data;
            }
            return null;
        }*/
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
                //TrashBin.IsEnabled = false;

            }
        }
        private void MakeIndicatorGreen()
        {
            (Owner as EditorWindow).HomePage.LessonsIndicator.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#A8D66D");
        }
        private void ContextMenuEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (LessonsList.SelectedItem != null & LessonsList.SelectedItems.Count == 1)
            {
                NewLessonWindowInstance.Add.Click -= NewLessonWindowInstance.Add_Click;
                NewLessonWindowInstance.Add.Click += NewLessonWindowInstance.Done_Click;
                NewLessonWindowInstance.Add.Content = "DONE";

                var currentElement = LessonsList.SelectedItem as SchoolLesson;
                NewLessonWindowInstance.NameTextBox.Text = currentElement.Name;
                NewLessonWindowInstance.SubjectComboBox.SelectedItem = currentElement.Subject;
                NewLessonWindowInstance.AudienceComboBox.SelectedItem = currentElement.Audience;
                NewLessonWindowInstance.TeacherComboBox.SelectedItem = currentElement.Teacher;
                NewLessonWindowInstance.GroupComboBox.SelectedItem = currentElement.Group;

                NewLessonWindowInstance.Show();
            }
            else
            {
                MessageBox.Show("Choose a lesson");
            }
        }
        private void ContextMenuCopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (LessonsList.SelectedItems != null)
            {
                foreach (SchoolLesson item in LessonsList.SelectedItems)
                {
                    var lesson = new SchoolLesson(item.Name, item.Subject, item.Teacher, item.Audience, item.Group);
                    Dict.dictionaryList.Add(lesson);
                    LessonsList.Items.Add(lesson);
                }
            }
        }
        private void ContextMenuDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (LessonsList.SelectedItems != null)
            {
                MessageBoxResult result = MessageBox.Show("Sure want to delete ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                    {
                        while (LessonsList.SelectedItems.Count != 0)
                        {
                            Dict.dictionaryList.RemoveAll(group => group.GetHashCode() == LessonsList.SelectedItems[0].GetHashCode());
                            LessonsList.Items.RemoveAt(LessonsList.Items.IndexOf(LessonsList.SelectedItems[0]));
                        }
                        EmptyDictionaryChecker();
                        break;
                    }

                    case MessageBoxResult.No:
                        break;
                }
            }
        }
    }
}

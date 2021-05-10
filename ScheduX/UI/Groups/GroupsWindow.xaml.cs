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
        public SchoolGroupDictionary Dict { get; set; }
        public GroupsWindow()
        {
            InitializeComponent();
            Dict = new SchoolGroupDictionary();
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

                var currentElement = GroupsList.SelectedItem as SchoolGroup;
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
                    Dict.dictionaryList.Add(group);
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
                            Dict.dictionaryList.RemoveAll(group => group.GetHashCode() == GroupsList.SelectedItems[0].GetHashCode());
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
            string[,] data = ExcelFileTools.UploadExcelData();
            if (data?.GetLength(1) >= UITools.FindVisualChildren<GridViewColumnHeader>(this).Count() - 2)
            {
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    int.TryParse(data[i, 1], out int studQuantity);

                    var group = new SchoolGroup(data[i, 0], studQuantity);
                    Dict.dictionaryList.Add(group);
                    GroupsList.Items.Add(group);
                }
            }
            else
            {
                MessageBox.Show("Wrong Column Data");
            }
        }            
        private void EmptyDictionaryChecker()
        {
            if (Dict.dictionaryList.Count == 0)
            {
                (Owner as EditorWindow).HomePage.GroupsIndicator.Fill = UITools.GetGreyColor();
            }
        }
    }
}

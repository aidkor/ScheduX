﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ScheduX.App_Logic;

namespace ScheduX.UI.Classes
{
    /// <summary>
    /// Interaction logic for ClassesWindow.xaml
    /// </summary>
    public partial class GroupsWindow : Window
    {
        public NewGroupWindow NewGroupWindowInstance { get; set; }
        public SchoolGroupDictionary SchoolGroupDict { get; set; }
        public GroupsWindow()
        {
            InitializeComponent();
            SchoolGroupDict = new SchoolGroupDictionary();
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
                MessageBox.Show("Choose one group");
            }
        }
        private void ContextMenuCopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsList.SelectedItems != null)
            {
                foreach (SchoolGroup item in GroupsList.SelectedItems)
                {
                    var group = new SchoolGroup(item.Name, item.StudentQuantity);
                    SchoolGroupDict.dictionaryList.Add(group);
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
                            SchoolGroupDict.dictionaryList.RemoveAll(group => group.GetHashCode() == GroupsList.SelectedItems[0].GetHashCode());
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
                    int.TryParse(data[i, 1], out int studQuantity);

                    var group = new SchoolGroup(data[i, 0], studQuantity);
                    SchoolGroupDict.dictionaryList.Add(group);
                    GroupsList.Items.Add(group);
                }
                (Owner as EditorWindow).HomePage.GroupsIndicator.Fill = ColorPalette.GetPredefinedColor(PredefinedColors.Green);
            }
            else if (data?.GetLength(1) < ElementsModification.FindVisualChildren<GridViewColumnHeader>(this).Count() - 2)
            {
                MessageBox.Show("Wrong Columns Format");
            }
        }
        private void EmptyDictionaryChecker()
        {
            if (SchoolGroupDict.dictionaryList.Count == 0)
            {
                (Owner as EditorWindow).HomePage.GroupsIndicator.Fill = ColorPalette.GetPredefinedColor(PredefinedColors.Grey);
            }
        }
        private void TrashBin_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Sure want to delete ALL ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            switch (result)
            {
                case MessageBoxResult.Yes:
                {
                    SchoolGroupDict.dictionaryList.Clear();
                    GroupsList.Items.Clear();
                    EmptyDictionaryChecker();
                    break;
                }
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
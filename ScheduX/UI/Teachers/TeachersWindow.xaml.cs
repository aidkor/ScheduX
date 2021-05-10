﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ScheduX.Resourses.AppLogic;
using ScheduX.Resourses.UILogic;

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
            UITools.RemoveIcon(this);
        }
        private void OnClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            UITools.HideChildWindow(this);
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

                var currentElement = TeachersList.SelectedItem as SchoolTeacher;
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
        private void EmptyDictionaryChecker()
        {
            if (SchoolTeacherDictionary.dictionaryList.Count == 0)
            {
                (Owner as EditorWindow).HomePage.TeachersIndicator.Fill = UITools.GetGreyColor();
            }
        }     
    }
}


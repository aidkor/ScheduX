﻿using System;
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
using ScheduX.Resourses.UILogic;
using ScheduX.Resourses.AppLogic;

namespace ScheduX.UI.Teachers
{
    /// <summary>
    /// Interaction logic for NewTeacherWindow.xaml
    /// </summary>
    public partial class NewTeacherWindow : Window
    {
        public NewTeacherWindow()
        {
            InitializeComponent();
        }
        private void ResetControls()
        {
            this.Visibility = Visibility.Hidden;
            foreach (TextBox item in FindVisualChildren<TextBox>(this))
            {
                item.Text = null;
            }
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
        private bool IsWrongTextBoxValue()
        {
            bool flag = false;
            foreach (TextBox item in FindVisualChildren<TextBox>(this))
            {
                if (item.Text == "")
                {
                    item.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
                    flag = true;
                }
                if (!uint.TryParse(item.Text, out uint _) & item.Name == "ExperienceTextBox")
                {
                    item.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
                    flag = true;
                }
            }
            return flag;
        }
        public void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var OwnerWindowInstance = (TeachersWindow)this.Owner;
                TeacherElement teacher = new SchoolTeacher(NameTextBox.Text,PostTextBox.Text,int.Parse(ExperienceTextBox.Text),AddressTextBox.Text,TelephoneTextBox.Text);
                OwnerWindowInstance.SchoolTeacherDictionary.dictionaryList.Add(teacher);
                OwnerWindowInstance.TeachersList.Items.Add(teacher);
                ((Owner as TeachersWindow).Owner as EditorWindow).HomePage.TeachersIndicator.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#A8D66D");
                ResetControls();
            }
        }
        public void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var ownerWindowInstance = (TeachersWindow)this.Owner;
                var teacher = (SchoolTeacher)ownerWindowInstance.SchoolTeacherDictionary.dictionaryList.Find(item => item.GetHashCode() == ownerWindowInstance.TeachersList.SelectedItem.GetHashCode());

                teacher.Name = NameTextBox.Text;
                teacher.Post = PostTextBox.Text;
                teacher.Experience = int.Parse(ExperienceTextBox.Text);
                teacher.Address = AddressTextBox.Text;
                teacher.Telephone = TelephoneTextBox.Text;

                int index = ownerWindowInstance.TeachersList.Items.IndexOf(ownerWindowInstance.TeachersList.SelectedItem);
                ownerWindowInstance.TeachersList.Items.Remove(ownerWindowInstance.TeachersList.SelectedItem);
                ownerWindowInstance.TeachersList.Items.Insert(index, teacher);

                ResetControls();
            }
        }
        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#595959");
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }
        private void OnClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ResetControls();
        }
    }
}

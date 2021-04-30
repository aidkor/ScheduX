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


namespace ScheduX.UI.Teachers
{
    /// <summary>
    /// Interaction logic for TeachersWindow.xaml
    /// </summary>
    public partial class TeachersWindow : Window
    {
        public NewTeacherWindow NewTeacherWindowInstance { get; set; }
        public TeacherDictionary SchoolTeacherDictionary { get; set; }
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
    }
}


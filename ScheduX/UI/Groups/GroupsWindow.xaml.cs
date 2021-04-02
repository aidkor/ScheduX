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
                NewGroupWindowInstance.MaxDayLoadTextBox.Text = currentElement.MaxDayLoad.ToString();
                NewGroupWindowInstance.MaxLessonsPerDayTextBox.Text = currentElement.MaxLessonsPerDay.ToString();

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
                    var group = new SchoolGroup(item.Name, item.StudentQuantity, item.MaxDayLoad, item.MaxLessonsPerDay);
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
                        break;
                    }

                    case MessageBoxResult.No:
                        break;
                }
            }
        }
    }
}

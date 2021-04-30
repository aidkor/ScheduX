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

namespace ScheduX.UI.Lessons
{
    /// <summary>
    /// Interaction logic for LessonsWindow.xaml
    /// </summary>
    public partial class LessonsWindow : Window
    {
        public NewLessonWindow NewLessonWindowInstance { get; set; }
        public LessonDictionary SchoolLessonDictionary { get; set; }
        public LessonsWindow()
        {
            InitializeComponent();
            SchoolLessonDictionary = new SchoolLessonDictionary();
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
            //NewLessonWindowInstance.Add.Click -= NewLessonWindowInstance.Done_Click;
           // NewLessonWindowInstance.Add.Click += NewLessonWindowInstance.Add_Click;
            NewLessonWindowInstance.Show();
        }
       /* private void ContextMenuEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (LessonsList.SelectedItem != null & LessonsList.SelectedItems.Count == 1)
            {
                NewLessonWindowInstance.Add.Click -= NewLessonWindowInstance.Add_Click;
                NewLessonWindowInstance.Add.Click += NewLessonWindowInstance.Done_Click;
                NewLessonWindowInstance.Add.Content = "DONE";

                var currentElement = (SchoolLesson)LessonsList.SelectedItem;
                NewLessonWindowInstance.NameTextBox.Text = currentElement.Name;
                NewLessonWindowInstance..Text = currentElement.StudentQuantity.ToString();
                NewLessonWindowInstance.MaxDayLoadTextBox.Text = currentElement.MaxDayLoad.ToString();
                NewLessonWindowInstance.MaxLessonsPerDayTextBox.Text = currentElement.MaxLessonsPerDay.ToString();

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
                    var lesson = new SchoolLesson(item.Name, item.StudentQuantity, item.MaxDayLoad, item.MaxLessonsPerDay);
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
        }*/
    }
}

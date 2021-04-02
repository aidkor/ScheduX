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

namespace ScheduX.UI.TimetableCalls
{
    /// <summary>
    /// Interaction logic for TimetableCallsWindow.xaml
    /// </summary>
    public partial class TimetableCallsWindow : Window
    {
        public NewTimetableCallsWindow NewTimetableCallsWindowInstance { get; set; }
        public TimetableCallsDictionary SchoolTimetableCallsDictionary { get; set; }
        public TimetableCallsWindow()
        {
            InitializeComponent();
            SchoolTimetableCallsDictionary = new SchoolTimetableCallsDictionary();
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
        private void Select_Click(object sender, RoutedEventArgs e)
        {
            if (TimetableCallsList.SelectedItem != null & TimetableCallsList.SelectedItems.Count == 1)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Choose a timetable");
            }
        }
        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewTimetableCallsWindowInstance = NewTimetableCallsWindowInstance ?? new NewTimetableCallsWindow();
            NewTimetableCallsWindowInstance.Owner = this;
            NewTimetableCallsWindowInstance.Add.Click -= NewTimetableCallsWindowInstance .Done_Click;
            NewTimetableCallsWindowInstance.Add.Click += NewTimetableCallsWindowInstance .Add_Click;
            NewTimetableCallsWindowInstance.Show();
        }
        private void ColumnSizeHandler(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width <= 100)
            {
                e.Handled = true;
                ((GridViewColumnHeader)sender).Column.Width = 100;
            }
        }
        private void ContextMenuSelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (TimetableCallsList.SelectedItem != null & TimetableCallsList.SelectedItems.Count == 1)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Choose a timetable");
            }
        }
        private void ContextMenuEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (TimetableCallsList.SelectedItem != null & TimetableCallsList.SelectedItems.Count == 1)
            {
                NewTimetableCallsWindowInstance.Add.Click -= NewTimetableCallsWindowInstance.Add_Click;
                NewTimetableCallsWindowInstance.Add.Click += NewTimetableCallsWindowInstance.Done_Click;
                NewTimetableCallsWindowInstance.Add.Content = "DONE";

                var currentElement = (SchoolTimetableCalls)TimetableCallsList.SelectedItem;
                NewTimetableCallsWindowInstance.NameTextBox.Text = currentElement.Name;
                NewTimetableCallsWindowInstance.WorkingDaysTextBox.Text = currentElement.WorkingDays.ToString();
                NewTimetableCallsWindowInstance.LessonsPerDayTextBox.Text = currentElement.LessonsPerDay.ToString();                
                
                NewTimetableCallsWindowInstance.Show();
            }
            else
            {
                MessageBox.Show("Choose one period");
            }
        }
        private void ContextMenuCopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (TimetableCallsList.SelectedItems != null)
            {
                foreach (SchoolTimetableCalls item in TimetableCallsList.SelectedItems)
                {
                    var timetableCalls = new SchoolTimetableCalls(item.Name, item.WorkingDays, item.LessonsPerDay);
                    SchoolTimetableCallsDictionary.dictionaryList.Add(timetableCalls);
                    TimetableCallsList.Items.Add(timetableCalls);
                }
            }
        }
        private void ContextMenuDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (TimetableCallsList.SelectedItems != null)
            {
                MessageBoxResult result = MessageBox.Show("Sure want to delete ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                    {
                        while (TimetableCallsList.SelectedItems.Count != 0)
                        {
                            SchoolTimetableCallsDictionary.dictionaryList.RemoveAll(timetableCalls => timetableCalls.GetHashCode() == TimetableCallsList.SelectedItems[0].GetHashCode());
                            TimetableCallsList.Items.RemoveAt(TimetableCallsList.Items.IndexOf(TimetableCallsList.SelectedItems[0]));
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

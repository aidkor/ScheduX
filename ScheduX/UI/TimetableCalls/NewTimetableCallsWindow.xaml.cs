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
    /// Interaction logic for NewTimetableCallsWindow.xaml
    /// </summary>
    public partial class NewTimetableCallsWindow : Window
    {
        public NewTimetableCallsWindow()
        {
            InitializeComponent();
        }
        public void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var OwnerWindowInstance = (TimetableCallsWindow)this.Owner;
                TimetableCallsElement timetableCalls = new SchoolTimetableCalls(NameTextBox.Text, int.Parse(WorkingDaysTextBox.Text), int.Parse(LessonsPerDayTextBox.Text));
                OwnerWindowInstance.SchoolTimetableCallsDictionary.dictionaryList.Add(timetableCalls);
                OwnerWindowInstance.TimetableCallsList.Items.Add(timetableCalls);
                ResetControls();
            }
        }
        public void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var ownerWindowInstance = (TimetableCallsWindow)this.Owner;
                var timetableCalls = (SchoolTimetableCalls)ownerWindowInstance.SchoolTimetableCallsDictionary.dictionaryList.Find(item => item.GetHashCode() == ownerWindowInstance.TimetableCallsList.SelectedItem.GetHashCode());

                timetableCalls.Name = NameTextBox.Text;
                timetableCalls.WorkingDays = int.Parse(WorkingDaysTextBox.Text);
                timetableCalls.LessonsPerDay = int.Parse(LessonsPerDayTextBox.Text);
                

                int index = ownerWindowInstance.TimetableCallsList.Items.IndexOf(ownerWindowInstance.TimetableCallsList.SelectedItem);
                ownerWindowInstance.TimetableCallsList.Items.Remove(ownerWindowInstance.TimetableCallsList.SelectedItem);
                ownerWindowInstance.TimetableCallsList.Items.Insert(index, timetableCalls);

                ResetControls();
            }
        }
        private bool IsWrongTextBoxValue()
        {
            bool flag = false;
            foreach (TextBox item in FindVisualChildren<TextBox>(this))
            {
                if (item.Name == "NameTextBox")
                {
                    if (item.Text == "")
                    {
                        item.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
                        flag = true;
                    }
                }
                else if (item.Text == "" || !uint.TryParse(item.Text, out uint _))
                {
                    item.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
                    flag = true;
                }
            }           
            return flag;
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
        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#595959");
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

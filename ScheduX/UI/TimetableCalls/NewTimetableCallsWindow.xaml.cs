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
using ScheduX.Resourses;
using ScheduX.Resourses.AppLogic;

namespace ScheduX.UI.TimetableCalls
{
    /// <summary>
    /// Interaction logic for NewTimetableCalls.xaml
    /// </summary>
    public partial class NewTimetableCallsWindow : Window
    {
        public CallScheduleDictionary CallScheduleDictionary { get; protected set; }
        public NewTimetableCallsWindow()
        {
            InitializeComponent();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckFieldIncorrectness())
            {
                byte.TryParse(LessonsPerDayTextBox.Text, out byte lessonsPerDay);
                byte.TryParse(WorkingDaysTextBox.Text, out byte workingDays);

                SchoolCallScheduleElement callSchedule = new SchoolCallScheduleElement(lessonsPerDay,workingDays);
                CallScheduleDictionary.dictionaryList.Add(callSchedule);
                foreach (ListView item in FindVisualChildren<ListView>(this.Owner))
                {
                    item.Items.Add(callSchedule);
                }
                ResetControls();
            }
        }
        private bool CheckFieldIncorrectness()
        {
            bool flag = false;
            foreach (TextBox item in FindVisualChildren<TextBox>(this))
            {
                if (item.Text == "" || !uint.TryParse(item.Text, out uint res))
                {
                    item.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
                    flag = true;
                }
            }
            return flag;
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
    }
}

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
using ScheduX.Resourses.UILogic;
using ScheduX.Resourses.AppLogic;

namespace ScheduX.UI.PeriodOfStudy
{
    /// <summary>
    /// Interaction logic for NewPeriodWindow.xaml
    /// </summary>
    public partial class NewPeriodWindow : Window
    {
        public NewPeriodWindow()
        {
            InitializeComponent();
        }
        public void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongValue())
            {
                var OwnerWindowInstance = (PeriodOfStudyWindow)this.Owner;
                var period = new SchoolPeriod(NameTextBox.Text, DatePicker_1.SelectedDate.Value, DatePicker_2.SelectedDate.Value);
                OwnerWindowInstance.Dict.dictionaryList.Add(period);
                OwnerWindowInstance.PeriodsList.Items.Add(period);
                ResetControls();
            }
        }
        public void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongValue())
            {
                var ownerWindowInstance = (PeriodOfStudyWindow)Owner;
                var period = (SchoolPeriod)ownerWindowInstance.Dict.dictionaryList.Find(item => item.GetHashCode() == ownerWindowInstance.PeriodsList.SelectedItem.GetHashCode());

                period.Name = NameTextBox.Text;
                period.Start = DatePicker_1.SelectedDate.Value;
                period.End = DatePicker_2.SelectedDate.Value;

                int index = ownerWindowInstance.PeriodsList.Items.IndexOf(ownerWindowInstance.PeriodsList.SelectedItem);
                ownerWindowInstance.PeriodsList.Items.Remove(ownerWindowInstance.PeriodsList.SelectedItem);
                ownerWindowInstance.PeriodsList.Items.Insert(index, period);

                ResetControls();
            }
        }
        private bool IsWrongValue()
        {
            bool flag = false;
            if (NameTextBox.Text == "")
            {
                ColorInRed(NameTextBox);
                flag = true;
            }
            if (DatePicker_1.Text == "")
            {
                ColorInRed(DatePicker_1);
                flag = true;
            }
            if (DatePicker_2.Text == "")
            {
                ColorInRed(DatePicker_2);
                flag = true;
            }          
            if ((DatePicker_2.SelectedDate ?? new DateTime()) < (DatePicker_1.SelectedDate ?? new DateTime()))
            {
                InfoLabel.Content = "* Start date is more than end date";
                flag = true;
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
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }
        private void OnClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ResetControls();
        }
        private void ColorInRed(Control control)
        {
            control.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
        }
        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#595959");
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ((DatePicker)sender).BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#595959");
        }
    }
}

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
            if (!IsWrongTextBoxValue())
            {
                var OwnerWindowInstance = (PeriodOfStudyWindow)this.Owner;
                PeriodElement period = new SchoolPeriod(NameTextBox.Text, int.Parse(WeeksTextBox.Text), int.Parse(YearTextBox_1.Text), int.Parse(YearTextBox_2.Text));
                OwnerWindowInstance.SchoolStudyPeriodDictionary.dictionaryList.Add(period);
                OwnerWindowInstance.PeriodsList.Items.Add(period);                
                ResetControls();
            }
        }
        public void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var ownerWindowInstance = (PeriodOfStudyWindow)Owner;
                var period = (SchoolPeriod)ownerWindowInstance.SchoolStudyPeriodDictionary.dictionaryList.Find(item => item.GetHashCode() == ownerWindowInstance.PeriodsList.SelectedItem.GetHashCode());

                period.Name = NameTextBox.Text;
                period.WorkingWeeks = int.Parse(WeeksTextBox.Text);
                period.StartYear = int.Parse(YearTextBox_1.Text);
                period.EndYear = int.Parse(YearTextBox_2.Text);

                int index = ownerWindowInstance.PeriodsList.Items.IndexOf(ownerWindowInstance.PeriodsList.SelectedItem);
                ownerWindowInstance.PeriodsList.Items.Remove(ownerWindowInstance.PeriodsList.SelectedItem);
                ownerWindowInstance.PeriodsList.Items.Insert(index, period);

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

            uint.TryParse(YearTextBox_1.Text, out uint startYear);
            uint.TryParse(YearTextBox_2.Text, out uint endYear);
            if (startYear > endYear)
            {
                YearTextBox_1.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
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

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
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                PeriodElement period = new SchoolPeriodElement(NameTextBox.Text, uint.Parse(WeeksTextBox.Text), uint.Parse(YearTextBox_1.Text), uint.Parse(YearTextBox_2.Text));
                (Owner as PeriodOfStudyWindow)?.SchoolStudyPeriodDictionary.dictionaryList.Add(period);
                foreach (ListView item in FindVisualChildren<ListView>(this.Owner))
                {
                    item.Items.Add(period);
                }
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

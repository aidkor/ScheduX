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
        private StudyPeriodDictionary periods;
        public NewPeriodWindow()
        {
            InitializeComponent();
            periods = new StudyPeriodDictionary();

        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ResetControls();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckEmptyFields())
            {
                if (!uint.TryParse(WeeksTextBox.Text, out uint weeks))
                {
                    WeeksTextBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
                }
                if (!uint.TryParse(YearTextBox_1.Text, out uint starts))
                {
                    YearTextBox_1.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
                }
                if (!uint.TryParse(YearTextBox_2.Text, out uint ends))
                {
                    YearTextBox_2.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
                }
                else
                {
                    SchoolPeriod period = new SchoolPeriod(NameTextBox.Text, weeks, starts, ends);
                    periods.dictionaryList.Add(period);
                    foreach (ListView item in FindVisualChildren<ListView>(this.Owner))
                    {
                        item.Items.Add(period);
                    }
                    ResetControls();
                }
            }
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
        public bool CheckEmptyFields()
        {
            foreach (TextBox item in FindVisualChildren<TextBox>(this))
            {
                if (item.Text == "")
                {
                    return true;
                }
            }
            return false;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#595959");
        }
    }
}

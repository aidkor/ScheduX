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

namespace ScheduX.UI.Classes
{
    /// <summary>
    /// Interaction logic for NewClassWindow.xaml
    /// </summary>
    public partial class NewClassWindow : Window
    {
        public NewClassWindow()
        {
            InitializeComponent();
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
        private bool IsWrongTextBoxValue()
        {
            bool flag = false;
            foreach (TextBox item in FindVisualChildren<TextBox>(this))
            {
                if (item.Name == "MaxDayLoadTextBox")
                {
                    uint.TryParse(item.Text, out uint value);
                    if (value < 1 || value > 10)
                    {
                        item.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
                        flag = true;
                    }
                }
                if (item.Text == "")
                {
                    item.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
                    flag = true;
                }
                if (!uint.TryParse(item.Text, out uint _) & item.Name != "NameTextBox")
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
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                GroupElement group = new SchoolClass(NameTextBox.Text, uint.Parse(StudentsQuantityTextBox.Text), uint.Parse(MaxDayLoadTextBox.Text), uint.Parse(MaxLessonsPerDayTextBox.Text));
                (Owner as ClassesWindow).SchoolClassDictionary.dictionaryList?.Add(group);  
                foreach (ListView item in FindVisualChildren<ListView>(this.Owner))
                {
                    item.Items.Add(group);
                }
                ResetControls();
            }
        }
    }
}

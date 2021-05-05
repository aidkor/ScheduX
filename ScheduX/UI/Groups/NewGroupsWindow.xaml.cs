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
using ScheduX.Resourses.UILogic;

namespace ScheduX.UI.Classes
{
    /// <summary>
    /// Interaction logic for NewClassWindow.xaml
    /// </summary>
    public partial class NewGroupWindow : Window
    {
        public NewGroupWindow()
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
        public void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var OwnerWindowInstance = (GroupsWindow)this.Owner;
                GroupElement group = new SchoolGroup(NameTextBox.Text, int.Parse(StudentsQuantityTextBox.Text));
                OwnerWindowInstance.SchoolGroupDictionary.dictionaryList.Add(group);
                OwnerWindowInstance.GroupsList.Items.Add(group);
                ((Owner as GroupsWindow).Owner as EditorWindow).HomePage.GroupsIndicator.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#A8D66D");
                ResetControls();
            }
        }
        public void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var ownerWindowInstance = (GroupsWindow)this.Owner;
                var group = (SchoolGroup)ownerWindowInstance.SchoolGroupDictionary.dictionaryList.Find(item => item.GetHashCode() == ownerWindowInstance.GroupsList.SelectedItem.GetHashCode());

                group.Name = NameTextBox.Text;
                group.StudentQuantity = int.Parse(StudentsQuantityTextBox.Text);               

                int index = ownerWindowInstance.GroupsList.Items.IndexOf(ownerWindowInstance.GroupsList.SelectedItem);
                ownerWindowInstance.GroupsList.Items.Remove(ownerWindowInstance.GroupsList.SelectedItem);
                ownerWindowInstance.GroupsList.Items.Insert(index, group);

                ResetControls();
            }
        }
    }
}

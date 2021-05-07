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

namespace ScheduX.UI.Audiences
{
    /// <summary>
    /// Interaction logic for NewAudienceWindow.xaml
    /// </summary>
    public partial class NewAudienceWindow : Window
    {
        public NewAudienceWindow()
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
                if (!uint.TryParse(item.Text, out uint _) & item.Name != "NameTextBox" & item.Name != "AudienceTypeTextBox")
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
                var OwnerWindowInstance = (AudiencesWindow)this.Owner;
                var audience = new SchoolAudience(NameTextBox.Text, AudienceTypeTextBox.Text, int.Parse(CapacityTextBox.Text));
                OwnerWindowInstance.Dict.dictionaryList.Add(audience);
                OwnerWindowInstance.AudiencesList.Items.Add(audience);
                ((Owner as AudiencesWindow).Owner as EditorWindow).HomePage.AudiencesIndicator.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#A8D66D");
                ResetControls();
            }
        }
        public void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var ownerWindowInstance = (AudiencesWindow)this.Owner;
                var audience = (SchoolAudience)ownerWindowInstance.Dict.dictionaryList.Find(item => item.GetHashCode() == ownerWindowInstance.AudiencesList.SelectedItem.GetHashCode());

                audience.Name = NameTextBox.Text;
                audience.AudienceType = AudienceTypeTextBox.Text;
                audience.Capacity = int.Parse(CapacityTextBox.Text);                

                int index = ownerWindowInstance.AudiencesList.Items.IndexOf(ownerWindowInstance.AudiencesList.SelectedItem);
                ownerWindowInstance.AudiencesList.Items.Remove(ownerWindowInstance.AudiencesList.SelectedItem);
                ownerWindowInstance.AudiencesList.Items.Insert(index, audience);

                ResetControls();
            }
        }
    }
}

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
        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).BorderBrush = UITools.GetDarkGreyColor();
        }
        private bool IsWrongTextBoxValue()
        {
            bool flag = false;
            foreach (TextBox item in UITools.FindVisualChildren<TextBox>(this))
            {                
                if (item.Text == "")
                {
                    item.BorderBrush = UITools.GetGreyColor(); 
                    flag = true;
                }
                if (!uint.TryParse(item.Text, out uint _) & item.Name != "NameTextBox")
                {
                    item.BorderBrush = UITools.GetGreyColor();
                    flag = true;
                }
            }
            return flag;
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            UITools.RemoveIcon(this);
        }
        private void OnClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            UITools.ResetControlText<TextBlock>(this);
        }
        public void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var OwnerWindowInstance = this.Owner as GroupsWindow;
                var group = new SchoolGroup(NameTextBox.Text, int.Parse(StudentsQuantityTextBox.Text));
                OwnerWindowInstance.Dict.dictionaryList.Add(group);
                OwnerWindowInstance.GroupsList.Items.Add(group);
                ((Owner as GroupsWindow).Owner as EditorWindow).HomePage.GroupsIndicator.Fill = UITools.GetGreenColor();
                UITools.ResetControlText<TextBlock>(this);
            }
        }
        public void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var ownerWindowInstance = this.Owner as GroupsWindow;
                var group = ownerWindowInstance.Dict.dictionaryList.Find(item => item.GetHashCode() == ownerWindowInstance.GroupsList.SelectedItem.GetHashCode());

                group.Name = NameTextBox.Text;
                group.StudentQuantity = int.Parse(StudentsQuantityTextBox.Text);               

                int index = ownerWindowInstance.GroupsList.Items.IndexOf(ownerWindowInstance.GroupsList.SelectedItem);
                ownerWindowInstance.GroupsList.Items.Remove(ownerWindowInstance.GroupsList.SelectedItem);
                ownerWindowInstance.GroupsList.Items.Insert(index, group);

                UITools.ResetControlText<TextBlock>(this);
            }
        }
    }
}

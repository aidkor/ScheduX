using System;
using System.Windows;
using System.Windows.Controls;
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
                    item.BorderBrush = UITools.GetRedColor();
                    flag = true;
                }
                if (!uint.TryParse(item.Text, out uint _) & item.Name != "NameTextBox" & item.Name != "AudienceTypeTextBox")
                {
                    item.BorderBrush = UITools.GetRedColor();
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
                var OwnerWindowInstance = this.Owner as AudiencesWindow;
                var audience = new SchoolAudience(NameTextBox.Text, AudienceTypeTextBox.Text, int.Parse(CapacityTextBox.Text));
                OwnerWindowInstance.Dict.dictionaryList.Add(audience);
                OwnerWindowInstance.AudiencesList.Items.Add(audience);
                ((Owner as AudiencesWindow).Owner as EditorWindow).HomePage.AudiencesIndicator.Fill = UITools.GetGreenColor();
                UITools.ResetControlText<TextBlock>(this); 
            }
        }
        public void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var ownerWindowInstance = (AudiencesWindow)this.Owner;
                var audience = ownerWindowInstance.Dict.dictionaryList.Find(item => item.GetHashCode() == ownerWindowInstance.AudiencesList.SelectedItem.GetHashCode());

                audience.Name = NameTextBox.Text;
                audience.AudienceType = AudienceTypeTextBox.Text;
                audience.Capacity = int.Parse(CapacityTextBox.Text);                

                int index = ownerWindowInstance.AudiencesList.Items.IndexOf(ownerWindowInstance.AudiencesList.SelectedItem);
                ownerWindowInstance.AudiencesList.Items.Remove(ownerWindowInstance.AudiencesList.SelectedItem);
                ownerWindowInstance.AudiencesList.Items.Insert(index, audience);

                UITools.ResetControlText<TextBlock>(this);
            }
        }
    }
}

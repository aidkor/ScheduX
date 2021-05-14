using System;
using System.Windows;
using System.Windows.Controls;
using ScheduX.App_Logic;

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
            (sender as TextBox).BorderBrush = ColorPalette.GetPredefinedColor(PredefinedColors.DarkGrey);
        }
        private bool IsWrongTextBoxValue()
        {
            bool flag = false;
            foreach (TextBox item in ElementsModification.FindVisualChildren<TextBox>(this))
            {               
                if (item.Text == "")
                {
                    item.BorderBrush = ColorPalette.GetPredefinedColor(PredefinedColors.Blood);
                    flag = true;
                }
                if (!uint.TryParse(item.Text, out uint _) & item.Name != "NameTextBox" & item.Name != "AudienceTypeTextBox")
                {
                    item.BorderBrush = ColorPalette.GetPredefinedColor(PredefinedColors.Blood);
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
            ElementsModification.ResetControlText<TextBox>(this);
        }
        public void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var OwnerWindowInstance = this.Owner as AudiencesWindow;
                var audience = new SchoolAudience(NameTextBox.Text, AudienceTypeTextBox.Text, int.Parse(CapacityTextBox.Text));
                OwnerWindowInstance.SchoolAudienceDict.dictionaryList.Add(audience);
                OwnerWindowInstance.AudiencesList.Items.Add(audience);
                ((Owner as AudiencesWindow).Owner as EditorWindow).HomePage.AudiencesIndicator.Fill = ColorPalette.GetPredefinedColor(PredefinedColors.Green);
                ElementsModification.ResetControlText<TextBox>(this); 
            }
        }
        public void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!IsWrongTextBoxValue())
            {
                var ownerWindowInstance = (AudiencesWindow)this.Owner;
                var audience = ownerWindowInstance.SchoolAudienceDict.dictionaryList.Find(item => item.GetHashCode() == ownerWindowInstance.AudiencesList.SelectedItem.GetHashCode());

                audience.Name = NameTextBox.Text;
                audience.AudienceType = AudienceTypeTextBox.Text;
                audience.Capacity = int.Parse(CapacityTextBox.Text);                

                int index = ownerWindowInstance.AudiencesList.Items.IndexOf(ownerWindowInstance.AudiencesList.SelectedItem);
                ownerWindowInstance.AudiencesList.Items.Remove(ownerWindowInstance.AudiencesList.SelectedItem);
                ownerWindowInstance.AudiencesList.Items.Insert(index, audience);

                ElementsModification.ResetControlText<TextBox>(this);
            }
        }
    }
}

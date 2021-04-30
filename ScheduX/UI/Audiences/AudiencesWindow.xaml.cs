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

namespace ScheduX.UI.Audiences
{
    /// <summary>
    /// Interaction logic for AudiencesWindow.xaml
    /// </summary>
    public partial class AudiencesWindow : Window
    {
        public NewAudienceWindow NewAudienceWindowInstance { get; set; }
        public AudienceDictionary SchoolAudienceDictionary { get; set; }
        public AudiencesWindow()
        {
            InitializeComponent();
            SchoolAudienceDictionary = new SchoolAudienceDictionary();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }
        private void OnClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            for (int i = 0; i < this.OwnedWindows.Count; i++)
            {
                this.OwnedWindows[i].Visibility = Visibility.Hidden;
            }
            this.Visibility = Visibility.Hidden;
        }
        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewAudienceWindowInstance = NewAudienceWindowInstance ?? new NewAudienceWindow();
            NewAudienceWindowInstance.Owner = this;
            NewAudienceWindowInstance.Add.Click -= NewAudienceWindowInstance.Done_Click;
            NewAudienceWindowInstance.Add.Click += NewAudienceWindowInstance.Add_Click;
            NewAudienceWindowInstance.Show();
        }
        private void ContextMenuEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (AudiencesList.SelectedItem != null & AudiencesList.SelectedItems.Count == 1)
            {
                NewAudienceWindowInstance.Add.Click -= NewAudienceWindowInstance.Add_Click;
                NewAudienceWindowInstance.Add.Click += NewAudienceWindowInstance.Done_Click;
                NewAudienceWindowInstance.Add.Content = "DONE";

                var currentElement = (SchoolAudience)AudiencesList.SelectedItem;
                NewAudienceWindowInstance.NameTextBox.Text = currentElement.Name;
                NewAudienceWindowInstance.AudienceTypeTextBox.Text = currentElement.AudienceType.ToString();
                NewAudienceWindowInstance.CapacityTextBox.Text = currentElement.Capacity.ToString();

                NewAudienceWindowInstance.Show();
            }
            else
            {
                MessageBox.Show("Choose an audience");
            }
        }
        private void ContextMenuCopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (AudiencesList.SelectedItems != null)
            {
                foreach (SchoolAudience item in AudiencesList.SelectedItems)
                {
                    var audience = new SchoolAudience(item.Name, item.AudienceType, item.Capacity);
                    SchoolAudienceDictionary.dictionaryList.Add(audience);
                    AudiencesList.Items.Add(audience);
                }
            }
        }
        private void ContextMenuDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (AudiencesList.SelectedItems != null)
            {
                MessageBoxResult result = MessageBox.Show("Sure want to delete ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                    {
                        while (AudiencesList.SelectedItems.Count != 0)
                        {
                            SchoolAudienceDictionary.dictionaryList.RemoveAll(period => period.GetHashCode() == AudiencesList.SelectedItems[0].GetHashCode());
                            AudiencesList.Items.RemoveAt(AudiencesList.Items.IndexOf(AudiencesList.SelectedItems[0]));
                        }
                        break;
                    }

                    case MessageBoxResult.No:
                        break;
                }
            }
        }
        private void ColumnSizeHandler(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width <= 100)
            {
                e.Handled = true;
                ((GridViewColumnHeader)sender).Column.Width = 100;
            }
        }
    }
}

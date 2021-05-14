using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ScheduX.App_Logic;

namespace ScheduX.UI.Audiences
{
    /// <summary>
    /// Interaction logic for AudiencesWindow.xaml
    /// </summary>
    public partial class AudiencesWindow : Window
    {
        public NewAudienceWindow NewAudienceWindowInstance { get; set; }
        public SchoolAudienceDictionary SchoolAudienceDict { get; set; }
        public AudiencesWindow()
        {
            InitializeComponent();
            SchoolAudienceDict = new SchoolAudienceDictionary();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }
        private void OnClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ElementsModification.HideChildWindow(this);
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
                    SchoolAudienceDict.dictionaryList.Add(audience);
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
                            SchoolAudienceDict.dictionaryList.RemoveAll(period => period.GetHashCode() == AudiencesList.SelectedItems[0].GetHashCode());
                            AudiencesList.Items.RemoveAt(AudiencesList.Items.IndexOf(AudiencesList.SelectedItems[0]));
                        }
                        EmptyDictionaryChecker();
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
                (sender as GridViewColumnHeader).Column.Width = 100;
            }
        }
        private void ImportExcel_Click(object sender, RoutedEventArgs e)
        {
            string[,] data = ExcelFileTools.UploadExcelData();
            // HACK: Change Length Checker
            if (data?.GetLength(1) >= ElementsModification.FindVisualChildren<GridViewColumnHeader>(this).Count() - 2)
            {
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    int.TryParse(data[i, 2], out int capacity);

                    var audience = new SchoolAudience(data[i, 0], data[i, 1], capacity);
                    SchoolAudienceDict.dictionaryList.Add(audience);
                    AudiencesList.Items.Add(audience);
                }
                (Owner as EditorWindow).HomePage.AudiencesIndicator.Fill = ColorPalette.GetPredefinedColor(PredefinedColors.Green);
            }
            else if (data?.GetLength(1) < ElementsModification.FindVisualChildren<GridViewColumnHeader>(this).Count() - 2)
            {
                MessageBox.Show("Wrong Columns Format");
            }
        }              
        private void EmptyDictionaryChecker()
        {
            if (SchoolAudienceDict.dictionaryList.Count == 0)
            {
                (Owner as EditorWindow).HomePage.AudiencesIndicator.Fill = ColorPalette.GetPredefinedColor(PredefinedColors.Grey);
            }
        }
        private void TrashBin_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Sure want to delete ALL ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            switch (result)
            {
                case MessageBoxResult.Yes:
                {
                    SchoolAudienceDict.dictionaryList.Clear();
                    AudiencesList.Items.Clear();
                    EmptyDictionaryChecker();
                    break;
                }
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}

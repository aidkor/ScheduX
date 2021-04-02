using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ScheduX.Resourses.UILogic;
using ScheduX.Resourses.AppLogic;
using ScheduX.UI.PeriodOfStudy;

namespace ScheduX.UI.PeriodOfStudy
{
    /// <summary>
    /// Interaction logic for PeriodOfStudy.xaml
    /// </summary>
    public partial class PeriodOfStudyWindow : Window
    {
        public NewPeriodWindow NewPeriodWindowInstance { get; set; }
        public StudyPeriodDictionary SchoolStudyPeriodDictionary { get; set; }
        public PeriodOfStudyWindow()
        {
            InitializeComponent();
            SchoolStudyPeriodDictionary = new SchoolStudyPeriodDictionary();
        }
        private void Select_Click(object sender, RoutedEventArgs e)
        {
            if (PeriodsList.SelectedItem != null & PeriodsList.SelectedItems.Count == 1)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Choose a period");
            }
        }
        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewPeriodWindowInstance = NewPeriodWindowInstance ?? new NewPeriodWindow();
            NewPeriodWindowInstance.Owner = this;
            NewPeriodWindowInstance.Add.Click -= NewPeriodWindowInstance.Done_Click;
            NewPeriodWindowInstance.Add.Click += NewPeriodWindowInstance.Add_Click;
            NewPeriodWindowInstance.Add.Content = "ADD";
            NewPeriodWindowInstance.Show();
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
        private void ColumnSizeHandler(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width <= 100)
            {
                e.Handled = true;
                ((GridViewColumnHeader)sender).Column.Width = 100;
            }
        }
        private void ContextMenuSelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (PeriodsList.SelectedItem != null & PeriodsList.SelectedItems.Count == 1)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Choose a period");
            }
        }
        private void ContextMenuEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (PeriodsList.SelectedItem != null & PeriodsList.SelectedItems.Count == 1)
            {
                NewPeriodWindowInstance.Add.Click -= NewPeriodWindowInstance.Add_Click;
                NewPeriodWindowInstance.Add.Click += NewPeriodWindowInstance.Done_Click;
                NewPeriodWindowInstance.Add.Content = "DONE";

                var currentElement = (SchoolPeriod)PeriodsList.SelectedItem;
                NewPeriodWindowInstance.NameTextBox.Text = currentElement.Name;
                NewPeriodWindowInstance.WeeksTextBox.Text = currentElement.WorkingWeeks.ToString();
                NewPeriodWindowInstance.YearTextBox_1.Text = currentElement.StartYear.ToString();
                NewPeriodWindowInstance.YearTextBox_2.Text = currentElement.EndYear.ToString();

                NewPeriodWindowInstance.Show();
            }
            else
            {
                MessageBox.Show("Choose one period");
            }
        }
        private void ContextMenuCopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (PeriodsList.SelectedItems != null)
            {
                foreach (SchoolPeriod item in PeriodsList.SelectedItems)
                {
                    var period = new SchoolPeriod(item.Name, item.WorkingWeeks, item.StartYear, item.EndYear);
                    SchoolStudyPeriodDictionary.dictionaryList.Add(period);
                    PeriodsList.Items.Add(period);
                }
            }
        }
        private void ContextMenuDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (PeriodsList.SelectedItems != null)
            {
                MessageBoxResult result = MessageBox.Show("Sure want to delete ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                    {
                        while (PeriodsList.SelectedItems.Count != 0)
                        {
                            SchoolStudyPeriodDictionary.dictionaryList.RemoveAll(period => period.GetHashCode() == PeriodsList.SelectedItems[0].GetHashCode());
                            PeriodsList.Items.RemoveAt(PeriodsList.Items.IndexOf(PeriodsList.SelectedItems[0]));
                        }
                        break;
                    }

                    case MessageBoxResult.No:
                        break;
                }
            }
        }
    }
}

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
using ScheduX.Resourses;
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
            if (PeriodsList.SelectedItem == null)
            {
                MessageBox.Show("Period is not selected");
            }
            else
            {
                this.Close();
            }
        }
        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewPeriodWindowInstance = NewPeriodWindowInstance ?? new NewPeriodWindow();
            NewPeriodWindowInstance.Owner = this;
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
            if (PeriodsList.SelectedItem == null)
            {
                MessageBox.Show("Period is not selected");
            }
            else
            {
                this.Close();
            }
        }

        private void EditMenuSelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (PeriodsList.SelectedItem != null)
            {
                
            }
            
        }

        private void CopyMenuSelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (PeriodsList.SelectedItems != null)
            {
                foreach (SchoolPeriodElement item in PeriodsList.SelectedItems)
                {
                    SchoolStudyPeriodDictionary.dictionaryList.Add(item);
                    PeriodsList.Items.Add(item);
                }
            }
        }
        private void DeleteMenuSelectButton_Click(object sender, RoutedEventArgs e)
        {
            // UNDONE: Delete Error
            if (PeriodsList.SelectedItems != null)
            {
                MessageBoxResult result = MessageBox.Show("Sure want to delete ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                    {                               
                        foreach (SchoolPeriodElement item in PeriodsList.SelectedItems)
                        {                
                            
                            MessageBox.Show(PeriodsList.SelectedItems.ToString());
                            SchoolStudyPeriodDictionary.dictionaryList.Remove(item);
                            PeriodsList.Items.Remove(item);
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

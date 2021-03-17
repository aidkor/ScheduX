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

namespace ScheduX.UI.PeriodOfStudy
{
    /// <summary>
    /// Interaction logic for PeriodOfStudy.xaml
    /// </summary>
    public partial class PeriodOfStudyWindow : Window
    {
        private NewPeriodWindow window;       
        public PeriodOfStudyWindow()
        {
            InitializeComponent();                                                    
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }
        private void Select_Click(object sender, RoutedEventArgs e)
        {

        }
        private void New_Click(object sender, RoutedEventArgs e)
        {
            window = window ?? new NewPeriodWindow();
            window.Owner = this;
            window.Show();
        }
        private void OnClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
            for (int i = 0; i < this.OwnedWindows.Count; i++)
            {
                this.OwnedWindows[i].Visibility = Visibility.Hidden;
            }
            Owner.Show();
        }
    }
}

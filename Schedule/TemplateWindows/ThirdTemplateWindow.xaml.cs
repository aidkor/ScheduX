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
using Schedule.Resourses;

namespace Schedule.TemplateWindows
{
    /// <summary>
    /// Interaction logic for ThirdTemplateWindow.xaml
    /// </summary>
    public partial class ThirdTemplateWindow : Window
    {
        public ThirdTemplateWindow()
        {
            InitializeComponent();
            // HACK: Change this in XAML code with data binding
            this.Height = SystemParameters.PrimaryScreenHeight / 2;
            this.Width = SystemParameters.PrimaryScreenWidth / 2.8;
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new SecondTemplateWindow().Show();
            Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FindLocationButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

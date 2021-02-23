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

namespace Schedule.TemplateWindows
{
    /// <summary>
    /// Interaction logic for FirstTemplateWindow.xaml
    /// </summary>
    public partial class FirstTemplateWindow : Window
    {
        public FirstTemplateWindow()
        {
            InitializeComponent();
            // HACK: Change this in XAML code with data binding
            this.Height = SystemParameters.PrimaryScreenHeight / 2;
            this.Width = SystemParameters.PrimaryScreenWidth / 5;
        }
        private void CreateNewProject(object sender, RoutedEventArgs e)
        {
            new SecondTemplateWindow().Show();
            Close();
        }

        private void OpenProject(object sender, RoutedEventArgs e)
        {

        }
        private void ProjectTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

     
    }
}

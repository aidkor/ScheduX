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
    /// Interaction logic for FirstTemplateWindow.xaml
    /// </summary>
    public partial class SecondTemplateWindow : Window
    {
        public SecondTemplateWindow()
        {
            InitializeComponent();
            // HACK: Change this in XAML code with data binding
            this.Height = SystemParameters.PrimaryScreenHeight / 2;
            this.Width = SystemParameters.PrimaryScreenWidth / 5;           
        }

        private void SchoolTemplate_Click(object sender, RoutedEventArgs e)
        {
            new ThirdTemplateWindow().Show();
            Close();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new FirstTemplateWindow().Show();
            Close();
        }
    }
}

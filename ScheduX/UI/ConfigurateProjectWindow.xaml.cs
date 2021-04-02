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
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using ScheduX.Resourses.UILogic;
using ScheduX.Resourses.AppLogic;

namespace ScheduX.UI
{
    public partial class ConfigurateProjectWindow : Window
    {
        private ProjectTemplate template;
        public ConfigurateProjectWindow()
        {
            InitializeComponent();

            // HACK: Change this in XAML code with data binding
           /* this.Height = SystemParameters.PrimaryScreenHeight / 2;
            this.Width = SystemParameters.PrimaryScreenWidth / 5;*/
        }
        // UNDONE: Change default file location 
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
            LocationTextBox.Text = @"C:\Users\Asus\Desktop\DataBase";
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Hide();
        }
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectNameTextBox.Text != "" & template != 0 & Directory.Exists(LocationTextBox.Text))
            {
                try
                {
                    CreateWithDelay();   
                }
                catch (Exception)
                {
                    InfoLabel.Content = "You already have file with this name. Try anouther one !";
                }

            }
            else if (ProjectNameTextBox.Text == "")
            {
                
            }
        }
        private void SearchPathButton_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    LocationTextBox.Text = fbd.SelectedPath;
                }
            }
        }
        private void SchoolTemplate_Selected(object sender, RoutedEventArgs e)
        {
            template = ProjectTemplate.SchoolTemplate;
        }
        public async void CreateWithDelay()
        {
            await Task.Delay(2500);            
            new EditorWindow(LocationTextBox.Text).Show();
            Owner.Close();
        }
    }
}

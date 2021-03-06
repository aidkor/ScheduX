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
using ScheduX.Resourses;


namespace ScheduX.UI
{
    enum ProjectTemplate : byte
    {
        Kindergarten = 0,
        SchoolTemplate = 1,
        UniversityTemplate = 2
    }
    /// <summary>
    /// Interaction logic for FirstTemplateWindow.xaml
    /// </summary>
    public partial class ConfigurateProjectWindow : Window
    {
        private ProjectTemplate template;
        public ConfigurateProjectWindow()
        {
            InitializeComponent();           

            // HACK: Change this in XAML code with data binding
            this.Height = SystemParameters.PrimaryScreenHeight / 2;
            this.Width = SystemParameters.PrimaryScreenWidth / 5;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new CreateOpenProjectWindow().Show();
            Close();
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
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectNameTextBox.Text != "" & Directory.Exists(LocationTextBox.Text) & IsTemplateSelected())
            {
                // TODO: Add logic for creating project folder/files

                new EditorWindow().Show();
                Close();
            }            

           
        }

        private void SchoolTemplate_Selected(object sender, RoutedEventArgs e)
        {
            template = ProjectTemplate.SchoolTemplate;
        }
        private bool IsTemplateSelected()
        {
            for (int i = 0; i < TemplateListBox.Items.Count; i++)
            {
                if (((ListBoxItem)TemplateListBox.Items[i]).IsSelected)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

using System;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Forms;
using ScheduX.App_Logic;
using System.Windows.Controls;
using System.Windows.Media;
using inputCursor = System.Windows.Input;

namespace ScheduX.UI
{
    public partial class ConfigurateProjectWindow : Window
    {
        private ProjectTemplate template;
        public ConfigurateProjectWindow()
        {
            InitializeComponent();            
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
                CreateWithDelay();
            }
            if (!Directory.Exists(LocationTextBox.Text))
            {
                LocationTextBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
            }
            if (ProjectNameTextBox.Text == "")
            {
                ProjectNameTextBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
            }
            if (template == 0)
            {
                TemplateListBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC82929");
            }
        }
        public async void CreateWithDelay()
        {
            this.Cursor = inputCursor.Cursors.Wait;
            await Task.Delay(2000);
            ShxFileTools.CreateShxFile(LocationTextBox.Text, ProjectNameTextBox.Text);
            new EditorWindow(LocationTextBox.Text).Show();
            Owner.Close();
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
            TemplateListBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#ABADB3");
            SchoolTemplate.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFF");
            template = ProjectTemplate.SchoolTemplate;
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);            
            LocationTextBox.Text = @"C:\";
        }
        private void ProjectNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProjectNameTextBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#ABADB3");
        }
        private void LocationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LocationTextBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#ABADB3");
        }
        private void OnClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Owner.Close();
        }
    }
}

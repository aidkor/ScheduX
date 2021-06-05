using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Win32;
using ScheduX.App_Logic;
using System.Windows.Controls;

namespace ScheduX.UI
{
    public partial class CreateOpenProjectWindow : Window
    {
        private IEnumerable<string> shxFiles;
        private ConfigurateProjectWindow window;
        public CreateOpenProjectWindow()
        {
            InitializeComponent();
            LoadShxFiles();
        }
        private void CreateNewProject(object sender, RoutedEventArgs e)
        {
            window = window ?? new ConfigurateProjectWindow();
            window.Owner = this;
            window.Show();
            this.Hide();
        }
        private void OpenProject(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "shx Files (*.shx)|*.shx";
            if (openFileDialog.ShowDialog() == true)
            {
                new EditorWindow(openFileDialog.FileName).Show();
                this.Close();
            }
        }
        private void LoadShxFiles()
        {
            // UNDONE: Add default folder path
            string projectsPath = $"{Properties.Settings.Default.ProjectsPath}";
            shxFiles = (Directory.Exists(projectsPath)) ? Directory.GetFiles(projectsPath, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".shx")) : null;            
            if (shxFiles != null)
            {
                foreach (string item in shxFiles)
                {
                    ProjectListBox.Items.Add(CreateListBoxItem(item));
                }
            }
        }
        private Button CreateListBoxItem(string filePath)
        {
            Image img;
            {
                img = new Image();
                img.Source = new BitmapImage(new Uri($@"{Properties.Settings.Default.ImagesPath}/file.png", UriKind.Relative));
                img.Width = 25;
                img.Margin = new Thickness(0, 0, 410, 0);
            }
            TextBlock[] textBlock;
            {
                textBlock = new TextBlock[] { new TextBlock(), new TextBlock() };                     

                textBlock[0].Margin = new Thickness(35, 0, 0, 12);
                textBlock[0].FontSize = 12;
                textBlock[0].Text = filePath.Split('\\')[filePath.Split('\\').Length - 1];

                textBlock[1].Margin = new Thickness(35, 0, 0, 0);
                textBlock[1].FontSize = 9;
                textBlock[1].Text = filePath;
                textBlock[1].Foreground = Brushes.Gray;
                textBlock[1].VerticalAlignment = VerticalAlignment.Bottom;
            }
            Grid grid;
            {
                grid = new Grid();
                grid.Children.Add(img);
                grid.Children.Add(textBlock[0]);
                grid.Children.Add(textBlock[1]);
            }
            Button button;
            {
                button = new Button();
                button.Height = 50;
                button.Foreground = Brushes.Black;
                button.Background = Brushes.AliceBlue;
                button.BorderThickness = new Thickness(0);
                button.Content = grid;
                button.Tag = $"{filePath}";
                button.Click += OpenProjectFromProjectListBoxByClick;
            }
            return button;
        }
        private void OpenProjectFromProjectListBoxByClick(object sender, RoutedEventArgs e)
        {
            new EditorWindow(((Button)sender).Tag.ToString()).Show();
            this.Close();
        }
        private void ProjectTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProjectListBox.Items.Clear();
            string fileName;
            if (shxFiles != null)
            {
                foreach (string item in shxFiles)
                {
                    fileName = item.Split('\\')[item.Split('\\').Length - 1].Split('.')[0].ToLower();
                    if (fileName.StartsWith(ProjectTextBox.Text.ToLower()))
                    {
                        ProjectListBox.Items.Add(CreateListBoxItem(item));
                    }
                }
            }
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }
    }
}

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
using Microsoft.Win32;
using ScheduX.Resourses;
using System.Collections;

namespace ScheduX.UI
{
    public partial class CreateOpenProjectWindow : Window
    {
        private ArrayList shxFiles = new ArrayList();
        private ConfigurateProjectWindow window;
        public CreateOpenProjectWindow()
        {
            InitializeComponent();
            //FillProjectListBox();
            // HACK: Rewrite in XAML code
            this.Height = SystemParameters.PrimaryScreenHeight / 2;
            this.Width = SystemParameters.PrimaryScreenWidth / 5;
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
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

        // UNDONE: Handling System For Projects ListBox
        /*   
        private void ProjectTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // UNDONE: Make sorting system
        }
        // TODO: Make as new stream
        private void OpenProjectFromProjectListBoxByClick(object sender, RoutedEventArgs e)
        {
            new EditorWindow(((Button)sender).Tag).Show();
            this.Close();
        }
        private void FillProjectListBox()
        {
            // UNDONE: Change project path for default
            string scheduxProjectDir = @"C:\Users\Asus\Desktop\DataBase";
            SHXfileSearchSystem(scheduxProjectDir, ref shxFiles);

            for (int i = 0; i < shxFiles.Count; i++)
            {
                Image image;
                {
                    image = new Image();
                    image.Source = new BitmapImage(new Uri(@"..\Images\file.png", UriKind.Relative));
                    image.Width = 25;
                    image.Margin = new Thickness(0, 0, 410, 0);
                }
                TextBlock[] textBlock;
                {
                    textBlock = new TextBlock[] { new TextBlock(), new TextBlock() };
                    string filePathName = shxFiles[i].ToString();

                    textBlock[0].Margin = new Thickness(35, 0, 0, 12);
                    textBlock[0].FontSize = 12;
                    textBlock[0].Text = filePathName.Split('\\')[filePathName.Split('\\').Length - 1];

                    textBlock[1].Margin = new Thickness(35, 0, 0, 0);
                    textBlock[1].FontSize = 9;
                    textBlock[1].VerticalAlignment = VerticalAlignment.Bottom;
                    textBlock[1].Foreground = Brushes.Gray;
                    textBlock[1].Text = filePathName;
                }
                Grid grid;
                {
                    grid = new Grid();
                    grid.Children.Add(image);
                    grid.Children.Add(textBlock[0]);
                    grid.Children.Add(textBlock[1]);
                }
                Button button;
                {
                    button = new Button();
                    button.Height = 40;
                    button.Width = 450;
                    button.Background = Brushes.White;
                    button.Foreground = Brushes.Black;
                    button.BorderThickness = new Thickness(0);
                    button.Content = grid;
                    button.Tag = $"{shxFiles[i]}";
                    button.Click += OpenProjectFromProjectListBoxByClick;
                }
                ProjectListBox.Items.Add(button);
            }
        }
        private void SHXfileSearchSystem(string dirPath, ref ArrayList files)
        {
            try
            {
                if (Directory.Exists(dirPath))
                {
                    foreach (string item in FindShxFileFromOther(Directory.GetFiles(dirPath)))
                    {
                        files.Add(item);
                    }
                    foreach (string item in Directory.GetDirectories(dirPath))
                    {
                        SHXfileSearchSystem(item, ref files);
                    }
                }
            }
            catch
            {
                return;
            }

        }
        private ArrayList FindShxFileFromOther(string[] files)
        {
            ArrayList result = new ArrayList();

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Split('.')[files[i].Split('.').Length - 1] == "shx")
                {
                    result.Add(files[i]);
                }
            }

            return result;
        }*/
    }
}

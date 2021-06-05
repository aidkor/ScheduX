using System.Windows;
using System.Windows.Controls;

namespace ScheduX.UI.Pages
{
    /// <summary>
    /// Interaction logic for Help.xaml
    /// </summary>
    public partial class Help : Page
    {
        public EditorWindow ParentWindowInstance { get; set; }
        public Help(EditorWindow instance)
        {
            InitializeComponent();
            ParentWindowInstance = instance;            
        }
        private void HomePageDocx_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start($@"{Properties.Settings.Default.doc}\HomePage.docx");
            }
            catch (System.Exception)
            {
                MessageBox.Show($"Can not open the file {Properties.Settings.Default.doc}\\HomePage.docx");
            }
            
        }

        private void SettingsPageDocx_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start($@"{Properties.Settings.Default.doc}\SettingsPage.docx");
            }
            catch (System.Exception)
            {
                MessageBox.Show($"Can not open the file {Properties.Settings.Default.doc}\\SettingsPage.docx");
            }
        }

        private void SchedulePageDocx_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start($@"{Properties.Settings.Default.doc}\SchedulePage.docx");
            }
            catch (System.Exception)
            {
                MessageBox.Show($"Can not open the file {Properties.Settings.Default.doc}\\SchedulePage.docx");
            }
        }

        private void GitHubDownload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start($@"{Properties.Settings.Default.doc}\GitHub.docx");
            }
            catch (System.Exception)
            {
                MessageBox.Show($"Can not open the file {Properties.Settings.Default.doc}\\GitHub.docx");
            }
        }

        private void StartWindow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start($@"{Properties.Settings.Default.doc}\StartWindow.docx");
            }
            catch (System.Exception)
            {
                MessageBox.Show($"Can not open the file {Properties.Settings.Default.doc}\\StartWindow.docx");
            }
        }
    }
}

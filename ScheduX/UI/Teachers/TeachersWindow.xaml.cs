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
using ScheduX.Resourses.AppLogic;
using ScheduX.Resourses;


namespace ScheduX.UI.Teachers
{
    /// <summary>
    /// Interaction logic for TeachersWindow.xaml
    /// </summary>
    public partial class TeachersWindow : Window
    {
        public NewTeacherWindow NewTeacherWindowInstance { get; set; }
        public TeacherDictionary SchoolTeacherDictionary { get; set; }
        public TeachersWindow()
        {
            InitializeComponent();
            SchoolTeacherDictionary = new SchoolTeacherDictionary();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }
        private void OnClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            for (int i = 0; i < this.OwnedWindows.Count; i++)
            {
                this.OwnedWindows[i].Visibility = Visibility.Hidden;
            }
            this.Visibility = Visibility.Hidden;
        }
        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewTeacherWindowInstance = NewTeacherWindowInstance ?? new NewTeacherWindow();
            NewTeacherWindowInstance.Owner = this;
            NewTeacherWindowInstance.Show();
        }
    }
}

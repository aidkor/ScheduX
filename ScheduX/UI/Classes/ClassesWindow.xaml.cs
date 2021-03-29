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

namespace ScheduX.UI.Classes
{
    /// <summary>
    /// Interaction logic for ClassesWindow.xaml
    /// </summary>
    public partial class ClassesWindow : Window
    {
        public NewClassWindow NewClassWindowInstance { get; set; }
        public GroupDictionary SchoolClassDictionary { get; set; }
        public ClassesWindow()
        {
            InitializeComponent();
            SchoolClassDictionary = new SchoolGroupDictionary();
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
            NewClassWindowInstance = NewClassWindowInstance ?? new NewClassWindow();
            NewClassWindowInstance.Owner = this;
            NewClassWindowInstance.Show();
        }
    }
}

using System.Threading.Tasks;
using System.Windows;

namespace ScheduX.UI
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {        
        public StartWindow()
        {
            InitializeComponent();        
            DelayVisualization();
        }
        private async void DelayVisualization()
        {          
            await Task.Delay(2500);
            new CreateOpenProjectWindow().Show();
            Close();
        }
    }
}

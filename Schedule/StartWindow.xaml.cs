using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Schedule
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
            Width = SystemParameters.PrimaryScreenWidth / 4;
            Height = SystemParameters.PrimaryScreenHeight / 4 + 40;            
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CloseWindow();            
        }

        private async void CloseWindow()
        {
            await ClosingTasks();
        }

        private async Task ClosingTasks()
        {
            await Task.Delay(2000);
            Close();
        }

    }
}

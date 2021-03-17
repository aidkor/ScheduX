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
            Height = SystemParameters.PrimaryScreenHeight / 3;            
            Width = SystemParameters.PrimaryScreenWidth / 3.5;
            DelayVisualization();
        }
        public async void DelayVisualization()
        {          
            await Task.Delay(2500);
            new CreateOpenProjectWindow().Show();
            Close();
        }
    }
}

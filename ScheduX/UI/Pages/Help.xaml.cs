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
    }
}

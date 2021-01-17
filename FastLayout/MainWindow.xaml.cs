using System.Windows;
using System.Windows.Input;

namespace FastLayout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel(
                new EditorPaneViewModel(),
                new PropertyPanelViewModel());

            InitializeComponent();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (DataContext as MainWindowViewModel).MouseDown();
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            (DataContext as MainWindowViewModel).MouseUp();
        }

        private void TextBlock_MouseMove(object sender, MouseEventArgs e)
        {
            (DataContext as MainWindowViewModel).MouseMove();
        }
    }
}

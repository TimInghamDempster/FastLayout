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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

using System.Windows;
using System.Windows.Controls;

namespace ModernComboBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            MessageBox.Show("Was clicked " + button.Content);
        }
    }
}

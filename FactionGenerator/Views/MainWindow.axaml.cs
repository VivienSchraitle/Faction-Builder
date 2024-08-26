using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia;


namespace FactionGenerator.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartFactionGenerationClick(object sender, RoutedEventArgs e)
        {
            var factionWindow = new FactionGeneratorWindow();
            factionWindow.Show();
            this.Close(); // Close the menu window
        }
    }
}

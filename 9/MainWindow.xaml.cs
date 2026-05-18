using Microsoft.UI.Xaml;

namespace pract9;

public sealed partial class MainWindow : Window
{
    public MainViewModel VM { get; }

    public MainWindow()
    {
        this.InitializeComponent();
        VM = App.VM;
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        VM.Add();
    }
}
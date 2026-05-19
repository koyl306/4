using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace pract10
{
    public sealed partial class AddExpPage : Page
    {
        public AddExpPage()
        {
            this.InitializeComponent();

            ViewModel = App.ViewModel;
        }

        public MainViewModel ViewModel { get; }

        private async void Button_Click(
            object sender,
            RoutedEventArgs e)
        {
            await ViewModel.SaveNewExperiment();
        }

        private void Button_Click_1(
            object sender,
            RoutedEventArgs e)
        {
            ViewModel.ClearNewExperiment();
        }
    }
}
using Microsoft.UI.Xaml.Controls;

namespace pract10
{
    public sealed partial class ExpPage : Page
    {
        public ExpPage()
        {
            this.InitializeComponent();

            ViewModel = App.ViewModel;
        }

        public MainViewModel ViewModel { get; }
    }
}
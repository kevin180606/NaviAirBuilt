using LinnerToolkit.Desktop.ModernUI.Windows.Controls;
using NaviAirBuilt.ViewModels;

namespace NaviAirBuilt.Views
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : NavigationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           ((MainPageViewModel)DataContext).StartCommand.Execute(null);
        }
    }
}

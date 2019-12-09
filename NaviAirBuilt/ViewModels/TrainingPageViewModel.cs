using FirstFloor.ModernUI.Windows.Navigation;
using LinnerToolkit.Desktop.ModernUI.Mvvm;
using LinnerToolkit.Desktop.ModernUI.Navigation;

namespace NaviAirBuilt.ViewModels
{
    public  class TrainingPageViewModel:ModernViewModelBase
    {
       // public ICommand GoBackCommand { get; }




        public TrainingPageViewModel(IModernNavigationService navigationService):base(navigationService)
        {
            //GoBackCommand = new RelayCommand(obj =>
            //{
            //    navigationService.NavigateTo("MainPage", null);
            //});
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
          
        }


        
    }
}

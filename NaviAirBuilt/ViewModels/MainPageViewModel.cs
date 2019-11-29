using FirstFloor.ModernUI.Presentation;
using LinnerToolkit.Desktop.ModernUI.Mvvm;
using LinnerToolkit.Desktop.ModernUI.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NaviAirBuilt.ViewModels
{
   public  class MainPageViewModel:ModernViewModelBase
    {
        public ICommand StartCommand { get; }

        public ICommand CloseCommand { get; }

        public MainPageViewModel(IModernNavigationService navigationService):base(navigationService)
        {
            StartCommand = new RelayCommand(obj =>
           {
               navigationService.NavigateTo("TrainingPage", null);
           });
            CloseCommand = new RelayCommand((obj) =>
            {
                System.Windows.Application.Current.Shutdown();
            });
        }

    }
}

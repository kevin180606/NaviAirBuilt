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
    }
}

using LinnerToolkit.Desktop.ModernUI.Windows;
using NaviAirBuilt.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NaviAirBuilt 
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : ModernApplication
    {
        public App()
        {
            
                RunApp("MainWindow/MainPage");
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
        protected override void RegisterTypes()
        {
            RegisterForNavigation<MainWindow>();
            RegisterForNavigation<MainPage>();
            RegisterForNavigation<TrainingPage>();
          
        }

    }
}

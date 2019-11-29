using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using LinnerToolkit.Desktop.ModernUI.Windows.Controls;
using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NaviAirBuilt.Views
{
    /// <summary>
    /// TrainingPage.xaml 的交互逻辑
    /// </summary>
    public partial class TrainingPage : NavigationPage
    {
     

        int actualWidth = 0, actualHeight = 0;
        public TrainingPage()
        {
            InitializeComponent();
            
            Loaded += TrainingPage_Loaded;
        }

        

        private void TrainingPage_Loaded(object sender, RoutedEventArgs e)
        {
      
        }

    

        private void AnimationButton_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void btnSerial_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTCP_Click(object sender, RoutedEventArgs e)
        {

        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
          
        }
    }

    public class RaiseHandToImageSource : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool raiseHand = (bool)value;
            return new BitmapImage(new Uri(raiseHand ? @"/Assets/Image/Training/RaiseHand_1.png" :
                @"/Assets/Image/Training/RaiseHand_2.png", UriKind.RelativeOrAbsolute));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class C_valueToImage : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte c_value = (byte)((float)value);
            int index = (int)(c_value / 12.5) + 1;
            if (index > 8)
                index = 8;
            return (ImageSource)new BitmapImage(new Uri(@"/Assets/Image/Training/Human_" + index.ToString() + ".png", UriKind.RelativeOrAbsolute));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class H_valueToString : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.Format("H : {0}", ((float)value).ToString("0"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class B_valueToString : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.Format("B : {0}", ((float)value).ToString("0"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

   
    public class IsStartedToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? "结束训练" : "开始训练";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsStartedToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

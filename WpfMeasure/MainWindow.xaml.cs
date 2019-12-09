using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMeasure
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }


    public class MyPanelParent : Panel
    {
        protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
        {
            foreach (UIElement item in this.InternalChildren)
            {
                item.Measure(new Size(120, 120));//这里是入口  传入的是120*120
            }

            return availableSize;
        }

        protected override System.Windows.Size  ArrangeOverride(System.Windows.Size finalSize)
        {
            double x = 0;
            foreach (UIElement item in this.InternalChildren)
            {
                item.Arrange(new Rect(x, 0, item.DesiredSize.Width, item.DesiredSize.Height));
                x += item.DesiredSize.Width;
            }

            return finalSize;
        }
    }

    public class MyPanel : Panel
    {
        protected override System.Windows.Size    MeasureOverride(System.Windows.Size availableSize)
        {
            foreach (UIElement item in this.InternalChildren)
            {
                item.Measure(availableSize);         //传入的参数为120*120
            }
            return new Size(50, 50);//MyPanel            返回它期望的大小
        }

        protected override System.Windows.Size ArrangeOverride(System.Windows.Size finalSize)
        {
            double xCordinate
 = 0;
            foreach (UIElement
 item in this.InternalChildren)
            {
                item.Arrange(new Rect(new Point(xCordinate,
 0), item.DesiredSize));
                xCordinate
 += item.DesiredSize.Width;
            }
            return finalSize;
        }
    }


}

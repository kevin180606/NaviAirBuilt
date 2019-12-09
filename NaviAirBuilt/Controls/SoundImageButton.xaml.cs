using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NaviAirBuilt.Controls
{
    /// <summary>
    /// SoundImageButton.xaml 的交互逻辑
    /// </summary>
    public partial class SoundImageButton : Button
    {
        public static DependencyProperty NormalImageProperty =
               DependencyProperty.Register("NormalImage", typeof(ImageSource), typeof(SoundImageButton), new FrameworkPropertyMetadata(null));
        public ImageSource NormalImage
        {
            get
            {
                return (ImageSource)this.GetValue(NormalImageProperty);
            }
            set
            {
                this.SetValue(NormalImageProperty, value);
            }
        }

        public static DependencyProperty PressedImageProperty =
            DependencyProperty.Register("PressedImage", typeof(ImageSource), typeof(SoundImageButton), new FrameworkPropertyMetadata(null));
        public ImageSource PressedImage
        {
            get
            {
                return (ImageSource)this.GetValue(PressedImageProperty);
            }
            set
            {
                this.SetValue(PressedImageProperty, value);
            }
        }

        public static DependencyProperty DisabledImageProperty =
            DependencyProperty.Register("DisabledImage", typeof(ImageSource), typeof(SoundImageButton), new FrameworkPropertyMetadata(null));
        public ImageSource DisabledImage
        {
            get
            {
                return (ImageSource)this.GetValue(DisabledImageProperty);
            }
            set
            {
                this.SetValue(DisabledImageProperty, value);
            }
        }

        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register(nameof(Stretch), typeof(Stretch), typeof(SoundImageButton), new PropertyMetadata(Stretch.None));
        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        public static readonly DependencyProperty ClickSoundProperty = DependencyProperty.Register(
            nameof(ClickSound), typeof(string), typeof(SoundImageButton));
        public string ClickSound
        {
            get { return (string)GetValue(ClickSoundProperty); }
            set { SetValue(ClickSoundProperty, value); }
        }

        public SoundImageButton()
        {
            ClickSound =@"Assets\Sound\Common\Button_Click.wav";

            InitializeComponent();
            this.FocusVisualStyle = null;

            Click += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(ClickSound))
                    new SoundPlayer(Path.Combine(Directory.GetCurrentDirectory(), ClickSound)).Play();
            };
        }
    }
}

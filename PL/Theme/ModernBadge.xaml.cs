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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.Theme
{
    /// <summary>
    /// Interaction logic for ModernBadge.xaml
    /// </summary>
    public partial class ModernBadge : UserControl
    {
        public static readonly DependencyProperty BadgeTextProperty =
            DependencyProperty.Register("BadgeText", typeof(string), typeof(ModernBadge), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty BadgeColorProperty =
                DependencyProperty.Register("BadgeColor", typeof(Brush), typeof(ModernBadge), new PropertyMetadata(Brushes.LightGray));

        public string BadgeText
        {
            get { return (string)GetValue(BadgeTextProperty); }
            set { SetValue(BadgeTextProperty, value); }
        }

        public Brush BadgeColor
        {
            get { return (Brush)GetValue(BadgeColorProperty); }
            set { SetValue(BadgeColorProperty, value); }
        }

        private void StartPulsingAnimation()
        {
            var storyboard = new Storyboard();

            var opacityAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.2,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            Storyboard.SetTarget(opacityAnimation, PulsingDot);
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(Ellipse.OpacityProperty));

            storyboard.Children.Add(opacityAnimation);
            storyboard.Begin();
        }


        public ModernBadge()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            StartPulsingAnimation();
        }
    }
}

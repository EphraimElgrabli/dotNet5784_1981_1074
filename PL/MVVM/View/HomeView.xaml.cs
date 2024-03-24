using PL.MVVM.ViewModel;
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

namespace PL.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {

        static readonly BlApi.IBl? s_bl = BlApi.Factory.Get();
        public BO.User? UserHome
        {
            get { return (BO.User?)GetValue(UserHomeProperty); }
            set { SetValue(UserHomeProperty, value); }
        }
        public static readonly DependencyProperty UserHomeProperty =
            DependencyProperty.Register("UserHome", typeof(BO.User), typeof(HomeView), new PropertyMetadata(null));
        public HomeView()
        {
            InitializeComponent();
        }
    }
}

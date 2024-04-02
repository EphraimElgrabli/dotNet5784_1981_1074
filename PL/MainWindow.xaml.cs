using PL.User;
using BO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.MVVM.ViewModel;
using PL.MVVM.View;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public DateTime? BIBIHAMELH
        //{
        //    get { return (DateTime)GetValue(ClockNow); }
        //    set { SetValue(ClockNow, value); }
        //}
        //public static readonly DependencyProperty ClockNow =
        //    DependencyProperty.Register("BIBIHAMELH", typeof(DateTime), typeof(SettingsView), new PropertyMetadata(DateTime.Now));
        static readonly BlApi.IBl? s_bl = BlApi.Factory.Get();
        public MainWindow(BO.User? viewer = null)
        {
            //BIBIHAMELH = s_bl?.DateNow;
            InitializeComponent();
            if (viewer != null)
            {
                
                var mainViewModel = (MainViewModel)DataContext;
                mainViewModel.Viewer = viewer;
                mainViewModel.HomeVM.Viewer = viewer;
                mainViewModel.TasksVM.Viewer = viewer;
                mainViewModel.HomeVM.Time = s_bl.DateNow;
                mainViewModel.Time = s_bl.DateNow;
                mainViewModel.TasksVM.GetUserTasks();
            }
            
        }
        
       
        private void btnAdminpanel_click(object sender, RoutedEventArgs e)
        {
            //new AdminPanel().Show();
        }

        private void MoveAdminWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
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

        public MainWindow(BO.User? viewer = null)
        {
            InitializeComponent();
            if (viewer != null)
            {
                var mainViewModel = (MainViewModel)DataContext;
                mainViewModel.Viewer = viewer;
                mainViewModel.HomeVM.Viewer = viewer;
                mainViewModel.TasksVM.Viewer = viewer;
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
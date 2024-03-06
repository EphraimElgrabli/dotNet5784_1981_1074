using PL.User;
using PL.Admin;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            new UserList().Show();
        }

        private void initDb_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you to initialize the Database?", "InitDB - Warning.", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DalTest.Initialization.Do();
            }
        }

        private void btnAdminpanel_click(object sender, RoutedEventArgs e)
        {
            new AdminPanel().Show();
        }
    }
}
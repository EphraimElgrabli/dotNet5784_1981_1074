using PL.User;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;
using MessageBoxButton = System.Windows.MessageBoxButton;
using MessageBoxResult = System.Windows.MessageBoxResult;

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
    }
    //(MessageBox.Show("Are you sure you to initialize the Database?","InitDB - Warning.", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
}
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
using System.Windows.Shapes;
using BO;
using PL;
using PL.User;

namespace PL
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public string Id { get; set; }
        public string Password { get; set; }
        static readonly BlApi.IBl? s_bl = BlApi.Factory.Get();
        public BO.User? UserToLogin
        {
            get { return (BO.User?)GetValue(UserToLoginProperty); }
            set { SetValue(UserToLoginProperty, value); }
        }
        public static readonly DependencyProperty UserToLoginProperty =
            DependencyProperty.Register("UserToLogin", typeof(BO.User), typeof(LoginScreen), new PropertyMetadata(null));
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void loginBtn_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string password = Password;
            UserToLogin = s_bl?.User.Read((int.Parse(Id)));
            new MainWindow().Show();
           
              
                if (UserToLogin.Level == BO.UserLevel.Producer)
                {
                    new MainWindow().Show();
                }
              
              
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        ex.Message,
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Hand,
                        MessageBoxResult.Cancel);
            }
        }

        private void MoveLoginWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        
    }
}

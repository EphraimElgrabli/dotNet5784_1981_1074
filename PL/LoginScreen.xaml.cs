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
using PL;
using PL.User;

namespace PL
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
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
              
                if (s_bl.User.Read(UserToLogin.Id).Level == BO.UserLevel.Producer)
                {
                    new MainWindow().Show();
                }
              
                new MainWindow().Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MoveLoginWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        
    }
}

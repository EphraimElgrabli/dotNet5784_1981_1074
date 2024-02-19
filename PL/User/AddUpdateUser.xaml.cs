using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.User
{
    /// <summary>
    /// Interaction logic for AddUpdateUser.xaml
    /// </summary>
    public partial class AddUpdateUser : Window
    {
        static readonly BlApi.IBl? s_bl = BlApi.Factory.Get();
        public bool flag = true;
        public BO.User ThisUser
        {
            get { return (BO.User)GetValue(ThisUserProperty); }
            set { SetValue(ThisUserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThisUserProperty =
            DependencyProperty.Register("user", typeof(BO.User), typeof(AddUpdateUser));


        public AddUpdateUser(int Id = 0)
        {
            try
            {
                if (Id == 0)
                {
                    ThisUser = new BO.User() { Id = 0, Email = "", Level = BO.UserLevel.None, Name = "", PhoneNumber = "+972" };
                    flag = true;
                }
                else
                {
                    ThisUser = s_bl.User.Read(Id)!;
                    flag = false;
                }
                InitializeComponent();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.User user = new BO.User()
                {
                    Id = ThisUser.Id,
                    Name = ThisUser.Name,
                    PhoneNumber = ThisUser.PhoneNumber,
                    Email = ThisUser.Email,
                    Level = ThisUser.Level
                };
                if (flag == false)
                    s_bl?.User.Update(user);
                else
                    s_bl?.User.Create(user);
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
    }
}
using PL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace PL.User
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList : Window
    {
        static readonly BlApi.IBl? s_bl = BlApi.Factory.Get();

        public IEnumerable<BO.User?> UserListing
        {
            get { return (IEnumerable<BO.User?>)GetValue(UserListingProperty); }
            set { SetValue(UserListingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UserListing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserListingProperty =
            DependencyProperty.Register("UserListing", typeof(IEnumerable<BO.User?>), typeof(UserList), new PropertyMetadata(null));

        public BO.UserLevel usrLevel { get; set; } = BO.UserLevel.None;

        public UserList()
        {
            InitializeComponent();
            UserListing = s_bl?.User.ReadAllUser()!;
        }

        private void ComboBox_UserLevelSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserListing = (usrLevel == BO.UserLevel.None) ? s_bl?.User.ReadAllUser()! : s_bl?.User.ReadAllUser(item => item.Level == usrLevel)!;
        }

        private void btn_AddUserInList(object sender, RoutedEventArgs e)
        {
            new AddUpdateUser().ShowDialog();
        }

        private void usrLstView_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            BO.User? userToUpdate = (sender as ListView)?.SelectedItem as BO.User;
            new AddUpdateUser(userToUpdate.Id).ShowDialog();
        }
    }
}
﻿using PL.User;
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
            InitializeComponent();
            if (viewer != null)
            {
                
                var mainViewModel = (MainViewModel)DataContext;
             
                mainViewModel.Viewer = viewer;
                mainViewModel.HomeVM.Viewer = viewer;
                mainViewModel.TasksVM.Viewer = viewer;
                mainViewModel.Time = s_bl.DateNow;
                mainViewModel.SettingsVM.Time= s_bl.DateNow;
                mainViewModel.HomeVM.Time = s_bl.DateNow;
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

        private void rdb_click(object sender, MouseEventArgs e)
        {
            var mainViewModel = (MainViewModel)DataContext;
            mainViewModel.Time = s_bl.DateNow;
            mainViewModel.HomeVM.Time = s_bl.DateNow;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
﻿using DalTest;
using PL.MVVM.ViewModel;
using PL.User;
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
using PL.Task;
namespace PL.MVVM.View
{
    /// <summary>
    /// Interaction logic for TasksView.xaml
    /// </summary>
    public partial class TasksView : UserControl
    {
        public TasksView()
        {
            InitializeComponent();
        }

        private void btn_AddTaskInList(object sender, RoutedEventArgs e)
        {
            new AddUpdateTask().ShowDialog();
            //UserListing = s_bl?.User.ReadAllUser()!;
        }
    }
}
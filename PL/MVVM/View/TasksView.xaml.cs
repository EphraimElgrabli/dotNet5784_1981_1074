using DalTest;
using PL.MVVM.ViewModel;
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
using PL;
using System.ComponentModel;
namespace PL.MVVM.View
{
    /// <summary>
    /// Interaction logic for TasksView.xaml
    /// </summary>
    public partial class TasksView : UserControl, INotifyPropertyChanged
    {
        static readonly BlApi.IBl? s_bl = BlApi.Factory.Get();
        public bool flag = true;
        public BO.Task ThisTask
        {
            get { return (BO.Task)GetValue(ThisUserProperty); }
            set { SetValue(ThisUserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThisUserProperty =
            DependencyProperty.Register("task", typeof(BO.Task), typeof(AddUpdateTask));

        public event PropertyChangedEventHandler? PropertyChanged;

        public TasksView()
        { 
            InitializeComponent();
        }

        private void btn_AddTaskInList(object sender, RoutedEventArgs e)
        {
            new AddUpdateTask().ShowDialog();
            var taskViewModel = (TasksViewModel)DataContext;
            taskViewModel.GetUserTasks();
        }
        private void TaskListView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Task? taskToUpdate = (sender as ListView)?.SelectedItem as BO.Task;
            new AddUpdateTask(taskToUpdate!.Id).ShowDialog();
            var taskViewModel = (TasksViewModel)DataContext;
            taskViewModel.GetUserTasks();
        }
    }
}
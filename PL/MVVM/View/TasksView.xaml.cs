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
    public partial class TasksView : UserControl
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

        public TasksView()
        {

            //DataContext = new TasksViewModel();
            //var taskViewModel = (TasksViewModel)DataContext;
            //var viewer = taskViewModel.Viewer;
            //DataContext= new MainViewModel();
            //var v = (MainViewModel)DataContext;
            //var viewer = v.Viewer;

            //if (viewer != null)
            //{
            //    if ((int)viewer.Level >= 4)
            //        TaskListing = s_bl?.Task.ReadAllTask()!; //TODO: maor fix this line so only tasks available to the user will appear
            //    else
            //    {
            //        IEnumerable<BO.Task> tasks = new List<BO.Task>();
            //        TaskListing = s_bl.Task.changeTaskList(s_bl?.User.Read(viewer.Id).Tasks!);
            //    }
            //}
            TaskListing = s_bl?.Task.ReadAllTask()!;

            InitializeComponent();
        }

        private void btn_AddTaskInList(object sender, RoutedEventArgs e)
        {
            new AddUpdateTask().ShowDialog();
            //Refresh();
        }
        public void Refresh()
        {
            var parent = Parent as Panel;
            if (parent != null)
            {
                var index = parent.Children.IndexOf(this);
                parent.Children.RemoveAt(index);
                parent.Children.Insert(index, new TasksView());
            }
        }
        private void TaskListView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Task? taskToUpdate = (sender as ListView)?.SelectedItem as BO.Task;
            new AddUpdateTask(taskToUpdate!.Id).ShowDialog();
            //Refresh();
        }
    }
}
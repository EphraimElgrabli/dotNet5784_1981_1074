using BO;
using System;
using System.Collections.Generic;
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

namespace PL.Task
{
    public partial class AddUpdateTask : Window
    {
        /// <summary>
        /// Interaction logic for AddUpdateTask.xaml
        /// </summary>
        static readonly BlApi.IBl? s_bl = BlApi.Factory.Get();
        public bool flag = true;
        public BO.Task ThisTask
        {
            get { return (BO.Task)GetValue(ThisUserProperty); }
            set { SetValue(ThisUserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThisUserProperty =
            DependencyProperty.Register("ThisTask", typeof(BO.Task), typeof(AddUpdateTask));

        public int? UserId
        { 
            get { return (int?)GetValue(UserIdProperty); }
            set { SetValue(UserIdProperty, value); }
}
        public static readonly DependencyProperty UserIdProperty =
    DependencyProperty.Register("UserId", typeof(int?), typeof(AddUpdateTask));

        public AddUpdateTask(int Id = 0)
        {

            try
            {
                if (Id == 0)
                {
                    ThisTask = new BO.Task()
                    {
                        Id = 0,
                        Alias = "",
                        Description = "",
                        pracentstart = 0,
                        pracentbetween = 0,
                        pracentend = 0,
                        CreatedAtDate = DateTime.Now,
                        StartDate = null,
                        ScheduledDate = null,
                        DeadlineDate = null,
                        CompleteDate = null,
                        Deliverables = "",
                        Remarks = "",
                        Complexity = BO.UserLevel.None

                    };
                    flag = true;
                }
                else
                {
                    ThisTask = s_bl.Task.Read(Id)!;
                    UserId = ThisTask.User?.Id;
                    flag = false;
                }
                InitializeComponent();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); Close(); }


        }


        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {

            try
            {
               

                if (UserId != null)
                {

                    ThisTask.User = s_bl?.Task.userintask(UserId);//(ThisTask.User.Id);
                }
                else
                {
                    ThisTask.User = null;
                }
                
                BO.Task task = new BO.Task()
                {
                    Id = ThisTask.Id,
                    Alias = ThisTask.Alias,
                    Description = ThisTask.Description,
                    CreatedAtDate = ThisTask.CreatedAtDate,
                    StartDate = ThisTask.StartDate,
                    ScheduledDate = ThisTask.ScheduledDate,
                    DeadlineDate = ThisTask.DeadlineDate,
                    CompleteDate = ThisTask.CompleteDate,
                    Deliverables = ThisTask.Deliverables,
                    Remarks = ThisTask.Remarks,
                    Complexity = ThisTask.Complexity,
                    Cost = ThisTask.Cost,
                    User = ThisTask.User,

                };
                if (flag == false)
                    s_bl?.Task.Update(task);
                else
                    s_bl?.Task.Create(task);
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

        private void AddUser_Draggable(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
            UserId = (sender as TextBox)?.Text == "" ? null : int.Parse((sender as TextBox)?.Text);

            }
            catch (Exception ex)
            {
                UserId = 0;
            }
        }
    }
}


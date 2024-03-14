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

namespace PL.MVVM.View
{
    /// <summary>
    /// Interaction logic for GanttView.xaml
    /// </summary>
    public partial class GanttView : UserControl
    {
        static readonly BlApi.IBl? s_bl = BlApi.Factory.Get();

        public IEnumerable<BO.Task?> TaskListing
        {
            get { return (IEnumerable<BO.Task?>)GetValue(TaskListingProperty); }
            set { SetValue(TaskListingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UserListing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskListingProperty =
            DependencyProperty.Register("TaskListing", typeof(IEnumerable<BO.Task?>), typeof(GanttView), new PropertyMetadata(null));

        public GanttView()
        {
            InitializeComponent();
            TaskListing = s_bl?.Task.ReadAllTask()!;
            TaskListing = TaskListing.OrderBy(Task => Task.pracentstart);
        }
    }
}

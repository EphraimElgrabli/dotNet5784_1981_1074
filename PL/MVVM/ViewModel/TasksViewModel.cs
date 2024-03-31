using PL.Core;
using PL.MVVM.View;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PL.MVVM.ViewModel
{
    public class TasksViewModel : ObservableObject
    {
        private BO.User _viewer;
        public BO.User Viewer
        {
            get { return _viewer; }
            set
            {
                _viewer = value;
                OnPropertyChanged(nameof(Viewer));
            }
        }

        static readonly BlApi.IBl? s_bl = BlApi.Factory.Get();

        public IEnumerable<BO.Task?> TaskListing
        {
            get { return _taskListing; }
            set
            {
                _taskListing = value;
                OnPropertyChanged(nameof(TaskListing));
            }
        }

        private IEnumerable<BO.Task?> _taskListing;

        public void GetUserTasks()
        {
            if (Viewer != null)
            {
                if ((int)Viewer.Level >= 4)
                    TaskListing = s_bl?.Task.ReadAllTask()!;
                else
                {
                    TaskListing = s_bl?.Task.ReadAllTask(t => t.User?.Id == Viewer.Id)!;
                }
            }
            else
            {
                TaskListing = Enumerable.Empty<BO.Task>();
            }
        }
    }
}
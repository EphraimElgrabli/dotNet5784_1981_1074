using PL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.MVVM.ViewModel
{
    class MainViewModel: ObservableObject
    {
        public RelayCommend  HomeViewCommand{ get; set; }
        public RelayCommend UserListViewCommand { get; set; }
        public RelayCommend TasksViewCommand { get; set; }
        public TasksViewModel TasksVM {  get; set; }
        public HomeViewModel HomeVM { get; set; } 
        public UsersListViewModel UserListVM { get; set; }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set 
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            UserListVM = new UsersListViewModel ();
            TasksVM = new TasksViewModel();
            CurrentView = HomeVM;
            HomeViewCommand = new RelayCommend(o =>
            {
                CurrentView = HomeVM;
            });
            UserListViewCommand = new RelayCommend(o =>
            {
                CurrentView = UserListVM;
            });
            TasksViewCommand = new RelayCommend(o =>
            {
                CurrentView = TasksVM;
            });
        }
    }
}

using PL.Core;
using PL.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.MVVM.ViewModel
{
    class MainViewModel: ObservableObject
    {
        private BO.User _viewer;
        public BO.User Viewer
        {
            get { return _viewer; }
            set
            {
                _viewer = value;
                OnPropertyChanged();
            }
        }
        public RelayCommend  HomeViewCommand{ get; set; }
        public RelayCommend UserListViewCommand { get; set; }
        public RelayCommend TasksViewCommand { get; set; }
        public RelayCommend GanttViewCommand { get; set; }
        public RelayCommend SettingsViewCommand { get; set; }
        public TasksViewModel TasksVM {  get; set; }
        public SettingsViewModel SettingsVM { get; set; }
        public HomeViewModel HomeVM { get; set; } 
        public UsersListViewModel UserListVM { get; set; }
        public GanttViewModel GanttVM { get; set; }
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
            HomeVM = new HomeViewModel ();
            UserListVM = new UsersListViewModel ();
            TasksVM = new TasksViewModel();
            GanttVM = new GanttViewModel();
            SettingsVM = new SettingsViewModel ();

            CurrentView = HomeVM;
            HomeViewCommand = new RelayCommend(o =>
            {
                CurrentView = HomeVM;
            });
            SettingsViewCommand = new RelayCommend(o =>
            {
                CurrentView = SettingsVM;
            });
            UserListViewCommand = new RelayCommend(o =>
            {
                CurrentView = UserListVM;
            });
            TasksViewCommand = new RelayCommend(o =>
            {
                CurrentView = TasksVM;
            });
            GanttViewCommand = new RelayCommend(o =>
            {
                CurrentView = GanttVM;
            });
        }
    }
}

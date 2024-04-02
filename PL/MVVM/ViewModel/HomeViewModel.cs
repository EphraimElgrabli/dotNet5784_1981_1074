using PL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.MVVM.ViewModel
{
    public class HomeViewModel : ObservableObject
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

        private DateTime _time;
        public DateTime Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }
    }
}

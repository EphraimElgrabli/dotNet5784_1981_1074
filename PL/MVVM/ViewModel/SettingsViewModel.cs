using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.Core;

namespace PL.MVVM.ViewModel
{
    class SettingsViewModel:ObservableObject
    {
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

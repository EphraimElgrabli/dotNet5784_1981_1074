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
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        static readonly BlApi.IBl? s_bl = BlApi.Factory.Get();
        public DateTime Date
        {
            get { return (DateTime)GetValue(ClockNow); }
            set { SetValue(ClockNow, value); }
        }
        public static readonly DependencyProperty ClockNow =
            DependencyProperty.Register("Date", typeof(DateTime), typeof(SettingsView), new PropertyMetadata(DateTime.Now));
        //DateTime Hour { get; set Hour=ClockNow; }
        //DateTime Min { get; set; }
        //DateTime Sec { get; set; }
        public SettingsView()
        {
            Date = s_bl.DateNow;
            InitializeComponent();
            
        }
        private void initDb_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you to initialize the Database?", "InitDB - Warning.", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DalTest.Initialization.Do();
                
            }
        }
        public static readonly DependencyProperty ClockPropety =
            DependencyProperty.Register("Clock", typeof(DateTime), typeof(SettingsView), new PropertyMetadata(null));

        private void ResetDB_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you to reset the Database?", "InitDB - Warning.", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DalTest.Initialization.Do();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //do the code
        }
    }
}

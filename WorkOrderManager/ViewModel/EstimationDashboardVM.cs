using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOrderManager.Model;

namespace WorkOrderManager.ViewModel
{
    public class EstimationDashboardVM : INotifyPropertyChanged {

        private ObservableCollection<Workorder> workorders;
        public ObservableCollection<Workorder> Workorders {
            get { return workorders; }
            set { 
                workorders = value;
                OnPropertyChanged(nameof(workorders));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName) {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}

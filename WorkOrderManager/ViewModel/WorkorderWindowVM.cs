using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkOrderManager.Model;

namespace WorkOrderManager.ViewModel
{
    public class WorkorderWindowVM : INotifyPropertyChanged {

        ListVM ListVM { get; set; }

        private Workorder openedWorkorder;

        public Workorder OpenedWorkorder {
            get { return openedWorkorder; }
            set { 
                openedWorkorder = value; 
                OnPropertyChanged(nameof(OpenedWorkorder));
            }
        }

        public WorkorderWindowVM() {

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName) {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

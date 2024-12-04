using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkOrderManager.ViewModel.Commands
{
    public class NewWorkorderWindowCommand : ICommand {

        public ListVM ViewModel { get; set; }

        public event EventHandler? CanExecuteChanged;

        public NewWorkorderWindowCommand(ListVM vm) {

            ViewModel = vm;
        }

        public bool CanExecute(object? parameter) {

            return true;
        }

        public void Execute(object? parameter) {

            ViewModel.OpenNewWorkorderWindow();
        }
    }
}

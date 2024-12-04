using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkOrderManager.ViewModel.Commands
{
    public class SubmitCustomerCommand : ICommand {
        public NewCustomerVM ViewModel { get; set; }

        public event EventHandler? CanExecuteChanged;

        public SubmitCustomerCommand(NewCustomerVM vm) {

            ViewModel = vm;
        }

        public bool CanExecute(object? parameter) {

            return true;
        }

        public void Execute(object? parameter) {

            ViewModel.SubmitCustomer();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WorkOrderManager.Filters;
using WorkOrderManager.Model;
using WorkOrderManager.View;
using WorkOrderManager.ViewModel.Commands;
using WorkOrderManager.ViewModel.Helpers;

namespace WorkOrderManager.ViewModel
{
    public class ListVM : INotifyPropertyChanged {

        private ObservableCollection<Workorder> workorders;

        public ObservableCollection<Workorder> Workorders {

            get { return workorders; }
            set { 
                workorders = value;
                OnPropertyChanged(nameof(Workorders));
            }
        }

        public ObservableCollection<Customer> Customers { get; set; }
        public ObservableCollection<string> CustomerNames { get; set; }
        public ObservableCollection<Workorder.StatusCode> StatusCodes { get; set; }
        public ObservableCollection<Workorder.ServiceTagCode> ServiceCodes { get; set; }

        private Workorder selectedWorkorder;
        public Workorder SelectedWorkorder {

            get { return selectedWorkorder; }
            set {
                selectedWorkorder = value;
                OnPropertyChanged(nameof(SelectedWorkorder));
            }
        }

        private string searchInput;

        public string SearchInput {
            get { return searchInput; }
            set { 
                searchInput = value;
                OnPropertyChanged(nameof(SearchInput));
                SearchList();
            }
        }

        private Workorder.StatusCode statusFilter;

        public Workorder.StatusCode StatusFilter {
            get { return statusFilter; }
            set { 
                statusFilter = value; 
                OnPropertyChanged(nameof(StatusFilter));
                FilterList();
            }
        }

        private Workorder.ServiceTagCode serviceTagFilter;

        public Workorder.ServiceTagCode ServiceTagFilter {
            get { return serviceTagFilter; }
            set { 
                serviceTagFilter = value; 
                OnPropertyChanged(nameof(ServiceTagFilter));
                FilterList();
            }
        }

        private string customerFilter;

        public string CustomerFilter {
            get { return customerFilter; }
            set { 
                customerFilter = value; 
                OnPropertyChanged(nameof(CustomerFilter));
                FilterByCustomer();
            }
        }




        public NewWorkorderWindowCommand NewWorkorderWindowCommand { get; set; }
        public NewCustomerWindowCommand NewCustomerWindowCommand { get; set; }
        public OpenEstimationDashboardWindowCommand OpenEstimationDashboardWindowCommand { get; set; }
        public RefreshCommand RefreshCommand { get; set; }
        public QuickSaveCommand QuickSaveCommand { get; set; }
        public QuickDeleteCommand QuickDeleteCommand { get; set; }
        public QuickOpenCommand QuickOpenCommand { get; set; }
        public TextChangedCommand TextChangedCommand { get; set; }
        public ClearFiltersCommand ClearFiltersCommand { get; set; }


        public ListVM() {

            NewWorkorderWindowCommand = new NewWorkorderWindowCommand(this);
            NewCustomerWindowCommand = new NewCustomerWindowCommand(this);
            OpenEstimationDashboardWindowCommand = new OpenEstimationDashboardWindowCommand(this);
            RefreshCommand = new RefreshCommand(this);
            QuickSaveCommand = new QuickSaveCommand(this);
            QuickDeleteCommand = new QuickDeleteCommand(this);
            QuickOpenCommand = new QuickOpenCommand(this);
            TextChangedCommand = new TextChangedCommand(this);
            ClearFiltersCommand = new ClearFiltersCommand(this);

            Workorders = new ObservableCollection<Workorder>();
            Customers = new ObservableCollection<Customer>();
            CustomerNames = new ObservableCollection<string>();

            GetCodes();
            GetCustomers();
            GetWorkorders();
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) {
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OpenWorkorder() {

            WorkorderWindow workorderWindow = new WorkorderWindow(SelectedWorkorder);
            workorderWindow.Owner = Application.Current.MainWindow;
            workorderWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            workorderWindow.ShowDialog();
        }

        public void OpenNewWorkorderWindow() {

            NewWorkorderWindow newWorkorderWindow = new NewWorkorderWindow() {

            };
            newWorkorderWindow.Owner = Application.Current.MainWindow;
            newWorkorderWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            newWorkorderWindow.ShowDialog();
        }

        public void OpenNewCustomerWindow() {

            NewCustomerWindow newCustomerWindow = new NewCustomerWindow() {

            };
            newCustomerWindow.Owner = Application.Current.MainWindow;
            newCustomerWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            newCustomerWindow.ShowDialog();
        }

        public void OpenEstimationDashboardWindow() {

            EstimationDashboardWindow estimationDashboardWindow = new EstimationDashboardWindow() {
            };
            estimationDashboardWindow.Owner = Application.Current.MainWindow;
            estimationDashboardWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            estimationDashboardWindow.Show();
            estimationDashboardWindow.Owner = null;
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = estimationDashboardWindow;
        }

        public void SearchList() {

            FilterList();
            ObservableCollection<Workorder> filteredWorkorders = new ObservableCollection<Workorder>();
            filteredWorkorders = ListFilter.GetFilteredWorkorderListBySearchInput(Workorders, SearchInput);
            Workorders = filteredWorkorders;
        }

        private void FilterList() {

            GetWorkorders();
            ObservableCollection<Workorder> filteredWorkorders = new ObservableCollection<Workorder>();
            filteredWorkorders = ListFilter.GetFilteredWorkorderListByFilters(Workorders, StatusFilter, ServiceTagFilter);
            //filteredWorkorders = ListFilter.FilterByCustomer(filteredWorkorders, CustomerFilter);
            Workorders = filteredWorkorders;
        }

        private void FilterByCustomer() {

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Workorders);

            view.Filter = item => {

                if (item is Workorder workorder) {

                    return workorder.CustomerId.Contains(customerFilter, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };

            view.Refresh();
        }

        public void ClearFilters() {

            StatusFilter = Workorder.StatusCode.None;
            ServiceTagFilter = Workorder.ServiceTagCode.None;
            CustomerFilter = "None";
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Workorders);

            view.Filter = null;

            view.Refresh();

            
        }

        public void RefreshContent() {

            GetCustomers();
            GetWorkorders();
        }

        public void QuickSave() {

            DatabaseHelper.Update<Workorder>(SelectedWorkorder);
            RefreshContent();
        }

        public void QuickDelete() {

            MessageBoxResult result = MessageBox.Show(
                $"Are you sure you want to delete WO#{SelectedWorkorder.Id}, {SelectedWorkorder.ServiceTag} for {SelectedWorkorder.CustomerId} at {SelectedWorkorder.Location}?",
                "Quick Delete Workorder",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes) {

                MessageBoxResult secondChance = MessageBox.Show(
                    $"Workorder #{SelectedWorkorder.Id} will be deleted.",
                    "Confirmation",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);

                if (secondChance == MessageBoxResult.OK) {

                    DatabaseHelper.Delete<Workorder>(SelectedWorkorder);
                    RefreshContent();
                }
            } 
        }

        private void GetCustomers() {

            var customers = DatabaseHelper.Read<Customer>();

            Customers.Clear();
            CustomerNames.Clear();

            CustomerNames.Add("None");
            foreach (var customer in customers) {

                Customers.Add(customer);
                CustomerNames.Add(customer.CompanyName);
            }
        }

        private void GetWorkorders() {

            var workorders = DatabaseHelper.Read<Workorder>();

            Workorders.Clear();

            foreach (var workorder in workorders) {

                Workorders.Add(workorder);
            }
        }

        private void GetCodes() {

            StatusCodes = Workorder.GetStatusCodes();
            ServiceCodes = Workorder.GetServiceCodes();
        }

        //Open Selected Workorder
    }
}

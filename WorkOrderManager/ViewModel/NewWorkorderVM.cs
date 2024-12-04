using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WorkOrderManager.Model;
using WorkOrderManager.View;
using WorkOrderManager.ViewModel.Commands;
using WorkOrderManager.ViewModel.Helpers;

namespace WorkOrderManager.ViewModel
{
    public class NewWorkorderVM : INotifyPropertyChanged {

        public ObservableCollection<string> CustomerNames { get; set; }

        private DispatcherTimer timer;

        private string currentTime;
        public string CurrentTime {

            get { return currentTime; }
            set {
                currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }

        private int workorderId;
        public int WorkorderId {
            get { return workorderId; }
            set {
                workorderId = value;
                OnPropertyChanged(nameof(WorkorderId));
            }
        }

        private Workorder workorder;
        public Workorder Workorder {
            get { return workorder; }
            set { 
                workorder = value;
            }
        }

        private string customer;
        public string Customer {
            get { return customer; }
            set {
                customer = value;
                OnPropertyChanged(nameof(Customer));
            }
        }

        private int customerPO;
        public int CustomerPO {
            get { return customerPO; }
            set { 
                customerPO = value;
                OnPropertyChanged(nameof(CustomerPO));
            }
        }

        private string location;
        public string Location {
            get { return location; }
            set {
                location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        private string street;
        public string Street {
            get { return street; }
            set { 
                street = value; 
                OnPropertyChanged(nameof(Street));
            }
        }

        private string city;
        public string City {
            get { return city; }
            set {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        private string region;
        public string Region {
            get { return region; }
            set { 
                region = value;
                OnPropertyChanged(nameof(Region));
            }
        }

        private string postalCode;
        public string PostalCode {
            get { return postalCode; }
            set { postalCode = value; }
        }

        private decimal nTE;
        public decimal NTE {
            get { return nTE; }
            set {
                nTE = value; 
                OnPropertyChanged(nameof(NTE));
            }
        }

        private Workorder.StatusCode status;
        public Workorder.StatusCode Status {
            get { return status; }
            set { 
                status = value; 
                OnPropertyChanged(nameof(Status));
            }
        }

        private Workorder.ServiceTagCode serviceTag;
        public Workorder.ServiceTagCode ServiceTag {
            get { return serviceTag; }
            set { 
                serviceTag = value;
                OnPropertyChanged(nameof(ServiceTag));
            }
        }

        private DateTime selectedDate;
        public DateTime SelectedDate {
            get { return selectedDate; }
            set {
                selectedDate = value; 
                OnPropertyChanged(nameof(SelectedDate));
                ConvertToETA();
            }
        }

        private string selectedHour;
        public string SelectedHour {
            get { return selectedHour; }
            set {
                selectedHour = value; 
                OnPropertyChanged(nameof(SelectedHour));
                CompileTime();
            }
        }

        private string selectedMinute;
        public string SelectedMinute {
            get { return selectedMinute; }
            set {
                selectedMinute = value;
                OnPropertyChanged(nameof(SelectedMinute));
                CompileTime();
            }
        }

        private string selectedMeridiem;
        public string SelectedMeridiem {
            get { return selectedMeridiem; }
            set { 
                selectedMeridiem = value; 
                OnPropertyChanged(nameof(SelectedMeridiem));
                CompileTime();
            }
        }

        private string selectedTime;
        public string SelectedTime {

            get { return selectedTime; }
            set {
                selectedTime = value;
                OnPropertyChanged(nameof(SelectedTime));
                ConvertToETA();
            }
        }

        private DateTime selectedETA;
        public DateTime SelectedETA {

            get { return selectedETA; }
            set {
                selectedETA = value;
                OnPropertyChanged(nameof(SelectedETA));
            }
        }

        private string serviceDescription;
        public string ServiceDescription {
            get { return serviceDescription; }
            set {
                serviceDescription = value; 
                OnPropertyChanged(nameof(ServiceDescription));
            }
        }

        public SubmitWorkorderCommand SubmitWorkorderCommand { get; set; }

        public NewWorkorderVM() {

            InitializeNewWorkorder();

            CustomerNames = new ObservableCollection<string>();

            SubmitWorkorderCommand = new SubmitWorkorderCommand(this);

            GetCustomerNames();

            InitializeTimer();
        }



        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName) {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Timer_Tick(object sender, EventArgs e) {

            CurrentTime = DateTime.Now.ToString("dddd, MMMM, dd, yyyy HH:mm:ss");
        }

        private void InitializeNewWorkorder() {

            Workorder newWorkorder = new Workorder();
            Workorder = newWorkorder;
            WorkorderId = newWorkorder.Id;
        }

        private void GetCustomerNames() {

            var customerNames = DatabaseHelper.Read<Customer>();

            CustomerNames.Clear();

            foreach (var customer in customerNames) {

                CustomerNames.Add(customer.CompanyName);
            }
        }

        public void SubmitWorkorder() {

            Workorder.CustomerId = Customer;
            Workorder.CustomerPO = CustomerPO;
            Workorder.Location = Location;
            Workorder.Street = Street;
            Workorder.City = City;
            Workorder.Region = Region;
            Workorder.PostalCode = PostalCode;
            Workorder.DateCreated = DateTime.Now;
            Workorder.DateUpdated = DateTime.Now;
            Workorder.NTE = NTE;
            Workorder.Status = Status;
            Workorder.ServiceTag = ServiceTag;
            Workorder.ETA = SelectedETA;
            Workorder.ServiceDescription = ServiceDescription;
            
            MessageBox.Show("Submitted");

            DatabaseHelper.Insert(Workorder);
        }

        private void InitializeTimer() {

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public void ConvertToETA() {

            TimeSpan time = TimeSpan.Parse(SelectedTime);
            DateTime partialDateTime = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day);
            SelectedETA = partialDateTime.Add(time);
        }

        public void CompileTime() {

            if (selectedMeridiem == "AM" && selectedHour != "12") {

                SelectedTime = $"{SelectedHour}:{SelectedMinute}:00";

            } else if (selectedMeridiem == "AM" && selectedHour == "12") {

                SelectedTime = $"00:{SelectedMinute}:00";

            } else if (selectedMeridiem == "PM" && selectedHour != "12") {

                SelectedTime = $"{int.Parse(SelectedHour) + 12}:{SelectedMinute}:00";

            } else if (selectedMeridiem == "PM" && selectedHour == "12") {

                SelectedTime = $"{SelectedHour}:{SelectedMinute}:00";

            } else {
            }
        }


    }
}

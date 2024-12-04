using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkOrderManager.Model;
using WorkOrderManager.ViewModel.Commands;
using WorkOrderManager.ViewModel.Helpers;

namespace WorkOrderManager.ViewModel
{
    public class NewCustomerVM : INotifyPropertyChanged {

        private Customer customer;
        public Customer Customer {
            get { return customer; }
            set { 
                customer = value; 
            }
        }

        private string companyName;
        public string CompanyName {
            get { return companyName; }
            set { 
                companyName = value; 
                OnPropertyChanged(nameof(CompanyName));
            }
        }

        private string address;
        public string Address {
            get { return address; }
            set { 
                address = value; 
                OnPropertyChanged(nameof(Address));
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
            set { 
                postalCode = value; 
                OnPropertyChanged(nameof(PostalCode));
            }
        }

        private string country;
        public string Country {
            get { return country; }
            set { 
                country = value; 
                OnPropertyChanged(nameof(Country));
            }
        }

        private string email;
        public string Email {
            get { return email; }
            set { 
                email = value; 
                OnPropertyChanged(nameof(Email));
            }
        }

        private string phone;
        public string Phone {
            get { return phone; }
            set { 
                phone = value; 
                OnPropertyChanged(nameof(Phone));
            }
        }

        private string alternateEmail;
        public string AlternateEmail {
            get { return alternateEmail; }
            set { 
                alternateEmail = value; 
                OnPropertyChanged(nameof(AlternateEmail));
            }
        }

        private string aHPhone;
        public string AHPhone {
            get { return aHPhone; }
            set { 
                aHPhone = value; 
                OnPropertyChanged(nameof(AHPhone));
            }
        }

        private string billingAddress;
        public string BillingAddress {
            get { return billingAddress; }
            set { 
                billingAddress = value; 
                OnPropertyChanged(nameof(BillingAddress));
            }
        }

        private string billingCity;
        public string BillingCity {
            get { return billingCity; }
            set {
                billingCity = value;
                OnPropertyChanged(nameof(BillingCity));
            }
        }

        private string billingRegion;
        public string BillingRegion {
            get { return billingRegion; }
            set { 
                billingRegion = value; 
                OnPropertyChanged(nameof(BillingRegion));
            }
        }

        private string billingPostalCode;
        public string BillingPostalCode {
            get { return billingPostalCode; }
            set { 
                billingPostalCode = value; 
                OnPropertyChanged(nameof(BillingPostalCode));
            }
        }

        private string billingCountry;
        public string BillingCountry {
            get { return billingCountry; }
            set {
                billingCountry = value;
                OnPropertyChanged(nameof(BillingCountry));
            }
        }

        private string nickname;
        public string Nickname {
            get { return nickname; }
            set { 
                nickname = value; 
                OnPropertyChanged(nameof(Nickname));
            }
        }

        public SubmitCustomerCommand SubmitCustomerCommand { get; set; }

        public NewCustomerVM() {

            InitializeNewCustomer();

            SubmitCustomerCommand = new SubmitCustomerCommand(this);


        }

        private void InitializeNewCustomer() {

            Customer newCustomer = new Customer();
            Customer = newCustomer;
        }

        public void SubmitCustomer() {

            Customer.CompanyName = CompanyName;
            Customer.Email = Email;
            Customer.Phone = Phone;
            Customer.Address = Address;
            Customer.City = City;
            Customer.Region = Region;
            Customer.PostalCode = PostalCode;
            Customer.Country = Country;
            Customer.Nickname = Nickname;
            Customer.AHPhone = AHPhone;
            Customer.AlternateEmail = AlternateEmail;
            Customer.BillingAddress = BillingAddress;
            Customer.BillingCity = BillingCity;
            Customer.BillingRegion = BillingRegion;
            Customer.BillingPostalCode = BillingPostalCode;
            Customer.BillingCountry = BillingCountry;

            MessageBox.Show("Submitted");

            DatabaseHelper.Insert(Customer);
        }















        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

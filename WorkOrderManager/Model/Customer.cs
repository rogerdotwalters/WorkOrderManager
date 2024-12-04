using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WorkOrderManager.Model
{
    public class Customer {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Nickname { get; set; }
        public string Address { get; set; }
        public string BillingAddress { get; set; }
        public string City { get; set; }
        public string BillingCity { get; set; }
        public string Region { get; set; }
        public string BillingRegion { get; set; }
        public string PostalCode { get; set; }
        public string BillingPostalCode { get; set; }
        public string Country { get; set; }
        public string BillingCountry { get; set; }
        public string Phone { get; set; }
        public string AHPhone { get; set; }
        public string Email { get; set; }
        public string AlternateEmail { get; set; }
    }
}

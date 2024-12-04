using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WorkOrderManager.Model
{
    public class Workorder {

        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        public DateTime ETA { get; set; }
        public StatusCode Status { get; set; }
        [ForeignKey(nameof(Customer.Id))]
        public string CustomerId { get; set; }
        public int CustomerPO { get; set; }
        public string Location { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public ServiceTagCode ServiceTag { get; set; }
        public DateTime DateUpdated { get; set; }
        public decimal NTE { get; set; }
        public DateTime DateCreated { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public DateTime DateReceived { get; set; }
        public string ServiceDescription { get; set; }

        public enum StatusCode {

            None,
            Assigned,
            Scheduled,
            QuoteNeeded,
            Quoted,
            QuotedIncurred,
            Approved,
            Rejected,
            Completed,
            Closed,
            Finalized,
            Invoiced,
            Exported,
            Cancelled
        }
        public enum ServiceTagCode {

            None,
            General,
            Windows,
            Doors,
            Concrete,
            Painting,
            Hardware,
            Repair,
            Equipment,
            Drywall,
            Tile,
            Flooring
        }

        public static ObservableCollection<StatusCode> GetStatusCodes() {

            ObservableCollection<StatusCode> codeCollection = new ObservableCollection<StatusCode>();
            
            var codes = Enum.GetValues(typeof(StatusCode)).Cast<StatusCode>();

            foreach (var code in codes) {

                codeCollection.Add(code);
            }

            return codeCollection;
        }

        public static ObservableCollection<ServiceTagCode> GetServiceCodes() {

            ObservableCollection<ServiceTagCode> codeCollection = new ObservableCollection<ServiceTagCode>();
            var codes = Enum.GetValues(typeof(ServiceTagCode)).Cast<ServiceTagCode>();

            foreach (var code in codes) {

                codeCollection.Add(code);
            }

            return codeCollection;
        }
    }
}

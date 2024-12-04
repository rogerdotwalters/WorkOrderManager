using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOrderManager.Model;

namespace WorkOrderManager.Filters
{
    public class ListFilter {

        public static ObservableCollection<Workorder> GetFilteredWorkorderListBySearchInput(ObservableCollection<Workorder> workorders, string searchInput) {

            ObservableCollection<Workorder> filteredWorkorders = new ObservableCollection<Workorder>();

            if (int.TryParse(searchInput, out int intResult)) {

                if (searchInput.Length == 5) {

                    foreach (var workorder in workorders) {

                        if (workorder.Id == intResult) {

                            filteredWorkorders.Add(workorder);
                        }
                    }
                } else {

                    foreach (var workorder in workorders) {

                        if (workorder.Id == intResult) {

                            filteredWorkorders.Add(workorder);
                        }

                        if (workorder.CustomerPO == intResult) {

                            filteredWorkorders.Add(workorder);
                        }
                    }
                }

            }
            if (filteredWorkorders.Count > 0) {

                return filteredWorkorders;

            } else return workorders;
        }

        public static ObservableCollection<Workorder> GetFilteredWorkorderListByFilters(ObservableCollection<Workorder> workorders, Workorder.StatusCode status, Workorder.ServiceTagCode serviceTag) {

            ObservableCollection<Workorder> filteredWorkorders = new ObservableCollection<Workorder>();

            if (status != Workorder.StatusCode.None && serviceTag != Workorder.ServiceTagCode.None) {

                foreach (var workorder in workorders) {

                    if (status == workorder.Status && serviceTag == workorder.ServiceTag) {

                        filteredWorkorders.Add(workorder);
                    }
                }
            } else if (status != Workorder.StatusCode.None) {

                foreach (var workorder in workorders) {

                    if (status == workorder.Status) {

                        filteredWorkorders.Add(workorder);
                    }
                }
            } else if (serviceTag != Workorder.ServiceTagCode.None) {

                foreach (var workorder in workorders) {

                    if (serviceTag == workorder.ServiceTag) {

                        filteredWorkorders.Add(workorder);
                    }
                }
            } else {

                filteredWorkorders = workorders;
            }

            if (filteredWorkorders.Count > 0) {

                return filteredWorkorders;

            } else return filteredWorkorders;
        }

        //public static ObservableCollection<Workorder> FilterByCustomer(ObservableCollection<Workorder> workorders, string customerId) {

        //    ObservableCollection<Workorder> filteredWorkorders = new ObservableCollection<Workorder>();

        //    if (customerId != "None" && customerId != null) {

        //        foreach (var workorder in workorders) {

        //            if (customerId == workorder.CustomerId) {

        //                filteredWorkorders.Add(workorder);
        //            }
        //        }
        //        return filteredWorkorders;
        //    } else {

        //        return workorders;
        //    }
        //}
    }
}

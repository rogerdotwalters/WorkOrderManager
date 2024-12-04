using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WorkOrderManager.ViewModel.Converters
{
    public class AddressMultiValueConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {

            string address = values[0] as string;
            string city = values[1] as string;
            string region = values[2] as string;
            string postalCode = values[3] as string;

            return $"{address},\n{city} \n{region} {postalCode}".Trim(new char[] { ',', ' ', '\n' });
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {

            throw new NotImplementedException();
        }
    }
}

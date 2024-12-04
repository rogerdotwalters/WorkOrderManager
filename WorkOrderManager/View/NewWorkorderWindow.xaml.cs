using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static WorkOrderManager.Model.Workorder;

namespace WorkOrderManager.View
{
    /// <summary>
    /// Interaction logic for NewWorkorderWindow.xaml
    /// </summary>
    public partial class NewWorkorderWindow : Window
    {
        public NewWorkorderWindow()
        {
            InitializeComponent();
            PopulateDropDowns();
        }


        public void PopulateDropDowns() {

            foreach (StatusCode statusCode in Enum.GetValues(typeof(StatusCode))) {

                StatusComboBox.Items.Add(statusCode.ToString());
            }

            foreach (ServiceTagCode serviceTagCode in Enum.GetValues(typeof(ServiceTagCode))) {

                ServiceTagComboBox.Items.Add(serviceTagCode.ToString());
            }

            for (int i = 0; i < 60; i++) {

                if (i < 10) {

                    MinuteComboBox.Items.Add("0" + i.ToString());
                } else {

                    MinuteComboBox.Items.Add(i.ToString());
                }
            }

            for (int i = 0; i < 12; i++) {

                if (i < 9) {

                    HourComboBox.Items.Add("0" + (i+1).ToString());
                } else {

                    HourComboBox.Items.Add((i+1).ToString());
                }
            }

            MeridiemComboBox.Items.Add("AM");
            MeridiemComboBox.Items.Add("PM");
        }

        private void DecimalTextBox_TextChanged(object sender, TextChangedEventArgs e) {

            TextBox textBox = (TextBox)sender;

            if (!Regex.IsMatch(textBox.Text, @"^[0-9]*(\.?[0-9]{0,2})?$")) {

                int caretIndex = textBox.CaretIndex;
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                textBox.CaretIndex = caretIndex - 1;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {

            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to cancel?",
                "Cancel New Workorder",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
                );

            if (result == MessageBoxResult.Yes) {

                Close();

            } else if (result == MessageBoxResult.No) {

            };
        }

        private void Button_Click(object sender, RoutedEventArgs e) {

            Close();
        }

    }
}

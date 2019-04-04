using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HW3
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Tip tip;

        public MainWindow()
        {
            InitializeComponent();
            tip = new Tip();
        }

        private void BillAmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PerformCalculation();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            PerformCalculation();
        }

        private void PerformCalculation()
        {
            var selectedRadio = mainGrid.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);
            if (selectedRadio == null) return;
            tip.CalculateTip(billAmountTextBox.Text, double.Parse(selectedRadio.Tag.ToString()));
            amountToTip.Content = tip.TipAmount;
            totalBill.Content = tip.TotalAmount;
        }

        private void BillAmountTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            billAmountTextBox.Text = "";
        }

        private void BillAmountTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            billAmountTextBox.Text = tip.BillAmount;
        }
    }
}

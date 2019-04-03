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
        String lastLegalAmount = "¥0.00";

        public MainWindow()
        {
            InitializeComponent();
        }

        private bool IsLegalAmount(String amount)
        {
            int cnt = 0, p = -1;
            if (amount.Length == 0 || amount[0] != '¥') return false;
            for (int i = 1; i < amount.Length; ++i)
            {
                if (amount[i] >= '0' && amount[i] <= '9') ;
                else if (amount[i] == '.')
                {
                    ++cnt;
                    if (cnt == 2) return false;
                    p = i;
                }
                else return false;
            }
            if (p != -1 && amount.Length > p + 3) return false;
            return true;
        }

        private void BillAmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (!IsLegalAmount(tb.Text)) tb.Text = lastLegalAmount;
            else lastLegalAmount = tb.Text;
        }

        private void BillAmountTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            lastLegalAmount = Normalize(lastLegalAmount);
            tb.Text = lastLegalAmount;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            PerformCalculation();
        }

        private void PerformCalculation()
        {
            var selectedRadio = mainGrid.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);
            double bill = Double.Parse(lastLegalAmount.Substring(1)), rate = Double.Parse(selectedRadio.Tag.ToString()), tmp;
            tmp = bill * rate;
            amountToTip.Content = Normalize("¥" + Math.Round(tmp, 2));
            tmp += bill;
            totalBill.Content = Normalize("¥" + Math.Round(tmp, 2));
        }

        private String Normalize(String str)
        {
            int lastPos = str.LastIndexOf('.'), len = str.Length;
            String extra = "";
            if (lastPos == -1) extra = ".00";
            else if (lastPos == len - 1) extra = "00";
            else if (lastPos == len - 2) extra = "0";
            str += extra;
            return str;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PerformCalculation();
        }
    }
}

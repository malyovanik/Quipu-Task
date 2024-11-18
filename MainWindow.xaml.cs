using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Quipu_Task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal depositAmount = decimal.Parse(DepositAmountTextBox.Text);
                int term = int.Parse(TermTextBox.Text);
                string paymentMethod = (PaymentMethodComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                string currency = (CurrencyComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                decimal rate = 0.05m; // Example fixed rate of 5%
                decimal totalIncome = 0;

                if (paymentMethod == "Capitalization")
                {
                    totalIncome = depositAmount * (decimal)Math.Pow((double)(1 + rate / 12), term) - depositAmount;
                }
                else if (paymentMethod == "Monthly Payout")
                {
                    totalIncome = depositAmount * (rate / 12) * term;
                }

                ResultTextBlock.Text = $"Expected Income in {currency}: {totalIncome:C}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
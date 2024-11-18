using Quipu_Task.ViewModel;
using System.Windows;

namespace Quipu_Task.Model
{
    public class DepositCalculatorModel
    {
        public decimal CalculateTotalIncome(decimal depositAmount, int term, decimal annualRate, PaymentMethod paymentMethod)
        {
            try
            {
                switch (paymentMethod)
                {
                    case PaymentMethod.Capitalization:
                        return depositAmount * (decimal)Math.Pow((double)(1 + annualRate / 12), term) - depositAmount;

                    case PaymentMethod.MonthlyPayout:
                        return depositAmount * (annualRate / 12) * term;

                    default:
                        throw new ArgumentException("Invalid payment method.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
    }
}
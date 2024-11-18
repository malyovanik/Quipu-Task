using Quipu_Task.ViewModel;

namespace Quipu_Task.Model
{
    public class DepositCalculatorModel
    {
        public decimal CalculateTotalIncome(decimal depositAmount, int term, decimal annualRate, PaymentMethod paymentMethod)
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
    }
}
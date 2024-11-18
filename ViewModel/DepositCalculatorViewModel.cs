using Quipu_Task.Command;
using Quipu_Task.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Quipu_Task.ViewModel
{
    public enum PaymentMethod
    {
        Capitalization,
        MonthlyPayout
    }

    public enum Currency
    {
        USD,
        EUR,
        GBP
    }

    public class DepositCalculatorViewModel : INotifyPropertyChanged
    {
        private readonly DepositCalculatorModel _model = new();

        private decimal _depositAmount;
        private int _term;
        private PaymentMethod _selectedPaymentMethod;
        private Currency _selectedCurrency;
        private decimal _result;
        private decimal _annualRate = 0.05m; // Example fixed rate

        public decimal DepositAmount
        {
            get => _depositAmount;
            set { _depositAmount = value; OnPropertyChanged(); }
        }

        public int Term
        {
            get => _term;
            set { _term = value; OnPropertyChanged(); }
        }

        public PaymentMethod SelectedPaymentMethod
        {
            get => _selectedPaymentMethod;
            set { _selectedPaymentMethod = value; OnPropertyChanged(); }
        }

        public Currency SelectedCurrency
        {
            get => _selectedCurrency;
            set { _selectedCurrency = value; OnPropertyChanged(); }
        }

        public decimal Result
        {
            get => _result;
            set { _result = value; OnPropertyChanged(); }
        }

        public ICommand CalculateCommand { get; }

        public DepositCalculatorViewModel()
        {
            CalculateCommand = new RelayCommand(ExecuteCalculate, CanExecuteCalculate);
        }

        private bool CanExecuteCalculate(object parameter) =>
            DepositAmount > 0 && Term > 0;

        private void ExecuteCalculate(object parameter)
        {
            Result = _model.CalculateTotalIncome(DepositAmount, Term, _annualRate, SelectedPaymentMethod);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
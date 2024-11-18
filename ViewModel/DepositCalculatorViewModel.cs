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

        private string _depositAmount;
        private string _term;
        private PaymentMethod _selectedPaymentMethod;
        private Currency _selectedCurrency;
        private decimal _result;
        private decimal _annualRate = 0.05m; // Example fixed rate
        private decimal _depositValue;
        private int _termValue;

        public string DepositAmount
        {
            get => _depositAmount;
            set
            {
                _depositAmount = value;
                IsDepositWarningVisible = !decimal.TryParse(_depositAmount, out decimal depositValue) || depositValue <= 0;
                _depositValue = depositValue;
                IsFormValid = !IsTermWarningVisible && !IsDepositWarningVisible;
                OnPropertyChanged();
            }
        }

        public string Term
        {
            get => _term;
            set
            {
                _term = value;
                IsTermWarningVisible = !int.TryParse(_term, out int termValue) || termValue <= 0;
                _termValue = termValue;
                IsFormValid = !IsTermWarningVisible && !IsDepositWarningVisible;
                OnPropertyChanged();
            }
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

        private bool _isFormValid;

        public bool IsFormValid
        {
            get => _isFormValid;
            set
            {
                if (_isFormValid != value)
                {
                    _isFormValid = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isDepositWarningVisible;

        public bool IsDepositWarningVisible
        {
            get => _isDepositWarningVisible;
            set
            {
                _isDepositWarningVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isTermWarningVisible;

        public bool IsTermWarningVisible
        {
            get => _isTermWarningVisible;
            set
            {
                _isTermWarningVisible = value;
                OnPropertyChanged();
            }
        }

        public ICommand CalculateCommand { get; }

        public DepositCalculatorViewModel()
        {
            CalculateCommand = new RelayCommand(ExecuteCalculate);
            DepositAmount = "1000"; // Set 1k by default.
            Term = "12"; // Set 1 year by default.
        }

        private void ExecuteCalculate(object parameter)
        {
            Result = _model.CalculateTotalIncome(_depositValue, _termValue, _annualRate, SelectedPaymentMethod);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
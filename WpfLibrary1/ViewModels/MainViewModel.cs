using RealNumberApp.Models;
using System.Windows.Input;

namespace RealNumberApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public double AValue { get; set; }
        public double BValue { get; set; }

        private string _result;
        public string Result
        {
            get => _result;
            set { _result = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; }
        public ICommand SubCommand { get; }
        public ICommand MulCommand { get; }
        public ICommand DivCommand { get; }

        public MainViewModel()
        {
            AddCommand = new RelayCommand(Add);
            SubCommand = new RelayCommand(Sub);
            MulCommand = new RelayCommand(Mul);
            DivCommand = new RelayCommand(Div);
        }

        private void Add()
        {
            var a = RealNumber.FromDouble(AValue);
            var b = RealNumber.FromDouble(BValue);
            Result = (a + b).ToString();
        }

        private void Sub()
        {
            var a = RealNumber.FromDouble(AValue);
            var b = RealNumber.FromDouble(BValue);
            Result = (a - b).ToString();
        }

        private void Mul()
        {
            var a = RealNumber.FromDouble(AValue);
            var b = RealNumber.FromDouble(BValue);
            Result = (a * b).ToString();
        }

        private void Div()
        {
            var a = RealNumber.FromDouble(AValue);
            var b = RealNumber.FromDouble(BValue);
            Result = (a / b).ToString();
        }
    }
}
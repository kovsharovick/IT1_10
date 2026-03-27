using RealNumberApp.Models;
using System.Windows.Input;

namespace RealNumberApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        // 🔹 Число A
        public int ASign { get; set; } = 1;
        public double AMantissa { get; set; }
        public int AExponent { get; set; }

        // 🔹 Число B
        public int BSign { get; set; } = 1;
        public double BMantissa { get; set; }
        public int BExponent { get; set; }

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

        private RealNumber CreateA() =>
            new RealNumber(ASign, AMantissa, AExponent);

        private RealNumber CreateB() =>
            new RealNumber(BSign, BMantissa, BExponent);

        private void Add()
        {
            Result = (CreateA() + CreateB()).ToString();
        }

        private void Sub()
        {
            Result = (CreateA() - CreateB()).ToString();
        }

        private void Mul()
        {
            Result = (CreateA() * CreateB()).ToString();
        }

        private void Div()
        {
            Result = (CreateA() / CreateB()).ToString();
        }
    }
}
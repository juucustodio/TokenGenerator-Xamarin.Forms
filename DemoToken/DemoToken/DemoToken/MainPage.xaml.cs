using OtpSharp;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace DemoToken
{
    public partial class MainPage : ContentPage
    {
        public string Codigo
        {
            get => _codigo;
            set
            {
                _codigo = value;
                OnPropertyChanged();
            }
        }
        private string _codigo;

        public string Segundos
        {
            get => _segundos;
            set
            {
                _segundos = value;
                OnPropertyChanged();
            }
        }
        private string _segundos;

        private readonly byte[] secretKey;
        private Totp totp;
        private Timer timer;

        public MainPage()
        {
            //Identificador
            string Id = "100";

            InitializeComponent();
            BindingContext = this;

            secretKey = Encoding.UTF8.GetBytes(Id);
        }

        public void Callback(object state)
        {
            totp = new Totp(secretKey);

            Codigo = totp.ComputeTotp();
            var remainingTime = totp.RemainingSeconds();
            Segundos = $"00:{remainingTime:00}";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            timer = new Timer(Callback, null, 0, 1000);
        }

        protected override void OnDisappearing()
        {
            timer.Dispose();
            base.OnDisappearing();
        }
    }
}

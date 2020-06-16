using DebugConsole.Implementations.WPF;
using System.ComponentModel;
using System.Windows;

namespace DebugConsole.TestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DebugConsole.Handler = new TextBoxItemsControlDebugHandler(Input, Output);
            this.DataContext = this;
        }

        private string _text = "hello";
        
        [DebugProperty]
        public static string StaticText
        {
            get => (Application.Current.MainWindow as MainWindow).Text;
            set => (Application.Current.MainWindow as MainWindow).Text = value;
        }

        public string Text
        {
            get => _text;
            set {
                _text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

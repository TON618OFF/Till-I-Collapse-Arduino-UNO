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
using System.Timers;
using System.IO.Ports;



namespace practice_4
{
    public partial class MainWindow : Window
    {
        private SerialPort serialPort;

        public MainWindow()
        {
            InitializeComponent();
            serialPort = new SerialPort("COM5", 9600);
            serialPort.Open();
            serialPort.DataReceived += SerialPort_DataReceived;
        }

        private void first_btn_Click(object sender, RoutedEventArgs e)
        {
            string selectedTime = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();

            string delay = "100"; 
            if (selectedTime == "0.1 sec") delay = "100";
            if (selectedTime == "0.5 sec") delay = "500";
            if (selectedTime == "1 sec") delay = "1000";

            serialPort.WriteLine("LED_ON_" + delay);
            LabelText.Content = $"Светодиоды моргают с частотой {selectedTime}";
        }

        private void second_btn_Click(object sender, RoutedEventArgs e)
        {
            serialPort.WriteLine("PLAY_MELODY");
            LabelText.Content = "Играет мелодия";
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = serialPort.ReadLine();
            Dispatcher.Invoke(() =>
            {
                if (data.Contains("Button1"))
                {
                    LabelText.Content = "Нажата первая кнопка";
                }
                else if (data.Contains("Button2"))
                {
                    LabelText.Content = "Нажата вторая кнопка";
                }
            });
        }

        private void Window_Closing(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace ProjectParking
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            ArduinoFound();
        }
        SerialPort currentPort= new SerialPort();
        private delegate void updateDelegate(string txt);

        private void Button_add_Click(object sender, RoutedEventArgs e)
        {
            
            Move(1);
        }

        private void Button_Grap_Click(object sender, RoutedEventArgs e)
        {
            
            Move(2);
        }

        Dictionary<string, DateTime> time = new Dictionary<string, DateTime>();

        Dictionary<string, int> dictionary = new Dictionary<string, int>();

        private void Move(int n)
        {
            if (!currentPort.IsOpen) {TextBlock1.Text = "ПНХ"; return; }
            TextBlock1.Text = "OK";
            currentPort.Write("0");
            string Code_Card = currentPort.ReadLine();
            if (n == 1)
            {
                for (int i = 1; i <= 24; i++)
                {
                    if (!dictionary.ContainsValue(i))
                    {
                        dictionary.Add(Code_Card, i);
                        time.Add(Code_Card, DateTime.Now);
                        if (!currentPort.IsOpen) return;
                        currentPort.Write("1 "+ i);
                        break;
                    }
                }
            }
            else if (n == 2)
            {
                if (dictionary.ContainsKey(Code_Card))
                {
                    int k = 0;
                    dictionary.TryGetValue(Code_Card, out k);
                    dictionary.Remove(Code_Card);
                    TimeSpan t = new TimeSpan();
                    t = DateTime.Now - time[Code_Card];
                    time.Remove(Code_Card);
                    int m = 0;
                    if (!currentPort.IsOpen) return;
                    currentPort.Write("2 "+k);
                    m = t.Seconds + t.Minutes * 60 + t.Hours * 3600;
                    TextBlock1.Text = n + " " + k + " TIME " + m;

                }
                else { TextBlock1.Text = "ПНХ"; }
            }

        }


   
       
        
        private bool ArduinoDetected()
        {
            try
            {
                currentPort.Open();
                System.Threading.Thread.Sleep(500);
                // небольшая пауза, ведь SerialPort не терпит суеты 

                string returnMessage = currentPort.ReadLine();
                currentPort.Close();
                if (returnMessage == null) { return false; }
                // необходимо чтобы void loop() в скетче содержал код Serial.println("Info from Arduino"); 
                if (returnMessage.Contains("Info from Arduino"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        private void ArduinoFound()
        {
            bool ArduinoPortFound = false;

            try
            {
                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    currentPort = new SerialPort(port, 9600 , Parity.None, 8, StopBits.One);
                    if (ArduinoDetected())
                    {
                        ArduinoPortFound = true;
                        break;
                    }
                    else
                    {
                        ArduinoPortFound = false;
                    }
                }
            }
            catch { }

            if (ArduinoPortFound == false) return;

            System.Threading.Thread.Sleep(1000); // wait a lot after closing

            currentPort.BaudRate = 9600;
            currentPort.DtrEnable = true;
            currentPort.ReadTimeout = 1000;
            try
            {
                currentPort.Open();
            }
            catch { }
        }
    }
}

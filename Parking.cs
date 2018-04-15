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
            

        }

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
            string Code_Card = " ";
            if (n == 1)
            {
                for (int i = 1; i <= 24; i++)
                {
                    if (!dictionary.ContainsValue(i))
                    {
                        dictionary.Add(Code_Card, i);
                        time.Add(Code_Card, DateTime.Now);
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
                    m = t.Seconds + t.Minutes * 60 + t.Hours * 3600;
                    TextBlock1.Text = n + " " + k + " TIME " + m;

                }
                else { TextBlock1.Text = "ПНХ"; }
            }

        }


    }

}

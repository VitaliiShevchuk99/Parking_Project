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
        Dictionary<string,int> dictionary = new Dictionary< string, int>();


        private void Button_add_Click(object sender, RoutedEventArgs e)
        {
            Move(1);
        }

        private void Button_Grap_Click(object sender, RoutedEventArgs e)
        {
            Move(2);
        }
        private void Move(int n){
            string Code_Card="";
            if (n == 1)
            {
                for(int i = 1; i <= 24; i++)
                {
                    if (!dictionary.ContainsValue(i))
                    {
                        dictionary.Add(Code_Card, i);

                        break;
                    }
                }
            }else if (n == 2)
            {
                int k = 0;
                dictionary.TryGetValue(Code_Card,out k);
                TextBlock1.Text=n+ " "+k;
            }
        
        }


       
    }

}

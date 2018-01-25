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
using System.Windows.Shapes;

namespace AAI_BOT
{

    public partial class MainBot : Window
    {

        public string[,] buttons = new string[3, 2] { {"Рад помоч!", "Пшел нахуй!" }, {"Ебаш инфу", "Про замая пизди"}, {"none", "none"} };
        int k = 0;

        public MainBot()
        {
            InitializeComponent();
            CreateMess(buttons[0, 0], false);
            CreateMess(buttons[1, 1], true);
        }

        public void CreateMess(string mess, bool im)
        {
            Label lab = new Label();
            lab.Content = mess;
            lab.Foreground = Brushes.Black;
            //lab.FontSize = 26;

            if (im)
            {
                lab.Background = new SolidColorBrush(Color.FromArgb(255, 136, 217, 230));
                lab.HorizontalAlignment = HorizontalAlignment.Right;
            }
            else
            {
                lab.Background = new SolidColorBrush(Color.FromArgb(255, 138, 161, 177));
                lab.HorizontalAlignment = HorizontalAlignment.Left;
            }

            lab.VerticalAlignment = VerticalAlignment.Top;

            lab.Margin = new Thickness(10, 10 * k + 10, 10, 0);
            lab.Padding = new Thickness(5, 5, 5, 5);
            la.Children.Add(lab);
            k++;
        }
    }
}

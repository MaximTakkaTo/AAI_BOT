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
    public partial class Console : Window
    {
        public TextBox[] tb = new TextBox[25];
        public Label[] lab = new Label[25];
        int k = 0;

        public Console()
        {
            InitializeComponent();

            lab[k] = new Label();
            scroll.Children.Add(lab[k]);
            lab[k].Margin = new Thickness(0, 0, 0, 0);
            lab[k].Foreground = Brushes.Lime;
            lab[k].FontFamily = new FontFamily("Lucida Console");
            lab[k].FontSize = 12;
            lab[k].Content = "Хорошая работа, Олег!";

            tb[k] = new TextBox();
            scroll.Children.Add(tb[k]);
            tb[k].Foreground = Brushes.Lime;
            tb[k].Background = Brushes.Black;
            tb[k].FontSize = 16;
            tb[k].BorderThickness = new Thickness(0);
            tb[k].FontFamily = new FontFamily("Lucida Console");
            tb[k].Margin = new Thickness(lab[k].Content.ToString().Length * 6.5d, 5, 0, 0);
            tb[k].Focus();
            tb[k].KeyDown += new KeyEventHandler(Press);

        }

        private void Press(object sender, KeyEventArgs e)
        {
            var str = tb[k].Text.Replace(" ", "");
            if (e.Key == Key.Enter && str != "")
            {
                tb[k].IsReadOnly = true;
                k++;

                lab[k] = new Label();
                scroll.Children.Add(lab[k]);
                lab[k].Margin = new Thickness(0, 15 * k, 0, 0);
                lab[k].Foreground = Brushes.Lime;
                lab[k].Content = "Хорошая работа, Олег!";
                lab[k].FontFamily = new FontFamily("Lucida Console");
                lab[k].FontSize = 12;

                tb[k] = new TextBox();
                scroll.Children.Add(tb[k]);
                tb[k].Margin = new Thickness(lab[k].Content.ToString().Length * 6.5d + 15, 15 * k + 5, 0, 0);
                tb[k].FontFamily = new FontFamily("Lucida Console");
                tb[k].Foreground = Brushes.Lime;
                tb[k].Background = Brushes.Black;
                tb[k].FontSize = 12;
                tb[k].BorderThickness = new Thickness(0);
                tb[k].Focus();
                tb[k].KeyDown += new KeyEventHandler(Press);
            }
        }
    }
}

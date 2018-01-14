using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        
            for(int i = 0; i < tb.Length;i++ )
            {
                lab[i] = new Label();
                tb[i] = new TextBox();
                lab[i].Foreground = Brushes.Lime;
                lab[i].FontFamily = new FontFamily("Consolas");
                lab[i].FontSize = 14;
                lab[i].Margin = new Thickness(0, 0, 0, 0);

                tb[i].Foreground = Brushes.Lime;
                tb[i].Background = Brushes.Black;
                tb[i].BorderThickness = new Thickness(0);
                tb[i].FontSize = 14;
                tb[i].FontFamily = new FontFamily("Consolas");
            }

            scroll.Children.Add(lab[k]);
            lab[k].Content = "AI BOT PASHA - HUESOS";
            NextTb();
        }

        private void Press(object sender, KeyEventArgs e)
        {
            var str = tb[k].Text.Replace(" ", "");
            if (e.Key == Key.Enter && str != "")
                NextTb();
        }
        
        private void NextTb()
        {
            tb[k].IsReadOnly = true;
            k++;
            scroll.Children.Add(lab[k]);
            if(k == 1)
                lab[k].Margin = new Thickness(0, 25, 0, 0);
            else
                lab[k].Margin = new Thickness(0, 15 * k + 10, 0, 0);

            lab[k].Content = "Хорошая работа, Олег!>";
            scroll.Children.Add(tb[k]);
            tb[k].Margin = new Thickness(lab[k].Content.ToString().Length * 6.5d + 31,15 * k + 15, 0, 0);
            tb[k].Focus();
            tb[k].KeyDown += Press;
        }
    }
}

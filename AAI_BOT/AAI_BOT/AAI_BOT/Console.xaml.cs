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
        int log = 0;

        public string[] codes = new string[5] { "", "admin", "admin", "Pasha", "Oleg" };
        public string[] mess = new string[5] { "AI BOT HAS ACTIVATED", "LOGIN>", "PASSWORD>", "GOD.NAME>", "RETARD.NAME>" };

        string errMess = "WRONG! PLEASE REPEAT.";

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
            lab[k].Content = mess[k];
            lab[k].HorizontalAlignment = HorizontalAlignment.Center;
            NextTb();
        }

        private void Press(object sender, KeyEventArgs e)
        {
            var str = tb[k].Text.Replace(" ", "");
            if (e.Key == Key.Enter && str != "")
                    if (str.ToLower() == codes[k - log].ToLower())
                    {
                        if ((k - log + 1) != codes.Length)
                            NextTb();
                        else
                        {
                            MessageBox.Show("BOT ACTIVATED");
                            tb[k].IsReadOnly = true;
                        }
                    }
                    else
                    {
                        log++;
                        Label err = new Label();
                        scroll.Children.Add(err);
                        err.Foreground = Brushes.Red;
                        err.FontFamily = new FontFamily("Consolas");
                        err.Content = errMess;
                        err.FontSize = 14;
                        err.Margin = new Thickness(0, tb[k].Margin.Top + 10, 0, 0);
                        NextTb();
                    }
        }
        
        private void NextTb()
        {
            tb[k].IsReadOnly = true;
            k++;
            scroll.Children.Add(lab[k]);
            if(k == 1)
                lab[k].Margin = new Thickness(0, 25, 0, 0);
            else
                lab[k].Margin = new Thickness(0, 15 * (k + log) + 10, 0, 0);

            lab[k].Content = mess[k - log];
            scroll.Children.Add(tb[k]);
            lab[k].Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            tb[k].Margin = new Thickness(lab[k].DesiredSize.Width - 5, 15 * (k + log) + 15, 0, 0);
            tb[k].Focus();
            tb[k].KeyDown += Press;
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading.Tasks;

namespace AAI_BOT
{
    public partial class Console : Window
    {
        public TextBox[] tb = new TextBox[25];
        public Label[] lab = new Label[25];
        int k = 0;
        int log = 0;
        bool f1 = false;
        int cls;

        public string[] codes = new string[6] { "", "admin", "admin","" , "Pasha", "Oleg" };
        public string[] mess = new string[6] { "AI BOT HAS ACTIVATED", "LOGIN>", "PASSWORD>", "                                                                                                                                                   ", "GOD.NAME>", "RETARD.NAME>" };

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

        async Task Pause()
        {
            await Task.Delay(500);
        }

        private async void Press(object sender, KeyEventArgs e)
        {
            f1 = false;
            var str = tb[k].Text.Replace(" ", "");
            if (e.Key == Key.Enter)
            {
                if ((k - log) == 1 || (k - log) == 2 && (k - log) != 3)
                {
                    if (str == codes[k - log])
                    { 
                        f1 = false;
                        NextTb();
                    }
                    else
                        f1 = true;
                }
                else
                {
                    if (str.ToLower() == codes[k - log].ToLower())
                    {
                        NextTb();
                        f1 = false;
                    }
                    else
                        f1 = true;
                }
                if (f1)
                    Eror(errMess);
                if (k - log == 3)
                {
                    lab[k - log].Content = "Loading ";
                    for (int i = 0; i < 7; i++)
                    {
                        lab[k - log].Content += ". ";
                        await Pause();
                    }
                    clscr();
                    NextTb();
                }
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
                lab[k].Margin = new Thickness(0, 15 * (k + log - cls) + 10 , 0, 0);

            lab[k].Content = mess[k - log];
            scroll.Children.Add(tb[k]);
            lab[k].Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            tb[k].Margin = new Thickness(lab[k].DesiredSize.Width - 5, 15 * (k + log - cls) + 15, 0, 0);
            tb[k].Focus();
            tb[k].KeyDown += Press;
        }

        private void Eror(string mess)
        {
            Label err = new Label();
            log++;
            err.FontFamily = new FontFamily("Consolas");
            err.Content = mess;
            err.FontSize = 14;
            err.Margin = new Thickness(0, tb[k].Margin.Top + 10, 0, 0);
            err.Foreground = Brushes.Red;
            NextTb();          
            scroll.Children.Add(err);
        }
        
        private void clscr()
        {
            cls = k + 2 + log;
            scroll.Children.Clear();
        }
    }
}

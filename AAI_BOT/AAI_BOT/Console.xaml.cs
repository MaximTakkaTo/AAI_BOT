using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Globalization;

namespace AAI_BOT
{
    public partial class Console : Window
    {
        public TextBox[] tb = new TextBox[50];
        public Label[] lab = new Label[50];
        int k = 0;
        int log = 0;
        bool f1 = false;
        int cls;
        string time,report;

        public string[] codes = new string[23] { "", "admin", "admin", "", "", "", "/status", "1", "2", "4", "5", "6", "7", "8", "9", "10", "11", "/firmware", "/repair", "1", "12", "/login",""};
        public string[] mess = new string[23] { "AI BOT v1.0.2.8.2.0 HAS ACTIVATED", "login:", "password:", "", "Welcome to the technical terminal of AI BOT of SCHOOL 38", "In this terminal you can find out the status of the bot and get to the main terminal of the bot.(Use /status)", "38school@AI_BOT$>","","", "===========================================", "Block 1: GOOD", "Block 2: GOOD", "RAM: GOOD", "Firmware : BAD (BROKEN)", "CPU: GOOD", "===========================================", "A fault has been found in the firmware (You can enter the firmware block and fix the problem (use /firmware and then /repair))", "38school@AI_BOT$>", "38school@AI_BOT:~/Firmware$>", "", "Successfully completed!Now you can enter the main terminal of the bot (use /login).", "38school@AI_BOT:~/Firmware$>", "" };



        string errMess = "A BREAKDOWN WAS DETECTED.THIS COMMAND DOESN\'T WORK!";
        public Console()
        {
            InitializeComponent();

            view.Cursor = Cursors.None;
            for(int i = 0; i < tb.Length;i++)
            {
                lab[i] = new Label();
                tb[i] = new TextBox();
                lab[i].Foreground = Brushes.Lime;
                lab[i].FontFamily = new FontFamily("Consolas");
                lab[i].FontSize = 14;
                lab[i].Margin = new Thickness(0, 0, 0, 0);
                lab[i].Cursor = Cursors.None;

                tb[i].Foreground = Brushes.Lime;
                tb[i].Background = Brushes.Black;
                tb[i].BorderThickness = new Thickness(0);
                tb[i].FontSize = 14;
                tb[i].FontFamily = new FontFamily("Consolas");
                tb[i].Cursor = Cursors.None;
            }   
            scroll.Children.Add(lab[k]);
            lab[k].Content = mess[k];
            lab[k].HorizontalAlignment = HorizontalAlignment.Center;
            NextTb();
        }

        async Task Pause(int time)
        {
            await Task.Delay(time);
        }

        private async void Press(object sender, KeyEventArgs e)
        {
            Time();
            f1 = false;
            var str = tb[k].Text.Replace(" ", "");
            if (e.Key == Key.Enter && str != "")
            {
                if (((k - log) == 1 || (k - log) == 2) | (k - log) != 7)
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
                {
                    if((k - log) == 6 || (k - log) == 1 || (k - log) == 2)
                    {
                        if (k - log == 6)
                        {
                            Eror("An error or breakdown was detected in the bot.Only the /status command is available");
                        }
                        if ((k - log) == 1 || (k - log) == 2)
                        {
                            if (k - log == 1)
                                Eror("WRONG LOGIN. TRY AGAIN!");
                            if (k - log == 2)
                                Eror("WRONG PASSWORD. TRY AGAIN!");
                        }
                    }
                    else
                        Eror(errMess);
                }

                if (k - log == 3)
                {
                    Label kak = new Label();
                    kak.Content = "Loading ";
                    scroll.Children.Add(kak);
                    kak.Foreground = Brushes.Lime;
                    kak.FontFamily = new FontFamily("Consolas");
                    kak.FontSize = 14;
                    kak.Margin = new Thickness(0, lab[k].Margin.Top, 0, 0);
                    tb[k].KeyDown -= Press;
                    tb[k].IsReadOnly = true;
                    for (int i = 0; i < 7; i++)
                    {
                        kak.Content += ". ";
                        await Pause(500);
                    }
                    clscr();
                    NextTb();
                }
                if(k - log == 4)
                {
                    lab[k].HorizontalAlignment = HorizontalAlignment.Center;
                    tb[k].HorizontalAlignment = HorizontalAlignment.Center;
                    NextTb();
                    lab[k].HorizontalAlignment = HorizontalAlignment.Center;
                    tb[k].HorizontalAlignment = HorizontalAlignment.Center;
                    NextTb();
                    lab[k].Margin = new Thickness(0, 25, 0, 0);
                    tb[k].Margin = new Thickness(lab[k].DesiredSize.Width - 5, 30, 0, 0);
                }
                if (k - log == 7)
                {
                    mess[8] = report + ":";
                    Label da = new Label();
                    da.Content = "Loading ";
                    scroll.Children.Add(da);
                    da.Foreground = Brushes.Lime;
                    da.FontFamily = new FontFamily("Consolas");
                    da.FontSize = 14;
                    da.Margin = new Thickness(0, lab[k].Margin.Top, 0, 0);
                      tb[k].KeyDown -= Press;
                    tb[k].IsReadOnly = true;
                    for (int i = 0; i < 7; i++)
                    {
                        da.Content += ". ";
                        await Pause(500);
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        NextTb();
                    }
                    lab[k - 1].Foreground = Brushes.Red;
                    lab[k - 4].Foreground = Brushes.Red;
                }
                if ((k - log) == 19)
                {
                    mess[8] = report + ":";
                    Label da = new Label();
                    da.Content = "Loading ";
                    scroll.Children.Add(da);
                    da.Foreground = Brushes.Lime;
                    da.FontFamily = new FontFamily("Consolas");
                    da.FontSize = 14;
                    da.Margin = new Thickness(0, lab[k].Margin.Top, 0, 0);
                    tb[k].KeyDown -= Press;
                    tb[k].IsReadOnly = true;
                    for (int i = 0; i < 7; i++)
                    {
                        da.Content += ". ";
                        await Pause(1000);
                    }
                    NextTb();
                    NextTb();
                }
                if ((k - log) == 22)
                    MessageBox.Show("Вы успешно прошли эту хуйню. Поздравляю.");
            }
        }   

        private void NextTb()
        {   
            tb[k].IsReadOnly = true;
            k++;
            scroll.Children.Add(lab[k]);
            if (k == 1)
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

        private void Time()
        {
            DateTime localDate = DateTime.Now;
            var culture = new CultureInfo("ru-RU");
            time = localDate.ToString(culture);
            report = "Status report at " + time;
        }
    }
}

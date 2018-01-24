using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;

namespace AAI_BOT
{
    public partial class Console : Window
    {
        public TextBox[] tb = new TextBox[50];
        public Label[] lab = new Label[50];
        int k = 0;//Счетчик лейблов
        int log = 0;//Счетчик лейблов - ошибок.
        bool f1 = false; //Флаг неправильно введенной команды.
        int cls;
        string time,report;

        public string[] mess = new string[23] // Массив строк с текстами для label'ов
        {
            "AI BOT v1.0.2 АКТИВИРОВАН",
            "login:",
            "password:",
            "",//loading
            "Добро пожаловать в технический терминал бота c ИИ 38 школы",
            "В этом терминале вы можете посмотреть техническое сотояние (статус) бота и войти в основной терминал.(Для просмотра статуса бота используйте команду: /status)",
            "38school@AI_BOT$>",
            "",//loading
            "",//status
            "===========================================",
            "БЛОК 1: ВСЁ ИСПРАВНО",
            "БЛОК 2: ВСЁ ИСПРАВНО",
            "ОЗУ: ВСЁ ИСПРАВНО",
            "ПРОШИВКА: ОБНАРУЖЕНА НЕИСПРАВНОСТЬ ",
            "ЦП: ВСЁ ИСПРАВНО",
            "===========================================",
            "Была обнаружена поломка в прошивке бота.(Вы можете войти в блок прошивки бота, использовав команду: /firmware; и исправить ошибку в блоке прошивки, использовав комаду: /repair).",
            "38school@AI_BOT$>", "38school@AI_BOT:~/Firmware$>",
            "load",
            "Успешно выполнено! Теперь вы можете войти в основной терминал бота, использовав команду: /login.",
            "38school@AI_BOT:~/Firmware$>",
            ""//end
        };

        public string[] codes = new string[23]; //Массив строк с ответами

        public Console()
        {
            InitializeComponent();
            //Настройка логина и пароля
            settLog();
            StreamReader sl = new StreamReader("login.txt");
            StreamReader sp = new StreamReader("password.txt");
            //Присваивание значений массиву команд
            codes[1] = sl.ReadLine();
            codes[2] = sp.ReadLine();
            codes[6] = "/status";
            codes[17] = "/firmware";
            codes[18] = "/repair";
            codes[21] = "/login";
            view.Cursor = Cursors.None;
            //Стандартные значения для всех label'ов и textbox'ов 
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
        //Метод, который вызывается при нажатии на клавишу ENTER. 
        private async void Press(object sender, KeyEventArgs e)
        {
            Time();
            f1 = false;
            var str = tb[k].Text.Replace(" ", ""); //Убирание пробелов из строк.
            //Общие случаи после ввода команды. 
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
                            Eror("Обнаружена ошибка или неисправность бота. Используйте команду: /status, для получения информации");
                        }
                        if ((k - log) == 1 || (k - log) == 2)
                        {
                            if (k - log == 1)
                                Eror("Неправильный логин! Попробуйте ещё раз!");
                            if (k - log == 2)
                                Eror("Неправильный пароль! Попробуйте ещё раз!");
                        }
                    }
                    else
                        Eror("ОБНАРУЖЕНА ПОЛОМКА! ЭТА КОМАНДА НЕ РАБОТАЕТ!");
                }
                //Частные случаи после ввода команды
                if (k - log == 3)
                {
                    Label kak = new Label();
                    kak.Content = "Загрузка ";
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
                    da.Content = "Загрузка ";
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
                        NextTb();
                    lab[k - 1].Foreground = Brushes.Red;
                    lab[k - 4].Foreground = Brushes.Red;
                }
                if ((k - log) == 19)
                {
                    mess[8] = report + ":";
                    Label da = new Label();
                    da.Content = "Загрузка ";
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
        // Метод, который "переводит курсор на новую строчку".
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
        //Метод, который выводит сообщение - ошибку.
        //Параметры mess - строка, которую нужно вывести.
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

        //Ассинхронный метод задержки.
        //Параметры time - количество времени в МС, на которое нужно задержать выполнение
        async Task Pause(int time)
        {
            await Task.Delay(time);
        }
        //Метод, который "очищает экран" путем удаления элементов с Grid'а
        private void clscr()
        {
            cls = k + 2 + log;
            scroll.Children.Clear();
        }
        //Метод, который формирует строку статус - репорта. 
        private void Time()
        {
            DateTime localDate = DateTime.Now;
            var culture = new CultureInfo("ru-RU");
            time = localDate.ToString(culture);
            report = "Статус репорт на " + time;
        }
        //Метод, который проверяет файлы настроек логина и пароля.
        //При наличии файла: продолжает выполнять программу.
        //При отсутствии файла: создает новый и записывает в него строку "admin".
        private void settLog()
        {
            try
            {
                StreamReader sr = new StreamReader("login.txt");
                sr.Close();
            }
            catch (FileNotFoundException)
            {
                StreamWriter sw = new StreamWriter("login.txt");
                sw.WriteLine("admin");
                sw.Close();
            }
            
            try
            {
                StreamReader sr = new StreamReader("password.txt");
                sr.Close();
            }
            catch (FileNotFoundException)
            {
                StreamWriter sw = new StreamWriter("password.txt");
                sw.WriteLine("admin");
                sw.Close();
            }
        }
    }
}

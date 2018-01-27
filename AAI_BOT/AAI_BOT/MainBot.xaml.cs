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
        //buttons - текст для кнопок
        //botMess - сообщения бота
        public string[,] buttons = new string[3, 2] { {"Рад помоч!", "Пшел нахуй!" }, {"Ебаш инфу", "Про замая пизди"}, {"none", "none"} };
        public string[,] botMess = new string[3, 2] { { "Спасибо, что починил меня, Олег!", "" }, { "Вам что-нибудь нужно?", "Долбоеб, что хотел?" }, { "none", "none" } };
        //k - кол-во лейблов
        //l - нужно, чтобы сообщение от батонов сдвигалось нормально
        int k = 0, l = 0;
        string source = @"F:\projects\AIProjectX\AAI_BOT\AAI_BOT\bin\Debug\stalin.jpg";

        public MainBot()
        {
            InitializeComponent();
            First.Content = buttons[k, 0];
            Second.Content = buttons[k, 1];
            CreateMess(botMess[k, 0], false);
            k++;
        }

        //Создание сообщения
        //mess - текст сообщения
        //im - true - пользователь отправил / false - бот отправил
        public void CreateMess(string mess, bool im)
        {
            Label lab = new Label();
            lab.Content = mess;
            lab.Foreground = Brushes.Black;

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

            lab.Margin = new Thickness(10, 25 * (k + l) + 10, 10, 0);
            lab.Padding = new Thickness(5, 5, 5, 5);
            la.Children.Add(lab);
        }

        //нормальные ответ
        private void Answer(object sender, RoutedEventArgs e)
        {
            //создание сообщения от пользователя
            CreateMess(buttons[k - 1, 0], true);
            //свдиг для ответа бота и создание его сообщения
            l++;
            CreateMess(botMess[k, 0], false);
            //следующие сообщения пользователя buttons
            First.Content = buttons[k, 0];
            Second.Content = buttons[k, 1];
            k++;
            if (k == 3)
                ShowImg();
        }

        //плохие ответы
        private void AnswerBad(object sender, RoutedEventArgs e)
        {
            //создание сообщения от пользователя
            CreateMess(buttons[k - 1, 1], true);
            //свдиг для ответа бота и создание его сообщения
            l++;
            CreateMess(botMess[k, 1], false);
            //следующие сообщения пользователя buttons
            First.Content = buttons[k, 0];
            Second.Content = buttons[k, 1];
            k++;
            if (k == 3)
                ShowImg();
        }

        //Показ картиночки
        public void ShowImg()
        {
            Border bord = new Border();
            bord.Margin = new Thickness(10, 25 * (k + l - 1) + 10, 0, 0);
            bord.Padding = new Thickness(5, 5, 5, 5);
            bord.HorizontalAlignment = HorizontalAlignment.Left;
            bord.VerticalAlignment = VerticalAlignment.Top;
            //размер / пишешь любую ширину, чтобы найти высоту, то ширину умножаешь на 1,398907103825137 (можешь округлить до целого)
            //1,398907103825137 - коэф. высчитывается, как оригинальная высота/оригинальная ширина, или наоборот
            bord.Width = 170; 
            bord.Height = 237;
            bord.Background = new SolidColorBrush(Color.FromArgb(255, 138, 161, 177));
            la.Children.Add(bord);
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(source));
            bord.Child = img;
        }
        
    }
}

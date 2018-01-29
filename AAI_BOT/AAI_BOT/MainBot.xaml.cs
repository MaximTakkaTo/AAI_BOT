﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace AAI_BOT
{
    public partial class MainBot : Window
    {
        //buttons - текст для кнопок
        public string[,] buttons = new string[4, 2] 
        { 
            {"Рады помочь!", "А кто тебя сломал?" },
            {"","" },
            {"Да, давай, рассказывай!", "Нет, нам это не интересно"},
            {"", ""}
        };

        //botMess - сообщения бота
        public string[,] botMess = new string[4, 2] 
        { 
            { "Спасибо, что починил меня, Пользователь!", "" },
            { "Помню только нежные руки и тонкие пальцы.", "Чёрт. Я даже не запомнил.\nПомню только то, что кто - то тонкими пальцами нажимал клавиши на моей клавиатуре."},
            { "Тайна КПСС. Секреты Сталина и его преспешников. Хотите узнать больше?", "Тайна КПСС. Секреты Сталина и его преспешников. Хотите узнать больше?" },
            { "s", "s" }
        };

        //k - кол-во лейблов
        //l - нужно, чтобы сообщение от батонов сдвигалось нормально
        int k = 0, l = 0;
        float m = 0;

        string source = @"Pictures/Stalin.png";//Картинка Сталина.

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
        public async void CreateMess(string mess, bool im)
        {
            StackPanel sp = new StackPanel();
            Polygon pol = new Polygon();
            PointCollection polygonPoints = new PointCollection();
            Point Point1 = new Point(0, 0);
            Point Point2 = new Point(32, 0);
            Point Point3 = new Point(32, -20);
            polygonPoints.Add(Point1);
            polygonPoints.Add(Point2);
            polygonPoints.Add(Point3);
            pol.Points = polygonPoints;
            pol.Fill = new SolidColorBrush(Color.FromArgb(255, 138, 161, 177));
            sp.Children.Add(pol);

            StackPanel sp1 = new StackPanel();
            Polygon pol1 = new Polygon();
            PointCollection polygonPoints1 = new PointCollection();
            Point Point4 = new Point(0, 0);
            Point Point5 = new Point(-32, 0);
            Point Point6 = new Point(-32, -20);
            polygonPoints1.Add(Point4);
            polygonPoints1.Add(Point5);
            polygonPoints1.Add(Point6);
            pol1.Points = polygonPoints1;
            pol1.Fill = new SolidColorBrush(Color.FromArgb(255, 138, 233, 248));
            sp1.Children.Add(pol1);

            Label lab = new Label();
            lab.Content = mess;
            lab.FontSize = 24;
            lab.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            Border br = new Border();
            br.BorderBrush = Brushes.Black;
            br.Width = lab.DesiredSize.Width + 10;
            br.Height = lab.DesiredSize.Height + 10;
            br.CornerRadius = new CornerRadius(15);
            br.Child = lab;
            br.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            if (im)
            {
                sp1.Margin = new Thickness(0, 60 * (k + l + m) + lab.Margin.Top + br.DesiredSize.Height + 10, 18, 0);
                sp1.HorizontalAlignment = HorizontalAlignment.Right;
                br.Background = new SolidColorBrush(Color.FromArgb(255, 138, 233, 248));
                br.Margin = new Thickness(0, 60 * (k + l + m) + 10, 10 + 27, 0);
                br.HorizontalAlignment = HorizontalAlignment.Right;
            }
            else
            {
                sp.Margin = new Thickness(18, 60 * (k + l + m) + lab.Margin.Top + br.DesiredSize.Height + 10, 0, 0);

                if (k == 2)
                {
                    ShowImg();
                    sp.Margin = new Thickness(18, 60 * (k + l + m) + lab.Margin.Top + br.DesiredSize.Height + 10, 0, 0);
                }
                    
                br.Background = new SolidColorBrush(Color.FromArgb(255, 138, 161, 177));
                br.Margin = new Thickness(10 + 27, 60 * (k + l + m) + 10, 10, 0);
                br.HorizontalAlignment = HorizontalAlignment.Left;
                Typing.Content = "Бот пишет";
                Typing.Visibility = Visibility.Visible;
                First.Visibility = Visibility.Collapsed;
                Second.Visibility = Visibility.Collapsed;
                for (int i = 0; i < 8; i++)
                {
                    Typing.Content += " .";
                    await Pause(500);
                    if (i % 3 == 0)
                        Typing.Content = "Бот пишет";
                }
                Typing.Visibility = Visibility.Collapsed;
                First.Visibility = Visibility.Visible;
                Second.Visibility = Visibility.Visible;
            }
            br.VerticalAlignment = VerticalAlignment.Top;
            la.Children.Add(sp);
            la.Children.Add(sp1);
            br.Padding = new Thickness(5, 5, 5, 5);
            la.Children.Add(br);
        }       

        //нормальные ответы
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
        }

        //Показ картиночки
        public void ShowImg()
        {
            Border bord = new Border();
            bord.Margin = new Thickness(37, 65 * (k + l), 0, 0);
            bord.Padding = new Thickness(5, 5, 5, 5);
            bord.HorizontalAlignment = HorizontalAlignment.Left;
            bord.VerticalAlignment = VerticalAlignment.Top;

            StackPanel sp = new StackPanel();
            Polygon pol = new Polygon();
            PointCollection polygonPoints = new PointCollection();
            Point Point1 = new Point(0, 0);
            Point Point2 = new Point(32, 0);
            Point Point3 = new Point(32, -20);
            polygonPoints.Add(Point1);
            polygonPoints.Add(Point2);
            polygonPoints.Add(Point3);
            pol.Points = polygonPoints;
            pol.Fill = new SolidColorBrush(Color.FromArgb(255, 138, 161, 177));
            sp.Children.Add(pol);
            sp.Margin = new Thickness(18, 497, 0, 0);
            la.Children.Add(sp);

            //размер / пишешь любую ширину, чтобы найти высоту, то ширину умножаешь на 1,398907103825137 (можешь округлить до целого)
            //1,398907103825137 - коэф. высчитывается, как оригинальная высота/оригинальная ширина, или наоборот
            bord.Width = 170; 
            bord.Height = 237;
            bord.Background = new SolidColorBrush(Color.FromArgb(255, 138, 161, 177));
            la.Children.Add(bord);
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(source, UriKind.Relative));
            bord.Child = img;
            m = 4.3F;
        }

        async Task Pause(int time)
        {
            await Task.Delay(time);
        }
    }
}

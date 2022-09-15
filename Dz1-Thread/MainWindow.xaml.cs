using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

namespace Dz1_Thread
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "notepad.exe";
            proc.Start();
            proc.WaitForExit();
            MessageBox.Show("Код процесса " + proc.ExitCode);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button b1 = new Button();
            Process proc = new Process();
            proc.StartInfo.FileName = "notepad.exe";
            proc.Start();
            if (MessageBox.Show("Остановить текущий процесс?",
                     "Save file",
                     MessageBoxButton.YesNo,
                     MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                proc.Kill();
            }
            proc.WaitForExit();          
            MessageBox.Show("Код процесса " + proc.ExitCode);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            double a = 3;
            double b = 7;
            var thread = new Thread(() => Sum(a, b, '+'));
            thread.Start();
            thread.Join();

        }
        public double Sum(double a, double b, char oper)
        {
            double total = 0;
            if (oper == '+')
            {
                total = a + b;
                MessageBox.Show("Cумма " + a + " и " + b + " равна " + total + ".");
            }
            else if (oper == '-')
            {
                total = a - b;
                MessageBox.Show("Разность " + a + " и " + b + " равна " + total + ".");
            }
            else if (oper == '*')
            {
                total = a * b;
                MessageBox.Show("Умножение " + a + " на " + b + " равно " + total + ".");
            }
            else if (oper == '/')
            {
                total = a / b;
                MessageBox.Show("Деление " + a + " на " + b + " равно " + total + ".");
            }
            else
            {
                MessageBox.Show("Неизвестный оператор.");
            }
            return total;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string path = @"C:\Users\karas\source\repos\Dz1-Thread\primer.txt";
            var thread = new Thread(() => Count(path));
            thread.Start();
            thread.Join();
        }
        private void Count(string path)
        {
            int count = 0;
            Regex regex = new Regex($@"bicycle(\w*)");
            var file = new StreamReader(path);
            while (!file.EndOfStream)
            {
                MatchCollection matches = regex.Matches(file.ReadLine());
                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                        count++;
                }
                else
                {
                    MessageBox.Show("Совпадений не найдено");
                }
            }
            MessageBox.Show($"Слово bicycle встречается в файле {count} раз.");
        }
    }
}

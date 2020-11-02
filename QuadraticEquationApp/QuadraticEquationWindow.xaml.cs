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
using System.Numerics;

namespace QuadraticEquationApp
{
    /// <summary>
    /// Логика взаимодействия для QuadraticEquationWindow.xaml
    /// </summary>
    public partial class QuadraticEquationWindow : Window
    {
        //константа укзавает, до скольки округлять значение в  ответе
        private const int rounding = 3;

        public QuadraticEquationWindow()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            #region Начальные значения переменных
            //сделать рамку вокруг текстовых полей серой
            txta.BorderBrush = Brushes.Gray;
            txtb.BorderBrush = Brushes.Gray;
            txtc.BorderBrush = Brushes.Gray;
            statusTexta.Text = "";
            statusTextb.Text = "";
            statusTextc.Text = "";
            //логическая переменная, показывающая, что проверка
            //на правильность ввода значений успешна
            bool IsCorrectA, IsCorrectB, IsCorrectC;

            IsCorrectA = IsCorrectB = IsCorrectC = false;

            string errorEnter = "Введите корректное значение";
            #endregion

            #region Проверка на правильность ввода
            //переменные для методов TryParse
            double value = 0;

            double a, b, c;
            a = b = c = 0;

            //значение a
            if (!double.TryParse(txta.Text, out value))
            {
                txta.BorderBrush = Brushes.Red;
                statusTexta.Text = errorEnter;
            }
            else
            {
                a = value;
                IsCorrectA = true;
            }

            //значение b

            if (!double.TryParse(txtb.Text, out value))
            {
                txtb.BorderBrush = Brushes.Red;
                statusTextb.Text = errorEnter;
            }
            else
            {
                b = value;
                IsCorrectB = true;
            }
            //значение c

            if (!double.TryParse(txtc.Text, out value))
            {
                txtc.BorderBrush = Brushes.Red;
                statusTextc.Text = errorEnter;
            }
            else
            {
                c = value;
                IsCorrectC = true;
            }
            #endregion
            if (IsCorrectA && IsCorrectB && IsCorrectC)
            {
                #region Поиск решения

                if (a != 0 && b != 0 && c != 0)
                {
                    Discrimimant(a, b, c);
                }
                else if (a != 0 && b != 0 && c == 0)
                {
                    //линейное уравнение
                    double firstRoot = 0;
                    double secondRoot = -b / a;
                    LinearOutputResults("two", firstRoot, secondRoot);
                }
                else if (a == 0 && b != 0 && c != 0)
                {
                    //линейное уравнение
                    double root = -c / b;
                    LinearOutputResults("one", root);
                }
                else if (a == 0 && b == 0 && c == 0)
                {
                    string msg = $@"Уравнение не задано!
Коэффициент при всех трех слагаемых не может равняться 0";

                    MessageBox.Show(msg, "Результат", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else if (a != 0 && b == 0 && c != 0)
                {
                    //линейное уравнение
                    if (c < 0)
                    {
                        double firstRoot = Math.Sqrt(-c / a);
                        double secondRoot = -firstRoot;
                        LinearOutputResults("two", firstRoot, secondRoot);
                    }
                    else if (c > 0)
                    {
                        double firstRootRealPart = c / a;
                        double secondRootRealPart = -firstRootRealPart;
                        string imaginaryPart = "i";
                        ComplexOutputResults(firstRootRealPart, secondRootRealPart, imaginaryPart);
                    }
                }
                else if ((a == 0 && b != 0 && c == 0) 
                    || (a != 0 && b == 0 && c == 0))
                {
                    MessageBox.Show("Уравнение имеет один корень: 0", "Результат",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    
                }
                else if(a == 0 && b == 0 && c != 0)
                {
                    MessageBox.Show("Уравнение не имеет смысла!", "Результат",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }

                #endregion
            }
        }

        private static void Discrimimant(double a, double b, double c)
        {


            double discriminant = Math.Pow(b, 2) - 4 * a * c;

            switch (discriminant)
            {
                case double d when d < 0:
                    d *= -1;
                    string firstComplexRoot = $"{-b} - {Math.Round(Math.Sqrt(d),rounding)}i / {2 * a}";
                    string secondComplexRoot = $"{-b} + {Math.Round(Math.Sqrt(d), rounding)}i / {2 * a}";
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine($"Уравнение имеет два комплексных корня: \n");
                    stringBuilder.AppendLine($"Первый корень: {firstComplexRoot}");
                    stringBuilder.AppendLine($"Второй корень: {secondComplexRoot}");

                    MessageBox.Show(stringBuilder.ToString(), "Результат",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    break;

                case double d when d == 0:
                    double root = -b / (2 * a);
                    DiscriminantOutputResults("equals zero", root);
                    break;

                case double d when d > 0:
                    double firstRoot = (-b - Math.Sqrt(discriminant)) / (2 * a);
                    double secondRoot = (-b + Math.Sqrt(discriminant)) / (2 * a);
                    DiscriminantOutputResults("Above zero", firstRoot, secondRoot);
                    break;

                default:
                    MessageBox.Show("Что-то пошло не так...", "Результат",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }    
        }


        private static void LinearOutputResults
            (string rootsNumber, double root, double secondRoot = 0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("");
            switch (rootsNumber) 
            {

                case string s when s.Equals("one", StringComparison.OrdinalIgnoreCase):
                    stringBuilder.AppendLine($"Линейное уравнение с единственным корнем: " +
                        $"{Math.Round(root, rounding)}");

                    MessageBox.Show(stringBuilder.ToString(), "Результат",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    break;

                case string s when s.Equals("two", StringComparison.OrdinalIgnoreCase):
                    stringBuilder.AppendLine($"Линейное уравнение с двумя корнями: \n");
                    stringBuilder.AppendLine($"Первый корень уравнения: " +
                        $"{Math.Round(root, rounding)}");
                    stringBuilder.AppendLine($"Второй корень уравнения: " +
                        $"{Math.Round(secondRoot, rounding)}");

                    MessageBox.Show(stringBuilder.ToString(), "Результат",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }

        }


        private static void DiscriminantOutputResults(string discriminantValue,
            double root, double secondRoot = 0)
        {
            StringBuilder stringBuilder = new StringBuilder();

            switch (discriminantValue)
            {
                //если дискриминант больше 0
                case string s when s.Equals("above zero", StringComparison.OrdinalIgnoreCase):
                    stringBuilder.AppendLine("Уравнение имеет два корня: \n");
                    stringBuilder.AppendLine($"Первый корень: " +
                        $"{Math.Round(root, rounding)}");
                    stringBuilder.AppendLine($"Второй корень: " +
                        $"{Math.Round(secondRoot, rounding)}");

                    MessageBox.Show(stringBuilder.ToString(), "Результат",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    break;

                //если дискриминант равен 0
                case string s when s.Equals("equals zero", StringComparison.OrdinalIgnoreCase):
                    stringBuilder.AppendLine($"Квадратное уравнение имеет " +
                        $"один действительный корень:{Math.Round(root, rounding)}");
                    MessageBox.Show(stringBuilder.ToString(), "Результат",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                default:
                    MessageBox.Show("Ошибка в коде...", "Результат",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
            
        }


        private static void ComplexOutputResults
            (double firstRootRealPart, double secondRootRealPart, string imaginaryPart = "i")
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Уравнение имеет два комплексных корня \n");
            stringBuilder.AppendLine($"Первый корень: " +
                $"{Math.Round(firstRootRealPart, rounding)}{imaginaryPart}");
            stringBuilder.AppendLine($"Второй корень: " +
                $"{Math.Round(secondRootRealPart, rounding)}{imaginaryPart}");

            MessageBox.Show(stringBuilder.ToString(), "Результат",
                        MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

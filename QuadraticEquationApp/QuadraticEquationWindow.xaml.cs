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
            int intValue = 0;
            double doubleValue = 0;

            int int_a, int_b, int_c;
            int_a = int_b = int_c = 0;

            double double_a, double_b, double_c;
            double_a = double_b = double_c = 0;

            //значение a
            if (!int.TryParse(txta.Text, out intValue)
                && !double.TryParse(txta.Text, out doubleValue))
            {
                txta.BorderBrush = Brushes.Red;
                statusTexta.Text = errorEnter;
            }
            else if (txta.Text == "0")
            {
                txta.BorderBrush = Brushes.Red;
                statusTexta.Text = "Коэффициент при первом слагаемом не может быть равным нулю";
            }
            else
            {
                int_a = intValue;
                double_a = doubleValue;
                IsCorrectA = true;

            }

            //значение b

            if (!int.TryParse(txtb.Text, out intValue)
                && !double.TryParse(txtb.Text, out doubleValue))
            {
                txtb.BorderBrush = Brushes.Red;
                statusTextb.Text = errorEnter;
            }
            else
            {
                int_b = intValue;
                double_b = doubleValue;
                IsCorrectB = true;
            }
            //значение c

            if (!int.TryParse(txtc.Text, out intValue)
                && !double.TryParse(txtc.Text, out doubleValue))
            {
                txtc.BorderBrush = Brushes.Red;
                statusTextc.Text = errorEnter;
            }
            else
            {
                int_c = intValue;
                double_c = doubleValue;
                IsCorrectC = true;
            }
            #endregion
            if (IsCorrectA && IsCorrectB && IsCorrectC)
            {
                #region Решение дискриминантом

                var a = (int_a != 0 && double_a == 0) ? int_a : double_a;
                var b = (int_b != 0 && double_b == 0) ? int_b : double_b;
                var c = (int_c != 0 && double_c == 0) ? int_c : double_c;

                var discriminant = Math.Pow(b, 2) - 4 * a * c;

                var x1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                var x2 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                #endregion

                #region Вывод результатов на экран
                StringBuilder @string = new StringBuilder();

                if (double.IsNaN(x1))
                {
                    @string.AppendLine($"Первый корень уравнения не является числом");
                    if (double.IsNaN(x2))
                    {
                        @string.AppendLine($"Второй корень уравнения не является числом");

                    }
                }
                else if (double.IsPositiveInfinity(x1))
                {
                    @string.AppendLine($"Первый корень уравнения равен плюс бесконечности");
                    if (double.IsNegativeInfinity(x1))
                    {
                        @string.AppendLine($"Первый корень уравнения равен минус бесконечности");
                    }
                }
                else if (double.IsPositiveInfinity(x2))
                {
                    @string.AppendLine($"Второй корень уравнения равен плюс бесконечности");
                    if (double.IsPositiveInfinity(x2))
                    {
                        @string.AppendLine($"Второй корень уравнения равен плюс бесконечности");
                    }
                }
                else
                {
                    @string.AppendLine($"Первый корень уравнения: {x1}");
                    @string.AppendLine($"Второй корень уравнения: {x2}");
                }

                MessageBox.Show(@string.ToString(), "Результат",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                #endregion
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Lab2_RomanovK.S._BPI_24_01
{
    public partial class MainWindow : Window
    {
        private static readonly Brush ErrorBrush = new SolidColorBrush(Color.FromRgb(211, 47, 47));
        private readonly Dictionary<TextBox, Brush> _originalBorders = new Dictionary<TextBox, Brush>();

        private static readonly Dictionary<string, string> FieldDisplayNames = new Dictionary<string, string>
        {
            { "R1TextA", "Формула 1 → a" },
            { "R2TextA", "Формула 2 → a" },
            { "R2TextB", "Формула 2 → b" },
            { "R3TextA", "Формула 3 → a" },
            { "R3TextB", "Формула 3 → b" },
            { "R4TextD", "Формула 4 → d" },
            { "R4TextA", "Формула 4 → a" },
            { "R5TextN", "Формула 5 → N" },
            { "R5TextK", "Формула 5 → K" },
            { "R5TextX", "Формула 5 → x" },
            { "R5TextY", "Формула 5 → y" }
        };

        public MainWindow()
        {
            InitializeComponent();

            Img1.Source = ImageHelper.BytesToImage(new Formula1().Source);
            Img2.Source = ImageHelper.BytesToImage(new Formula2().Source);
            Img3.Source = ImageHelper.BytesToImage(new Formula3().Source);
            Img4.Source = ImageHelper.BytesToImage(new Formula4().Source);
            Img5.Source = ImageHelper.BytesToImage(new Formula5().Source);

            // Начальное состояние темы
            if (App.CurrentTheme == AppTheme.Dark)
                rbDark.IsChecked = true;
            else
                rbLight.IsChecked = true;

            UpdateInputStates();
        }

        private void Theme_Checked(object sender, RoutedEventArgs e)
        {
            if (!(sender is RadioButton rb)) return;
            if (rb.Tag == null) return;

            AppTheme theme = rb.Tag.ToString() == "Dark"
                ? AppTheme.Dark
                : AppTheme.Light;

            App.ApplyTheme(theme);
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
            => WindowState = WindowState.Minimized;

        private void Maximize_Click(object sender, RoutedEventArgs e)
            => WindowState = WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;

        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
                Maximize_Click(sender, e);
        }

        private void Radio_Checked(object sender, RoutedEventArgs e)
            => UpdateInputStates();

        private void UpdateInputStates()
        {
            if (R1Group == null) return;

            R1Group.IsEnabled = Radio1.IsChecked == true;
            R2Group.IsEnabled = Radio2.IsChecked == true;
            R3Group.IsEnabled = Radio3.IsChecked == true;
            R4Group.IsEnabled = Radio4.IsChecked == true;
            R5Group.IsEnabled = Radio5.IsChecked == true;

            CalcButton.IsEnabled =
                Radio1.IsChecked == true ||
                Radio2.IsChecked == true ||
                Radio3.IsChecked == true ||
                Radio4.IsChecked == true ||
                Radio5.IsChecked == true;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb) ValidateTextBox(tb);
        }

        private bool ValidateTextBox(TextBox tb)
        {
            string name = tb.Name;
            string text = tb.Text.Trim();

            bool isInteger = false;
            double minValue = double.MinValue;
            string defaultValue = "0";
            string friendlyName = FieldDisplayNames.ContainsKey(name)
                ? FieldDisplayNames[name]
                : name;

            switch (name)
            {
                case "R1TextA":
                case "R2TextA":
                case "R2TextB":
                case "R3TextA":
                case "R3TextB":
                case "R4TextA":
                case "R5TextX":
                case "R5TextY":
                    defaultValue = "0,00";
                    break;

                case "R4TextD":
                    isInteger = true;
                    minValue = 0;
                    defaultValue = "0";
                    break;

                case "R5TextN":
                case "R5TextK":
                    isInteger = true;
                    minValue = 1;
                    defaultValue = "5";
                    break;

                default:
                    return true;
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                MarkInvalid(tb, friendlyName, "поле не может быть пустым.");
                tb.Text = defaultValue;
                return false;
            }

            if (isInteger)
            {
                if (!int.TryParse(text, out int intValue))
                {
                    MarkInvalid(tb, friendlyName, "введите целое число.");
                    tb.Text = defaultValue;
                    return false;
                }
                if (intValue < minValue)
                {
                    MarkInvalid(tb, friendlyName, $"значение должно быть не меньше {minValue}.");
                    tb.Text = defaultValue;
                    return false;
                }
            }
            else
            {
                if (!double.TryParse(text, NumberStyles.Float | NumberStyles.AllowThousands,
                                     CultureInfo.CurrentCulture, out double _))
                {
                    MarkInvalid(tb, friendlyName, "введите число.");
                    tb.Text = defaultValue;
                    return false;
                }
            }

            MarkValid(tb);
            return true;
        }

        private void MarkInvalid(TextBox tb, string friendlyName, string reason)
        {
            if (!_originalBorders.ContainsKey(tb))
                _originalBorders[tb] = tb.BorderBrush;

            tb.Tag = "error";                      
            tb.BorderBrush = ErrorBrush;
            tb.BorderThickness = new Thickness(2);
            tb.ToolTip = $"{friendlyName}: {reason}";

            MessageBox.Show(
                $"Поле «{friendlyName}»: {reason}",
                "Ошибка ввода",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            tb.Focus();
            tb.SelectAll();
        }

        private void MarkValid(TextBox tb)
        {
            if (_originalBorders.TryGetValue(tb, out var original))
                tb.BorderBrush = original;

            tb.Tag = null;                     
            tb.ToolTip = null;
        }

        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Radio1.IsChecked == true)
                {
                    if (!ValidateTextBox(R1TextA)) return;
                }
                else if (Radio2.IsChecked == true)
                {
                    if (!ValidateTextBox(R2TextA)) return;
                    if (!ValidateTextBox(R2TextB)) return;
                }
                else if (Radio3.IsChecked == true)
                {
                    if (!ValidateTextBox(R3TextA)) return;
                    if (!ValidateTextBox(R3TextB)) return;
                }
                else if (Radio4.IsChecked == true)
                {
                    if (!ValidateTextBox(R4TextD)) return;
                    if (!ValidateTextBox(R4TextA)) return;
                }
                else if (Radio5.IsChecked == true)
                {
                    if (!ValidateTextBox(R5TextN)) return;
                    if (!ValidateTextBox(R5TextK)) return;
                    if (!ValidateTextBox(R5TextX)) return;
                    if (!ValidateTextBox(R5TextY)) return;
                }
                else
                {
                    MessageBox.Show("Выберите формулу.", "Ошибка",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                MainFunction formula = null;

                if (Radio1.IsChecked == true)
                {
                    formula = new Formula1
                    {
                        A = Convert.ToDouble(R1TextA.Text),
                        F = Convert.ToDouble(((ComboBoxItem)R1ComboF.SelectedItem).Content)
                    };
                }
                else if (Radio2.IsChecked == true)
                {
                    formula = new Formula2
                    {
                        A = Convert.ToDouble(R2TextA.Text),
                        B = Convert.ToDouble(R2TextB.Text),
                        F = Convert.ToDouble(((ComboBoxItem)R2ComboF.SelectedItem).Content)
                    };
                }
                else if (Radio3.IsChecked == true)
                {
                    formula = new Formula3
                    {
                        A = Convert.ToDouble(R3TextA.Text),
                        B = Convert.ToDouble(R3TextB.Text),
                        C = Convert.ToDouble(((ComboBoxItem)R3ComboC.SelectedItem).Content),
                        D = Convert.ToDouble(((ComboBoxItem)R3ComboD.SelectedItem).Content)
                    };
                }
                else if (Radio4.IsChecked == true)
                {
                    formula = new Formula4
                    {
                        A = Convert.ToDouble(R4TextA.Text),
                        D = Convert.ToInt32(R4TextD.Text),
                        C = Convert.ToDouble(((ComboBoxItem)R4ComboC.SelectedItem).Content)
                    };
                }
                else if (Radio5.IsChecked == true)
                {
                    formula = new Formula5
                    {
                        N = Convert.ToInt32(R5TextN.Text),
                        K = Convert.ToInt32(R5TextK.Text),
                        X = Convert.ToDouble(R5TextX.Text),
                        Y = Convert.ToDouble(R5TextY.Text)
                    };
                }

                if (formula != null)
                {
                    double result = formula.Calculate();
                    MessageBox.Show("Ответ: " + result.ToString("F4"), "Результат вычисления",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
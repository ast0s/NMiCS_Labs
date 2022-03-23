using NMiCS_Labs.Tools;
using org.mariuszgromada.math.mxparser;
using System;
using System.Threading;
using System.Windows;

namespace NMiCS_Labs
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            InitializeComponent();
        }

        private void FindRootsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Function func = new Function("f(x) = " + ExpressionTextBox.Text);
                if (!func.checkSyntax()) throw new Exception("Function syntax error!");
                EquationCalculator ec = new EquationCalculator(double.Parse(EpsilonTextBox.Text));

                Equation eq;
                if (double.Parse(BeginRangeTextBox.Text) > double.Parse(EndRangeTextBox.Text))
                {
                    eq = new Equation(double.Parse(EndRangeTextBox.Text), double.Parse(BeginRangeTextBox.Text),
                    x => func.calculate(x));
                }
                else
                {
                    eq = new Equation(double.Parse(BeginRangeTextBox.Text), double.Parse(EndRangeTextBox.Text),
                    x => func.calculate(x));
                }
                
                var result = ec.SolveEquation(eq);
                ResultList.ItemsSource = result;
            }
            catch (Exception exception)
            {
                _ = MessageBox.Show($"An error has occured! Message: {exception.Message}");
            }
        }
    }
}

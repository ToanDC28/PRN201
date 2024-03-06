using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FirstWPFAPP
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
            if (height.Text == "" || weight.Text == "")
            {
                MessageBox.Show("Height or Weight is empty!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var h = Double.Parse(height.Text);
                var w = Double.Parse(weight.Text);
                if (h <= 0 || w <= 0)
                {
                    MessageBox.Show("Invalid input!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    
                    var h2 = h / 100;
                    var bmi = w / (h2 * h2);
                    txtBMI.Text = Math.Round(bmi, 2).ToString();
                    if (bmi < 18.5)
                    {
                        txtStatus.Text = Status.Underweight.ToString();
                        txtStatus.FontWeight = FontWeights.Bold;
                    }
                    else if (bmi <= 24.9 && bmi >= 18.5)
                    {
                        txtStatus.Text = Status.Healthy.ToString();
                        txtStatus.FontWeight = FontWeights.Bold;
                    }
                    else if (bmi <= 29.9 && bmi >= 25.0)
                    {
                        txtStatus.Text = Status.Overweight.ToString();
                        txtStatus.FontWeight = FontWeights.Bold;
                    }
                    else
                    {
                        txtStatus.Text = Status.obese.ToString();
                        txtStatus.FontWeight = FontWeights.Bold;
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            height.Text = String.Empty;
            weight.Text = String.Empty;
            txtBMI.Text = String.Empty;
            txtStatus.Text = String.Empty;
        }
    }
}
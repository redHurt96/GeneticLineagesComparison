using LineagesComparison.Calculation;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace LineagesComparison
{
    public partial class MainWindow : Window
    {
        private Label _namesPathLabel;
        private Label _pathLabel;
        private TextBox _namesTextBox;
        private TextBox _resultTextBox;
        private Button _calculateButton;
        private Button _copyButton;

        private string _path;
        private string _result;
        private SamplesPerLineages _samplesPerLineages;

        public MainWindow()
        {
            InitializeComponent();

            object labelObj = FindName("fileName_label");
            if (labelObj != null)
                _pathLabel = labelObj as Label;
            
            object namesLabelObj = FindName("fileName_NamesLabel");
            if (labelObj != null)
                _namesPathLabel = namesLabelObj as Label;

            object textBox = FindName("result_textBox");
            if (textBox != null)
                _resultTextBox = textBox as TextBox;

            object namesTextBox = FindName("names_textBox");
            if (textBox != null)
                _namesTextBox = namesTextBox as TextBox;

            object calculateButton = FindName("calculate_button");
            if (calculateButton != null)
            { 
                _calculateButton = calculateButton as Button;
                _calculateButton.Visibility = Visibility.Hidden;
            }
            
            object copyButton = FindName("copy_button");
            if (calculateButton != null)
            {
                _copyButton = copyButton as Button;
                _copyButton.Visibility = Visibility.Hidden;
            }
        }

        private void choose_names_file_button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выбери файл с названиями";
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            bool? result = openFileDialog.ShowDialog();

            if (result != null && result == true)
            {
                string namesFilePath = openFileDialog.FileName;
                _samplesPerLineages = SamplesPerLineagesParser.Execute(namesFilePath);
                _namesTextBox.Text = _samplesPerLineages.ToString();
                _namesPathLabel.Content = namesFilePath;
            }

            UpdateCalculateButtonVisibility();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выбери файл со сравнением";
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            bool? result = openFileDialog.ShowDialog();

            if (result != null && result == true)
            {
                _path = openFileDialog.FileName;
                _pathLabel.Content = _path;
            }

            UpdateCalculateButtonVisibility();
        }

        private void calculate_button_Click(object sender, RoutedEventArgs e)
        {
            _result = AverageCalculator.Execute(_path, _samplesPerLineages);
            _resultTextBox.Text = _result;
            _copyButton.Visibility = Visibility.Visible;
        }

        private void UpdateCalculateButtonVisibility() => 
            _calculateButton.Visibility = _path != null && _samplesPerLineages != null
                ? Visibility.Visible
                : Visibility.Hidden;

        private void copy_Button_Click(object sender, RoutedEventArgs e) => 
            Clipboard.SetText(_result);
    }
}

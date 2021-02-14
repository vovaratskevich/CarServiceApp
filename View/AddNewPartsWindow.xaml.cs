using CarServiceApp.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace CarServiceApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewPartsWindow.xaml
    /// </summary>
    public partial class AddNewPartsWindow : Window
    {
        public AddNewPartsWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

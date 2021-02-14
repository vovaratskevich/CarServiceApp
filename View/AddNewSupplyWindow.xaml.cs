using CarServiceApp.ViewModel;
using System.Windows;

namespace CarServiceApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewSupplyWindow.xaml
    /// </summary>
    public partial class AddNewSupplyWindow : Window
    {
        public AddNewSupplyWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
        }
    }
}

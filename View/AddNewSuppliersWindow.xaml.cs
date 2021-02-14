using CarServiceApp.ViewModel;
using System.Windows;

namespace CarServiceApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewSuppliersWindow.xaml
    /// </summary>
    public partial class AddNewSuppliersWindow : Window
    {
        public AddNewSuppliersWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
        }
    }
}

using CarServiceApp.Model;
using CarServiceApp.ViewModel;
using System.Windows;

namespace CarServiceApp.View
{
    /// <summary>
    /// Логика взаимодействия для EditSupplyWindow.xaml
    /// </summary>
    public partial class EditSupplyWindow : Window
    {
        public EditSupplyWindow(Supply supplyToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedSupply = supplyToEdit;
            DataManageVM.SupplyDate = supplyToEdit.Date;
            DataManageVM.SupplyCountOfPart = supplyToEdit.CountOfPart;
        }
    }
}

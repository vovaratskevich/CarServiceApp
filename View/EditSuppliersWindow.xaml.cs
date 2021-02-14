using CarServiceApp.Model;
using CarServiceApp.ViewModel;
using System.Windows;

namespace CarServiceApp.View
{
    /// <summary>
    /// Логика взаимодействия для EditSuppliersWindow.xaml
    /// </summary>
    public partial class EditSuppliersWindow : Window
    {
        public EditSuppliersWindow(Supplier supplierToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedSupplier = supplierToEdit;
            DataManageVM.SupplierAdress = supplierToEdit.Adress;
            DataManageVM.SupplierName = supplierToEdit.Name;
            DataManageVM.SupplierPhone = supplierToEdit.Phone;
        }
    }
}

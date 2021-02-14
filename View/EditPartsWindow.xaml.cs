using CarServiceApp.Model;
using CarServiceApp.ViewModel;
using System.Windows;


namespace CarServiceApp.View
{
    /// <summary>
    /// Логика взаимодействия для EditPartsWindow.xaml
    /// </summary>
    public partial class EditPartsWindow : Window
    {
        public EditPartsWindow(Part partToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedPart = partToEdit;
            DataManageVM.PartName = partToEdit.Name;
            DataManageVM.PartNote = partToEdit.Note;
            DataManageVM.PartPrice = partToEdit.Price;
            DataManageVM.PartVendorCode = partToEdit.VendorCode;
        }
    }
}

using CarServiceApp.ViewModel;
using System.Windows;
using System.Windows.Controls;


namespace CarServiceApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListView AllSuppliersView;
        public static ListView AllPartsView;
        public static ListView AllSupplysView;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            AllSuppliersView = ViewAllSuppliers;
            AllPartsView = ViewAllParts;
            AllSupplysView = ViewAllSupplys;            
        }
    }
}

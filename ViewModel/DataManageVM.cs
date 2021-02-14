using CarServiceApp.Model;
using CarServiceApp.View;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CarServiceApp.ViewModel 
{
    public class DataManageVM : INotifyPropertyChanged
    {        
        //все поставщики
        private List<Supplier> allSuppliers = DataWorker.GetAllSuppliers(); 
        public List<Supplier> AllSuppliers
        {
            get { return allSuppliers; }
            set
            {
                allSuppliers = value;
                NotifyPropertyChanged("AllSuppliers");
            }
        }
        //все детали
        private List<Part> allParts = DataWorker.GetAllParts();
        public List<Part> AllParts
        {
            get { return allParts; }
            set
            {
                allParts = value;
                NotifyPropertyChanged("AllParts");
            }
        }
        //все поставки
        private List<Supply> allSupplys = DataWorker.GetAllSupplys();
        public List<Supply> AllSupplys
        {
            get { return allSupplys; }
            set
            {
                allSupplys = value;
                NotifyPropertyChanged("AllSupplys");
            }
        }
        #region All Properies 
        //свойства для поставщика
        public static string SupplierName { get; set; }
        public static string SupplierAdress{ get; set; }
        public static int SupplierPhone { get; set; }

        //свойства для детали 
        public static string PartName { get; set; }
        public static decimal PartVendorCode { get; set; }
        public static decimal PartPrice { get; set; }
        public static string PartNote { get; set; }
        public static Supplier PartSupplier { get; set; }

        //свойства для поставки 
        public static int SupplyCountOfPart { get; set; }
        public static string SupplyDate { get; set; }
        public static Part SupplyPart { get; set; }

        //свойства для выделенных элементов
        public TabItem SelectedTabItem { get; set; }
        public static Supply SelectedSupply { get; set; }
        public static Part SelectedPart { get; set; }
        public static Supplier SelectedSupplier { get; set; }
        #endregion

        private RelayCommand addNewSupply;
        public RelayCommand AddNewSupply
        {
            get
            {
                return addNewSupply ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";

                    if (
                        SupplyCountOfPart == 0 ||                        
                        SupplyDate == null || SupplyDate.Replace(" ", "").Length == 0 ||
                        SupplyPart == null
                        )
                    {
                        SetRedBlockControll(wnd, "CountOfPartBlock");
                        SetRedBlockControll(wnd, "DateBlock");                        
                    }
                    /*                                    
                    if (SupplyPart == null)
                    {
                        MessageBox.Show("Укажите поставщика!");
                    }
                    */
                    else
                    {
                        resultStr = DataWorker.CreateSupply(SupplyCountOfPart, SupplyDate, SupplyPart);
                        UpdateAllDataView();
                        ShowMessageToUser(resultStr);
                        SetNullValuesToPropertyView();
                        wnd.Close();
                    }
                });
            }
        }

        #region Commands To Add
        private RelayCommand addNewPart;
        public RelayCommand AddNewPart
        {
            get
            {
                return addNewPart ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";

                    if (PartName == null || PartName.Replace(" ","").Length == 0 ||
                    PartVendorCode == 0 ||
                    PartPrice == 0 ||
                    PartNote == null || PartNote.Replace(" ", "").Length == 0 ||
                    PartSupplier == null
                    )
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                        SetRedBlockControll(wnd, "VendorCodeBlock");
                        SetRedBlockControll(wnd, "PriceBlock");
                        SetRedBlockControll(wnd, "NoteBlock");
                    }
                    /*                                    
                    if (PartSupplier == null)
                    {
                        MessageBox.Show("Укажите поставщика!");
                    }
                    */
                    else
                    {
                        resultStr = DataWorker.CreatePart(PartName, PartVendorCode, PartPrice, PartNote, PartSupplier);
                        UpdateAllDataView();
                        ShowMessageToUser(resultStr);                        
                        SetNullValuesToPropertyView();
                        wnd.Close();
                    }                
                });
            }
        }
        private RelayCommand addNewSupplier;
        public RelayCommand AddNewSupplier
        {
            get
            {
                return addNewSupplier ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";

                    if (SupplierName == null || SupplierName.Replace(" ", "").Length == 0 ||
                    SupplierAdress == null || SupplierAdress.Replace(" ", "").Length == 0 ||
                    SupplierPhone == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                        SetRedBlockControll(wnd, "AdressBlock");
                        SetRedBlockControll(wnd, "PhoneBlock");
                    }
                    else 
                    {
                        resultStr = DataWorker.CreateSupplier(SupplierName, SupplierAdress, SupplierPhone);
                        UpdateAllDataView();
                        ShowMessageToUser(resultStr);
                        SetNullValuesToPropertyView();
                        wnd.Close();
                    }                    
                });
            }
        }
        #endregion
        
        private RelayCommand deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ?? new RelayCommand(obj =>
               {
                   //Window wnd = obj as Window;
                   string resultStr = "Ничего не выбрано!";
                   //если поставщик
                   if (SelectedTabItem.Name == "SuppliersTab" && SelectedSupplier != null)
                   {
                       resultStr = DataWorker.DeleteSupplier(SelectedSupplier);
                       UpdateAllDataView();
                   }
                   //если детали
                   if (SelectedTabItem.Name == "PartsTab" && SelectedPart != null)
                   {
                       resultStr = DataWorker.DeletePart(SelectedPart);
                       UpdateAllDataView();
                   }
                   //если поставки
                   if (SelectedTabItem.Name == "SupplyTab" && SelectedSupply != null)
                   {
                       resultStr = DataWorker.DeleteSupply(SelectedSupply);
                       UpdateAllDataView();
                   }
                   //обновление
                   SetNullValuesToPropertyView();
                   ShowMessageToUser(resultStr);
               });
            }
        }

        #region Edit Commands
        private RelayCommand editSypply;
        public RelayCommand EditSypply
        {
            get
            {
                return editSypply ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбрана поставка!";
                    string onPartsStr = "Не выбрана деталь!";                   
                    if (SelectedSupply != null)
                    {
                        if (SupplyPart != null)
                        {
                            resultStr = DataWorker.EditeSupply(SelectedSupply, SupplyCountOfPart, SupplyDate, SupplyPart);
                            UpdateAllDataView();
                            SetNullValuesToPropertyView();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }       
                        else ShowMessageToUser(onPartsStr);
                    }
                    else ShowMessageToUser(resultStr);
                });
            }
        }

        private RelayCommand editPart;
        public RelayCommand EditPart
        {
            get
            {
                return editPart ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбрана деталь!";
                    string onPartsStr = "Не выбран поставщик!";                    
                    if (SelectedPart != null)
                    {
                        if (PartSupplier != null)
                        {
                            resultStr = DataWorker.EditePart(SelectedPart, PartName, PartVendorCode, PartPrice, PartNote, PartSupplier);
                            UpdateAllDataView();
                            SetNullValuesToPropertyView();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else ShowMessageToUser(onPartsStr);
                    }
                    else ShowMessageToUser(resultStr);
                });
            }
        }

        private RelayCommand editSupplier;
        public RelayCommand EditSupplier
        {
            get
            {
                return editSupplier ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран поствщик!";                   
                    if (SelectedSupplier != null)
                    {
                        resultStr = DataWorker.EditeSupplier(SelectedSupplier, SupplierName, SupplierAdress, SupplierPhone);
                        UpdateAllDataView();
                        SetNullValuesToPropertyView();
                        ShowMessageToUser(resultStr);
                        window.Close();                   
                    }
                    else ShowMessageToUser(resultStr);
                });
            }
        }
        #endregion

        #region Commands To Open Windows
        private RelayCommand openAddNewSuppliersWnd;
        public RelayCommand OpenAddNewSuppliersWnd
        {
            get
            {
                return openAddNewSuppliersWnd ?? new RelayCommand(obj =>
                {
                    OpenAddNewSuppliersWindowMethod();
                }
                );
            }
        }
        private RelayCommand openAddNewPartsWnd;
        public RelayCommand OpenAddNewPartsWnd
        {
            get
            {
                return openAddNewPartsWnd ?? new RelayCommand(obj =>
                {
                    OpenAddNewPartsWindowMethod();
                }
                );
            }
        }
        private RelayCommand openAddNewSupplyWnd;
        public RelayCommand OpenAddNewSupplyWnd
        {
            get
            {
                return openAddNewSupplyWnd ?? new RelayCommand(obj =>
                {
                    OpenAddNewSupplyWindowMethod();
                }
                );
            }
        }

        #endregion

        private RelayCommand openEditeItem;
        public RelayCommand OpenEditeItem
        {
            get
            {
                return openEditeItem ?? new RelayCommand(obj =>
                {                
                    //если поставщик
                    if (SelectedTabItem.Name == "SuppliersTab" && SelectedSupplier != null)
                    {
                        OpenEditSuppliersWindowMethod(SelectedSupplier);
                    }
                    //если детали
                    if (SelectedTabItem.Name == "PartsTab" && SelectedPart != null)
                    {
                        OpenEditPartsWindowMethod(SelectedPart);
                    }
                    //если поставки
                    if (SelectedTabItem.Name == "SupplyTab" && SelectedSupply != null)
                    {
                        OpenEditSupplyWindowMethod(SelectedSupply);
                    }
                });
            }
        }

        #region Method To Open Window
        //методы открытия окон
        private void OpenAddNewSuppliersWindowMethod()
                {
                    AddNewSuppliersWindow newSupplierWindow = new AddNewSuppliersWindow();
                    SetCenterPositionAndOpen(newSupplierWindow);
                }
                private void OpenAddNewSupplyWindowMethod()
                {
                    AddNewSupplyWindow newSupplyWindow = new AddNewSupplyWindow();
                    SetCenterPositionAndOpen(newSupplyWindow);
                }
                private void OpenAddNewPartsWindowMethod()
                {
                    AddNewPartsWindow newPartWindow = new AddNewPartsWindow();
                    SetCenterPositionAndOpen(newPartWindow);
                }

                //окна редактирования 
                private void OpenEditSuppliersWindowMethod(Supplier supplier)
                {
                    EditSuppliersWindow editSupplierWindow = new EditSuppliersWindow(supplier);
                    SetCenterPositionAndOpen(editSupplierWindow);
                }
                private void OpenEditSupplyWindowMethod(Supply supply)
                {
                    EditSupplyWindow editSupplyWindow = new EditSupplyWindow(supply);
                    SetCenterPositionAndOpen(editSupplyWindow);
                }
                private void OpenEditPartsWindowMethod(Part part)
                {
                    EditPartsWindow editPartWindow = new EditPartsWindow(part);
                    SetCenterPositionAndOpen(editPartWindow);
                }
        #endregion

        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        #region Update Views
        private void SetNullValuesToPropertyView()
        {
            //для поставки 
            SupplyCountOfPart = 0;
            SupplyDate = null;
            SupplyPart = null;
            //для детали
            PartName = null;
            PartVendorCode = 0;
            PartPrice = 0;
            PartNote = null;
            PartSupplier = null;
            //для поставщика
            SupplierName = null;
            SupplierAdress = null;
            SupplierPhone = 0;
        }

        private void UpdateAllDataView()
        {
            UpdateAllSuppliersView();
            UpdateAllPartsView();
            UpdateAllSupplysView();

        }
        private void UpdateAllSuppliersView()
        {
            //наполнили, обнунлили и опять добавили
            AllSuppliers = DataWorker.GetAllSuppliers();
            MainWindow.AllSuppliersView.ItemsSource = null;
            MainWindow.AllSuppliersView.Items.Clear();
            MainWindow.AllSuppliersView.ItemsSource = AllSuppliers;
            MainWindow.AllSuppliersView.Items.Refresh();
        }
        private void UpdateAllPartsView()
        {
            //наполнили, обнунлили и опять добавили
            AllParts = DataWorker.GetAllParts();
            MainWindow.AllPartsView.ItemsSource = null;
            MainWindow.AllPartsView.Items.Clear();
            MainWindow.AllPartsView.ItemsSource = AllParts;
            MainWindow.AllPartsView.Items.Refresh();
        }
        private void UpdateAllSupplysView()
        {
            //наполнили, обнунлили и опять добавили
            AllSupplys = DataWorker.GetAllSupplys();
            MainWindow.AllSupplysView.ItemsSource = null;
            MainWindow.AllSupplysView.Items.Clear();
            MainWindow.AllSupplysView.ItemsSource = AllSupplys;
            MainWindow.AllSupplysView.Items.Refresh();
        }
        #endregion

        private void ShowMessageToUser(string message)
        {
            MessageView messageView = new MessageView(message);
            SetCenterPositionAndOpen(messageView);
        }

        private void SetCenterPositionAndOpen (Window window)
        {            
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

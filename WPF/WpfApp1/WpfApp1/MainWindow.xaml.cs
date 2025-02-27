﻿using ManagerWpfLibrary;
using Newtonsoft.Json;
using StockEntModelLibrary;
using StockEntModelLibrary.BookEnt;
using StockEntModelLibrary.CustumerEnt;
using StockEntModelLibrary.Document;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.dialogs;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

        /// ОСНОВНОЙ ПОСТАВЩИК - 1, ОСНОВНОЙ ПОКУПАТЕЛЬ - 2
    public partial class MainWindow : Window
    {
        List<Book> books;
        List<BookFullDescription> bookFullDescriptions;
        List<Custumer> custumers;
        List<CustumerDescription> custumerDescriptions;
        List<SaleDoc> saleDocs;
        List<PurchaseDoc> purchaseDocs;
        List<SaleDocRec> saleDocRecs;
        List<PurchaseDocRec> purchaseDocRecs;
        public string APP_CONNECT = "http://localhost:47914/api/";
        //public string APP_CONNECT = "http://192.168.0.109:47914/api/";
        HttpClient client = new HttpClient();
        CustumerManager CustManager = new CustumerManager();
        PurchaseDocsManager PurchaseDocsManager = new PurchaseDocsManager();
        SaleDocManager SaleDocManager = new SaleDocManager();
        BookManager BookManager = new BookManager();
        int selectedColumn;
        int selectedId;
        int selectedIdJournal;
        string rbChose=""; //нужен для проверки какой РадиоБаттон выбран 
        List<BookAndDesc> bookAndDescs = new List<BookAndDesc>();
        List<custAndDesc> custAndDescs = new List<custAndDesc>();
        public enum API_CON_TYPE //выбор АпиКонтроллера
        {
            Custumer,
            CustumerDesription,
            Book,
            BookDescription,
            SaleDoc,
            PurchaseDoc,
            PurchaseDocRec,
            SaleDocRec,
            CustumerForGetByName,
            SpecialCustumer,
            UnitedPurchaseDoc,
            PurchaseDocChange,
            UnitedSaleDoc,
            SaleDocChange,
            PurchaseDocDel,
            SaleDocDel,
            BookDel,
            CustumerDel


        }
        public List<Custumer> getCustumers
        {
            get { return custumers; }
        }

        public MainWindow()
        {
            InitializeComponent();
            LoadDatas();
            var winHeight= System.Windows.SystemParameters.PrimaryScreenHeight;
            var winWidth = System.Windows.SystemParameters.PrimaryScreenWidth;

            MainWind.MaxHeight = winHeight;
            MainWind.MaxWidth = winWidth;
            //string API_CUSTUMER = "Custumer";
            CustPanel_ShowTrue_HideFalse(false);
            PricePanel_ShowTrue_HideFalse(false);
            stackPanelFilters.Visibility = Visibility.Hidden;
            labelsStackPanelFilters.Visibility = Visibility.Hidden;
            wrapPanelClientFilter.Visibility = Visibility.Hidden;
            Title = "Book Storage";
            //ТЕСТЫ ТЕСТЫ
            #region
            //StockDBcontext ctx = new StockDBcontext();
            //ctx.Custumers.Load();
            //Custumer custumer = new Custumer();
            //custumer.CustumerTitle = "SomeShit";
            //custumer.BuyerTrue_SuplierFalse = true;
            //custumer.Balance = 0;

            //var responceById = client.GetAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString() +"/"+"5").Result;//конектимся и получаем по ид кастомера
            //var jsonFromResponceById = responceById.Content.ReadAsStringAsync().Result;//здесь полеченное делается JSON-ом 
            //   Custumer custumerById = JsonConvert.DeserializeObject<Custumer>(jsonFromResponceById);//здесь JSON превращается непосредственно в Кастомера
            //custumerById.CustumerTitle = "New Shit 33";

            //var respCustDesc = client.GetAsync(APP_CONNECT + API_CON_TYPE.CustumerDesription.ToString()+custumerById.Id.ToString()).Result;
            //var jsonFromRespCustDesc = respCustDesc.Content.ReadAsStringAsync().Result;
            //CustumerDescription cd = JsonConvert.DeserializeObject<CustumerDescription>(jsonFromRespCustDesc);


            //// UpdateCust(custumerById); 

            //var jjson = JsonConvert.SerializeObject(custumerById);//здесь Кастумер перегоняется в JSON 
            //var jjsonDesc = JsonConvert.SerializeObject(cd);
            ////var resp = client.PostAsJsonAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString(), jjson);
            //client.PutAsJsonAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString() + "/" + "5", jjson); //здесь JSON-Кастумер передаётся в АПИ-Контроллер


            //StockDBcontext stockDBcontext = new StockDBcontext();
            //// var responce2 = client.GetAsync(APP_CONNECT+API_CUSTUMER).Result;
            //IEnumerable<SaleDocRec> saleDocRecs = stockDBcontext.SaleDocRecs;
            //string jjj = JsonConvert.SerializeObject(saleDocRecs);

            //var responce = client.GetAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString()).Result;
            //var json = responce.Content.ReadAsStringAsync().Result;
            //List<Custumer> p = JsonConvert.DeserializeObject<List<Custumer>>(json);
            //dataGrid1.ItemsSource = p;
            //var jj = JsonConvert.SerializeObject(p);
            #endregion //ТЕСТЫ ТЕСТЫ
            //ТЕСТЫ ТЕСТЫ
        }

       
        // для обновления отдельных данных
        public void LoadCustumers()
        {
            try
            {
                var responceCust = client.GetAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString()).Result;
                var jsonRespCust = responceCust.Content.ReadAsStringAsync().Result;
                custumers = JsonConvert.DeserializeObject<List<Custumer>>(jsonRespCust);

            }
            catch
            {

            }
            try
            {
                var respCustDesc = client.GetAsync(APP_CONNECT + API_CON_TYPE.CustumerDesription.ToString()).Result;
                var jsonRespCustDesc = respCustDesc.Content.ReadAsStringAsync().Result;
                custumerDescriptions = JsonConvert.DeserializeObject<List<CustumerDescription>>(jsonRespCustDesc);
            }
            catch
            {

            }
            custAndDescs.Clear();
            foreach (var item in custumers)
            {
                var cnd = new custAndDesc();
                cnd.custumer = item;
                foreach (var item1 in custumerDescriptions)
                {
                    if (item.Id == item1.Id)
                    {
                        cnd.custumerDescription = item1;
                    }
                }
                custAndDescs.Add(cnd);
            }


        }
        public void LoadPurchaseDocs()
        {
            try
            {
                var respPurchaseDocs = client.GetAsync(APP_CONNECT + API_CON_TYPE.PurchaseDoc.ToString()).Result;
                var jsonRespPDocs = respPurchaseDocs.Content.ReadAsStringAsync().Result;
                purchaseDocs = JsonConvert.DeserializeObject<List<PurchaseDoc>>(jsonRespPDocs);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error load Purchase Docs: " + ex);
            }
        }



        public void LoadSaleDocs()
        {
            try
            {
                var respSaleDocs = client.GetAsync(APP_CONNECT + API_CON_TYPE.SaleDoc.ToString()).Result;
                var jsonRespSDocs = respSaleDocs.Content.ReadAsStringAsync().Result;
                saleDocs = JsonConvert.DeserializeObject<List<SaleDoc>>(jsonRespSDocs);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error load sale Docs: " + ex);
            }
        }


        public void LoadBooks()
        {
            try
            {
                var responceBook = client.GetAsync(APP_CONNECT + API_CON_TYPE.Book.ToString()).Result;
                var jsonRespBook = responceBook.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<List<Book>>(jsonRespBook);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error load Book: " + ex);
            }

            try
            {
                var respBooDesk = client.GetAsync(APP_CONNECT + API_CON_TYPE.BookDescription.ToString()).Result;
                var jsonRespBookDesk = respBooDesk.Content.ReadAsStringAsync().Result;
                bookFullDescriptions = JsonConvert.DeserializeObject<List<BookFullDescription>>(jsonRespBookDesk);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error load Book desc: " + ex);
            }
            bookAndDescs.Clear();
            foreach (var item in books)
            {
                var bnd = new BookAndDesc();
                bnd.book = item;
                foreach (var item1 in bookFullDescriptions)
                {
                    if (item.Id == item1.Id)
                    {
                        bnd.bookFullDescription = item1;
                    }
                }
                bookAndDescs.Add(bnd);
            }

        }
        // для обновления отдельных данных






        public void LoadDatas()//загружает на старте все необходимые данные
        {
            try
            {
                var responceBook = client.GetAsync(APP_CONNECT + API_CON_TYPE.Book.ToString()).Result;
                var jsonRespBook = responceBook.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<List<Book>>(jsonRespBook);
            }
            catch
                { }

            try
            {
                var respBooDesk = client.GetAsync(APP_CONNECT + API_CON_TYPE.BookDescription.ToString()).Result;
                var jsonRespBookDesk = respBooDesk.Content.ReadAsStringAsync().Result;
                bookFullDescriptions = JsonConvert.DeserializeObject<List<BookFullDescription>>(jsonRespBookDesk);
            }
            catch
            {

            }

            try
            {
                var responceCust = client.GetAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString()).Result;
                var jsonRespCust = responceCust.Content.ReadAsStringAsync().Result;
                custumers = JsonConvert.DeserializeObject<List<Custumer>>(jsonRespCust);
              
            }
            catch
            {

            }
            try
            {
                var respCustDesc = client.GetAsync(APP_CONNECT + API_CON_TYPE.CustumerDesription.ToString()).Result;
                var jsonRespCustDesc = respCustDesc.Content.ReadAsStringAsync().Result;
                custumerDescriptions = JsonConvert.DeserializeObject<List<CustumerDescription>>(jsonRespCustDesc);
            }
            catch
            {
              
            }
            try
            {
                var respSaleDoc = client.GetAsync(APP_CONNECT + API_CON_TYPE.SaleDoc.ToString()).Result;
                var jsonRespSaleDoc = respSaleDoc.Content.ReadAsStringAsync().Result;
                saleDocs = JsonConvert.DeserializeObject<List<SaleDoc>>(jsonRespSaleDoc);
            }
            catch (Exception e)
            {
                saleDocs = new List<SaleDoc>();
                SaleDoc c = new SaleDoc();
                c.Id = 0;
                saleDocs.Add(c);
            }
            try { 
            var respPurchaseDoc = client.GetAsync(APP_CONNECT + API_CON_TYPE.PurchaseDoc.ToString()).Result;
                var jsonRespPurchaseDoc = respPurchaseDoc.Content.ReadAsStringAsync().Result;
                purchaseDocs = JsonConvert.DeserializeObject<List<PurchaseDoc>>(jsonRespPurchaseDoc);
            }
            catch
            {

            }
            bookAndDescs.Clear();
            foreach (var item in books)
            {
                var bnd = new BookAndDesc();
                bnd.book = item;
                foreach (var item1 in bookFullDescriptions)
                {
                    if (item.Id == item1.Id)
                    {
                        bnd.bookFullDescription = item1;
                    }
                }
                bookAndDescs.Add(bnd);
            }
            custAndDescs.Clear();
            foreach (var item in custumers)
            {
                var cnd = new custAndDesc();
                cnd.custumer = item;
                foreach (var item1 in custumerDescriptions)
                {
                    if (item.Id == item1.Id)
                    {
                        cnd.custumerDescription = item1;
                    }
                }
                custAndDescs.Add(cnd);
            }



        }



        private void PricePanel_ShowTrue_HideFalse(bool check)
        {
            spBook.Visibility = (check==true) ? Visibility.Visible : Visibility.Hidden;
            descStackPanel.Visibility = (check == true) ? Visibility.Visible : Visibility.Hidden;
            tbDescription.Visibility = (check == true) ? Visibility.Visible : Visibility.Hidden;
            imgDesc.Visibility = (check == true) ? Visibility.Visible : Visibility.Hidden;
        }
        private void CustPanel_ShowTrue_HideFalse(bool check)
        {
            CustDescStackPanel.Visibility = (check == true) ? Visibility.Visible : Visibility.Hidden;
            spCust.Visibility = (check == true) ? Visibility.Visible : Visibility.Hidden;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
           
            RadioButton pressed = (RadioButton)sender;
            string check = pressed.Content.ToString();
            DataTable dt;
            //MessageBox.Show(pressed.Content.ToString());
            switch (check)
            {
                case "Номенклатура":
                    LoadBooks();
                    rbChose = check;
                    stacDocBtns.Visibility = Visibility.Hidden;
                    dataGrid1.Visibility = Visibility.Visible;
                    dataGridJournal.Visibility = Visibility.Hidden;
                    dataGridSellJournal.Visibility = Visibility.Hidden;
                    stackPanelFilters.Visibility = Visibility.Visible;
                    labelsStackPanelFilters.Visibility = Visibility.Visible;
                    wrapPanelClientFilter.Visibility = Visibility.Hidden;

                    //spBook.Visibility = Visibility.Visible;
                    //descStackPanel.Visibility = Visibility.Visible;
                    //tbDescription.Visibility = Visibility.Visible;
                    //imgDesc.Visibility = Visibility.Visible;
                    PricePanel_ShowTrue_HideFalse(true);
                    //CustDescStackPanel.Visibility = Visibility.Hidden;
                    //spCust.Visibility = Visibility.Hidden;
                    CustPanel_ShowTrue_HideFalse(false);
                    dt = BookManager.LoadBook(books, bookFullDescriptions);
                    dataGrid1.ItemsSource = dt.DefaultView;
                    break;

                case "Контрагенты":
                    LoadCustumers();
                    rbChose = check;
                    stacDocBtns.Visibility = Visibility.Hidden;
                    dataGrid1.Visibility = Visibility.Visible;
                    dataGridJournal.Visibility = Visibility.Hidden;
                    dataGridSellJournal.Visibility = Visibility.Hidden;
                    stackPanelFilters.Visibility = Visibility.Hidden;
                    labelsStackPanelFilters.Visibility = Visibility.Hidden;
                    wrapPanelClientFilter.Visibility = Visibility.Visible;

                    //spBook.Visibility = Visibility.Hidden;
                    //descStackPanel.Visibility = Visibility.Hidden;
                    //tbDescription.Visibility = Visibility.Hidden;
                    //imgDesc.Visibility = Visibility.Hidden;
                    PricePanel_ShowTrue_HideFalse(false);
                    //CustDescStackPanel.Visibility = Visibility.Visible;
                    //spCust.Visibility = Visibility.Visible;
                    CustPanel_ShowTrue_HideFalse(true);
                    dt = CustManager.LoadCustemer(custumers);
                    dataGrid1.ItemsSource = dt.DefaultView;
                    break;
                case "Журнал Приходных":
                    LoadPurchaseDocs();
                    PricePanel_ShowTrue_HideFalse(false);
                    CustPanel_ShowTrue_HideFalse(false);
                    stacDocBtns.Visibility = Visibility.Visible;
                    dataGridJournal.Visibility = Visibility.Visible;
                    dataGridSellJournal.Visibility = Visibility.Hidden;
                    stackPanelFilters.Visibility = Visibility.Hidden;
                    labelsStackPanelFilters.Visibility = Visibility.Hidden;
                    wrapPanelClientFilter.Visibility = Visibility.Hidden;
                    AddPurDocBtn.Visibility = Visibility.Visible;
                    AddSelDocBtn.Visibility = Visibility.Hidden;
                    rbChose = check;
                    dt = PurchaseDocsManager.LoadPurchaseDocsDataTable(purchaseDocs,custumers);
                    dataGridJournal.ItemsSource = dt.DefaultView;
                    break;
                case "Журнал Расходных":
                    LoadSaleDocs();
                    PricePanel_ShowTrue_HideFalse(false);
                    CustPanel_ShowTrue_HideFalse(false);
                    AddSelDocBtn.Visibility = Visibility.Visible;
                    AddPurDocBtn.Visibility = Visibility.Hidden;
                    stacDocBtns.Visibility = Visibility.Visible;
                    dataGridJournal.Visibility = Visibility.Hidden;
                    dataGridSellJournal.Visibility = Visibility.Visible;
                    stackPanelFilters.Visibility = Visibility.Hidden;
                    wrapPanelClientFilter.Visibility = Visibility.Hidden;
                    labelsStackPanelFilters.Visibility = Visibility.Hidden;
                    rbChose = check;
                    dt = SaleDocManager.LoadSaleDocsDataTable(saleDocs, custumers);
                    dataGridSellJournal.ItemsSource = dt.DefaultView;
                    break;
                default: MessageBox.Show("wrong select");
                    break;

            }


        }
        private void RadioButton_Checked_1(object sender, RoutedEventArgs e) //нашел вывод в ДатаГрид симпотичней моего... может позже переделаю на него. А пока не вызываемый метод.
        {
            var s = books.Select(x => new { x.Id, x.BarcodeISBN, x.BookTitle });
            dataGrid1.ItemsSource = s.ToList();
        }

        public void DGLoadBookDesc(List<Book> books, List<BookFullDescription> bookFullDescriptions, int BookId)
        {
            descStackPanel.Visibility = Visibility.Visible;
            tbBookTitle.Text = "";
            tbBarcode.Text = "";
            tbFirstYear.Text = "";
            tbLastYear.Text = "";
            tbSeria.Text = "";
            tbSection.Text = "";
            tbAuthor.Text = "";
            tbPublisher.Text = "";
            tbPurchasePrice.Text = "";
            tbRetailPrice.Text = "";
            tbDescription.Text = "";
            imgDesc.Background = null;
            Book book = null;
            BookFullDescription bookFull = null;
            try
            {
                foreach (var item in books)
                {
                    if (item.Id == BookId)
                    { book = item; }
                }
                foreach (var item in bookFullDescriptions)
                {
                    if (item.Id == BookId)
                        bookFull = item;
                }
                tbBookTitle.Text = book.BookTitle;
                tbBarcode.Text = book.BarcodeISBN;
                tbFirstYear.Text = bookFull.FirstYearBookPublishing;
                tbLastYear.Text = bookFull.YearBookPublishing;
                tbSeria.Text = bookFull.Serie;
                tbSection.Text = bookFull.Section;
                tbAuthor.Text = bookFull.Author;
                tbPublisher.Text = bookFull.Publisher;
                tbPurchasePrice.Text = book.PurchasePrice.ToString();
                tbRetailPrice.Text = book.RetailPrice.ToString();
                tbDescription.Text = bookFull.Description;
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(bookFull.ImageUrl, UriKind.RelativeOrAbsolute));
                imgDesc.Background = ib;
            }
            catch (Exception e)
            {
                MessageBox.Show("LoadBookDesc Error: " + e.ToString());
            }
        }
        
        
        
        
        
        
        //это штука загружает Полное Описание в выделенные секции. Вызывается из ивента на DataGrid по клику на поле
        

        public void DGLoadCustDesc(List<Custumer> custumers, List<CustumerDescription> custumerDescriptions, int CustId)
        {
            tbCustTitle.Text = "";
            tbCustFullName.Text = "";
            tbCustAddress.Text = "";
            tbCustPhone.Text = "";
            tbCustEmail.Text = "";
            tbCustBalance.Text = "";
            labelCustType.Content = "";
            Custumer custumer = null;
            CustumerDescription custumerDescription = null;
            try
            {
                foreach (var item in custumers)
                {
                    if (item.Id == CustId)
                        custumer = item;
                }
                foreach (var item in custumerDescriptions)
                {
                    if (item.Id == CustId)
                        custumerDescription = item;
                }
                tbCustTitle.Text = custumer.CustumerTitle;
                tbCustFullName.Text = custumerDescription.FullName;
                tbCustAddress.Text = custumerDescription.Address;
                tbCustPhone.Text = custumerDescription.Phone;
                tbCustEmail.Text = custumerDescription.Email;
                tbCustBalance.Text = custumer.Balance.ToString();
                labelCustType.Content = (custumer.BuyerTrue_SuplierFalse)?"Покупатель":"Поставщик";
            }
            catch(Exception e)
            {
                MessageBox.Show("DGLoadCustDesc Error: " + e.ToString());
            }
        }
        
        
        //это штука загружает Полное Описание в выделенные секции. Вызывается из ивента на DataGrid по клику на поле

        public void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)//этот весь метод для того, что загрузить в область Полного Описания книги(или клиента)
        {
            //--------------------------------------------------------------------------------------------------------------------------------------
            string StringBookId = "";
            try
            {


                selectedColumn = dataGrid1.CurrentCell.Column.DisplayIndex;
                var selectedCell = dataGrid1.SelectedCells[0];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);    //эта вся махинация, чтобы получить ИД выбранной книги 
                if (cellContent is TextBlock)
                {
                    StringBookId = (cellContent as TextBlock).Text;
                }
                selectedId = Convert.ToInt32(StringBookId);
                //---------------------------------------------------------------------------------------------------------------------------------------
                switch (rbChose)
                {
                    case "Номенклатура":
                        DGLoadBookDesc(books, bookFullDescriptions, selectedId);
                        break;
                    case "Контрагенты":
                        DGLoadCustDesc(custumers, custumerDescriptions, selectedId);
                        break;

                    default:
                        MessageBox.Show("wrong Chose DataGrid1_SelectionChanged");
                        break;
                }
            }
            catch
            {

            }
        }

        public void DataGridJpurnal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string StringBookId = "";
            try
            {


                selectedColumn = dataGridJournal.CurrentCell.Column.DisplayIndex;
                var selectedCell = dataGridJournal.SelectedCells[0];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);    //эта вся махинация, чтобы получить ИД выбранной книги 
                if (cellContent is TextBlock)
                {
                    StringBookId = (cellContent as TextBlock).Text;
                }
                selectedIdJournal = Convert.ToInt32(StringBookId);
                //---------------------------------------------------------------------------------------------------------------------------------------
               
            }
            catch
            {

            }
        }

        public void DataGridSellJpurnal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string StringBookId = "";
            selectedIdJournal = 0;
            try
            {


                selectedColumn = dataGridSellJournal.CurrentCell.Column.DisplayIndex;
                var selectedCell = dataGridSellJournal.SelectedCells[0];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);    //эта вся махинация, чтобы получить ИД выбранной книги 
                if (cellContent is TextBlock)
                {
                   
                       StringBookId = (cellContent as TextBlock).Text;
                }
                selectedIdJournal = Convert.ToInt32(StringBookId);
                //---------------------------------------------------------------------------------------------------------------------------------------

            }
            catch
            {

            }
        }


        private void AddCustBtn_Click(object sender, RoutedEventArgs e) //создание нового Клиента, по факту, переводит в диалогове окно для создания, там уже вся логика
        {
            tbCustTitle.Text = "";
            tbCustFullName.Text = "";
            tbCustAddress.Text = "";
            tbCustPhone.Text = "";
            tbCustEmail.Text = "";
            tbCustBalance.Text = "";
            labelCustType.Content = "";
            LoadCustumers();
            int id = custumers.Select(p => p.Id).Max();
            DialogCreateCustomer createCustomer = new DialogCreateCustomer(id);
            if (createCustomer.ShowDialog() == true)
            {
                LoadCustumers();
                DataTable dt;
                dt = CustManager.LoadCustemer(custumers);
                dataGrid1.ItemsSource = dt.DefaultView;
            }
            
        }

        //public void EditCust(int idCust)
        //{
        //    DialogEditCust dialogEditCust = new DialogEditCust(idCust);
        //    if (dialogEditCust.ShowDialog() == true)
        //    {
        //        MessageBox.Show("edit done");
        //    }
        //}

       
        private void EditCustBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DialogEditCust dialogEditCust = new DialogEditCust(selectedId);
                if (dialogEditCust.ShowDialog() == true)
                {
                    dataGrid1.ItemsSource = null;
                    tbCustTitle.Text = "";
                    tbCustFullName.Text = "";
                    tbCustAddress.Text = "";
                    tbCustPhone.Text = "";
                    tbCustEmail.Text = "";
                    tbCustBalance.Text = "";
                    labelCustType.Content = "";
                    LoadCustumers();
                    DataTable dt;
                    dt = CustManager.LoadCustemer(custumers);
                    dataGrid1.ItemsSource = dt.DefaultView;
                    MessageBox.Show("edit done");
                }
                LoadCustumers();
                //EditCust(selectedId);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error select from grid in Edit-Click: " + ex);
            }
        }

        private void AddBookBtn_Click(object sender, RoutedEventArgs e)
        {
            int id = books.Select(p => p.Id).Max();
            DialCreateBook createBook = new DialCreateBook(id);
            if (createBook.ShowDialog() == true)
            {
                LoadBooks();
                DataTable dt;
                dt = BookManager.LoadBook(books,bookFullDescriptions);
                dataGrid1.ItemsSource = dt.DefaultView;
               // MessageBox.Show("if ");
            }


        }

        private void EditBookBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DialogEditBook dialogEditBook = new DialogEditBook(selectedId);
                if (dialogEditBook.ShowDialog() == true)
                {
                    dataGrid1.ItemsSource = null;
                    tbBookTitle.Text = "";
                    tbBarcode.Text = "";
                    tbFirstYear.Text = "";
                    tbLastYear.Text = "";
                    tbSeria.Text = "";
                    tbSection.Text = "";
                    tbAuthor.Text = "";
                    tbPublisher.Text = "";
                    tbPurchasePrice.Text = "";
                    tbRetailPrice.Text = "";
                    tbDescription.Text = "";
                    
                    //imgDesc = null;
                    LoadBooks();
                    DataTable dt = BookManager.LoadBook(books, bookFullDescriptions);
                    dataGrid1.ItemsSource = dt.DefaultView;

                    MessageBox.Show("edit done");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error in edit book: " + ex);
            }
        }
        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            switch (rbChose)
            {
                case "Номенклатура":
                    Book b = books.Find(i => i.Id == selectedId);
                    var json = JsonConvert.SerializeObject(b.Id);
                    var responce = client.GetAsync(APP_CONNECT + API_CON_TYPE.BookDel.ToString() + "/" + b.Id).Result;
                    var jsonResp = responce.Content.ReadAsStringAsync().Result;
                    ErrorsMessage t = JsonConvert.DeserializeObject<ErrorsMessage>(jsonResp);
                    if (t.boolen == 0)
                    {
                        MessageBox.Show(t.message);
                    }
                    LoadDatas();
                    dt = BookManager.LoadBook(books, bookFullDescriptions);
                    dataGrid1.ItemsSource = dt.DefaultView;
                    break;
                case "Контрагенты":
                    Custumer c = custumers.Find(i => i.Id == selectedId);
                    var jsonC = JsonConvert.SerializeObject(c.Id);
                    var responceC = client.GetAsync(APP_CONNECT + API_CON_TYPE.CustumerDel.ToString() + "/" + c.Id).Result;
                    var jsonRespC = responceC.Content.ReadAsStringAsync().Result;
                    ErrorsMessage tc = JsonConvert.DeserializeObject<ErrorsMessage>(jsonRespC);
                    if (tc.boolen == 0)
                    {
                        MessageBox.Show(tc.message);
                    }
                    LoadDatas();
                    dt = CustManager.LoadCustemer(custumers);
                    dataGrid1.ItemsSource = dt.DefaultView;
                    
                    break;
                default:
                    MessageBox.Show("wrong");
                    break;
            }


        }


        private void DelDocBtn_Click(object sender, RoutedEventArgs e)
        {
            
            DataTable dt = new DataTable();
            switch (rbChose)
            {
                case "Журнал Приходных":
                   
                    PurchaseDoc purchaseDoc = purchaseDocs.Find(i => i.Id == selectedIdJournal);
                    var json = JsonConvert.SerializeObject(purchaseDoc.Id);
                    var responce = client.GetAsync(APP_CONNECT + API_CON_TYPE.PurchaseDocDel.ToString() + "/" + purchaseDoc.Id).Result;
                    var jsonResp = responce.Content.ReadAsStringAsync().Result;
                    ErrorsMessage t = JsonConvert.DeserializeObject<ErrorsMessage>(jsonResp);
                    if (t.boolen == 0)
                    {
                        MessageBox.Show(t.message);
                    }
                    LoadDatas();
                    dt = PurchaseDocsManager.LoadPurchaseDocsDataTable(purchaseDocs, custumers);
                    dataGridJournal.ItemsSource = dt.DefaultView;
                    //MessageBox.Show(purchaseDoc.Id.ToString());

                    break;
                case "Журнал Расходных":
                    SaleDoc saleDoc = saleDocs.Find(i => i.Id == selectedIdJournal);
                    var jsonS = JsonConvert.SerializeObject(saleDoc.Id);
                    var responceS = client.GetAsync(APP_CONNECT + API_CON_TYPE.SaleDocDel.ToString() + "/" + saleDoc.Id).Result;
                    var jsonRespS = responceS.Content.ReadAsStringAsync().Result;
                    ErrorsMessage ts = JsonConvert.DeserializeObject<ErrorsMessage>(jsonRespS);
                    if (ts.boolen == 0)
                    {
                        MessageBox.Show(ts.message);
                    }
                    LoadDatas();
                    dt = SaleDocManager.LoadSaleDocsDataTable(saleDocs, custumers);
                    dataGridSellJournal.ItemsSource = dt.DefaultView;

                    // MessageBox.Show(saleDoc.Id.ToString());

                    break;
                default:
                    MessageBox.Show("wrong chose in DelDocBtn_Click");
                    break;
            }


        }



        private void DataGridJournal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int sId=0;
            DialogPurchaseDoc dialogPurchase;
            try
            {
                string StringBookId = "";
                var selectedColumn = dataGridJournal.CurrentCell.Column.DisplayIndex;
                var selectedCell = dataGridJournal.SelectedCells[0];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);    //эта вся махинация, чтобы получить ИД выбранной книги 
                if (cellContent is TextBlock)
                {
                    StringBookId = (cellContent as TextBlock).Text;
                }
                sId = Convert.ToInt32(StringBookId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error select ID" + ex);
            }
            PurchaseDoc purchaseDoc = purchaseDocs.Find(i => i.Id == sId);
            try
            {
                Custumer custumer = custumers.Find(i => i.Id == purchaseDoc.CustumerId);

                var respDocRecs = client.GetAsync(APP_CONNECT + API_CON_TYPE.PurchaseDocRec.ToString() + "/" + purchaseDoc.Id).Result;
                var jsonRespDocRecsk = respDocRecs.Content.ReadAsStringAsync().Result;
                var purchaseRecs = JsonConvert.DeserializeObject<List<PurchaseDocRec>>(jsonRespDocRecsk);

                dialogPurchase = new DialogPurchaseDoc(custumer, purchaseDoc, books, purchaseRecs, bookFullDescriptions, false);
            }
            catch
            {
                return;
            }
            dialogPurchase.Show();

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            int sId = 0;
            try
            {
                string StringBookId = "";
                var selectedColumn = dataGridJournal.CurrentCell.Column.DisplayIndex;
                var selectedCell = dataGridJournal.SelectedCells[0];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);    //эта вся махинация, чтобы получить ИД выбранной книги 
                if (cellContent is TextBlock)
                {
                    StringBookId = (cellContent as TextBlock).Text;
                }
                sId = Convert.ToInt32(StringBookId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error select ID" + ex);
            }

            var json = JsonConvert.SerializeObject(sId);
            var responce = client.GetAsync(APP_CONNECT + API_CON_TYPE.PurchaseDocChange.ToString()+"/"+ sId).Result;
            var jsonResp = responce.Content.ReadAsStringAsync().Result;
            ErrorsMessage t = JsonConvert.DeserializeObject<ErrorsMessage>(jsonResp);
            if (t.boolen == 0)
            {
                MessageBox.Show(t.message);
            }
            
            LoadPurchaseDocs();
            
            MessageBox.Show(json.ToString());
        }


        private void MenuItemSell_Click(object sender, RoutedEventArgs e)
        {
            int sId = 0;
            try
            {
                string StringBookId = "";
                var selectedColumn = dataGridSellJournal.CurrentCell.Column.DisplayIndex;
                var selectedCell = dataGridSellJournal.SelectedCells[0];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);    //эта вся махинация, чтобы получить ИД выбранной книги 
                if (cellContent is TextBlock)
                {
                    StringBookId = (cellContent as TextBlock).Text;
                }
                sId = Convert.ToInt32(StringBookId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error select ID" + ex);
            }
            

            var json = JsonConvert.SerializeObject(sId);
            var responce = client.GetAsync(APP_CONNECT + API_CON_TYPE.SaleDocChange.ToString() + "/" + sId).Result;
            var jsonResp = responce.Content.ReadAsStringAsync().Result;
            ErrorsMessage t = JsonConvert.DeserializeObject<ErrorsMessage>(jsonResp);
            if (t.boolen == 0)
            {
                MessageBox.Show(t.message);
            }
            


            LoadSaleDocs();

            MessageBox.Show(json.ToString());
        }



        private void AddPurDocBtn_Click(object sender, RoutedEventArgs e)
        {
            Custumer custumer = null;
            var c = custumers.Where((i => i.CustumerTitle.Contains("Основной Поставщик")));
            foreach (var item in c)
            {
                custumer = item;
            }
            PurchaseDoc purchaseDoc = new PurchaseDoc();
            purchaseDoc.Status = StaticDatas.DocStatuses.Непроведен.ToString();
            List<PurchaseDocRec> purchaseRecs = new List<PurchaseDocRec>();
            
            DialogPurchaseDoc dialogPurchase = new DialogPurchaseDoc(custumer, purchaseDoc, books, purchaseRecs, bookFullDescriptions, true);
            dialogPurchase.Show();


        }
        private void AddSelDocBtn_Click(object sender, RoutedEventArgs e)
        {
            Custumer custumer = null;
            var c = custumers.Where(i => i.CustumerTitle.Contains("Основной Покупатель"));
            foreach (var item in c)
            {
                custumer = item;
            }

            SaleDoc saleDoc = new SaleDoc();
            saleDoc.Status = StaticDatas.DocStatuses.Непроведен.ToString();
            List<SaleDocRec> saleRecs = new List<SaleDocRec>();


            DialogSellDoc dialogSell = new DialogSellDoc(custumer, saleDoc, saleRecs, books, bookFullDescriptions, true);
            dialogSell.Show();
        }





        private void DataGridSellJournal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int sId = 0;
            DialogSellDoc dialogSell;
            try
            {
                string StringBookId = "";
                var selectedColumn = dataGridSellJournal.CurrentCell.Column.DisplayIndex;
                var selectedCell = dataGridSellJournal.SelectedCells[0];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);    //эта вся махинация, чтобы получить ИД выбранной книги 
                if (cellContent is TextBlock)
                {
                    StringBookId = (cellContent as TextBlock).Text;
                }
                sId = Convert.ToInt32(StringBookId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error select ID" + ex);
            }
            try
            {
                SaleDoc saleDoc = saleDocs.Find(i => i.Id == sId);
                Custumer custumer = custumers.Find(i => i.Id == saleDoc.CustumerId);

                var respDocRecs = client.GetAsync(APP_CONNECT + API_CON_TYPE.SaleDocRec.ToString() + "/" + saleDoc.Id).Result;
                var jsonRespDocRecsk = respDocRecs.Content.ReadAsStringAsync().Result;
                var saleRecs = JsonConvert.DeserializeObject<List<SaleDocRec>>(jsonRespDocRecsk);

                dialogSell = new DialogSellDoc(custumer, saleDoc, saleRecs, books, bookFullDescriptions, false);
            }
            catch
            {
                return;
            }
            dialogSell.Show();
            
        }


        private void TbBarcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid1.ItemsSource = null;
            dataGrid1.Items.Clear();
            string barcode = tbBarcodeFilter.Text;
            string title = tbTitleFilter.Text;
            string author = tbAuthorFilter.Text;
            string serie = tbSerieFilter.Text;
            string section = tbSectionFilter.Text;

            var dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Штрихкод");
            dt.Columns.Add("Наименование");
            dt.Columns.Add("Цена");
            dt.Columns.Add("Автор");
            dt.Columns.Add("Серия");
            dt.Columns.Add("Секция");
            dt.Columns.Add("кол-во");

            var bd = bookAndDescs.Where(i => i.book.BarcodeISBN.Contains(barcode)).Where(q => 
            q.book.BookTitle.ToLower().Contains(title.ToLower())).Where(w => 
            w.bookFullDescription.Author.ToLower().Contains(author.ToLower())).Where(t => 
            t.bookFullDescription.Serie.ToLower().Contains(serie.ToLower())).Where(r => 
            r.bookFullDescription.Section.ToLower().Contains(section.ToLower()));

            foreach (var item in bd)
            {
                dt.Rows.Add(item.book.Id, item.book.BarcodeISBN, item.book.BookTitle, item.book.RetailPrice, 
                    item.bookFullDescription.Author, item.bookFullDescription.Serie, 
                    item.bookFullDescription.Section, item.book.Count);
            }
            dataGrid1.ItemsSource = dt.DefaultView;
        }




        private void tbCustTitleFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string title = tbCustTitleFilter.Text;
            var dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Имя Клиента");
            var cd = custAndDescs.Where(i => i.custumer.CustumerTitle.ToLower().Contains(title.ToLower()));
            foreach (var item in cd)
            {
                dt.Rows.Add(item.custumer.Id, item.custumer.CustumerTitle);
            }
            dataGrid1.ItemsSource = dt.DefaultView;
        }



    }

}

using online_store;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace practical_task_17
{
    public class Interaction : INotifyPropertyChanged
    {
        public Interaction(int id=0)
        {
            idBuyer = id;
        }
        public Action CloseAction { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged (String prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        //Открываю выбранное окно
        private RelayCommand openTheSelectedWindow;
        public RelayCommand OpenTheSelectedWindow
        {
            get
            {
                return openTheSelectedWindow ?? new RelayCommand(obj =>
                {
                    if (i == 0)
                    {
                        Authorization authorization = new Authorization();
                        authorization.Show();
                    }
                    if (i == 1)
                    {
                        using (MSSQLLocalOnlineStoreEntities db = new MSSQLLocalOnlineStoreEntities())
                        {
                            SellerWindow sellerWindow = new SellerWindow();
                            var loadTables = db.TableInfoBuyer.Join(db.TableWorkingWithTheBuyer,
                                buyer => buyer.id,
                                product => product.Id,
                                (buyer, product) => new
                                {
                                    buyer.id,
                                    buyer.Surname,
                                    buyer.MiddleName,
                                    buyer.PhoneNumber,
                                    buyer.Email,
                                    buyer.Name,
                                    product.ProductCode,
                                    product.ProductName,
                                });

                            sellerWindow.gridView.ItemsSource = loadTables.ToList();
                            sellerWindow.Show();
                        }
                    }
                    CloseAction();
                }
                );
            }
        }
        /// <summary>
        /// Заголовок таблицы покупателя
        /// </summary>
        public string Tittle
        {
            get
            {
                return   $"Все ваши покупки. Ваш id = {idBuyer}";
            } 
        }
        //Проверяю есть ли такой покупатель, открывю его окно если true
        private RelayCommand newAuthentication;
        public RelayCommand NewAuthentication
        {
            get
            {
                return newAuthentication ?? new RelayCommand(obj =>
                {
                    idBuyer = DataManagement.Authentication(Login, Password);
                    if (idBuyer == -1) MessageBox.Show("Проверьте правильность заполнения");
                    else
                    {
                        BuyerWindow buyerWindow = new BuyerWindow(idBuyer);
                        buyerWindow.gridView.ItemsSource = DataManagement.LoadTableWorkingWithTheBuyer(idBuyer);
                        buyerWindow.Show();
                        CloseAction();
                    }
                }
                );
            }
        }
        //Открываю окно регистрации
        private RelayCommand openTheRegistrationWindow;
        public RelayCommand OpenTheRegistrationWindow
        {
            get
            {
                return openTheRegistrationWindow ?? new RelayCommand(obj =>
                {
                    СustomerВataEntry сustomerВataEntry = new СustomerВataEntry();
                    сustomerВataEntry.Show();
                    CloseAction();
                }
                );
            }
        }
        #region Свойства
        // Получаю индекс из ComboBox
        public int i { get; set; }
        private string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value.Replace(" ", "");
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value.Replace(" ", "");
            }
        }
        private string middleName;
        public string MiddleName
        {
            get { return middleName; }
            set
            {
                middleName = value.Replace(" ", "");
            }
        }
        public long PhoneNumber { get; set; }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value.Replace(" ", "");
            }
        }
        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value.Replace(" ", "");
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value.Replace(" ", "");
            }
        }
        private string productCode;
        public string ProductCode
        {
            get { return productCode; }
            set
            {
                productCode = value.Replace(" ", "");
            }
        }
        public int id { get; set; }
        public string ProductName { get; set; }
        private int idBuyer { get; set; }//хранит id покупателя
        #endregion
        //Регистрирую нового покупателя
        private RelayCommand registrationOfANewBuyer;
        public RelayCommand RegistrationOfANewBuyer
        {
            get
            {
                return registrationOfANewBuyer ?? new RelayCommand(obj =>
                {
                    try
                    {
                        string message = DataManagement.AddUser(Surname, Name, MiddleName,
                        PhoneNumber, Email, Login, Password);
                        if (message == "Готово")
                        {
                            if (String.IsNullOrEmpty(Login)|| String.IsNullOrEmpty(password))
                                MessageBox.Show("Заполните все поля");
                            else
                            {
                                MessageBox.Show(message);
                                idBuyer = DataManagement.Authentication(Login, Password);
                                BuyerWindow buyerWindow = new BuyerWindow(idBuyer);
                                buyerWindow.Show();
                                CloseAction();
                            }
                        }
                        else MessageBox.Show(message);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Заполните все поля");
                    }
                }
                );
            }
        }
        //Открываю окно добавления товара
        private RelayCommand newAddProductWindow;
        public RelayCommand NewAddProductWindow
        {
            get
            {
                return newAddProductWindow ?? new RelayCommand(obj =>
                {
                    AddProduct addProduct = new AddProduct();
                    addProduct.Show();
                }
                );
            }
        }
        //Добавление товара
         private RelayCommand addProductWindow;
        public RelayCommand AddProductWindow
        {
            get
            {
                return addProductWindow ?? new RelayCommand(obj =>
                {
                    if (id > DataManagement.Authentication("", "", true))
                        MessageBox.Show("Покупатель с таким id не найден!");
                    else
                    {
                        try
                        {
                            DataManagement.AddProduct(id, ProductName, ProductCode);
                            CloseAction();
                        }
                        catch (Exception )
                        {
                            MessageBox.Show("Заполните все поля");
                        }
                    }
                }
                );
            }
        }
    }
}

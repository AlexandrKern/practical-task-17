using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace practical_task_17
{
    public static class DataManagement
    {
        /// <summary>
        /// Добавляет нового покупателя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string AddUser(string surname, string name, string middleName, long phoneNumber,string email ,string login,string password)
        {
            string result = "Уже есть пользователь с таким логином!";
            using(MSSQLLocalOnlineStoreEntities db = new MSSQLLocalOnlineStoreEntities())
            {
                bool check = db.TableLoginPassword.Any(x => x.Login == login);
                if (!check) 
                {
                    TableInfoBuyer tableInfoBuyer = new TableInfoBuyer
                    {
                        Name = name,
                        MiddleName = middleName,
                        Surname = surname,
                        PhoneNumber = phoneNumber,
                        Email = email
                    };
                    db.TableInfoBuyer.Add(tableInfoBuyer);
                    TableLoginPassword tableLogin = new TableLoginPassword 
                    { Login =  login,Password = password };
                    db.TableLoginPassword.Add(tableLogin);
                    db.SaveChanges();
                    result = "Готово";
                }
                return result;
            }
        }
        /// <summary>
        /// Проверка логина и пароля
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static int Authentication (string login = "",string password = "",bool flag = false)
        {
            int result = -1;
            using (MSSQLLocalOnlineStoreEntities db = new MSSQLLocalOnlineStoreEntities())
            {
                if (flag)
                {
                    db.TableLoginPassword.Load();
                    List<TableLoginPassword> tableLogins = db.TableLoginPassword.Local.ToList();
                    result = tableLogins.Count;
                    return result;
                }
                else
                {
                    db.TableLoginPassword.Load();
                    List<TableLoginPassword> tableLogins = db.TableLoginPassword.Local.ToList();
                    for (int i = 0; i < tableLogins.Count; i++)
                    {
                        if (login == tableLogins[i].Login && password == tableLogins[i].Password)
                            result = i+1;
                    }
                    return result;
                }
            }
        }
        /// <summary>
        /// Добавляет товар
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productName"></param>
        /// <param name="productCode"></param>
        public static void AddProduct(int id,string productName,string productCode)
        {
            using(MSSQLLocalOnlineStoreEntities db = new MSSQLLocalOnlineStoreEntities())
            {
                TableWorkingWithTheBuyer withTheBuyer = new TableWorkingWithTheBuyer
                {
                    Id = id,
                    ProductCode = productCode,
                    ProductName = productName
                };
                db.TableWorkingWithTheBuyer.Add(withTheBuyer);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Загружает таблицу товара
        /// </summary>
        /// <returns></returns>
        public static List<TableWorkingWithTheBuyer> LoadTableWorkingWithTheBuyer(int id)
        {
            using (MSSQLLocalOnlineStoreEntities db = new MSSQLLocalOnlineStoreEntities())
            {
                db.TableWorkingWithTheBuyer.Load();
                List<TableWorkingWithTheBuyer> tableWorkingWithTheBuyer = db.TableWorkingWithTheBuyer.Local.ToList();
                List<TableWorkingWithTheBuyer> tableWorkingWithTheBuyer1 = new List<TableWorkingWithTheBuyer>();
                for (int i = 0; i < tableWorkingWithTheBuyer.Count; i++)
                {
                    if (id == tableWorkingWithTheBuyer[i].Id)
                    {
                        tableWorkingWithTheBuyer1.Add(tableWorkingWithTheBuyer[i]);
                    }
                }
                return tableWorkingWithTheBuyer1;
            }
        }
    }
}

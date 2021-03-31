using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using CourseWork.Model;
using System.Text.RegularExpressions;

namespace CourseWork.View
{
    class ViewModel : INotifyPropertyChanged
    {
        public string CurrentAdminDataGridObjectString = "Orders";
        private string searchText;
        Goods goodToAdd = new Goods();
        Order orderToAdd = new Order();
        User currentUser = new User();
        SqlConnection connection;
        string connectionString;
        SqlDataAdapter adapter;
        DataTable usersTable = new DataTable();
        private DataView Itemsourse;
        private string loginText;
        private string chkRegPasswordText;
        private string regPasswordText;
        private string passwordText;
        private string passwordTextt;
        public string selectedPage = "AuthPage";
        string selectedUserPage = "UserDataPage";
        string selectedAdminPage = "AdminDataPage";

        public ObservableCollection<Order> ChangedOrders { get; set; } = new ObservableCollection<Order>();
        public ObservableCollection<Goods> Goodss { get; set; } = new ObservableCollection<Goods>();
        public ObservableCollection<Order> SearchOrders { get; set; } = new ObservableCollection<Order>();
        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();



        public void GetOrders()
        {
            try

            {
                Orders.Clear();
                SqlConnection connection;


                connection = new SqlConnection(connectionString);


                string sql = "SELECT * FROM Orders";


                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();


                if (reader.HasRows)
                {
                    if (!CurrentUser.Admin)
                        while (reader.Read())
                        {
                            string checklog = reader["Customer"].ToString();

                            {
                                if (checklog == currentUser.Login)
                                    Orders.Add(new Order(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetDateTime(6), reader.GetDateTime(7), reader.GetBoolean(8), reader.GetBoolean(9)));
                            }

                        }
                    else
                        while (reader.Read())
                        {
                            Orders.Add(new Order(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetDateTime(6), reader.GetDateTime(7), reader.GetBoolean(8), reader.GetBoolean(9)));

                        }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        public void GetUsers()
        {
            string sql = "SELECT * FROM Users";

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Users.Add(new User(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetBoolean(8)));
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }


        public void regist()
        {
            try
            {
                User user = new User();
                user.FirstName = RegFirstName;
                user.LastName = RegLastName;
                user.Country = RegCountry;
                user.TelephoneNumber = RegPhone;
                user.Login = RegLogin;
                user.Email = RegEmail;
                user.Organization = RegOrganization;
                user.Password = RegPasswordText;
                string UserId;
                string Passwordhash = null;

                bool flag = false;

                Passwordhash = user.Password.GetHashCode().ToString();
                string sql1 = "SELECT * FROM Users ";
                connection.Open();

                SqlCommand command2 = new SqlCommand(sql1, connection);
                SqlDataReader reader = command2.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())
                    {
                        string strr = reader["Email"].ToString();
                        string str = reader["Login"].ToString();
                        if ((str == RegLogin) || (strr == RegEmail))
                            flag = true;
                    }
                    if (!flag)
                    {
                        connection.Close();
                        string sqlExpression = "INSERT INTO Users (FirstName, LastName,Login,Email,Organization,Password,Admin,TelephoneNumber,Country) VALUES ('" + user.FirstName + "', '" + user.LastName + "', '" + user.Login + "','" + user.Email + "','" + user.Organization + "','" + user.Password + "','" + 0 + "','" + user.TelephoneNumber + "','" + user.Country + "') ";
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        UserId = (string)command.ExecuteScalar();
                        connection.Close();
                        MessageBox.Show("Вы успешно зарегистрированы!!!");
                    }
                    else MessageBox.Show("Пользователь с таким логином или Email уже существует");
                }
                reader.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        private Command acceptedOrders;
        public Command AcceptedOrders
        {
            get
            {
                return acceptedOrders ??
                  (acceptedOrders = new Command(obj =>
                  {
                      GetAccOrders();
                  }));
            }
        }
        public void GetAccOrders()
        {
            string sql = "SELECT * FROM Orders";

            SqlConnection connection = null;
            try
            {
                Orders.Clear();
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (!CurrentUser.Admin)
                        while (reader.Read())
                        {

                            string checklog = reader["Customer"].ToString();


                            if (checklog == currentUser.Login)
                                if ((bool)reader["Accepted"] && !(bool)reader["Complited"])
                                    Orders.Add(new Order(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetDateTime(6), reader.GetDateTime(7), reader.GetBoolean(8), reader.GetBoolean(9)));
                        }
                    else
                    {
                        while (reader.Read())
                        {
                            if ((bool)reader["Accepted"] && !(bool)reader["Complited"])
                                Orders.Add(new Order(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetDateTime(6), reader.GetDateTime(7), reader.GetBoolean(8), reader.GetBoolean(9)));
                        }
                    }
                }
                reader.Close();
            }
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            finally
            {
                connection?.Close();
            }
        }




        private Command complitedOrders;
        public Command ComplitedOrders
        {
            get
            {
                return complitedOrders ??
                  (complitedOrders = new Command(obj =>
                  {
                      GetCompOrders();
                  }));
            }
        }
        public void GetCompOrders()
        {
            string sql = "SELECT * FROM Orders";

            SqlConnection connection = null;
            try
            {
                Orders.Clear();
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (!CurrentUser.Admin)
                        while (reader.Read())
                        {
                            string checklog = reader["Customer"].ToString();


                            if (checklog == currentUser.Login)
                                if ((bool)reader["Complited"])

                                    Orders.Add(new Order(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetDateTime(6), reader.GetDateTime(7), reader.GetBoolean(8), reader.GetBoolean(9)));
                        }
                    else
                    {
                        while (reader.Read())
                        {
                            if ((bool)reader["Complited"])
                                Orders.Add(new Order(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetDateTime(6), reader.GetDateTime(7), reader.GetBoolean(8), reader.GetBoolean(9)));
                        }
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)

                    connection.Close();
            }
        }


        private Command compliteOrder;
        public Command CompliteOrder
        {
            get
            {
                return compliteOrder ??
                  (compliteOrder = new Command(obj =>
                  {
                      CompOrd();
                      GetOrders();
                  }));
            }
        }

        private Command saveUserChanges;
        public Command SaveUserChanges
        {
            get
            {
                return saveUserChanges ??
                  (saveUserChanges = new Command(obj =>
                  {
                      string pattern2 = @"^[а-яА-ЯёЁa-zA-Z0-9\.\,]+$";
                      string pattern1 = @"^[0-9]+$";
                      string pattern = @"^[а-яА-ЯёЁa-zA-Z]+$";
                      string pattern3 = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
                      Regex regex = new Regex(pattern3, RegexOptions.IgnoreCase);
                      Regex regex1 = new Regex(pattern1, RegexOptions.IgnoreCase);
                      Regex regex2 = new Regex(pattern2, RegexOptions.IgnoreCase);
                      Regex regex3 = new Regex(pattern, RegexOptions.IgnoreCase);

                      if (regex3.IsMatch(CurrentUser.FirstName) && regex3.IsMatch(CurrentUser.LastName) && regex3.IsMatch(CurrentUser.Country) && regex3.IsMatch(CurrentUser.Organization) && regex1.IsMatch(CurrentUser.TelephoneNumber))
                          if (regex.IsMatch(CurrentUser.Email))
                              SaveUscChanges();
                          else MessageBox.Show("Некорректный Email");
                      else MessageBox.Show("Некорректно заполнены поля");

                  }));
            }
        }
        public void SaveUscChanges()
        {
            try
            {
                bool check = false;

                bool flag = false;

                string sql1 = "SELECT * FROM Users ";
                connection.Open();

                SqlCommand command2 = new SqlCommand(sql1, connection);
                SqlDataReader reader = command2.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())
                    {
                        string strr = reader["Email"].ToString();
                        if (strr == CurrentUser.Email)
                            flag = true;
                        string str1 = reader["Password"].ToString();
                        if (passwordTextt == str1)
                            check = true;



                    }
                }
                connection.Close();



                if (!flag)
                {
                    if (check)
                    {
                        string sqlExpression = "update Users Set FirstName = '" + CurrentUser.FirstName + "' , LastName = '" + CurrentUser.LastName + "' , Email = '" + CurrentUser.Email + "', Country = '" + CurrentUser.Country + "' , Organization = '" + CurrentUser.Organization + "'  , TelephoneNumber = '" + CurrentUser.TelephoneNumber + "' where Login = '" + CurrentUser.Login + "'";
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        command.ExecuteNonQuery();
                        connection.Close();

                        MessageBox.Show("User is changed");
                    }
                    else MessageBox.Show("Неверно введен пароль");
                }
                else MessageBox.Show("Невозможно сохранить изменения. Пользователь с таким Email уже существует");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)

                    connection.Close();
            }
        }
        public void CompOrd()
        {
            try
            {
                {

                    string sqlExpression = "update Orders Set Complited = 'true' where OrderId = '" + SelectedOrder.OrderId + "'";
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    ChangedOrders.Add(new Order(selectedOrder.OrderId, selectedOrder.Good, selectedOrder.Customer, selectedOrder.TransportType, selectedOrder.DepartureAddress, selectedOrder.DestinationAddress, selectedOrder.TimeOfLoading, selectedOrder.TimeOfUnloading, selectedOrder.Accepted, selectedOrder.Complited));

                    MessageBox.Show("Order is Complited");


                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)

                    connection.Close();
            }
        }



        private Command deleteOrder;
        public Command DeleteOrder
        {
            get
            {
                return deleteOrder ??
                  (deleteOrder = new Command(obj =>
                  {
                      DelOrd();
                      GetOrders();
                  }));
            }
        }
        public void DelOrd()
        {
            try
            {
                {

                    string sqlExpression = "delete from Orders where OrderId = '" + SelectedOrder.OrderId + "'";
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Order is Deleted");


                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)

                    connection.Close();
            }
        }



        private Command acceptOrder;
        public Command AcceptOrder
        {
            get
            {
                return acceptOrder ??
                  (acceptOrder = new Command(obj =>
                  {

                      AccOrd();
                      GetOrders();

                  }));
            }
        }



        public void AccOrd()
        {
            try
            {
                {
                    string sql1 = "SELECT * FROM Goods ";
                    connection.Open();

                    SqlCommand command2 = new SqlCommand(sql1, connection);
                    SqlDataReader reader = command2.ExecuteReader();
                    if (reader.HasRows) // если есть данные
                    {
                        connection.Close();

                        string sqlExpression = "update Orders Set Accepted = 'true' where OrderId = '" + SelectedOrder.OrderId + "'";
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        command.ExecuteNonQuery();
                        connection.Close();
                        ChangedOrders.Add(new Order(selectedOrder.OrderId, selectedOrder.Good, selectedOrder.Customer, selectedOrder.TransportType, selectedOrder.DepartureAddress, selectedOrder.DestinationAddress, selectedOrder.TimeOfLoading, selectedOrder.TimeOfUnloading, selectedOrder.Accepted, selectedOrder.Complited));

                        MessageBox.Show("Order is accepted");
                    }

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)

                    connection.Close();
            }
        }




        private Command resetSearch;
        public Command ResetSearch
        {
            get
            {
                return resetSearch ??
                  (resetSearch = new Command(obj =>
                  {
                      CurrentAdminDataGridObjectString = "Order";
                      OnPropertyChanged("CurrentAdminDataGridObject");
                  }));
            }
        }
        private Command search;
        public Command Search
        {
            get
            {
                return search ??
                  (search = new Command(obj =>
                  {
                      Searching();
                      CurrentAdminDataGridObjectString = "SearchOrder";
                      OnPropertyChanged("CurrentAdminDataGridObject");
                  }));
            }
        }
        public void Searching()
        {
            try
            {
                SearchOrders.Clear();
                string sql2 = "select * from Orders Where Customer like '%" + SearchText + "%' or TransportType like '%" + SearchText + "%' or DepartureAddress like '%" + SearchText + "%' or DestinationAddress like '%" + SearchText + "%' or TimeOfLoading like '%" + SearchText + "%' or TimeOfUnloading like '%" + SearchText + "%'";
                connection.Open();

                SqlCommand command3 = new SqlCommand(sql2, connection);
                SqlDataReader reader = command3.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    if (!CurrentUser.Admin)
                        while (reader.Read())
                        {

                            string checklog = reader["Customer"].ToString();


                            if (checklog == currentUser.Login)
                                SearchOrders.Add(new Order(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetDateTime(6), reader.GetDateTime(7), reader.GetBoolean(8), reader.GetBoolean(9)));
                        }
                    else
                        while (reader.Read())
                            SearchOrders.Add(new Order(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetDateTime(6), reader.GetDateTime(7), reader.GetBoolean(8), reader.GetBoolean(9)));

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)

                    connection.Close();
            }
        }



        private Command createOrder;
        public Command CreateOrder
        {
            get
            {
                return createOrder ??
                  (createOrder = new Command(obj =>
                  {


                      string pattern2 = @"^[а-яА-ЯёЁa-zA-Z0-9\.\,]+$";
                      string pattern1 = @"^[0-9]+$";
                      string pattern = @"^[а-яА-ЯёЁa-zA-Z0-9]+$";
                      Regex regex1 = new Regex(pattern, RegexOptions.IgnoreCase);
                      Regex regex2 = new Regex(pattern1, RegexOptions.IgnoreCase);
                      Regex regex3 = new Regex(pattern2, RegexOptions.IgnoreCase);
                      if (!String.IsNullOrWhiteSpace(GoodToAdd.GoodName) && !String.IsNullOrWhiteSpace(GoodToAdd.GoodType) && !String.IsNullOrWhiteSpace(OrderToAdd.DepartureAddress) && !String.IsNullOrWhiteSpace(OrderToAdd.DestinationAddress) && !String.IsNullOrWhiteSpace(OrderToAdd.TransportType) && (GoodToAdd.GoodSize != 0) && (GoodToAdd.Weight != 0))
                          if (regex1.IsMatch(GoodToAdd.GoodName) && regex1.IsMatch(GoodToAdd.GoodType) && regex1.IsMatch(OrderToAdd.TransportType) && regex3.IsMatch(OrderToAdd.DepartureAddress) && regex3.IsMatch(OrderToAdd.DestinationAddress))
                              if (regex2.IsMatch(Convert.ToString(GoodToAdd.GoodSize)) && regex2.IsMatch(Convert.ToString(GoodToAdd.Weight)))
                                  AddOrder();
                              else MessageBox.Show("Неправильно заполнено поле 'Масса' или 'Объем'");
                          else MessageBox.Show("Неправильно заполнены поля!");
                      else MessageBox.Show("Заполнены не все поля!");
                  }));
            }
        }
        public void AddOrder()
        {
            try
            {
                string sql1 = "SELECT * FROM Goods ";
                connection.Open();

                SqlCommand command2 = new SqlCommand(sql1, connection);
                SqlDataReader reader = command2.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    connection.Close();
                    string sqlExpression = "INSERT INTO Goods (GoodName,GoodType,Weight,Size) VALUES ('" + GoodToAdd.GoodName + "', '" + GoodToAdd.GoodType + "', '" + GoodToAdd.Weight + "','" + GoodToAdd.GoodSize + "') ";
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                    connection.Close();

                }


                string sql3 = "SELECT GoodId FROM Goods Where GoodName = '" + GoodToAdd.GoodName + "' and GoodType= '" + GoodToAdd.GoodType + "' and Weight= '" + GoodToAdd.Weight + "' and Size= '" + GoodToAdd.GoodSize + "'";
                connection.Open();

                SqlCommand commandd = new SqlCommand(sql3, connection);
                var rreader = commandd.ExecuteReader();
                while (rreader.Read())
                {
                    GoodToAdd.GoodId = (int)rreader["GoodId"];

                }
                connection.Close();


                string sql2 = "SELECT * FROM Orders ";
                connection.Open();

                SqlCommand command3 = new SqlCommand(sql2, connection);
                SqlDataReader readerr = command3.ExecuteReader();
                if (readerr.HasRows) // если есть данные
                {
                    connection.Close();
                    string sqlExpression = "INSERT INTO Orders (Good,Customer,TransportType,DepartureAddress,DestinationAddress,TimeOfLoading,TimeOfUnloading,Accepted,Complited) VALUES ('" + GoodToAdd.GoodId + "', '" + CurrentUser.Login + "', '" + OrderToAdd.TransportType + "','" + OrderToAdd.DepartureAddress + "','" + OrderToAdd.DestinationAddress + "','" + OrderToAdd.TimeOfLoading + "','" + OrderToAdd.TimeOfUnloading + "','" + 0 + "','" + 0 + "') ";
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);

                    command.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Заказ успешно создан");
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)

                    connection.Close();
            }
        }


        private Command Adus;
        public Command adus
        {
            get
            {
                return Adus ?? (Adus = new Command(obj =>
                  {

                      string pattern2 = @"^[а-яА-ЯёЁa-zA-Z0-9\.\,]+$";
                      string pattern1 = @"^[0-9]+$";
                      string pattern = @"^[а-яА-ЯёЁa-zA-Z]+$";
                      string pattern3 = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
                      Regex regex = new Regex(pattern3, RegexOptions.IgnoreCase);
                      Regex regex1 = new Regex(pattern1, RegexOptions.IgnoreCase);
                      Regex regex2 = new Regex(pattern2, RegexOptions.IgnoreCase);
                      Regex regex3 = new Regex(pattern, RegexOptions.IgnoreCase);

                      if (regex2.IsMatch(RegLogin) && regex3.IsMatch(RegFirstName) && regex3.IsMatch(RegLastName) && regex3.IsMatch(RegCountry) && regex3.IsMatch(RegOrganization))
                          if (regex.IsMatch(RegEmail))
                              if (regex1.IsMatch(RegPhone))
                                  regist();
                              else MessageBox.Show("Неправильно введен номер телефона");
                          else MessageBox.Show("Некорректный Email");
                      else MessageBox.Show("Некорректно заполнены поля");
                  }));
            }
        }
        // КОМАНДЫ ПЕРЕХОДА МЕЖДУ СТРАНИЦАМИ
        #region

        private Command toOrderAdd;
        public Command ToOrderAdd
        {
            get
            {
                return toOrderAdd ??
                  (toOrderAdd = new Command(obj =>
                  {
                      selectedUserPage = "AddOrder";
                      OnPropertyChanged("CurrentUserPage");
                      OrderToAdd.TimeOfLoading = DateTime.Now;
                      OrderToAdd.TimeOfUnloading = DateTime.Now;
                  }));
            }
        }
        private Command toMain;
        public Command ToMain
        {
            get
            {
                return toMain ??
                  (toMain = new Command(obj =>
                  {
                      GetOrders();
                      selectedAdminPage = "AdminDataPage";
                      selectedUserPage = "UserDataPage";
                      OnPropertyChanged("CurrentUserPage");
                      OnPropertyChanged("CurrentAdminPage");
                  }));
            }
        }
        private Command toCustomers;
        public Command ToCstomers
        {
            get
            {
                return toCustomers ??
                    (toCustomers = new Command(obj =>
                    {
                        selectedAdminPage = "Customers";
                        OnPropertyChanged("CurrentAdminPage");
                    }));
            }
        }

        private Command toAccount;
        public Command ToAccount
        {
            get
            {
                return toAccount ??
                  (toAccount = new Command(obj =>
                  {
                      selectedUserPage = "Account";
                      selectedAdminPage = "Account";
                      OnPropertyChanged("CurrentUserPage");
                      OnPropertyChanged("CurrentAdminPage");
                  }));
            }
        }

        public Page CurrentAdminPage
        {
            get
            {
                if (selectedAdminPage == "AdminDataPage")
                    return new AdminDataPage();
                if (selectedAdminPage == "Account")
                    return new Account();
                if (selectedAdminPage == "AddOrder")
                    return new AddOrder();
                else return null;

            }
            set
            {

                OnPropertyChanged("CurrentUserPage");

            }
        }
        public Page CurrentUserPage
        {
            get
            {
                if (selectedUserPage == "UserDataPage")
                    return new UserDataPage();
                if (selectedUserPage == "Account")
                    return new Account();
                if (selectedUserPage == "AddOrder")
                    return new AddOrder();
                else return null;

            }
            set
            {

                OnPropertyChanged("CurrentUserPage");

            }
        }
        public Page CurrentPage
        {
            get
            {
                if (selectedPage == "RegPage")
                    return new RegistrationPage();
                if (selectedPage == "UserMainPage")
                    return new UserMainPage();
                if (selectedPage == "AdminMainPage")
                    return new AdminMainPage();
                else return new AuthorizationPage();

            }
            set
            {

                OnPropertyChanged("CurrentPage");

            }
        }
        private Command toAuth;
        public Command ToAuth
        {
            get
            {
                return toAuth ??
                  (toAuth = new Command(obj =>
                  {
                      selectedAdminPage = "AdminDataPage";
                      selectedUserPage = "UserDataPage";
                      selectedPage = "AuthPage";
                      OnPropertyChanged("CurrentPage");
                  }));
            }
        }

        private Command logIn;
        public Command LogIn
        {
            get
            {
                return logIn ??
                  (logIn = new Command(obj =>
                  {
                      if (CheckDB(LoginText, PasswordText))
                      {

                          GetOrders();
                          OnPropertyChanged("CurrentPage");

                      }
                      PasswordText = "";

                      OnPropertyChanged("PasswordPage");
                  }));
            }
        }
        private Command registration;
        public Command Registration
        {
            get
            {
                return registration ??
                  (registration = new Command(obj =>
                  {
                      selectedPage = "RegPage";
                      OnPropertyChanged("CurrentPage");
                  }));
            }
        }
        #endregion

        private Order selectedOrder = new Order();
        public Order SelectedOrder
        {
            get => selectedOrder;
            set
            {
                Goodss.Clear();
                selectedOrder = value;
                CurrGood();
                OnPropertyChanged("SelectedOrder");
            }
        }
        public void CurrGood()
        {
            if (selectedOrder == null) return;
            const string sql = "SELECT * FROM Goods";

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int check = (int)reader["GoodId"];
                    if (selectedOrder.Good == check)
                    {
                        Goodss.Add(new Goods(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDecimal(3), reader.GetDecimal(4)));
                    }
                }
                OnPropertyChanged("Goods");
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }
        public bool CheckDB(string login, string password)
        {
            SqlConnection connection = null;
            connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM [Users] ", connection);

            connection.Open();

            SqlDataReader rd = command.ExecuteReader();

            bool flag = false;
            while (rd.Read())
            {
                bool admin = (bool)rd["Admin"];
                string userLogin = rd["Login"].ToString();
                string passwordDB = rd["Password"].ToString(); ;
                if (login == userLogin && password == passwordDB)
                {
                    CurrentUser = new User(rd.GetString(0), rd.GetString(1), rd.GetString(2), rd.GetString(3), rd.GetString(4), rd.GetString(5), rd.GetString(6), rd.GetString(7), rd.GetBoolean(8));
                    if (admin)
                        selectedPage = "AdminMainPage";
                    if (!admin)
                        selectedPage = "UserMainPage";
                    connection.Close();
                    flag = true;

                    CheckChanges();
                    return flag;
                }

            }
            if (!flag)
                MessageBox.Show("Неправильный логин или пароль");
            return flag;
        }
        public void CheckChanges()
        {
            foreach (Order a in ChangedOrders)
            {
                if (a.Customer == CurrentUser.Login)
                    MessageBox.Show("Был изменен статус заказа № " + a.OrderId);

            }
        }

        public ViewModel()
        {

            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            GetUsers();
        }
        public static ViewModel instance;


        public static ViewModel GetInstance()
        {
            return instance ?? (instance = new ViewModel());
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));


        public DataView itemsourse
        {
            get => Itemsourse;
            set
            {
                Itemsourse = value;
                OnPropertyChanged("Itemsourse");
            }
        }


        private string regEmail;
        public string RegEmail
        {
            get => regEmail;
            set
            {
                regEmail = value;
                OnPropertyChanged("RegEmail");
            }
        }
        private string regCountry;
        public string RegCountry
        {
            get => regCountry;
            set
            {
                regCountry = value;
                OnPropertyChanged("RegCountry");
            }
        }
        private string regPhone;
        public string RegPhone
        {
            get => regPhone;
            set
            {
                regPhone = value;
                OnPropertyChanged("RegPhone");
            }
        }
        private string regOrganization;
        public string RegOrganization
        {
            get => regOrganization;
            set
            {
                regOrganization = value;
                OnPropertyChanged("RegOrganization");
            }
        }
        private string regLogin;
        public string RegLogin
        {
            get => regLogin;
            set
            {
                regLogin = value;
                OnPropertyChanged("RegLogin");
            }
        }
        private string regLastName;
        public string RegLastName
        {
            get => regLastName;
            set
            {
                regLastName = value;
                OnPropertyChanged("RegLastName");
            }
        }
        private string regFirstName;
        public string RegFirstName
        {
            get => regFirstName;
            set
            {
                regFirstName = value;
                OnPropertyChanged("RegFirstName");
            }
        }

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
            }
        }
        public Goods GoodToAdd
        {
            get => goodToAdd;
            set
            {
                goodToAdd = value;
                OnPropertyChanged("GoodToAdd");
            }
        }
        public Order OrderToAdd
        {
            get => orderToAdd;
            set
            {
                orderToAdd = value;
                OnPropertyChanged("OrderToAdd");
            }
        }
        public User CurrentUser
        {
            get => currentUser;
            set
            {
                currentUser = value;
                OnPropertyChanged("CurrentUser");
            }
        }
        public string LoginText
        {
            get => loginText;
            set
            {
                loginText = value;
                OnPropertyChanged("LoginText");
            }
        }
        public string ChkRegPasswordText
        {
            get => chkRegPasswordText;
            set
            {
                chkRegPasswordText = value;
                OnPropertyChanged("ChkRegPasswordText");
            }
        }
        public string RegPasswordText
        {
            get => regPasswordText;
            set
            {
                regPasswordText = value;
                OnPropertyChanged("RegPasswordText");
            }
        }
        public string PasswordTextt
        {
            get => passwordTextt;
            set
            {
                passwordTextt = value;
                OnPropertyChanged("PasswordTextt");
            }
        }
        public string PasswordText
        {
            get => passwordText;

            set
            {
                passwordText = value;
                OnPropertyChanged("PasswordText");
            }
        }
        public object CurrentAdminDataGridObject
        {
            get
            {
                if (CurrentAdminDataGridObjectString == "SearchOrder")
                    return SearchOrders;
                return Orders;
            }
            set => OnPropertyChanged("CurrentAdminDataGridObject");
        }
        public Goods CurrentGood
        {
            get => CurrentGood;

            set => OnPropertyChanged("CurrentGood");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Model
{
    public class User: INotifyPropertyChanged
    {

        private string firstName;
        private string lastName;
        private string email;
        private string login;
        private string country;
        private string organization;
        private string password;
        private string telephoneNumber;

        private bool admin;
        public User() { }
        public User(string firstName, string lastName,string email, string login, string country, string organization, string password, string telephonenumber, bool admin)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Login = login;
            Country = country;
            Organization = organization;
            Password = password;
            TelephoneNumber = telephonenumber;
            Admin = admin;
        }
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                OnPropertyChanged("Country");
            }
        }

        public string Organization
        {
            get { return organization; }
            set
            {
                organization = value;
                OnPropertyChanged("Organization");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        public string TelephoneNumber
        {
            get { return telephoneNumber; }
            set
            {
                telephoneNumber = value;
                OnPropertyChanged("TelephoneNumber");
            }
        }


        public bool Admin
        {
            get { return admin; }
            set
            {
                admin = value;
                OnPropertyChanged("Admin");
            }
        }

        internal View.ViewModel ViewModel
        {
            get => default;
            set
            {
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Model
{
    class Order : INotifyPropertyChanged
    {
        private int orderId;
        private int good;
        private string customer;
        private string transportType;
        private string departureAddress;
        private string destinationAddress;
        private string loadTime;
        private string unLoadTime;
        private DateTime timeOfLoading;
        private DateTime timeOfUnloading;
        private bool accepted;
        private bool complited;

        public Order()
        {

        }

        public Order(int orderId, int good, string customer, string transportType, string departureAddress, string destinationAddress, DateTime timeOfLoading, DateTime timeOfUnloading, bool accepted, bool complited)
        {
            OrderId = orderId;
            Good = good;
            Customer = customer;
            TransportType = transportType;
            DepartureAddress = departureAddress;
            DestinationAddress = destinationAddress;
            TimeOfLoading = timeOfLoading;
            TimeOfUnloading = timeOfUnloading;
            Accepted = accepted;
            Complited = complited;

        }
        public bool Complited
        {
            get { return complited; }
            set
            {
                complited = value;
                OnPropertyChanged("Complited");
            }
        }
        public bool Accepted
        {
            get { return accepted; }
            set
            {
                accepted = value;
                OnPropertyChanged("Accepted");
            }
        }
        public int Good
        {
            get { return good; }
            set
            {
                good = value;
                OnPropertyChanged("Good");
            }
        }
        public int OrderId
        {
            get { return orderId; }
            set
            {
                orderId = value;
                OnPropertyChanged("OrderId");
            }
        }
        public string Customer
        {
            get { return customer; }
            set
            {
                customer = value;
                OnPropertyChanged("Customer");
            }
        }

        public string TransportType
        {
            get { return transportType; }
            set
            {
                transportType = value;
                OnPropertyChanged("TransportType");
            }
        }

        public string DepartureAddress
        {
            get { return departureAddress; }
            set
            {
                departureAddress = value;
                OnPropertyChanged("DepartureAddress");
            }
        }
        public string DestinationAddress
        {
            get { return destinationAddress; }
            set
            {
                destinationAddress = value;
                OnPropertyChanged("DestinationAddress");
            }
        }
        public string UnLoadTime
        {
            get { return unLoadTime; }
            set
            {

                unLoadTime = value;
                //timeOfLoading = Convert.ToDateTime(unLoadTime);
                OnPropertyChanged("UnLoadTime");
            }
        }
        public string LoadTime
        {
            get { return loadTime; }
            set
            {

                loadTime = value;
                //timeOfLoading = Convert.ToDateTime(loadTime);

                OnPropertyChanged("LoadTime");
            }
        }
        public DateTime TimeOfLoading
        {
            get { return timeOfLoading; }
            set
            {

                timeOfLoading = value;
                loadTime = timeOfLoading.ToShortDateString();

                OnPropertyChanged("TimeOfLoading");
            }
        }
        public DateTime TimeOfUnloading
        {
            get { return timeOfUnloading; }
            set
            {
                timeOfUnloading = value;
                unLoadTime = timeOfUnloading.ToShortDateString();
                OnPropertyChanged("TimeOfUnloading");
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
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

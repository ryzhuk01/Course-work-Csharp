using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CourseWork.Model
{
    class Goods : INotifyPropertyChanged
    {
        private int goodId;
        private string goodName;
        private string goodType;
        private decimal weight;
        private decimal goodSize;

        public Goods()
        {

        }

        public Goods(int goodId, string goodName, string goodType, decimal weight, decimal goodSize)
        {
            GoodId = goodId;
            GoodName = goodName;
            GoodType = goodType;
            Weight = weight;
            GoodSize = goodSize;
        }

        public int GoodId
        {
            get { return goodId; }
            set
            {
                goodId = value;
                OnPropertyChanged("GoodId");
            }
        }

        public string GoodName
        {
            get { return goodName; }
            set
            {
                goodName = value;
                OnPropertyChanged("GoodName");
            }

        }

        public string GoodType
        {
            get { return goodType; }
            set
            {
                goodType = value;
                OnPropertyChanged("GoodType");
            }
        }

        public decimal Weight
        {
            get { return weight; }
            set
            {


                {
                    weight = value;
                    OnPropertyChanged("Weight");
                }
            }
        }
        public decimal GoodSize
        {
            get { return goodSize; }
            set
            {
                goodSize = value;
                OnPropertyChanged("GoodSize");
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

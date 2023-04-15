using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptek
{
    class Program
    {
        static void Main(string[] args)
        {
            Medicine med = new Medicine("adalzit",10,10);
            Medicine med2 = new Medicine("nospa", 15, 15);
            Pharmacy ph = new Pharmacy();
            ph.ListOfExistMedicines.Add(med);
            ph.ListOfExistMedicines.Add(med2);
            ph.Sell("Adalzit", 9);
            ph.Sell("nospa", 2);
        }
    }
    public class Medicine
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public static double _totalInCome { get; set; }
        public Medicine(string name, int price, int count)
        {
            Name = name;
            Price = price;
            Count = count;
        }
    }

    class CountException : Exception
    {
        private string _message;
        public CountException(string message)
        {
            _message = message;
        }
        public override string Message => _message;
    }
    class PriceException : Exception
    {
        private string _message;
        public PriceException(string message)
        {
            _message = message;
        }
        public override string Message => _message;

    }
    interface IPharmacy
    {
        List<Medicine> ListOfExistMedicines { get; set; }
        void Medicines(string medicine);
        void Sell(string medicineName, int buyProductCount);
        Medicine FindMedicineByName(string medicineName);
        void AddMedicine(Medicine medicine);

    }
    public class Pharmacy : IPharmacy
    {
        public List<Medicine> ListOfExistMedicines { get; set; }
        public Pharmacy()
        {
            this.ListOfExistMedicines = new List<Medicine>();
        }
        List<Medicine> IPharmacy.ListOfExistMedicines { get; set; }


        public void AddMedicine(Medicine medicine)
        {
            string lowercase = medicine.Name.Trim().ToLower();
            Medicine newmedicine = ListOfExistMedicines.Find(f => f.Name.Trim().ToLower().Equals(lowercase));
            if (newmedicine == null)
            {
                ListOfExistMedicines.Add(medicine);
            }
            else
            {
                Console.WriteLine($"This {medicine} is already exist!");
            }
        }

        public Medicine FindMedicineByName(string medicineName)
        {
            string med = medicineName.Trim().ToLower();
            Medicine med1 = ListOfExistMedicines.Find(m => m.Name.Trim().ToLower().Equals(med));
            if (med1 != null)
            {
                Console.WriteLine(med1.Name);
            }
            else
            {
                Console.WriteLine("false");
            }
            return med1;
        }

        public void Medicines(string medicine)
        {
            string find = medicine.Trim().ToLower();
            Medicine med = ListOfExistMedicines.Find(n => n.Name.Trim().ToLower().Equals(find));
            if (med != null)
            {
                Console.WriteLine($"name {med.Name} \n Count{med.Count} \n " + $"price {med.Price}");
            }
            else
            {
                Console.WriteLine("We do not have this medicine.");
            }
        }

        public void Sell(string medicineName, int buyProductCount)
        {
            string mediName = medicineName.Trim().ToLower();
            Medicine sell = ListOfExistMedicines.Find(s => s.Name.Trim().ToLower().Equals(mediName));
            if (sell.Count > buyProductCount)
            {
                sell.Count -= buyProductCount;
                double resultprice = buyProductCount * sell.Price;
                Medicine._totalInCome += resultprice;
                Console.WriteLine($"umumi qiymet {resultprice} manat, " + $"bazada ise umumi satisdan elde edilen gelir {Medicine._totalInCome} manatdir.");
            }
            else
            {
                throw new CountException("We do not have enough medicine.");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    [Serializable]
    public class Person : IPerson
    {
        private int? cardNumber;
        private string name = string.Empty;
        private string hashPassword = string.Empty;

        private DateTime birthday = DateTime.MinValue;
        public Person() { }
        public int? CardNumber
        {
            get { return cardNumber; }
            set
            {
                if (value.ToString().Length == 5)
                {
                    cardNumber = value;
                }
            }

        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        public string HashPassword
        {
            get {
                return hashPassword;
            }
            set
            {
                hashPassword = value;
            }
        }

        public Person(int? cardNumber, string name, DateTime birthday, string pass)
        {
            this.cardNumber = cardNumber;
            this.name = name;
            this.birthday = birthday;
            this.hashPassword = CalculateMD5Hash(pass);
        }
        public int calcAge(DateTime date)
        {
            int result = date.Year - birthday.Year - Convert.ToInt16(date.DayOfYear < birthday.DayOfYear);
            if (result >= 0) return result;
            return 0;
        }
        public bool Equals(Person p1)
        {
            return (p1.birthday == birthday) && (p1.cardNumber == cardNumber) && (this.name == p1.name);
        }
        private static string CalculateMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);


                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); //преобразует каждый байт в 16-ричную сс
                }
                return sb.ToString();
            }
        }
        
    }
}

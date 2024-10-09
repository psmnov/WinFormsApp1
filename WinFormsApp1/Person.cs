using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class Person: IPerson
    {
        private int cardNumber = 0;
        private string name  = string.Empty;

        private DateTime birthday  = DateTime.MinValue;
        public int CardNumber
        {
            get { return cardNumber; }
            set { 
                if(value.ToString().Length == 5)
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
            
           
        public Person(int cardNumber, string name, DateTime birthday ) {
            this.cardNumber = (cardNumber.ToString().Length == 5) ? cardNumber : throw new InvalidProgramException();
            this.name = name;
            this.birthday = birthday;
        
        }
        public int calcAge(DateTime date)
        {
            return date.Year - birthday.Year - Convert.ToInt16(date.DayOfYear < birthday.DayOfYear); 
        }
       /* public String toString(Person person)
        {
            return (String)(person.Name + person.calcAge + person.birthday);
        }*/
    }
}

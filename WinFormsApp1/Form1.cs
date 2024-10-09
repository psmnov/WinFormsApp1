using System;
using System.Security.Cryptography.Pkcs;
/*using System.Windows.Forms;
using System.Collections.Generic;*/

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
       

        private static List<IPerson> persons = new List<IPerson>();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddPersonDialogWindow addPersonDialogWindow = new AddPersonDialogWindow(string.Empty, null, DateTime.Now, true, true);
            if (addPersonDialogWindow.ShowDialog() == DialogResult.OK)
            {

                Person newPerson = addPersonDialogWindow.person;
                

                persons.Add(newPerson);


                refreshTheListBox(sender, e);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            checkIfTheFieldSelected(sender, e);
            Person selectedPerson = findPersonInList();


            AddPersonDialogWindow addPersonDialogWindow = new AddPersonDialogWindow(selectedPerson.Name, selectedPerson.CardNumber, selectedPerson.Birthday, false, false);

            if (addPersonDialogWindow.ShowDialog() == DialogResult.OK)
            {
                selectedPerson = addPersonDialogWindow.person;
                refreshTheListBox(sender, e);

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            checkIfTheFieldSelected(sender, e);

            DialogResult result = MessageBox.Show("Вы уверены что хотите удалить выбранного человека?", "Подтверждение", MessageBoxButtons.OKCancel);
            if(DialogResult.OK == result)
            {
                deletePerson();
                refreshTheListBox(sender, e);

            }

        }
        void checkIfTheFieldSelected(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите человека для изменения из списка");
                return;
            }
        }
        Person findPersonInList()
        {
            
            string selectedItem = listBox1.SelectedItem.ToString();
            string name = selectedItem.Substring(0, selectedItem.IndexOf("(") - 1);
            Person selectedPerson = (Person)persons.Find(p => p.Name == name);
            return selectedPerson;
        }
        void refreshTheListBox(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (Person p in persons)
            {
                listBox1.Items.Add($"{p.Name} (Возраст: {p.calcAge(DateTime.Now)})");
            }
        }
        void deletePerson()
        {
            string selectedItem = listBox1.SelectedItem.ToString();
            string name = selectedItem.Substring(0, selectedItem.IndexOf("(") - 1);
            persons.RemoveAll(person => person.Name == name);
            return;
        }
    }
}

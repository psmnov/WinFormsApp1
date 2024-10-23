using System;
using System.Security.Cryptography.Pkcs;


namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private static string fileName = "C:\\Users\\sebas\\source\\repos\\WinFormsApp1\\WinFormsApp1\\persosns.txt";
        private static List<IPerson> persons = new List<IPerson>();

        public Form1()
        {
            

            InitializeComponent();
            loadPersonsFromFile();

        }
        private void loadPersonsFromFile()
        {
            try
            {

                List<Person> persons1 = PersonFileHandler.LoadFromFile(fileName);

                persons.Clear(); 
                persons.AddRange(persons1);  

                refreshTheListBox();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Не получилось загрузить пользователей!");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Person emptyPerson = new Person(null, string.Empty, DateTime.Now, string.Empty);
            AddPersonDialogWindow addPersonDialogWindow = new AddPersonDialogWindow(emptyPerson, true);
            if (addPersonDialogWindow.ShowDialog() == DialogResult.OK)
            {

                Person newPerson = addPersonDialogWindow.person;


                persons.Add(newPerson);


                refreshTheListBox();


                PersonFileHandler.SaveToFile(persons, fileName);
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            int check = checkIfTheFieldSelected(sender, e);

            if (check == 1)
            {
                int selectedPersonIndex = findPersonInList();
                Person selectedPerson = (Person)persons[selectedPersonIndex];

                AddPersonDialogWindow addPersonDialogWindow = new AddPersonDialogWindow(selectedPerson, false);

                if (addPersonDialogWindow.ShowDialog() == DialogResult.OK)
                {
                    persons[selectedPersonIndex] = addPersonDialogWindow.person;
                    Random rand = new Random();
                    for (int i = 0; i < persons.Count(); i++)
                    {

                        IPerson tmp = persons[i];
                        int j = rand.Next(i + 1);
                        if (i != selectedPersonIndex && j != selectedPersonIndex)
                        {
                            persons[i] = persons[j];
                            persons[j] = tmp;
                        }
                    }
                    PersonFileHandler.SaveToFile(persons, fileName);
                    refreshTheListBox();
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int check = checkIfTheFieldSelected(sender, e);

            if (check == 1)
            {
                DialogResult result = MessageBox.Show("Вы уверены что хотите удалить выбранного человека?", "Подтверждение", MessageBoxButtons.OKCancel);
                if (DialogResult.OK == result)
                {
                    deletePerson();
                    refreshTheListBox();

                }
                PersonFileHandler.SaveToFile(persons, fileName);
            }

        }
        int checkIfTheFieldSelected(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите человека для изменения из списка");
                return 0;
            }
            return 1;
        }
        int findPersonInList()
        {
            int index = 0;
            string selectedItem = listBox1.SelectedItem.ToString();
            string name = selectedItem.Substring(0, selectedItem.IndexOf("(") - 1);
            for (int i = 0; i < persons.Count(); i++)
            {
                if (persons[i].Name == name) index = i;
            }
            return index;
        }
        void refreshTheListBox()
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace WinFormsApp1
{
    internal class LoginPassWindow: Form
    {
        private ComboBox cbLogin;
        private TextBox txtPassword;
        private Button btnOK;
        private Button btnCancel;
        private Person person;
        public string EnteredPassword { get; private set; }
        public LoginPassWindow(Person p)
        {

            cbLogin = new ComboBox();
            txtPassword = new TextBox();
            btnOK = new Button { Text = "OK" };
            btnCancel = new Button { Text = "Отмена" };
            person = p;

            Controls.Add(cbLogin);
            Controls.Add(txtPassword);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);


            cbLogin.Items.Add("user");
            cbLogin.Items.Add("admin");  
            cbLogin.SelectedIndexChanged += cbLogin_SelectedIndexChanged;
            cbLogin.SelectedIndex = 0;

            txtPassword.PasswordChar = '*';

            cbLogin.Location = new Point(10, 10);
            txtPassword.Location = new Point(10, 40);
            btnOK.Location = new Point(10, 70);
            btnCancel.Location = new Point(80, 70);


            cbLogin.Size = new Size(100, 20);
            txtPassword.Size = new Size(100, 20);
            btnOK.Size = new Size(70, 25);
            btnCancel.Size = new Size(70, 25);

            btnOK.Click += btnOk_Click;
            btnCancel.Click += btnCancel_Click;

        }
        private void cbLogin_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePasswordFieldAvailability();
        }
        private void UpdatePasswordFieldAvailability()
        {
            txtPassword.Enabled = (cbLogin.SelectedItem.ToString() == "user") ? false : true;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            EnteredPassword = /*cbLogin.Text + */txtPassword.Text;
            string md5Hash = CalculateMD5Hash(EnteredPassword);
            /*XDocument xmlDoc = XDocument.Load("C:\\Users\\sebas\\source\\repos\\WinFormsApp1\\WinFormsApp1\\app.config.xml");*/

            /*string storedPasswordHash = xmlDoc.Descendants("add")
                                       .Where(x => (string)x.Attribute("key") == "AdminPasswordHash")
                                       .Select(x => (string)x.Attribute("value"))
                                       .FirstOrDefault();*/
            string storedPasswordHash = person.HashPassword;

            if (storedPasswordHash == md5Hash)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Пароль неверный!");
            }

            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
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

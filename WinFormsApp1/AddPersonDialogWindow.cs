using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    internal class AddPersonDialogWindow : Form
    {
        
        public Person person { get; set; }

        private TextBox txtCardNumber;
        private TextBox txtName;
        private TextBox txtPersonalPassword;

        private DateTimePicker dtBirthday;

        private Button btnCancel;
        private Button btnOK;

        private string initialCardNumber;
        

        private bool isAdminMode = false;
        private bool avalibale = false;

        public AddPersonDialogWindow(Person p, bool enabled)
        {

            KeyPreview = true;

            Shown += (sender, e) =>
            {
                btnOK.Focus();
            };
            btnOK = new Button { Text = "Ок" };
            btnCancel = new Button { Text = "Отмена" };

            person = p;
            

            txtCardNumber = new TextBox { Text = (p.CardNumber != null) ? ValidateInput.formRightCardNumber(p.CardNumber) : p.CardNumber.ToString() };
            grayHint(txtCardNumber, "Номер карты");
            txtCardNumber.MaxLength = 5;

            txtCardNumber.KeyPress += txtCardNumber_KeyPress;
            initialCardNumber = txtCardNumber.Text;

            txtName = new TextBox { Text = p.Name };
            grayHint(txtName, "Введите имя");


            dtBirthday = new DateTimePicker();
            dtBirthday.Value = p.Birthday;


            txtPersonalPassword = new TextBox { Text = (enabled) ? string.Empty : person.HashPassword};
            txtPersonalPassword.PasswordChar = '*';
            avalibale = enabled;
            if (!enabled)
            {
                KeyDown += new KeyEventHandler(AddPersonDiologWindow_KeyDown);
                txtCardNumber.Enabled = false;
                dtBirthday.Enabled = false;
                txtPersonalPassword.Enabled = false;
            }
          

            txtCardNumber.Location = new Point(10, 10);
            txtName.Location = new Point(10, 40);
            dtBirthday.Location = new Point(10, 70);
            txtPersonalPassword.Location = new Point(10, 130);

            btnCancel.Location = new Point(80, 100);
            btnOK.Location = new Point(10, 100);

            Controls.Add(txtCardNumber);
            Controls.Add(txtName);
            Controls.Add(dtBirthday);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Controls.Add(txtPersonalPassword);

            btnOK.Click += btnOk_Click;
            btnCancel.Click += btnCancel_Click;
            MouseMove += AddPersonDialogWindow_MouseMove;

        }
        
        private void createResPerson()
        {
            if(!avalibale && !isAdminMode)
            {
                person.Name = txtName.Text;
                
            }
            else 
            {
                person = new Person(int.Parse(txtCardNumber.Text), txtName.Text, dtBirthday.Value, txtPersonalPassword.Text);
            }
          
        }
        public void grayHint(TextBox textBox, string hintText)
        {

            textBox.GotFocus += (sender, e) =>
            {
                if (textBox.Text == hintText)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };
            textBox.LostFocus += (sender, e) =>
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = hintText;
                    textBox.ForeColor = Color.Gray;

                }
            };
        }
        private void txtCardNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void AddPersonDiologWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.L)
            {
                LoginPassWindow loginPassWindow = new LoginPassWindow(person);
                if (loginPassWindow.ShowDialog() == DialogResult.OK)
                {
                    BackColor = Color.LightBlue;
                    txtCardNumber.ForeColor = Color.Red;
                    txtName.ForeColor = Color.Red;
                    dtBirthday.ForeColor = Color.Red;
                    btnOK.ForeColor = Color.Green;
                    btnCancel.ForeColor = Color.Green;
                    btnOK.BackColor = Color.FloralWhite;
                    btnCancel.BackColor = Color.FloralWhite;
                    txtCardNumber.Enabled = true;
                    dtBirthday.Enabled = true;
                    txtPersonalPassword.Enabled = true;
                    isAdminMode = true;
                    txtPersonalPassword.Text = loginPassWindow.EnteredPassword;
                }
            }
        }
        private void AddPersonDialogWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (isAdminMode && (txtCardNumber.Text.ToString() != initialCardNumber))
            {
                HandleAdminChanges.dontPressTheOk(sender, e, btnOK, ClientSize);
            }
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (isAdminMode && (txtCardNumber.Text.ToString() != initialCardNumber))
            {
                MessageBox.Show("Нельзя менять номер карты существующего пользователя!");
            }
            else if (!ValidateInput.checkTheName(txtName))
            {
                MessageBox.Show("Имя минимум из 2 слов и не содержит цифр!!");

            }
            else if ((ValidateInput.onlyNumbersInCard(txtCardNumber)) && !(isAdminMode && (txtCardNumber.Text.ToString() != initialCardNumber)))
            {
                createResPerson();
                DialogResult = DialogResult.OK;
                Close();
            }
            else MessageBox.Show("Номер карты должен состоять ровно из пяти цифр!");
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

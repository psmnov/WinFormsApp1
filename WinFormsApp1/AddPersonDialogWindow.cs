using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    internal class AddPersonDialogWindow : Form
    {
        public int CardNumber { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }

        private TextBox txtCardNumber;
        private TextBox txtName;
        private DateTimePicker dtBirthday;

        private Button btnCancel;
        private Button btnOK;
        private string initialCardNumber;
        private bool isEditingCardNumber = false;
        private bool isAdminMode = false;
        public bool EnableCardNumberEdit { get; set; } = true;
        public bool EnableBirthdayEdit { get; set; } = true;




        public AddPersonDialogWindow(string name, int? cardNumber, DateTime birthday, bool EnableCardNumberEdit, bool EnableBirthdayEdit)
        {

            KeyPreview = true;

            Shown += (sender, e) =>
            {
                btnOK.Focus();
            };
            btnOK = new Button { Text = "Ок" };
            btnCancel = new Button { Text = "Отмена" };



            txtCardNumber = new TextBox { Text = cardNumber.ToString() };
            grayHint(txtCardNumber, "Номер карты");
            txtCardNumber.MaxLength = 5;

            txtCardNumber.KeyPress += txtCardNumber_KeyPress;
            initialCardNumber = txtCardNumber.Text;

            txtName = new TextBox { Text = name };
            grayHint(txtName, "Введите имя");


            dtBirthday = new DateTimePicker();
            dtBirthday.Value = birthday;

            if (!EnableCardNumberEdit)
            {
                KeyDown += new KeyEventHandler(AddPersonDiologWindow_KeyDown);
                txtCardNumber.Enabled = false;
            }
            if (!EnableBirthdayEdit) dtBirthday.Enabled = false;

            txtCardNumber.Location = new Point(10, 10);
            txtName.Location = new Point(10, 40);
            dtBirthday.Location = new Point(10, 70);

            btnCancel.Location = new Point(80, 100);
            btnOK.Location = new Point(10, 100);

            Controls.Add(txtCardNumber);
            Controls.Add(txtName);
            Controls.Add(dtBirthday);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);


            btnOK.Click += btnOk_Click;
            btnCancel.Click += btnCancel_Click;
            MouseMove += AddPersonDialogWindow_MouseMove;
            /*btnOK.MouseMove += ButtonOk_MouseMove;*/

        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (isAdminMode && (txtCardNumber.Text.ToString() != initialCardNumber))
            {
                MessageBox.Show("Нельзя менять номер карты существующего пользователя!");
            }
            else if ((txtCardNumber.Text.Length == 5) && !(isAdminMode && (txtCardNumber.Text.ToString() != initialCardNumber)))
            {
                CardNumber = int.Parse(txtCardNumber.Text);
                Name = txtName.Text;
                Birthday = dtBirthday.Value;

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
        public void grayHint(TextBox textBox, string hintText)
        {

            /*textBox.ForeColor = Color.Gray;*/
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
                LoginPassWindow loginPassWindow = new LoginPassWindow();
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
                    isAdminMode = true;
                    /*MessageBox.Show((int.Parse(txtCardNumber.Text) == int.Parse(initialCardNumber)).ToString());*/
                }
            }
        }
        private void AddPersonDialogWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (isAdminMode && (txtCardNumber.Text.ToString() != initialCardNumber))
            {
                dontPressTheOk(sender, e);
            }
        }
        private void dontPressTheOk(object sender, MouseEventArgs e)
        {

            int newX = btnOK.Location.X;
            int newY = btnOK.Location.Y;

            // Движение по оси X
            if (e.X < btnOK.Location.X && btnOK.Location.X > 1)
                newX += 1;
            else if (e.X > btnOK.Location.X + btnOK.Width && btnOK.Location.X < ClientSize.Width - btnOK.Width)
                newX -= 1;

            // Движение по оси Y
            if (e.Y < btnOK.Location.Y && btnOK.Location.Y > 1)
                newY += 1;
            else if (e.Y > btnOK.Location.Y + btnOK.Height && btnOK.Location.Y < ClientSize.Height - btnOK.Height)
                newY -= 1;

            // Проверка границ
            if (newX < 0) newX = 0;
            if (newX > ClientSize.Width - btnOK.Width) newX = ClientSize.Width - btnOK.Width;
            if (newY < 0) newY = 0;
            if (newY > ClientSize.Height - btnOK.Height) newY = ClientSize.Height - btnOK.Height;
            
            if (IsButtonInCorner(btnOK))
            {
                Random r = new Random();
                newY = r.Next(1, btnOK.Height);
                newX = r.Next(1, btnOK.Width);
            }
            btnOK.Location = new Point(newX, newY);
            btnOK.BringToFront();
        }
        private bool IsButtonInCorner(Button button)
        {
            if ((button.Location.X == 0 && button.Location.Y == 0) ||
                (button.Location.X + button.Width == ClientSize.Width && button.Location.Y == 0) ||
                (button.Location.X == 0 && button.Location.Y + button.Height == ClientSize.Height) ||
                (button.Location.X + button.Width == ClientSize.Width && button.Location.Y + button.Height == ClientSize.Height))
                return true;

            return false;
        }

    }
}

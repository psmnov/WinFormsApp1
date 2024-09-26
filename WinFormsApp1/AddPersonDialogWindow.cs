using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Button btnOK;
        private Button btnCancel;
        private string initialCardNumber;
        private Point lastMousePosition;
        private bool isEditingCardNumber = false;

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
            initialCardNumber = txtCardNumber.ToString();

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
            btnOK.Location = new Point(10, 100);
            btnCancel.Location = new Point(80, 100);

            Controls.Add(txtCardNumber);
            Controls.Add(txtName);
            Controls.Add(dtBirthday);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);

            btnOK.Click += btnOk_Click;
            btnCancel.Click += btnCancel_Click;
            
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            CardNumber = int.Parse(txtCardNumber.Text);
            Name = txtName.Text;
            Birthday = dtBirthday.Value;

            DialogResult = DialogResult.OK;
            Close();
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
                    /*MouseEventArgs mouseEventArgs = new MouseEventArgs(MouseButtons.Left, 1, lastMousePosition.X, lastMousePosition.Y, 0);*/
                    if (isEditingCardNumber)
                    {
                        btnOK.MouseMove += dontPressTheOk;
                    }
                }
            }
        }
        private void dontPressTheOk(object sender, MouseEventArgs e)
        {
            // Получаем текущие координаты кнопки
            int newX = btnOK.Location.X;
            int newY = btnOK.Location.Y;

            // Определяем новое положение кнопки, чтобы она укрылась от курсора
            if (e.X < btnOK.Location.X)
                newX -= 10; // Если курсор слева, уводим кнопку влево
            else
                newX += 10; // Если курсор справа, уводим кнопку вправо

            if (newX < 0) newX = 0; // Проверка на выход за границы
            if (newX > this.ClientSize.Width - btnOK.Width) newX = this.ClientSize.Width - btnOK.Width;

            btnOK.Location = new Point(newX, newY); // Устанавливаем новое положение кнопки
        }
        private void cardNumberTextBox_Enter(object sender, EventArgs e)
        {
            isEditingCardNumber = true; // Вход в режим редактирования
        }

        private void cardNumberTextBox_Leave(object sender, EventArgs e)
        {
            isEditingCardNumber = false; // Выход из режима редактирования
        }
    }
}

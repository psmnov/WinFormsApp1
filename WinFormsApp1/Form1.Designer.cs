namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listBox1 = new ListBox();
            createNewNote = new Button();
            changeExisting = new Button();
            delExisting = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(93, 36);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(619, 199);
            listBox1.TabIndex = 0;
            // 
            // createNewNote
            // 
            createNewNote.Location = new Point(93, 248);
            createNewNote.Name = "createNewNote";
            createNewNote.Size = new Size(159, 38);
            createNewNote.TabIndex = 1;
            createNewNote.Text = "Создать новую запись";
            createNewNote.UseVisualStyleBackColor = true;
            createNewNote.Click += button1_Click;
            // 
            // changeExisting
            // 
            changeExisting.Location = new Point(93, 311);
            changeExisting.Name = "changeExisting";
            changeExisting.Size = new Size(159, 38);
            changeExisting.TabIndex = 2;
            changeExisting.Text = "Изменить выбранную";
            changeExisting.UseVisualStyleBackColor = true;
            changeExisting.Click += button2_Click;
            // 
            // delExisting
            // 
            delExisting.Location = new Point(93, 373);
            delExisting.Name = "delExisting";
            delExisting.Size = new Size(159, 38);
            delExisting.TabIndex = 3;
            delExisting.Text = "Удалить выбранную";
            delExisting.UseVisualStyleBackColor = true;
            delExisting.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(843, 500);
            Controls.Add(delExisting);
            Controls.Add(changeExisting);
            Controls.Add(createNewNote);
            Controls.Add(listBox1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox1;
        private Button createNewNote;
        private Button changeExisting;
        private Button delExisting;
    }
}

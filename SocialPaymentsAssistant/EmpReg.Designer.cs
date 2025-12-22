namespace SocialPaymentsAssistant
{
    partial class EmpReg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            empRegOK = new Button();
            panel1 = new Panel();
            groupBox1 = new GroupBox();
            password2 = new TextBox();
            password = new TextBox();
            eMail = new TextBox();
            login = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            groupBox2 = new GroupBox();
            label14 = new Label();
            inn = new TextBox();
            groupBox5 = new GroupBox();
            workBookSeries = new TextBox();
            workBookNum = new TextBox();
            label12 = new Label();
            label13 = new Label();
            groupBox4 = new GroupBox();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            passportIssuingDepartment = new ComboBox();
            passportSeriesEdit = new TextBox();
            passportNumberEdit = new TextBox();
            passportDivisionCode = new TextBox();
            birthDate = new DateTimePicker();
            passportDateOfIssue = new DateTimePicker();
            groupBox3 = new GroupBox();
            label17 = new Label();
            label16 = new Label();
            label15 = new Label();
            comboBox3 = new ComboBox();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            button1 = new Button();
            groupBox6 = new GroupBox();
            label18 = new Label();
            label19 = new Label();
            label20 = new Label();
            label21 = new Label();
            surname = new TextBox();
            name = new TextBox();
            patronymic = new TextBox();
            phone = new TextBox();
            noPatronymicCheck = new CheckBox();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox6.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(424, 46);
            label1.TabIndex = 0;
            label1.Text = "Регистрация сотрудника";
            // 
            // empRegOK
            // 
            empRegOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            empRegOK.Location = new Point(694, 543);
            empRegOK.Name = "empRegOK";
            empRegOK.Size = new Size(94, 29);
            empRegOK.TabIndex = 1;
            empRegOK.Text = "OK";
            empRegOK.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoScroll = true;
            panel1.CausesValidation = false;
            panel1.Controls.Add(groupBox6);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox3);
            panel1.Location = new Point(12, 58);
            panel1.Name = "panel1";
            panel1.Size = new Size(776, 479);
            panel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(password2);
            groupBox1.Controls.Add(password);
            groupBox1.Controls.Add(eMail);
            groupBox1.Controls.Add(login);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(723, 160);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Данные для входа";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // password2
            // 
            password2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            password2.Location = new Point(151, 119);
            password2.MaxLength = 20;
            password2.Name = "password2";
            password2.PasswordChar = '*';
            password2.Size = new Size(566, 27);
            password2.TabIndex = 4;
            // 
            // password
            // 
            password.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            password.Location = new Point(151, 86);
            password.MaxLength = 20;
            password.Name = "password";
            password.PasswordChar = '*';
            password.Size = new Size(566, 27);
            password.TabIndex = 4;
            // 
            // eMail
            // 
            eMail.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            eMail.Location = new Point(151, 53);
            eMail.Name = "eMail";
            eMail.Size = new Size(566, 27);
            eMail.TabIndex = 4;
            // 
            // login
            // 
            login.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            login.Location = new Point(151, 20);
            login.Name = "login";
            login.Size = new Size(566, 27);
            login.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 122);
            label5.Name = "label5";
            label5.Size = new Size(139, 20);
            label5.TabIndex = 3;
            label5.Text = "Повторите пароль";
            label5.Click += label5_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 89);
            label4.Name = "label4";
            label4.Size = new Size(62, 20);
            label4.TabIndex = 2;
            label4.Text = "Пароль";
            label4.Click += label4_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 56);
            label3.Name = "label3";
            label3.Size = new Size(52, 20);
            label3.TabIndex = 1;
            label3.Text = "E-Mail";
            label3.Click += label3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 23);
            label2.Name = "label2";
            label2.Size = new Size(52, 20);
            label2.TabIndex = 0;
            label2.Text = "Логин";
            // 
            // groupBox2
            // 
            groupBox2.AutoSize = true;
            groupBox2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox2.Controls.Add(label14);
            groupBox2.Controls.Add(inn);
            groupBox2.Controls.Add(groupBox5);
            groupBox2.Controls.Add(groupBox4);
            groupBox2.Location = new Point(3, 346);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(717, 445);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Документы";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(6, 395);
            label14.Name = "label14";
            label14.Size = new Size(42, 20);
            label14.TabIndex = 3;
            label14.Text = "ИНН";
            // 
            // inn
            // 
            inn.Location = new Point(54, 392);
            inn.Name = "inn";
            inn.Size = new Size(612, 27);
            inn.TabIndex = 2;
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            groupBox5.AutoSize = true;
            groupBox5.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox5.Controls.Add(workBookSeries);
            groupBox5.Controls.Add(workBookNum);
            groupBox5.Controls.Add(label12);
            groupBox5.Controls.Add(label13);
            groupBox5.Location = new Point(6, 277);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(660, 109);
            groupBox5.TabIndex = 1;
            groupBox5.TabStop = false;
            groupBox5.Text = "Данные трудовой книжки";
            // 
            // workBookSeries
            // 
            workBookSeries.Anchor = AnchorStyles.Right;
            workBookSeries.Location = new Point(74, 23);
            workBookSeries.Name = "workBookSeries";
            workBookSeries.Size = new Size(580, 27);
            workBookSeries.TabIndex = 1;
            // 
            // workBookNum
            // 
            workBookNum.Anchor = AnchorStyles.Right;
            workBookNum.Location = new Point(74, 56);
            workBookNum.Name = "workBookNum";
            workBookNum.Size = new Size(580, 27);
            workBookNum.TabIndex = 1;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Left;
            label12.AutoSize = true;
            label12.Location = new Point(6, 26);
            label12.Name = "label12";
            label12.Size = new Size(52, 20);
            label12.TabIndex = 3;
            label12.Text = "Серия";
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.Left;
            label13.AutoSize = true;
            label13.Location = new Point(6, 59);
            label13.Name = "label13";
            label13.Size = new Size(57, 20);
            label13.TabIndex = 3;
            label13.Text = "Номер";
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox4.AutoSize = true;
            groupBox4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox4.Controls.Add(label11);
            groupBox4.Controls.Add(label10);
            groupBox4.Controls.Add(label9);
            groupBox4.Controls.Add(label8);
            groupBox4.Controls.Add(label7);
            groupBox4.Controls.Add(label6);
            groupBox4.Controls.Add(passportIssuingDepartment);
            groupBox4.Controls.Add(passportSeriesEdit);
            groupBox4.Controls.Add(passportNumberEdit);
            groupBox4.Controls.Add(passportDivisionCode);
            groupBox4.Controls.Add(birthDate);
            groupBox4.Controls.Add(passportDateOfIssue);
            groupBox4.FlatStyle = FlatStyle.Flat;
            groupBox4.Location = new Point(6, 26);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(655, 245);
            groupBox4.TabIndex = 0;
            groupBox4.TabStop = false;
            groupBox4.Text = "Паспортные данные";
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Left;
            label11.AutoSize = true;
            label11.Location = new Point(6, 197);
            label11.Name = "label11";
            label11.Size = new Size(116, 20);
            label11.TabIndex = 3;
            label11.Text = "Дата рождения";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Left;
            label10.AutoSize = true;
            label10.Location = new Point(6, 164);
            label10.Name = "label10";
            label10.Size = new Size(97, 20);
            label10.TabIndex = 3;
            label10.Text = "Когда выдан";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Left;
            label9.AutoSize = true;
            label9.Location = new Point(6, 129);
            label9.Name = "label9";
            label9.Size = new Size(147, 20);
            label9.TabIndex = 3;
            label9.Text = "Код подразделения";
            label9.Click += label9_Click;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Left;
            label8.AutoSize = true;
            label8.Location = new Point(6, 95);
            label8.Name = "label8";
            label8.Size = new Size(85, 20);
            label8.TabIndex = 3;
            label8.Text = "Кем выдан";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Location = new Point(6, 62);
            label7.Name = "label7";
            label7.Size = new Size(57, 20);
            label7.TabIndex = 3;
            label7.Text = "Номер";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Location = new Point(6, 29);
            label6.Name = "label6";
            label6.Size = new Size(52, 20);
            label6.TabIndex = 3;
            label6.Text = "Серия";
            // 
            // passportIssuingDepartment
            // 
            passportIssuingDepartment.Anchor = AnchorStyles.Right;
            passportIssuingDepartment.FormattingEnabled = true;
            passportIssuingDepartment.Location = new Point(159, 92);
            passportIssuingDepartment.Name = "passportIssuingDepartment";
            passportIssuingDepartment.Size = new Size(490, 28);
            passportIssuingDepartment.Sorted = true;
            passportIssuingDepartment.TabIndex = 2;
            // 
            // passportSeriesEdit
            // 
            passportSeriesEdit.Anchor = AnchorStyles.Right;
            passportSeriesEdit.Location = new Point(159, 26);
            passportSeriesEdit.Name = "passportSeriesEdit";
            passportSeriesEdit.Size = new Size(490, 27);
            passportSeriesEdit.TabIndex = 1;
            // 
            // passportNumberEdit
            // 
            passportNumberEdit.Anchor = AnchorStyles.Right;
            passportNumberEdit.Location = new Point(159, 59);
            passportNumberEdit.Name = "passportNumberEdit";
            passportNumberEdit.Size = new Size(490, 27);
            passportNumberEdit.TabIndex = 1;
            // 
            // passportDivisionCode
            // 
            passportDivisionCode.Anchor = AnchorStyles.Right;
            passportDivisionCode.Location = new Point(159, 126);
            passportDivisionCode.Name = "passportDivisionCode";
            passportDivisionCode.Size = new Size(490, 27);
            passportDivisionCode.TabIndex = 1;
            // 
            // birthDate
            // 
            birthDate.Anchor = AnchorStyles.Right;
            birthDate.Location = new Point(159, 192);
            birthDate.Name = "birthDate";
            birthDate.Size = new Size(490, 27);
            birthDate.TabIndex = 0;
            birthDate.Value = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            birthDate.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // passportDateOfIssue
            // 
            passportDateOfIssue.Anchor = AnchorStyles.Right;
            passportDateOfIssue.Location = new Point(159, 159);
            passportDateOfIssue.Name = "passportDateOfIssue";
            passportDateOfIssue.Size = new Size(490, 27);
            passportDateOfIssue.TabIndex = 0;
            passportDateOfIssue.Value = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            passportDateOfIssue.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // groupBox3
            // 
            groupBox3.AutoSize = true;
            groupBox3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox3.Controls.Add(label17);
            groupBox3.Controls.Add(label16);
            groupBox3.Controls.Add(label15);
            groupBox3.Controls.Add(comboBox3);
            groupBox3.Controls.Add(comboBox2);
            groupBox3.Controls.Add(comboBox1);
            groupBox3.Controls.Add(button1);
            groupBox3.Location = new Point(3, 797);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(729, 149);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Данные о работе";
            groupBox3.Enter += groupBox3_Enter;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(6, 98);
            label17.Name = "label17";
            label17.Size = new Size(86, 20);
            label17.TabIndex = 2;
            label17.Text = "Должность";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(5, 64);
            label16.Name = "label16";
            label16.Size = new Size(50, 20);
            label16.TabIndex = 2;
            label16.Text = "Отдел";
            label16.Click += label16_Click;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(6, 30);
            label15.Name = "label15";
            label15.Size = new Size(62, 20);
            label15.TabIndex = 2;
            label15.Text = "Филиал";
            label15.Click += label15_Click;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(98, 95);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(625, 28);
            comboBox3.TabIndex = 1;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(98, 61);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(625, 28);
            comboBox2.TabIndex = 1;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(98, 27);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(506, 28);
            comboBox1.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(610, 26);
            button1.Name = "button1";
            button1.Size = new Size(113, 29);
            button1.TabIndex = 0;
            button1.Text = "Фильтровать";
            button1.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(noPatronymicCheck);
            groupBox6.Controls.Add(phone);
            groupBox6.Controls.Add(patronymic);
            groupBox6.Controls.Add(name);
            groupBox6.Controls.Add(surname);
            groupBox6.Controls.Add(label21);
            groupBox6.Controls.Add(label20);
            groupBox6.Controls.Add(label19);
            groupBox6.Controls.Add(label18);
            groupBox6.Location = new Point(3, 169);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(723, 167);
            groupBox6.TabIndex = 4;
            groupBox6.TabStop = false;
            groupBox6.Text = "Основные данные";
            groupBox6.Enter += groupBox6_Enter;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(6, 33);
            label18.Name = "label18";
            label18.Size = new Size(73, 20);
            label18.TabIndex = 0;
            label18.Text = "Фамилия";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(6, 66);
            label19.Name = "label19";
            label19.Size = new Size(39, 20);
            label19.TabIndex = 0;
            label19.Text = "Имя";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(6, 99);
            label20.Name = "label20";
            label20.Size = new Size(72, 20);
            label20.TabIndex = 0;
            label20.Text = "Отчество";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(6, 132);
            label21.Name = "label21";
            label21.Size = new Size(127, 20);
            label21.TabIndex = 0;
            label21.Text = "Номер телефона";
            label21.Click += label21_Click;
            // 
            // surname
            // 
            surname.Location = new Point(133, 30);
            surname.Name = "surname";
            surname.Size = new Size(584, 27);
            surname.TabIndex = 1;
            // 
            // name
            // 
            name.Location = new Point(133, 63);
            name.Name = "name";
            name.Size = new Size(584, 27);
            name.TabIndex = 1;
            // 
            // patronymic
            // 
            patronymic.Location = new Point(133, 96);
            patronymic.Name = "patronymic";
            patronymic.Size = new Size(458, 27);
            patronymic.TabIndex = 1;
            // 
            // phone
            // 
            phone.Location = new Point(133, 129);
            phone.Name = "phone";
            phone.Size = new Size(584, 27);
            phone.TabIndex = 1;
            // 
            // noPatronymicCheck
            // 
            noPatronymicCheck.AutoSize = true;
            noPatronymicCheck.Location = new Point(597, 99);
            noPatronymicCheck.Name = "noPatronymicCheck";
            noPatronymicCheck.Size = new Size(120, 24);
            noPatronymicCheck.TabIndex = 2;
            noPatronymicCheck.Text = "Нет отчества";
            noPatronymicCheck.UseVisualStyleBackColor = true;
            // 
            // EmpReg
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 584);
            Controls.Add(panel1);
            Controls.Add(empRegOK);
            Controls.Add(label1);
            Name = "EmpReg";
            Text = "Регистрация сотрудника";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

            this.comboBox1.Name = "branchOffice";
            this.comboBox2.Name = "department";
            this.comboBox3.Name = "position";
            this.button1.Name = "branchOfficeFilterBtn";

        }

        #endregion

        private Label label1;
        private Button empRegOK;
        private GroupBox groupBox1;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox password2;
        private TextBox password;
        private TextBox eMail;
        private TextBox login;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private GroupBox groupBox5;
        private GroupBox groupBox4;
        private ComboBox passportIssuingDepartment;
        private TextBox passportSeriesEdit;
        private TextBox passportNumberEdit;
        private TextBox passportDivisionCode;
        private DateTimePicker birthDate;
        private DateTimePicker passportDateOfIssue;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private TextBox workBookSeries;
        private TextBox workBookNum;
        private Label label12;
        private Label label13;
        private TextBox inn;
        private Label label14;
        private Label label17;
        private Label label16;
        private Label label15;
        private ComboBox comboBox3;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Button button1;
        private GroupBox groupBox6;
        protected Panel panel1;
        private Label label21;
        private Label label20;
        private Label label19;
        private Label label18;
        private TextBox phone;
        private TextBox patronymic;
        private TextBox name;
        private TextBox surname;
        private CheckBox noPatronymicCheck;
    }
}
namespace SocialPaymentsAssistant
{
    partial class RegistrationWidget
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
            label2 = new Label();
            empRadioButton = new RadioButton();
            applicantRadioButton = new RadioButton();
            loginButton = new Button();
            nextButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(225, 46);
            label1.TabIndex = 0;
            label1.Text = "Регистрация";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 152);
            label2.Name = "label2";
            label2.Size = new Size(173, 20);
            label2.TabIndex = 1;
            label2.Text = "Выберите тип аккаунта:";
            // 
            // empRadioButton
            // 
            empRadioButton.AutoSize = true;
            empRadioButton.Location = new Point(12, 175);
            empRadioButton.Name = "empRadioButton";
            empRadioButton.Size = new Size(103, 24);
            empRadioButton.TabIndex = 2;
            empRadioButton.TabStop = true;
            empRadioButton.Text = "Сотрудник";
            empRadioButton.UseVisualStyleBackColor = true;
            empRadioButton.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // applicantRadioButton
            // 
            applicantRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            applicantRadioButton.AutoSize = true;
            applicantRadioButton.Location = new Point(213, 175);
            applicantRadioButton.Name = "applicantRadioButton";
            applicantRadioButton.Size = new Size(101, 24);
            applicantRadioButton.TabIndex = 3;
            applicantRadioButton.TabStop = true;
            applicantRadioButton.Text = "Заявитель";
            applicantRadioButton.UseVisualStyleBackColor = true;
            // 
            // loginButton
            // 
            loginButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            loginButton.Location = new Point(12, 409);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(61, 29);
            loginButton.TabIndex = 4;
            loginButton.Text = "Войти";
            loginButton.UseVisualStyleBackColor = true;
            // 
            // nextButton
            // 
            nextButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            nextButton.Location = new Point(129, 409);
            nextButton.Name = "nextButton";
            nextButton.Size = new Size(201, 29);
            nextButton.TabIndex = 5;
            nextButton.Text = "Продолжить регистрацию";
            nextButton.UseVisualStyleBackColor = true;
            // 
            // RegistrationWidget
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(342, 450);
            Controls.Add(nextButton);
            Controls.Add(loginButton);
            Controls.Add(applicantRadioButton);
            Controls.Add(empRadioButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "RegistrationWidget";
            Text = "Регистрация";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private RadioButton empRadioButton;
        private RadioButton applicantRadioButton;
        private Button loginButton;
        private Button nextButton;
    }
}
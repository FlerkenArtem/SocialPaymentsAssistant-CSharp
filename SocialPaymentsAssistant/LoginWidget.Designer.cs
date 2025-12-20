namespace SocialPaymentsAssistant
{
    partial class LoginWidget
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
            label1 = new Label();
            startRegistrationButton = new Button();
            loginEdit = new TextBox();
            passwordEdit = new TextBox();
            endLoginButton = new Button();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(103, 46);
            label1.TabIndex = 0;
            label1.Text = "Вход";
            label1.Click += label1_Click_1;
            // 
            // startRegistrationButton
            // 
            startRegistrationButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            startRegistrationButton.AutoSize = true;
            startRegistrationButton.Location = new Point(12, 408);
            startRegistrationButton.Name = "startRegistrationButton";
            startRegistrationButton.Size = new Size(106, 30);
            startRegistrationButton.TabIndex = 1;
            startRegistrationButton.Text = "Регистрация";
            startRegistrationButton.UseVisualStyleBackColor = true;
            startRegistrationButton.Click += button1_Click;
            // 
            // loginEdit
            // 
            loginEdit.Anchor = AnchorStyles.Right;
            loginEdit.Location = new Point(116, 158);
            loginEdit.Name = "loginEdit";
            loginEdit.Size = new Size(281, 27);
            loginEdit.TabIndex = 2;
            // 
            // passwordEdit
            // 
            passwordEdit.Anchor = AnchorStyles.Right;
            passwordEdit.Location = new Point(116, 214);
            passwordEdit.Name = "passwordEdit";
            passwordEdit.PasswordChar = '*';
            passwordEdit.Size = new Size(281, 27);
            passwordEdit.TabIndex = 3;
            passwordEdit.TextChanged += passwordEdit_TextChanged;
            // 
            // endLoginButton
            // 
            endLoginButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            endLoginButton.AutoSize = true;
            endLoginButton.Location = new Point(336, 408);
            endLoginButton.Name = "endLoginButton";
            endLoginButton.Size = new Size(61, 30);
            endLoginButton.TabIndex = 4;
            endLoginButton.Text = "Войти";
            endLoginButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(12, 161);
            label2.Name = "label2";
            label2.Size = new Size(55, 20);
            label2.TabIndex = 5;
            label2.Text = "Логин:";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(12, 221);
            label3.Name = "label3";
            label3.Size = new Size(65, 20);
            label3.TabIndex = 6;
            label3.Text = "Пароль:";
            label3.Click += label3_Click;
            // 
            // LoginWidget
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(409, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(endLoginButton);
            Controls.Add(passwordEdit);
            Controls.Add(loginEdit);
            Controls.Add(startRegistrationButton);
            Controls.Add(label1);
            Name = "LoginWidget";
            Text = "Вход";
            Load += LoginWidget_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button startRegistrationButton;
        private TextBox loginEdit;
        private TextBox passwordEdit;
        private Button endLoginButton;
        private Label label2;
        private Label label3;
    }
}

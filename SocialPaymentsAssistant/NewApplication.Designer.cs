namespace SocialPaymentsAssistant
{
    partial class NewApplication
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
            label3 = new Label();
            amountEdit = new NumericUpDown();
            typeComboBox = new ComboBox();
            newApplicationOK = new Button();
            newApplicationCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)amountEdit).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(243, 46);
            label1.TabIndex = 0;
            label1.Text = "Новая заявка";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(12, 174);
            label2.Name = "label2";
            label2.Size = new Size(86, 20);
            label2.TabIndex = 1;
            label2.Text = "Тип заявки";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(12, 217);
            label3.Name = "label3";
            label3.Size = new Size(55, 20);
            label3.TabIndex = 1;
            label3.Text = "Сумма";
            label3.Click += label2_Click;
            // 
            // amountEdit
            // 
            amountEdit.Anchor = AnchorStyles.Right;
            amountEdit.DecimalPlaces = 2;
            amountEdit.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            amountEdit.Location = new Point(105, 215);
            amountEdit.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            amountEdit.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            amountEdit.Name = "amountEdit";
            amountEdit.Size = new Size(269, 27);
            amountEdit.TabIndex = 2;
            amountEdit.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // typeComboBox
            // 
            typeComboBox.Anchor = AnchorStyles.Right;
            typeComboBox.FormattingEnabled = true;
            typeComboBox.Location = new Point(105, 171);
            typeComboBox.Name = "typeComboBox";
            typeComboBox.Size = new Size(269, 28);
            typeComboBox.TabIndex = 3;
            // 
            // newApplicationOK
            // 
            newApplicationOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            newApplicationOK.Location = new Point(280, 409);
            newApplicationOK.Name = "newApplicationOK";
            newApplicationOK.Size = new Size(94, 29);
            newApplicationOK.TabIndex = 4;
            newApplicationOK.Text = "OK";
            newApplicationOK.UseVisualStyleBackColor = true;
            // 
            // newApplicationCancel
            // 
            newApplicationCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            newApplicationCancel.Location = new Point(180, 409);
            newApplicationCancel.Name = "newApplicationCancel";
            newApplicationCancel.Size = new Size(94, 29);
            newApplicationCancel.TabIndex = 5;
            newApplicationCancel.Text = "Отмена";
            newApplicationCancel.UseVisualStyleBackColor = true;
            // 
            // NewApplication
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(386, 450);
            Controls.Add(newApplicationCancel);
            Controls.Add(newApplicationOK);
            Controls.Add(typeComboBox);
            Controls.Add(amountEdit);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "NewApplication";
            Text = "Новая заявка";
            ((System.ComponentModel.ISupportInitialize)amountEdit).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private NumericUpDown amountEdit;
        private ComboBox typeComboBox;
        private Button newApplicationOK;
        private Button newApplicationCancel;
    }
}
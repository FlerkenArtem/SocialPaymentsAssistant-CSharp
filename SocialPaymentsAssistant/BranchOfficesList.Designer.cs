namespace SocialPaymentsAssistant
{
    partial class BranchOfficesList
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
            tableLayoutPanel1 = new TableLayoutPanel();
            acceptBranchOfficeFilterButton = new Button();
            cityComboBox = new ComboBox();
            branchOfiiceName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            branchOfficesTableView = new DataGridView();
            branchOfficeClose = new Button();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)branchOfficesTableView).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.Controls.Add(acceptBranchOfficeFilterButton, 2, 1);
            tableLayoutPanel1.Controls.Add(cityComboBox, 0, 1);
            tableLayoutPanel1.Controls.Add(branchOfiiceName, 1, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 1, 0);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(776, 57);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // acceptBranchOfficeFilterButton
            // 
            acceptBranchOfficeFilterButton.Location = new Point(519, 23);
            acceptBranchOfficeFilterButton.Name = "acceptBranchOfficeFilterButton";
            acceptBranchOfficeFilterButton.Size = new Size(254, 29);
            acceptBranchOfficeFilterButton.TabIndex = 0;
            acceptBranchOfficeFilterButton.Text = "Применить";
            acceptBranchOfficeFilterButton.UseVisualStyleBackColor = true;
            // 
            // cityComboBox
            // 
            cityComboBox.FormattingEnabled = true;
            cityComboBox.Location = new Point(3, 23);
            cityComboBox.Name = "cityComboBox";
            cityComboBox.Size = new Size(252, 28);
            cityComboBox.TabIndex = 1;
            // 
            // branchOfiiceName
            // 
            branchOfiiceName.Location = new Point(261, 23);
            branchOfiiceName.Name = "branchOfiiceName";
            branchOfiiceName.Size = new Size(252, 27);
            branchOfiiceName.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(51, 20);
            label1.TabIndex = 3;
            label1.Text = "Город";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(261, 0);
            label2.Name = "label2";
            label2.Size = new Size(141, 20);
            label2.TabIndex = 4;
            label2.Text = "Название филиала";
            // 
            // branchOfficesTableView
            // 
            branchOfficesTableView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            branchOfficesTableView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            branchOfficesTableView.Location = new Point(12, 75);
            branchOfficesTableView.Name = "branchOfficesTableView";
            branchOfficesTableView.RowHeadersWidth = 51;
            branchOfficesTableView.Size = new Size(776, 328);
            branchOfficesTableView.TabIndex = 1;
            // 
            // branchOfficeClose
            // 
            branchOfficeClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            branchOfficeClose.Location = new Point(694, 409);
            branchOfficeClose.Name = "branchOfficeClose";
            branchOfficeClose.Size = new Size(94, 29);
            branchOfficeClose.TabIndex = 2;
            branchOfficeClose.Text = "Закрыть";
            branchOfficeClose.UseVisualStyleBackColor = true;
            // 
            // BranchOfficesList
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(branchOfficeClose);
            Controls.Add(branchOfficesTableView);
            Controls.Add(tableLayoutPanel1);
            Name = "BranchOfficesList";
            Text = "BranchOfficesList";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)branchOfficesTableView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button acceptBranchOfficeFilterButton;
        private ComboBox cityComboBox;
        private TextBox branchOfiiceName;
        private Label label1;
        private Label label2;
        private DataGridView branchOfficesTableView;
        private Button branchOfficeClose;
    }
}
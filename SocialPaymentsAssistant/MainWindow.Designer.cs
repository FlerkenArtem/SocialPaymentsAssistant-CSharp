namespace SocialPaymentsAssistant
{
    partial class MainWindow
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
            tabControl1 = new TabControl();
            myApplicationsTab = new TabPage();
            myApplicationsTableView = new DataGridView();
            tableLayoutPanel1 = new TableLayoutPanel();
            myApplicationFilterOK = new Button();
            myApplicationTypeEdit = new ComboBox();
            myApplicationsStatusComboBox = new ComboBox();
            myApplicationDateEdit = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            acceptedApplications = new TabPage();
            acceptedApplicationsTableView = new DataGridView();
            tableLayoutPanel2 = new TableLayoutPanel();
            acceptDateEdit = new DateTimePicker();
            paymentTypeComboBox = new ComboBox();
            label4 = new Label();
            label5 = new Label();
            acceptedApplicationFilterOK = new Button();
            applicationMenu = new ToolStripMenuItem();
            newApplicationAct = new ToolStripMenuItem();
            myApplicationsAct = new ToolStripMenuItem();
            acceptedApplicationsAct = new ToolStripMenuItem();
            profileMenu = new ToolStripMenuItem();
            logoutAct = new ToolStripMenuItem();
            winMenu = new ToolStripMenuItem();
            fullscreenAct = new ToolStripMenuItem();
            exitAct = new ToolStripMenuItem();
            aboutMenu = new ToolStripMenuItem();
            branchOfficesAct = new ToolStripMenuItem();
            aboutAct = new ToolStripMenuItem();
            menuStrip = new MenuStrip();
            showApplicationDoc = new Button();
            saveApplicationDoc = new Button();
            tabControl1.SuspendLayout();
            myApplicationsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)myApplicationsTableView).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            acceptedApplications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)acceptedApplicationsTableView).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(myApplicationsTab);
            tabControl1.Controls.Add(acceptedApplications);
            tabControl1.Location = new Point(12, 31);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(776, 407);
            tabControl1.TabIndex = 1;
            // 
            // myApplicationsTab
            // 
            myApplicationsTab.Controls.Add(myApplicationsTableView);
            myApplicationsTab.Controls.Add(tableLayoutPanel1);
            myApplicationsTab.Location = new Point(4, 29);
            myApplicationsTab.Name = "myApplicationsTab";
            myApplicationsTab.Padding = new Padding(3);
            myApplicationsTab.Size = new Size(768, 374);
            myApplicationsTab.TabIndex = 0;
            myApplicationsTab.Text = "Мои заявки";
            myApplicationsTab.UseVisualStyleBackColor = true;
            // 
            // myApplicationsTableView
            // 
            myApplicationsTableView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            myApplicationsTableView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            myApplicationsTableView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            myApplicationsTableView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            myApplicationsTableView.Location = new Point(6, 68);
            myApplicationsTableView.Name = "myApplicationsTableView";
            myApplicationsTableView.RowHeadersWidth = 51;
            myApplicationsTableView.Size = new Size(756, 300);
            myApplicationsTableView.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.000618F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.0006256F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.0006256F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.9981289F));
            tableLayoutPanel1.Controls.Add(myApplicationFilterOK, 3, 1);
            tableLayoutPanel1.Controls.Add(myApplicationTypeEdit, 0, 1);
            tableLayoutPanel1.Controls.Add(myApplicationsStatusComboBox, 1, 1);
            tableLayoutPanel1.Controls.Add(myApplicationDateEdit, 2, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 1, 0);
            tableLayoutPanel1.Controls.Add(label3, 2, 0);
            tableLayoutPanel1.Location = new Point(6, 6);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(756, 56);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // myApplicationFilterOK
            // 
            myApplicationFilterOK.Location = new Point(570, 23);
            myApplicationFilterOK.Name = "myApplicationFilterOK";
            myApplicationFilterOK.Size = new Size(183, 29);
            myApplicationFilterOK.TabIndex = 0;
            myApplicationFilterOK.Text = "Применить";
            myApplicationFilterOK.UseVisualStyleBackColor = true;
            myApplicationFilterOK.Click += button1_Click;
            // 
            // myApplicationTypeEdit
            // 
            myApplicationTypeEdit.FormattingEnabled = true;
            myApplicationTypeEdit.Location = new Point(3, 23);
            myApplicationTypeEdit.Name = "myApplicationTypeEdit";
            myApplicationTypeEdit.Size = new Size(183, 28);
            myApplicationTypeEdit.TabIndex = 1;
            // 
            // myApplicationsStatusComboBox
            // 
            myApplicationsStatusComboBox.FormattingEnabled = true;
            myApplicationsStatusComboBox.Location = new Point(192, 23);
            myApplicationsStatusComboBox.Name = "myApplicationsStatusComboBox";
            myApplicationsStatusComboBox.Size = new Size(183, 28);
            myApplicationsStatusComboBox.TabIndex = 2;
            // 
            // myApplicationDateEdit
            // 
            myApplicationDateEdit.Location = new Point(381, 23);
            myApplicationDateEdit.Name = "myApplicationDateEdit";
            myApplicationDateEdit.Size = new Size(183, 27);
            myApplicationDateEdit.TabIndex = 3;
            myApplicationDateEdit.Value = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(103, 20);
            label1.TabIndex = 4;
            label1.Text = "Тип выплаты:";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(192, 0);
            label2.Name = "label2";
            label2.Size = new Size(106, 20);
            label2.TabIndex = 5;
            label2.Text = "Статус заявки:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(381, 0);
            label3.Name = "label3";
            label3.Size = new Size(99, 20);
            label3.TabIndex = 6;
            label3.Text = "Дата подачи:";
            // 
            // acceptedApplications
            // 
            acceptedApplications.Controls.Add(saveApplicationDoc);
            acceptedApplications.Controls.Add(showApplicationDoc);
            acceptedApplications.Controls.Add(acceptedApplicationsTableView);
            acceptedApplications.Controls.Add(tableLayoutPanel2);
            acceptedApplications.Location = new Point(4, 29);
            acceptedApplications.Name = "acceptedApplications";
            acceptedApplications.Padding = new Padding(3);
            acceptedApplications.Size = new Size(768, 374);
            acceptedApplications.TabIndex = 1;
            acceptedApplications.Text = "Принятые заявки";
            acceptedApplications.UseVisualStyleBackColor = true;
            // 
            // acceptedApplicationsTableView
            // 
            acceptedApplicationsTableView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            acceptedApplicationsTableView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            acceptedApplicationsTableView.Location = new Point(6, 68);
            acceptedApplicationsTableView.Name = "acceptedApplicationsTableView";
            acceptedApplicationsTableView.RowHeadersWidth = 51;
            acceptedApplicationsTableView.Size = new Size(756, 265);
            acceptedApplicationsTableView.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.Controls.Add(acceptDateEdit, 1, 1);
            tableLayoutPanel2.Controls.Add(paymentTypeComboBox, 0, 1);
            tableLayoutPanel2.Controls.Add(label4, 0, 0);
            tableLayoutPanel2.Controls.Add(label5, 1, 0);
            tableLayoutPanel2.Controls.Add(acceptedApplicationFilterOK, 2, 1);
            tableLayoutPanel2.Location = new Point(6, 6);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(756, 56);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // acceptDateEdit
            // 
            acceptDateEdit.Location = new Point(255, 23);
            acceptDateEdit.Name = "acceptDateEdit";
            acceptDateEdit.Size = new Size(246, 27);
            acceptDateEdit.TabIndex = 1;
            // 
            // paymentTypeComboBox
            // 
            paymentTypeComboBox.FormattingEnabled = true;
            paymentTypeComboBox.Location = new Point(3, 23);
            paymentTypeComboBox.Name = "paymentTypeComboBox";
            paymentTypeComboBox.Size = new Size(246, 28);
            paymentTypeComboBox.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 0);
            label4.Name = "label4";
            label4.Size = new Size(103, 20);
            label4.TabIndex = 3;
            label4.Text = "Тип выплаты:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(255, 0);
            label5.Name = "label5";
            label5.Size = new Size(115, 20);
            label5.TabIndex = 4;
            label5.Text = "Дата принятия:";
            // 
            // acceptedApplicationFilterOK
            // 
            acceptedApplicationFilterOK.Location = new Point(507, 23);
            acceptedApplicationFilterOK.Name = "acceptedApplicationFilterOK";
            acceptedApplicationFilterOK.Size = new Size(246, 29);
            acceptedApplicationFilterOK.TabIndex = 0;
            acceptedApplicationFilterOK.Text = "Применить";
            acceptedApplicationFilterOK.UseVisualStyleBackColor = true;
            // 
            // applicationMenu
            // 
            applicationMenu.DropDownItems.AddRange(new ToolStripItem[] { newApplicationAct, myApplicationsAct, acceptedApplicationsAct });
            applicationMenu.Name = "applicationMenu";
            applicationMenu.Size = new Size(71, 24);
            applicationMenu.Text = "Заявки";
            // 
            // newApplicationAct
            // 
            newApplicationAct.Name = "newApplicationAct";
            newApplicationAct.Size = new Size(214, 26);
            newApplicationAct.Text = "Создать заявку";
            // 
            // myApplicationsAct
            // 
            myApplicationsAct.Name = "myApplicationsAct";
            myApplicationsAct.Size = new Size(214, 26);
            myApplicationsAct.Text = "Мои заявки ";
            // 
            // acceptedApplicationsAct
            // 
            acceptedApplicationsAct.Name = "acceptedApplicationsAct";
            acceptedApplicationsAct.Size = new Size(214, 26);
            acceptedApplicationsAct.Text = "Принятые заявки";
            // 
            // profileMenu
            // 
            profileMenu.DropDownItems.AddRange(new ToolStripItem[] { logoutAct });
            profileMenu.Name = "profileMenu";
            profileMenu.Size = new Size(87, 24);
            profileMenu.Text = "Профиль";
            // 
            // logoutAct
            // 
            logoutAct.Name = "logoutAct";
            logoutAct.Size = new Size(222, 26);
            logoutAct.Text = "Выйти из профиля";
            // 
            // winMenu
            // 
            winMenu.DropDownItems.AddRange(new ToolStripItem[] { fullscreenAct, exitAct });
            winMenu.Name = "winMenu";
            winMenu.Size = new Size(59, 24);
            winMenu.Text = "Окно";
            // 
            // fullscreenAct
            // 
            fullscreenAct.Name = "fullscreenAct";
            fullscreenAct.Size = new Size(259, 26);
            fullscreenAct.Text = "Полноэкранный режим";
            // 
            // exitAct
            // 
            exitAct.Name = "exitAct";
            exitAct.Size = new Size(259, 26);
            exitAct.Text = "Выйти";
            // 
            // aboutMenu
            // 
            aboutMenu.DropDownItems.AddRange(new ToolStripItem[] { branchOfficesAct, aboutAct });
            aboutMenu.Name = "aboutMenu";
            aboutMenu.Size = new Size(81, 24);
            aboutMenu.Text = "Справка";
            // 
            // branchOfficesAct
            // 
            branchOfficesAct.Name = "branchOfficesAct";
            branchOfficesAct.Size = new Size(215, 26);
            branchOfficesAct.Text = "Список филиалов";
            // 
            // aboutAct
            // 
            aboutAct.Name = "aboutAct";
            aboutAct.Size = new Size(215, 26);
            aboutAct.Text = "О программе";
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { applicationMenu, profileMenu, winMenu, aboutMenu });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(800, 28);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            menuStrip.ItemClicked += menuStrip1_ItemClicked;
            // 
            // showApplicationDoc
            // 
            showApplicationDoc.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            showApplicationDoc.Location = new Point(466, 339);
            showApplicationDoc.Name = "showApplicationDoc";
            showApplicationDoc.Size = new Size(162, 29);
            showApplicationDoc.TabIndex = 2;
            showApplicationDoc.Text = "Просмотр справки";
            showApplicationDoc.UseVisualStyleBackColor = true;
            // 
            // saveApplicationDoc
            // 
            saveApplicationDoc.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            saveApplicationDoc.Location = new Point(634, 339);
            saveApplicationDoc.Name = "saveApplicationDoc";
            saveApplicationDoc.Size = new Size(125, 29);
            saveApplicationDoc.TabIndex = 3;
            saveApplicationDoc.Text = "Сохранить как";
            saveApplicationDoc.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "MainWindow";
            Text = "Интерактивный помощник для составления заявлений на получение социальных выплат";
            tabControl1.ResumeLayout(false);
            myApplicationsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)myApplicationsTableView).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            acceptedApplications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)acceptedApplicationsTableView).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TabControl tabControl1;
        private TabPage myApplicationsTab;
        private TabPage acceptedApplications;
        private TableLayoutPanel tableLayoutPanel1;
        private Button myApplicationFilterOK;
        private ComboBox myApplicationTypeEdit;
        private ComboBox myApplicationsStatusComboBox;
        private DataGridView myApplicationsTableView;
        private DateTimePicker myApplicationDateEdit;
        private Label label1;
        private Label label2;
        private Label label3;
        private TableLayoutPanel tableLayoutPanel2;
        private Button acceptedApplicationFilterOK;
        private ToolStripMenuItem applicationMenu;
        private ToolStripMenuItem newApplicationAct;
        private ToolStripMenuItem myApplicationsAct;
        private ToolStripMenuItem acceptedApplicationsAct;
        private ToolStripMenuItem profileMenu;
        private ToolStripMenuItem logoutAct;
        private ToolStripMenuItem winMenu;
        private ToolStripMenuItem fullscreenAct;
        private ToolStripMenuItem exitAct;
        private ToolStripMenuItem aboutMenu;
        private ToolStripMenuItem branchOfficesAct;
        private ToolStripMenuItem aboutAct;
        private MenuStrip menuStrip;
        private DataGridView acceptedApplicationsTableView;
        private DateTimePicker acceptDateEdit;
        private ComboBox paymentTypeComboBox;
        private Label label4;
        private Label label5;
        private Button saveApplicationDoc;
        private Button showApplicationDoc;
    }
}
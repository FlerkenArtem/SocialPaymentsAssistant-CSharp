namespace SocialPaymentsAssistant
{
    partial class EmpMainWindow
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
            menuStrip = new MenuStrip();
            empDataMenu = new ToolStripMenuItem();
            empWinMenu = new ToolStripMenuItem();
            empAboutMenu = new ToolStripMenuItem();
            empLogoutAct = new ToolStripMenuItem();
            empFullscreenAct = new ToolStripMenuItem();
            empQuitAct = new ToolStripMenuItem();
            empAboutAct = new ToolStripMenuItem();
            tabControl = new TabControl();
            empApplicationsTab = new TabPage();
            empReadTab = new TabPage();
            applicationsList = new DataGridView();
            open = new Button();
            applicationText = new RichTextBox();
            accept = new Button();
            cancel = new Button();
            menuStrip.SuspendLayout();
            tabControl.SuspendLayout();
            empApplicationsTab.SuspendLayout();
            empReadTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)applicationsList).BeginInit();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { empDataMenu, empWinMenu, empAboutMenu });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(800, 28);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            // 
            // empDataMenu
            // 
            empDataMenu.DropDownItems.AddRange(new ToolStripItem[] { empLogoutAct });
            empDataMenu.Name = "empDataMenu";
            empDataMenu.Size = new Size(111, 24);
            empDataMenu.Text = "Мои данные";
            // 
            // empWinMenu
            // 
            empWinMenu.DropDownItems.AddRange(new ToolStripItem[] { empFullscreenAct, empQuitAct });
            empWinMenu.Name = "empWinMenu";
            empWinMenu.Size = new Size(59, 24);
            empWinMenu.Text = "Окно";
            // 
            // empAboutMenu
            // 
            empAboutMenu.DropDownItems.AddRange(new ToolStripItem[] { empAboutAct });
            empAboutMenu.Name = "empAboutMenu";
            empAboutMenu.Size = new Size(81, 24);
            empAboutMenu.Text = "Справка";
            // 
            // empLogoutAct
            // 
            empLogoutAct.Name = "empLogoutAct";
            empLogoutAct.Size = new Size(224, 26);
            empLogoutAct.Text = "Выйти из профиля";
            // 
            // empFullscreenAct
            // 
            empFullscreenAct.Name = "empFullscreenAct";
            empFullscreenAct.Size = new Size(259, 26);
            empFullscreenAct.Text = "Полноэкранный режим";
            // 
            // empQuitAct
            // 
            empQuitAct.Name = "empQuitAct";
            empQuitAct.Size = new Size(259, 26);
            empQuitAct.Text = "Выйти";
            // 
            // empAboutAct
            // 
            empAboutAct.Name = "empAboutAct";
            empAboutAct.Size = new Size(224, 26);
            empAboutAct.Text = "О программе";
            // 
            // tabControl
            // 
            tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl.Controls.Add(empApplicationsTab);
            tabControl.Controls.Add(empReadTab);
            tabControl.Location = new Point(12, 31);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(776, 407);
            tabControl.TabIndex = 1;
            // 
            // empApplicationsTab
            // 
            empApplicationsTab.Controls.Add(open);
            empApplicationsTab.Controls.Add(applicationsList);
            empApplicationsTab.Location = new Point(4, 29);
            empApplicationsTab.Name = "empApplicationsTab";
            empApplicationsTab.Padding = new Padding(3);
            empApplicationsTab.Size = new Size(768, 374);
            empApplicationsTab.TabIndex = 0;
            empApplicationsTab.Text = "Список заявок";
            empApplicationsTab.UseVisualStyleBackColor = true;
            // 
            // empReadTab
            // 
            empReadTab.Controls.Add(cancel);
            empReadTab.Controls.Add(accept);
            empReadTab.Controls.Add(applicationText);
            empReadTab.Location = new Point(4, 29);
            empReadTab.Name = "empReadTab";
            empReadTab.Padding = new Padding(3);
            empReadTab.Size = new Size(768, 374);
            empReadTab.TabIndex = 1;
            empReadTab.Text = "Чтение текста заявки";
            empReadTab.UseVisualStyleBackColor = true;
            // 
            // applicationsList
            // 
            applicationsList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            applicationsList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            applicationsList.Location = new Point(6, 6);
            applicationsList.Name = "applicationsList";
            applicationsList.RowHeadersWidth = 51;
            applicationsList.Size = new Size(756, 327);
            applicationsList.TabIndex = 0;
            // 
            // open
            // 
            open.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            open.Location = new Point(668, 339);
            open.Name = "open";
            open.Size = new Size(94, 29);
            open.TabIndex = 1;
            open.Text = "Открыть";
            open.UseVisualStyleBackColor = true;
            // 
            // applicationText
            // 
            applicationText.Location = new Point(6, 6);
            applicationText.Name = "applicationText";
            applicationText.Size = new Size(756, 330);
            applicationText.TabIndex = 0;
            applicationText.Text = "";
            // 
            // accept
            // 
            accept.Location = new Point(668, 342);
            accept.Name = "accept";
            accept.Size = new Size(94, 29);
            accept.TabIndex = 1;
            accept.Text = "Принять";
            accept.UseVisualStyleBackColor = true;
            // 
            // cancel
            // 
            cancel.Location = new Point(568, 342);
            cancel.Name = "cancel";
            cancel.Size = new Size(94, 29);
            cancel.TabIndex = 2;
            cancel.Text = "Отклонить";
            cancel.UseVisualStyleBackColor = true;
            // 
            // EmpMainWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "EmpMainWindow";
            Text = "EmpMainWindow";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            tabControl.ResumeLayout(false);
            empApplicationsTab.ResumeLayout(false);
            empReadTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)applicationsList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem empDataMenu;
        private ToolStripMenuItem empLogoutAct;
        private ToolStripMenuItem empWinMenu;
        private ToolStripMenuItem empFullscreenAct;
        private ToolStripMenuItem empQuitAct;
        private ToolStripMenuItem empAboutMenu;
        private ToolStripMenuItem empAboutAct;
        private TabControl tabControl;
        private TabPage empApplicationsTab;
        private Button open;
        private DataGridView applicationsList;
        private TabPage empReadTab;
        private Button cancel;
        private Button accept;
        private RichTextBox applicationText;
    }
}
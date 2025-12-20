namespace SocialPaymentsAssistant
{
    partial class About
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
            aboutText = new RichTextBox();
            aboutCloseButton = new Button();
            SuspendLayout();
            // 
            // aboutText
            // 
            aboutText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            aboutText.DetectUrls = false;
            aboutText.Location = new Point(12, 12);
            aboutText.Name = "aboutText";
            aboutText.Size = new Size(776, 391);
            aboutText.TabIndex = 0;
            aboutText.Text = "";
            // 
            // aboutCloseButton
            // 
            aboutCloseButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            aboutCloseButton.Location = new Point(694, 409);
            aboutCloseButton.Name = "aboutCloseButton";
            aboutCloseButton.Size = new Size(94, 29);
            aboutCloseButton.TabIndex = 1;
            aboutCloseButton.Text = "Закрыть";
            aboutCloseButton.UseVisualStyleBackColor = true;
            aboutCloseButton.Click += aboutCloseButton_Click;
            // 
            // About
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(aboutCloseButton);
            Controls.Add(aboutText);
            Name = "About";
            Text = "About";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox aboutText;
        private Button aboutCloseButton;
    }
}
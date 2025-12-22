using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SocialPaymentsAssistant
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            LoadReadme();
            SetupForm();
        }

        private void LoadReadme()
        {
            string content = "Интерактивный помощник для составления заявлений на получение социальных выплат\n\n" +
                           "Основная информация:\n\n" +
                           "1. Система для подачи заявлений на социальные выплаты\n" +
                           "2. Разработана на C# с использованием .NET Framework/WinForms\n" +
                           "3. Использует PostgreSQL в качестве базы данных\n\n" +
                           "Авторы:\n" +
                           "Хромов А.Е., Горбунова Е.А., Карташова В.А., Сафонова Д.А., Тылик А.В.\n" +
                           "Научный руководитель: Левшин С.С.\n\n" +
                           "ВГТУ, СПК, ИСП-234о.\n" +
                           "Проект создан в учебных целях.";

            aboutText.Text = content;
        }

        private void SetupForm()
        {
            // Настройка формы
            this.Text = "О программе";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;

            // Настройка текстового поля
            aboutText.ReadOnly = true;
            aboutText.BackColor = Color.White;
            aboutText.ForeColor = Color.Black;
            aboutText.BorderStyle = BorderStyle.None;
            aboutText.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
            aboutText.DetectUrls = false;

            // Настройка кнопки
            aboutCloseButton.Text = "Закрыть";
            aboutCloseButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            // Автоматическое изменение размера текстового поля
            aboutText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        private void aboutCloseButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

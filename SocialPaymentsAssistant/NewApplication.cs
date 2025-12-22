using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SocialPaymentsAssistant
{
    public partial class NewApplication : Form
    {
        private int _applicantId = -1; // ID вошедшего пользователя

        public NewApplication()
        {
            InitializeComponent();

            // Инициализация элементов управления
            InitializeControls();

            // Загрузка типов заявок
            LoadApplicationTypes();
        }

        private void InitializeControls()
        {
            // Настройка ComboBox
            typeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // Настройка NumericUpDown
            amountEdit.DecimalPlaces = 2;
            amountEdit.Minimum = 1;
            amountEdit.Maximum = 99999999;
            amountEdit.Increment = 1000;
            amountEdit.Value = 10000;

            // Подписка на события кнопок
            newApplicationOK.Click += newApplicationOK_Click;
            newApplicationCancel.Click += newApplicationCancel_Click;

            // Настройка формы
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(404, 497);
        }

        // Загрузка типов заявок из базы данных
        private void LoadApplicationTypes()
        {
            try
            {
                string query = "SELECT type_name FROM TYPE_OF_SOCIAL_PAYMENT ORDER BY type_name";
                var dataTable = DatabaseHelper.ExecuteQuery(query);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    typeComboBox.Items.Clear();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        typeComboBox.Items.Add(row["type_name"].ToString());
                    }

                    if (typeComboBox.Items.Count > 0)
                        typeComboBox.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Не удалось загрузить типы заявок", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке типов заявок: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Установка ID текущего пользователя
        public void SetApplicantId(int applicantId)
        {
            _applicantId = applicantId;
        }

        // Обработчик нажатия кнопки OK
        private void newApplicationOK_Click(object sender, EventArgs e)
        {
            // Проверяем, установлен ли applicant_id
            if (_applicantId == -1)
            {
                MessageBox.Show("Не установлен ID пользователя.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем данные из формы
            string typeName = typeComboBox.SelectedItem?.ToString();
            decimal amount = amountEdit.Value;

            if (string.IsNullOrEmpty(typeName))
            {
                MessageBox.Show("Выберите тип заявки.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (amount <= 0)
            {
                MessageBox.Show("Сумма должна быть больше 0.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Вызываем хранимую процедуру new_application в указанном стиле
            string query = "CALL new_application(CAST(@applicant_id AS INTEGER), " +
                           "CAST(@type_name AS VARCHAR(38)), " +
                           "CAST(@amount AS DECIMAL(10,2)))";

            var parameters = new Npgsql.NpgsqlParameter[]
            {
                new Npgsql.NpgsqlParameter("@applicant_id", _applicantId),
                new Npgsql.NpgsqlParameter("@type_name", typeName),
                new Npgsql.NpgsqlParameter("@amount", amount)
            };

            try
            {
                int result = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (result >= 0)
                {
                    MessageBox.Show("Заявка успешно создана.", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось создать заявку.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании заявки: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработчик нажатия кнопки Отмена
        private void newApplicationCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
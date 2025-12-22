using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Npgsql;

namespace SocialPaymentsAssistant
{
    public partial class EmpReg : Form
    {
        public EmpReg()
        {
            InitializeComponent();

            // Настраиваем даты по умолчанию
            passportDateOfIssue.Value = DateTime.Now;
            birthDate.Value = DateTime.Now.AddYears(-30);

            // Загружаем данные из БД
            LoadBranches();
            LoadPositions();
            LoadPassportDepartments();
            LoadDepartments(-1);

            // Подключаем обработчики событий
            button1.Click += BranchOfficeFilterBtn_Click;
            noPatronymicCheck.CheckedChanged += CheckBox_Toggled;
            empRegOK.Click += EmpRegOK_Click;

            // Переименуем комбобоксы
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        private void LoadBranches()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add(new ComboBoxItem("Выберите филиал", -1));

            try
            {
                var query = "SELECT BRANCH_OFFICE_ID, BRANCH_NAME FROM BRANCH_OFFICE ORDER BY BRANCH_NAME";
                var dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    int id = Convert.ToInt32(row["branch_office_id"]);
                    string name = row["branch_name"].ToString();
                    comboBox1.Items.Add(new ComboBoxItem(name, id));
                }

                comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить список филиалов:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadPositions()
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add(new ComboBoxItem("Выберите должность", -1));

            try
            {
                var query = "SELECT EMPLOYEE_POSITION_ID, POSITION FROM EMPLOYEE_POSITION ORDER BY POSITION";
                var dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    int id = Convert.ToInt32(row["employee_position_id"]);
                    string position = row["position"].ToString();
                    comboBox3.Items.Add(new ComboBoxItem(position, id));
                }

                comboBox3.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить список должностей:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadPassportDepartments()
        {
            passportIssuingDepartment.Items.Clear();
            passportIssuingDepartment.Items.Add(new ComboBoxItem("Введите отдел", -1));

            try
            {
                var query = "SELECT PASSPORT_ISSUING_DEPARTMENT_ID, ISSUING_DEPARTMENT FROM PASSPORT_ISSUING_DEPARTMENT ORDER BY ISSUING_DEPARTMENT";
                var dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    int id = Convert.ToInt32(row["passport_issuing_department_id"]);
                    string department = row["issuing_department"].ToString();
                    passportIssuingDepartment.Items.Add(new ComboBoxItem(department, id));
                }

                passportIssuingDepartment.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить список отделов, выдавших паспорт:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadDepartments(int branchId)
        {
            comboBox2.Items.Clear();

            try
            {
                string query;
                NpgsqlParameter[] parameters = null;

                if (branchId == -1)
                {
                    // Загружаем все отделы
                    query = @"SELECT d.DEPARTMENT_ID, d.DEPARTMENT_NAME, b.BRANCH_NAME 
                             FROM DEPARTMENT d 
                             INNER JOIN BRANCH_OFFICE b ON d.BRANCH_OFFICE_ID = b.BRANCH_OFFICE_ID 
                             ORDER BY b.BRANCH_NAME, d.DEPARTMENT_NAME";
                }
                else
                {
                    // Загружаем отделы только для выбранного филиала
                    query = "SELECT DEPARTMENT_ID, DEPARTMENT_NAME FROM DEPARTMENT WHERE BRANCH_OFFICE_ID = @branchId ORDER BY DEPARTMENT_NAME";
                    parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("@branchId", branchId)
                    };
                }

                var dataTable = DatabaseHelper.ExecuteQuery(query, parameters);

                comboBox2.Items.Add(new ComboBoxItem("Выберите отдел", -1));

                foreach (DataRow row in dataTable.Rows)
                {
                    int id = Convert.ToInt32(row["department_id"]);
                    string name = row["department_name"].ToString();

                    if (branchId == -1)
                    {
                        string branch = row["branch_name"].ToString();
                        comboBox2.Items.Add(new ComboBoxItem($"{name} ({branch})", id));
                    }
                    else
                    {
                        comboBox2.Items.Add(new ComboBoxItem(name, id));
                    }
                }

                comboBox2.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить список отделов:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BranchOfficeFilterBtn_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is ComboBoxItem selectedItem)
            {
                int branchId = selectedItem.Value;

                if (branchId == -1)
                {
                    MessageBox.Show("Пожалуйста, выберите филиал для фильтрации",
                        "Выбор филиала", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LoadDepartments(branchId);
            }
        }

        private void CheckBox_Toggled(object sender, EventArgs e)
        {
            patronymic.Enabled = !noPatronymicCheck.Checked;
            if (noPatronymicCheck.Checked)
            {
                patronymic.Clear();
            }
        }

        private void EmpRegOK_Click(object sender, EventArgs e)
        {
            // Проверка обязательных полей
            if (string.IsNullOrEmpty(login.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(eMail.Text))
            {
                MessageBox.Show("Введите email", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(password.Text))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Text != password2.Text)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Text.Length < 8)
            {
                MessageBox.Show("Пароль должен содержать минимум 8 символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(surname.Text))
            {
                MessageBox.Show("Введите фамилию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(name.Text))
            {
                MessageBox.Show("Введите имя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(phone.Text))
            {
                MessageBox.Show("Введите номер телефона", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Паспортные данные
            if (string.IsNullOrEmpty(passportSeriesEdit.Text))
            {
                MessageBox.Show("Введите серию паспорта", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(passportNumberEdit.Text))
            {
                MessageBox.Show("Введите номер паспорта", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (passportIssuingDepartment.SelectedItem is ComboBoxItem passportItem && passportItem.Value == -1)
            {
                MessageBox.Show("Введите отдел, выдавший паспорт", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(passportDivisionCode.Text))
            {
                MessageBox.Show("Введите код подразделения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Трудовая книжка
            if (string.IsNullOrEmpty(workBookSeries.Text))
            {
                MessageBox.Show("Введите серию трудовой книжки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(workBookNum.Text))
            {
                MessageBox.Show("Введите номер трудовой книжки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ИНН
            if (string.IsNullOrEmpty(inn.Text))
            {
                MessageBox.Show("Введите ИНН", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Рабочие данные
            if (comboBox1.SelectedItem is ComboBoxItem branchItem && branchItem.Value == -1)
            {
                MessageBox.Show("Выберите филиал", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox2.SelectedItem is ComboBoxItem departmentItem && departmentItem.Value == -1)
            {
                MessageBox.Show("Выберите отдел", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox3.SelectedItem is ComboBoxItem positionItem && positionItem.Value == -1)
            {
                MessageBox.Show("Выберите должность", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Подготавливаем данные для процедуры
                string username = login.Text;
                string passwordText = password.Text;
                string email = eMail.Text;
                string surnameText = surname.Text;
                string nameText = name.Text;
                string patronymicText = noPatronymicCheck.Checked ? "" : patronymic.Text;
                string phoneNumber = phone.Text;
                string innText = inn.Text;
                string positionText = ((ComboBoxItem)comboBox3.SelectedItem).Text;
                //string positionQuery = "SELECT employee_position_id " +
                //                       "FROM employee_position " +
                //                       "WHERE position = @position ";
                //var positionParameters = new NpgsqlParameter[]
                //{
                //    new NpgsqlParameter("@position", positionText)
                //};
                //string positionId = DatabaseHelper.ExecuteScalar(positionQuery, positionParameters).ToString(); // Получение positionId из текста
                string departmentName = ((ComboBoxItem)comboBox2.SelectedItem).Text;
                //string departamentQuery = "SELECT department_id " +
                //    "FROM department " +
                //    "WHERE department_name = @department ";
                //var departmentParameters = new NpgsqlParameter[]
                //{
                //    new NpgsqlParameter("@department", departmentName)
                //};
                //string departmentId = DatabaseHelper.ExecuteScalar(departamentQuery, departmentParameters).ToString(); // Получение departmentId из текста
                string passportSeries = passportSeriesEdit.Text;
                string passportNumber = passportNumberEdit.Text;
                string passportIssuedBy = ((ComboBoxItem)passportIssuingDepartment.SelectedItem).Text;
                string passportIssueDate = passportDateOfIssue.Value.ToString("yyyy-MM-dd");
                string passportDivisionCodeText = passportDivisionCode.Text;
                string birthDateText = birthDate.Value.ToString("yyyy-MM-dd");
                string workBookSeriesText = workBookSeries.Text;
                string workBookNumber = workBookNum.Text;

                // Вызов хранимой процедуры
                string query = "CALL new_emp(CAST(@username AS VARCHAR(50)), " +
               "CAST(@password AS VARCHAR(20)), " +
               "CAST(@email AS VARCHAR(345)), " +
               "CAST(@surname AS VARCHAR(140)), " +
               "CAST(@name AS VARCHAR(2250)), " +
               "CAST(@patronymic AS VARCHAR(2255)), " +
               "CAST(@phoneNumber AS CHAR(18)), " +
               "CAST(@inn AS CHAR(12)), " +
               "CAST(@position AS VARCHAR(34)), " +
               "CAST(@department AS VARCHAR(100)), " +
               "CAST(@passportSeries AS CHAR(4)), " +
               "CAST(@passportNumber AS CHAR(6)), " +
               "CAST(@passportIssuedBy AS VARCHAR(129)), " +
               "CAST(@passportIssueDate::date AS DATE), " +
               "CAST(@passportDivisionCode AS CHAR(7)), " +
               "CAST(@birthDate::date AS DATE), " +
               "CAST(@workBookSeries AS VARCHAR(6)), " +
               "CAST(@workBookNumber AS CHAR(7)))";
                var parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@username", username),
                    new NpgsqlParameter("@password", passwordText),
                    new NpgsqlParameter("@email", email),
                    new NpgsqlParameter("@surname", surnameText),
                    new NpgsqlParameter("@name", nameText),
                    new NpgsqlParameter("@patronymic", patronymicText),
                    new NpgsqlParameter("@phoneNumber", phoneNumber),
                    new NpgsqlParameter("@inn", innText),
                    new NpgsqlParameter("@position", positionText),
                    new NpgsqlParameter("@department", departmentName),
                    new NpgsqlParameter("@passportSeries", passportSeries),
                    new NpgsqlParameter("@passportNumber", passportNumber),
                    new NpgsqlParameter("@passportIssuedBy", passportIssuedBy),
                    new NpgsqlParameter("@passportIssueDate", passportIssueDate),
                    new NpgsqlParameter("@passportDivisionCode", passportDivisionCodeText),
                    new NpgsqlParameter("@birthDate", birthDateText),
                    new NpgsqlParameter("@workBookSeries", workBookSeriesText),
                    new NpgsqlParameter("@workBookNumber", workBookNumber)
                };
                Console.WriteLine(query, parameters);
                int result = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (result >= 0)
                {
                    MessageBox.Show("Сотрудник успешно зарегистрирован!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Получаем ID только что созданного сотрудника
                    var idQuery = @"
                        SELECT e.employee_id 
                        FROM employee e 
                        JOIN account a ON e.account_id = a.account_id 
                        WHERE a.username = @username";

                    var idParams = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("@username", username)
                    };

                    var employeeIdResult = DatabaseHelper.ExecuteScalar(idQuery, idParams);

                    if (result >= 0)
                    {
                        int employeeId = Convert.ToInt32(employeeIdResult);
                        EmpMainWindow empWindow = new EmpMainWindow(employeeId);
                        empWindow.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось зарегистрировать сотрудника",
                        "Ошибка регистрации: " + result.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось зарегистрировать сотрудника:\n{ex.Message}",
                    "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Автоматически фильтруем отделы при выборе филиала
            if (comboBox1.SelectedItem is ComboBoxItem selectedItem)
            {
                LoadDepartments(selectedItem.Value);
            }
        }

        // Вспомогательный класс для ComboBox
        private class ComboBoxItem
        {
            public string Text { get; }
            public int Value { get; }

            public ComboBoxItem(string text, int value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        // Обработчики событий для Designer
        private void groupBox1_Enter(object sender, EventArgs e) 
        {

        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {
            
        }
        private void label3_Click(object sender, EventArgs e) 
        {

        }
        private void groupBox2_Enter(object sender, EventArgs e) 
        {

        }
        private void label9_Click(object sender, EventArgs e) 
        {

        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) 
        {

        }
        private void groupBox3_Enter(object sender, EventArgs e) 
        {

        }
        private void label16_Click(object sender, EventArgs e) 
        {

        }
        private void label15_Click(object sender, EventArgs e) 
        {

        }
        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }
        private void label21_Click(object sender, EventArgs e)
        {

        }
    }
}
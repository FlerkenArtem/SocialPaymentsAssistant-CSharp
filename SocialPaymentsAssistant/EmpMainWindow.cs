using System;
using System.Data;
using System.Windows.Forms;
using SocialPaymentsAssistant.Models;
using Npgsql;

namespace SocialPaymentsAssistant
{
    public partial class EmpMainWindow : Form
    {
        private int _employeeId;
        private EmpApplicationsModel _applicationsModel;
        private DataTable _applicationsDataTable;
        private DataView _applicationsDataView;
        private int _currentApplicationId = -1;

        public EmpMainWindow(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;

            InitializeData();
            SetupDataGridView();
            LoadEmployeeInfo();
            SetupEventHandlers();
            RefreshApplicationData();
        }

        private void InitializeData()
        {
            _applicationsModel = new EmpApplicationsModel(_employeeId);
            _applicationsDataTable = ConvertApplicationsModelToDataTable();
            _applicationsDataView = new DataView(_applicationsDataTable);
        }

        private void SetupDataGridView()
        {
            applicationsList.DataSource = _applicationsDataView;
            applicationsList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            applicationsList.MultiSelect = false;
            applicationsList.ReadOnly = true;
            applicationsList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            applicationsList.RowHeadersVisible = false;

            // Скрываем ID колонку
            if (applicationsList.Columns.Contains("ID заявки"))
                applicationsList.Columns["ID заявки"].Visible = false;
        }

        private DataTable ConvertApplicationsModelToDataTable()
        {
            DataTable dataTable = new DataTable();

            // Создаем колонки
            for (int i = 0; i < _applicationsModel.ColumnCount; i++)
            {
                dataTable.Columns.Add(_applicationsModel.GetColumnName(i));
            }

            // Заполняем данные
            for (int row = 0; row < _applicationsModel.RowCount; row++)
            {
                DataRow dataRow = dataTable.NewRow();
                for (int col = 0; col < _applicationsModel.ColumnCount; col++)
                {
                    var value = _applicationsModel.GetData(row, col);
                    dataRow[col] = value ?? DBNull.Value;
                }
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        private void LoadEmployeeInfo()
        {
            try
            {
                string query = @"
                    SELECT e.surname, e.name, e.patronymic, ep.position 
                    FROM employee e 
                    JOIN employee_position ep ON e.employee_position_id = ep.employee_position_id 
                    WHERE e.employee_id = CAST(@employee_id as integer)";

                var parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@employee_id", _employeeId)
                };

                var dataTable = DatabaseHelper.ExecuteQuery(query, parameters);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    string surname = row["surname"].ToString();
                    string name = row["name"].ToString();
                    string patronymic = row["patronymic"].ToString();
                    string position = row["position"].ToString();

                    string title = $"Сотрудник: {surname} {name} {patronymic} ({position})";
                    this.Text = title;
                }
                else
                {
                    MessageBox.Show("Не удалось загрузить данные сотрудника", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных сотрудника: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupEventHandlers()
        {
            // Обработчики кнопок
            open.Click += Open_Click;
            cancel.Click += Cancel_Click;
            accept.Click += Accept_Click;

            // Обработчики меню
            empLogoutAct.Click += EmpLogoutAct_Click;
            empFullscreenAct.Click += EmpFullscreenAct_Click;
            empQuitAct.Click += EmpQuitAct_Click;
            empAboutAct.Click += EmpAboutAct_Click;
        }

        private void RefreshApplicationData()
        {
            _applicationsModel.RefreshData();
            _applicationsDataTable.Clear();

            // Обновляем данные
            for (int row = 0; row < _applicationsModel.RowCount; row++)
            {
                DataRow dataRow = _applicationsDataTable.NewRow();
                for (int col = 0; col < _applicationsModel.ColumnCount; col++)
                {
                    var value = _applicationsModel.GetData(row, col);
                    dataRow[col] = value ?? DBNull.Value;
                }
                _applicationsDataTable.Rows.Add(dataRow);
            }

            _applicationsDataView.RowFilter = string.Empty;
        }

        #region Обработчики событий

        private void Open_Click(object sender, EventArgs e)
        {
            if (applicationsList.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заявку для просмотра", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rowIndex = applicationsList.SelectedRows[0].Index;
            DataRowView rowView = _applicationsDataView[rowIndex];

            int applicationId = Convert.ToInt32(rowView["ID заявки"]);
            _currentApplicationId = applicationId;

            ShowApplicationDetails(applicationId);
            tabControl.SelectedTab = empReadTab;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            if (_currentApplicationId == -1)
            {
                MessageBox.Show("Сначала откройте заявку", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Запрос подтверждения
            DialogResult confirmResult = MessageBox.Show(
                "Вы уверены, что хотите отклонить заявку?\n\n" +
                "Данное действие необратимо. Для подтверждения введите пароль.",
                "Подтверждение отклонения",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
                return;

            // Запрос пароля
            string password = ShowPasswordDialog("Подтверждение пароля", "Введите ваш пароль:");
            if (string.IsNullOrEmpty(password))
                return;

            try
            {
                // Вызов хранимой процедуры cancel_application
                string query = "CALL cancel_application(CAST(@application_id as integer), CAST(@employee_password as varchar))";
                var parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@application_id", _currentApplicationId),
                    new NpgsqlParameter("@employee_password", password)
                };

                int result = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (result >= 0)
                {
                    MessageBox.Show("Заявка успешно отклонена", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    RefreshApplicationData();
                    applicationText.Clear();
                    _currentApplicationId = -1;
                    tabControl.SelectedTab = empApplicationsTab;
                }
                else
                {
                    MessageBox.Show("Не удалось отклонить заявку", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось отклонить заявку:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            if (_currentApplicationId == -1)
            {
                MessageBox.Show("Сначала откройте заявку", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Получаем текущую сумму заявки
                double requestedAmount = GetApplicationAmount(_currentApplicationId);

                // Запрос пароля
                string password = ShowPasswordDialog("Подтверждение принятия", "Введите ваш пароль:");
                if (string.IsNullOrEmpty(password))
                    return;

                // Диалог для изменения суммы
                double approvedAmount = ShowAmountDialog("Сумма выплаты",
                    "Введите сумму выплаты:", requestedAmount);
                if (approvedAmount <= 0)
                    return;

                // Подтверждение
                DialogResult confirmResult = MessageBox.Show(
                    $"Вы уверены, что хотите принять заявку?\n\n" +
                    $"Сумма выплаты: {approvedAmount:F2} руб.\n" +
                    $"Данное действие необратимо.",
                    "Подтверждение принятия",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult != DialogResult.Yes)
                    return;

                // Вызов хранимой процедуры accept_application
                string query = "CALL accept_application((CAST(@application_id as integer), CAST(@employee_password as varchar), CAST(@new_amount as numeric(10,2)))";
                var parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@application_id", _currentApplicationId),
                    new NpgsqlParameter("@employee_password", password),
                    new NpgsqlParameter("@new_amount", approvedAmount)
                };

                int result = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (result >= 0)
                {
                    // Генерация и сохранение PDF-документа
                    GenerateAndSaveCertificate(_currentApplicationId, requestedAmount, approvedAmount);

                    MessageBox.Show("Заявка успешно принята и документ сформирован", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    RefreshApplicationData();
                    applicationText.Clear();
                    _currentApplicationId = -1;
                    tabControl.SelectedTab = empApplicationsTab;
                }
                else
                {
                    MessageBox.Show("Не удалось принять заявку", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось принять заявку:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EmpLogoutAct_Click(object sender, EventArgs e)
        {
            LoginWidget loginForm = new LoginWidget();
            loginForm.Show();
            this.Close();
        }

        private void EmpFullscreenAct_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void EmpQuitAct_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void EmpAboutAct_Click(object sender, EventArgs e)
        {
            About aboutForm = new About();
            aboutForm.ShowDialog();
        }

        #endregion

        #region Вспомогательные методы

        private void ShowApplicationDetails(int applicationId)
        {
            try
            {
                string query = @"
                    SELECT 
                        a.application_id, 
                        app.surname || ' ' || app.name || COALESCE(' ' || app.patronymic, '') as applicant_fio,
                        tosp.type_name, 
                        a.amount, 
                        a.date_of_creation, 
                        ast.status, 
                        e.surname || ' ' || e.name || COALESCE(' ' || e.patronymic, '') as employee_fio,
                        ep.position, 
                        app.inn, 
                        app.phone_number, 
                        app.applicant_id 
                    FROM application a 
                    JOIN applicant app ON a.applicant_id = app.applicant_id 
                    JOIN type_of_social_payment tosp ON a.type_of_social_payment_id = tosp.type_of_social_payment_id 
                    JOIN application_status ast ON a.application_status_id = ast.application_status_id 
                    JOIN employee e ON a.employee_id = e.employee_id 
                    JOIN employee_position ep ON e.employee_position_id = ep.employee_position_id 
                    WHERE a.application_id = (CAST(@application_id as integer)";

                var parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@application_id", applicationId)
                };

                var dataTable = DatabaseHelper.ExecuteQuery(query, parameters);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];

                    string details = $@"<html>
                                        <head>
                                        <style>
                                        body {{ font-family: Arial, sans-serif; margin: 20px; }}
                                        table {{ border-collapse: collapse; width: 100%; margin: 20px 0; }}
                                        th, td {{ border: 1px solid #ddd; padding: 8px; text-align: left; }}
                                        th {{ background-color: #f2f2f2; font-weight: bold; }}
                                        h2 {{ color: #333; }}
                                        </style>
                                        </head>
                                        <body>
                                        <h2>Детали заявки #{row["application_id"]}</h2>
                                        <table>
                                        <tr><td><b>ID заявки:</b></td><td>{row["application_id"]}</td></tr>
                                        <tr><td><b>ФИО заявителя:</b></td><td>{row["applicant_fio"]}</td></tr>
                                        <tr><td><b>ИНН заявителя:</b></td><td>{row["inn"]}</td></tr>
                                        <tr><td><b>Телефон заявителя:</b></td><td>{row["phone_number"]}</td></tr>
                                        <tr><td><b>Тип выплаты:</b></td><td>{row["type_name"]}</td></tr>
                                        <tr><td><b>Сумма:</b></td><td>{Convert.ToDouble(row["amount"]):F2} руб.</td></tr>
                                        <tr><td><b>Дата подачи:</b></td><td>{Convert.ToDateTime(row["date_of_creation"]):dd.MM.yyyy HH:mm}</td></tr>
                                        <tr><td><b>Статус:</b></td><td>{row["status"]}</td></tr>
                                        <tr><td><b>Ответственный сотрудник:</b></td><td>{row["employee_fio"]}</td></tr>
                                        <tr><td><b>Должность:</b></td><td>{row["position"]}</td></tr>
                                        <tr><td><b>ID заявителя:</b></td><td>{row["applicant_id"]}</td></tr>
                                        </table>
                                        <p><i>Для принятия или отклонения заявки используйте кнопки ниже</i></p>
                                        </body>
                                        </html>";

                    applicationText.Text = details;
                }
                else
                {
                    MessageBox.Show($"Заявка с ID {applicationId} не найдена в базе данных.", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки деталей заявки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private double GetApplicationAmount(int applicationId)
        {
            try
            {
                string query = "SELECT amount FROM application WHERE application_id = (CAST(@id as integer)";
                var parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@id", applicationId)
                };

                var result = DatabaseHelper.ExecuteScalar(query, parameters);
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToDouble(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка получения суммы заявки: {ex.Message}");
            }

            return 0;
        }

        private void GenerateAndSaveCertificate(int applicationId, double requestedAmount, double approvedAmount)
        {
            try
            {
                // Получаем данные для справки
                string query = @"
                    SELECT 
                        a.application_id, 
                        app.surname || ' ' || app.name || COALESCE(' ' || app.patronymic, '') as applicant_fio,
                        tosp.type_name, 
                        a.date_of_creation, 
                        e.surname || ' ' || e.name || COALESCE(' ' || e.patronymic, '') as employee_fio,
                        ep.position, 
                        app.inn, 
                        app.phone_number 
                    FROM application a 
                    JOIN applicant app ON a.applicant_id = app.applicant_id 
                    JOIN type_of_social_payment tosp ON a.type_of_social_payment_id = tosp.type_of_social_payment_id 
                    JOIN employee e ON a.employee_id = e.employee_id 
                    JOIN employee_position ep ON e.employee_position_id = ep.employee_position_id 
                    WHERE a.application_id = (CAST(@application_id as integer) ";

                var parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@application_id", applicationId)
                };

                var dataTable = DatabaseHelper.ExecuteQuery(query, parameters);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];

                    // Создаем структуру с информацией о заявке
                    var appInfo = new ApplicationInfo
                    {
                        ApplicationId = applicationId,
                        ApplicantName = row["applicant_fio"].ToString(),
                        PaymentType = row["type_name"].ToString(),
                        CreationDate = Convert.ToDateTime(row["date_of_creation"]),
                        RequestedAmount = requestedAmount,
                        ApprovedAmount = approvedAmount,
                        EmployeeName = row["employee_fio"].ToString(),
                        EmployeePosition = row["position"].ToString(),
                        BranchName = "Отделение социальных выплат",
                        ApplicantInn = row["inn"].ToString(),
                        ApplicantPhone = row["phone_number"].ToString(),
                        ApplicantAddress = "Адрес по регистрации"
                    };

                    // Генерируем PDF-документ
                    var pdfGenerator = new PdfDocumentGenerator();
                    byte[] pdfData = pdfGenerator.GenerateCertificate(appInfo);

                    if (pdfData != null && pdfData.Length > 0)
                    {
                        // Проверяем, есть ли уже сертификат для этой заявки
                        string checkQuery = "SELECT COUNT(*) FROM certificate WHERE application_id = @application_id";
                        var checkParams = new NpgsqlParameter[]
                        {
                            new NpgsqlParameter("@application_id", applicationId)
                        };

                        var countResult = DatabaseHelper.ExecuteScalar(checkQuery, checkParams);
                        int count = (countResult != null && countResult != DBNull.Value) ?
                                   Convert.ToInt32(countResult) : 0;

                        if (count > 0)
                        {
                            // Обновляем существующий сертификат
                            string updateQuery = @"
                                UPDATE certificate 
                                SET date_and_time_of_creation = CAST(@creation_date as date), 
                                    document = CAST(@document as bytea))  
                                WHERE application_id = (CAST(@application_id as integer) ";

                            var updateParams = new NpgsqlParameter[]
                            {
                                new NpgsqlParameter("@application_id", applicationId),
                                new NpgsqlParameter("@creation_date", DateTime.Now),
                                new NpgsqlParameter("@document", pdfData)
                            };

                            DatabaseHelper.ExecuteNonQuery(updateQuery, updateParams);
                        }
                        else
                        {
                            // Вставляем новый сертификат
                            string insertQuery = @"
                                INSERT INTO certificate (application_id, date_and_time_of_creation, document) 
                                VALUES (CAST(@application_id as integer), CAST(@creation_date as date), CAST(@document as bytea))";

                            var insertParams = new NpgsqlParameter[]
                            {
                                new NpgsqlParameter("@application_id", applicationId),
                                new NpgsqlParameter("@creation_date", DateTime.Now),
                                new NpgsqlParameter("@document", pdfData)
                            };

                            DatabaseHelper.ExecuteNonQuery(insertQuery, insertParams);
                        }

                        // Проверяем результат
                        string verifyQuery = @"
                            SELECT certificate_id, date_and_time_of_creation, LENGTH(document) as doc_size 
                            FROM certificate WHERE application_id = CAST(@application_id as integer)";

                        var verifyResult = DatabaseHelper.ExecuteQuery(verifyQuery, parameters);

                        if (verifyResult != null && verifyResult.Rows.Count > 0)
                        {
                            Console.WriteLine($"Сертификат успешно сохранен в БД: ID={verifyResult.Rows[0]["certificate_id"]}, " +
                                            $"Размер={verifyResult.Rows[0]["doc_size"]} байт");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удалось сгенерировать PDF-документ", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при генерации сертификата: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ShowPasswordDialog(string title, string prompt)
        {
            Form promptForm = new Form()
            {
                Width = 350,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterParent
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Width = 300, Text = prompt };
            TextBox textBox = new TextBox() { Left = 20, Top = 45, Width = 300, PasswordChar = '*' };
            Button confirmation = new Button() { Text = "OK", Left = 230, Width = 60, Top = 75, DialogResult = DialogResult.OK };

            confirmation.Click += (sender, e) => { promptForm.Close(); };
            promptForm.Controls.Add(textLabel);
            promptForm.Controls.Add(textBox);
            promptForm.Controls.Add(confirmation);
            promptForm.AcceptButton = confirmation;

            return promptForm.ShowDialog() == DialogResult.OK ? textBox.Text : string.Empty;
        }

        private double ShowAmountDialog(string title, string prompt, double defaultValue)
        {
            Form promptForm = new Form()
            {
                Width = 350,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterParent
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Width = 300, Text = prompt };
            NumericUpDown numericBox = new NumericUpDown()
            {
                Left = 20,
                Top = 45,
                Width = 300,
                Minimum = 0,
                Maximum = 10000000,
                DecimalPlaces = 2,
                Value = (decimal)defaultValue
            };
            Button confirmation = new Button() { Text = "OK", Left = 230, Width = 60, Top = 75, DialogResult = DialogResult.OK };

            confirmation.Click += (sender, e) => { promptForm.Close(); };
            promptForm.Controls.Add(textLabel);
            promptForm.Controls.Add(numericBox);
            promptForm.Controls.Add(confirmation);
            promptForm.AcceptButton = confirmation;

            return promptForm.ShowDialog() == DialogResult.OK ? (double)numericBox.Value : -1;
        }

        #endregion
    }
}
using System;
using System.Data;
using System.Windows.Forms;
using SocialPaymentsAssistant.Models;
using Npgsql;
using System.IO;
using System.Text;

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
            applicationsList.AutoGenerateColumns = false;
            applicationsList.DataSource = _applicationsDataView;
            applicationsList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            applicationsList.MultiSelect = false;
            applicationsList.ReadOnly = true;
            applicationsList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            applicationsList.RowHeadersVisible = false;

            // Добавляем колонки вручную
            applicationsList.Columns.Clear();
            applicationsList.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ID заявки",
                HeaderText = "ID заявки",
                Name = "ID заявки",
                Visible = false
            });
            applicationsList.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Заявитель",
                HeaderText = "Заявитель",
                Name = "Заявитель"
            });
            applicationsList.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Тип выплаты",
                HeaderText = "Тип выплаты",
                Name = "Тип выплаты"
            });
            applicationsList.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Дата создания",
                HeaderText = "Дата создания",
                Name = "Дата создания"
            });
            applicationsList.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Сумма",
                HeaderText = "Сумма",
                Name = "Сумма"
            });

            for (int i = 0; i < applicationsList.Columns.Count; i++)
            {
                applicationsList.Columns[i].DataPropertyName = applicationsList.Columns[i].Name;
            }
        }

        private DataTable ConvertApplicationsModelToDataTable()
        {
            DataTable dataTable = new DataTable();

            for (int i = 0; i < _applicationsModel.ColumnCount; i++)
            {
                dataTable.Columns.Add(_applicationsModel.GetColumnName(i));
            }

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
                    WHERE e.employee_id = @employee_id";

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
            open.Click += Open_Click;
            cancel.Click += Cancel_Click;
            accept.Click += Accept_Click;

            empLogoutAct.Click += EmpLogoutAct_Click;
            empFullscreenAct.Click += EmpFullscreenAct_Click;
            empQuitAct.Click += EmpQuitAct_Click;
            empAboutAct.Click += EmpAboutAct_Click;
        }

        private void RefreshApplicationData()
        {
            _applicationsModel.RefreshData();
            _applicationsDataTable.Clear();

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

            DialogResult confirmResult = MessageBox.Show(
                "Вы уверены, что хотите отклонить заявку?\n\n" +
                "Данное действие необратимо. Для подтверждения введите пароль.",
                "Подтверждение отклонения",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
                return;

            string password = ShowPasswordDialog("Подтверждение пароля", "Введите ваш пароль:");
            if (string.IsNullOrEmpty(password))
                return;

            try
            {
                string query = "CALL cancel_application(@application_id::integer, @employee_password::varchar)";
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
                double requestedAmount = GetApplicationAmount(_currentApplicationId);

                string password = ShowPasswordDialog("Подтверждение принятия", "Введите ваш пароль:");
                if (string.IsNullOrEmpty(password))
                    return;

                double approvedAmount = ShowAmountDialog("Сумма выплаты",
                    "Введите сумму выплаты:", requestedAmount);
                if (approvedAmount <= 0)
                    return;

                DialogResult confirmResult = MessageBox.Show(
                    $"Вы уверены, что хотите принять заявку?\n\n" +
                    $"Сумма выплаты: {approvedAmount:F2} руб.\n" +
                    $"Данное действие необратимо.",
                    "Подтверждение принятия",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult != DialogResult.Yes)
                    return;

                // 1. Сначала вызываем хранимую процедуру для принятия заявки
                string acceptQuery = "CALL accept_application(@application_id::integer, @employee_password::varchar, @new_amount::decimal)";
                var acceptParameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@application_id", _currentApplicationId),
                    new NpgsqlParameter("@employee_password", password),
                    new NpgsqlParameter("@new_amount", approvedAmount)
                };

                int result = DatabaseHelper.ExecuteNonQuery(acceptQuery, acceptParameters);

                if (result < -1)
                {
                    MessageBox.Show("Не удалось принять заявку через хранимую процедуру", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Получаем данные для TXT документа
                ApplicationInfo appInfo = GetApplicationInfoForCertificate(_currentApplicationId);
                if (appInfo == null)
                {
                    MessageBox.Show("Заявка принята, но не удалось получить данные для документа", "Внимание",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // Продолжаем выполнение - заявка уже принята
                }
                else
                {
                    appInfo.RequestedAmount = requestedAmount;
                    appInfo.ApprovedAmount = approvedAmount;

                    // 3. Генерируем и сохраняем TXT документ
                    byte[] txtData = GenerateTxtCertificate(appInfo);
                    SaveTxtToDatabase(_currentApplicationId, txtData);
                }

                MessageBox.Show("Заявка успешно принята", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefreshApplicationData();
                applicationText.Clear();
                _currentApplicationId = -1;
                tabControl.SelectedTab = empApplicationsTab;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось принять заявку:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Генерация TXT документа

        private byte[] GenerateTxtCertificate(ApplicationInfo appInfo)
        {
            // Заполняем пропущенные данные
            appInfo.ApplicantName = appInfo.ApplicantName ?? "Не указано";
            appInfo.PaymentType = appInfo.PaymentType ?? "Не указано";
            appInfo.EmployeeName = appInfo.EmployeeName ?? "Не указано";
            appInfo.EmployeePosition = appInfo.EmployeePosition ?? "Не указано";
            appInfo.ApplicantInn = appInfo.ApplicantInn ?? "Не указано";
            appInfo.ApplicantPhone = appInfo.ApplicantPhone ?? "Не указано";

            StringBuilder sb = new StringBuilder();

            // Заголовок
            sb.AppendLine("================================================");
            sb.AppendLine("            СПРАВКА");
            sb.AppendLine("   о принятии заявки на социальную выплату");
            sb.AppendLine("================================================");
            sb.AppendLine();

            // Информация о документе
            sb.AppendLine($"Номер документа: СП-{appInfo.ApplicationId}-{DateTime.Now:yyyyMMdd}");
            sb.AppendLine($"Дата формирования: {DateTime.Now:dd.MM.yyyy HH:mm}");
            sb.AppendLine();

            // Основные данные
            sb.AppendLine("--- ОСНОВНЫЕ ДАННЫЕ ---");
            sb.AppendLine($"Номер заявки: {appInfo.ApplicationId}");
            sb.AppendLine($"Заявитель: {appInfo.ApplicantName}");
            sb.AppendLine($"Тип выплаты: {appInfo.PaymentType}");
            sb.AppendLine($"Дата подачи: {appInfo.CreationDate:dd.MM.yyyy HH:mm}");
            sb.AppendLine($"Запрашиваемая сумма: {appInfo.RequestedAmount:F2} руб.");
            sb.AppendLine($"Утвержденная сумма: {appInfo.ApprovedAmount:F2} руб.");
            sb.AppendLine($"Дата принятия: {DateTime.Now:dd.MM.yyyy}");
            sb.AppendLine();

            // Информация о сотруднике
            sb.AppendLine("--- ОТВЕТСТВЕННЫЙ СОТРУДНИК ---");
            sb.AppendLine($"ФИО: {appInfo.EmployeeName}");
            sb.AppendLine($"Должность: {appInfo.EmployeePosition}");
            sb.AppendLine();

            // Дополнительная информация
            if (!string.IsNullOrEmpty(appInfo.ApplicantInn) && appInfo.ApplicantInn != "Не указано")
                sb.AppendLine($"ИНН заявителя: {appInfo.ApplicantInn}");

            if (!string.IsNullOrEmpty(appInfo.ApplicantPhone) && appInfo.ApplicantPhone != "Не указано")
                sb.AppendLine($"Телефон заявителя: {appInfo.ApplicantPhone}");

            sb.AppendLine();

            // Подтверждение
            sb.AppendLine("--- ПОДТВЕРЖДЕНИЕ ---");
            sb.AppendLine("Настоящая справка подтверждает, что заявка на получение");
            sb.AppendLine("социальной выплаты была рассмотрена и принята к исполнению.");
            sb.AppendLine();
            sb.AppendLine("Справка является официальным документом и может быть");
            sb.AppendLine("использована для получения указанной социальной выплаты");
            sb.AppendLine("в соответствии с установленным порядком.");
            sb.AppendLine();

            // Подпись
            sb.AppendLine("_________________________");
            sb.AppendLine(appInfo.EmployeeName);
            sb.AppendLine(appInfo.EmployeePosition);
            sb.AppendLine($"Дата: {DateTime.Now:dd.MM.yyyy}");
            sb.AppendLine();

            // Футер
            sb.AppendLine("================================================");
            sb.AppendLine($"Документ сформирован: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
            sb.AppendLine("Система: Интерактивный помощник для составления заявлений");

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        private bool SaveTxtToDatabase(int applicationId, byte[] txtData)
        {
            try
            {
                // Проверяем, есть ли уже сертификат
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
                    // Обновляем существующий
                    string updateQuery = @"
                        UPDATE certificate 
                        SET date_and_time_of_creation = @creation_date, 
                            document = @document  
                        WHERE application_id = @application_id";

                    var updateParams = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("@application_id", applicationId),
                        new NpgsqlParameter("@creation_date", DateTime.Now),
                        new NpgsqlParameter("@document", txtData)
                    };

                    return DatabaseHelper.ExecuteNonQuery(updateQuery, updateParams) > 0;
                }
                else
                {
                    // Вставляем новый
                    string insertQuery = @"
                        INSERT INTO certificate (application_id, date_and_time_of_creation, document) 
                        VALUES (@application_id::integer, @creation_date::date, @document::bytea)";

                    var insertParams = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("@application_id", applicationId),
                        new NpgsqlParameter("@creation_date", DateTime.Now),
                        new NpgsqlParameter("@document", txtData)
                    };

                    return DatabaseHelper.ExecuteNonQuery(insertQuery, insertParams) > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения сертификата в БД: {ex.Message}");
                return false;
            }
        }

        #endregion

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
                    LEFT JOIN employee e ON a.employee_id = e.employee_id 
                    LEFT JOIN employee_position ep ON e.employee_position_id = ep.employee_position_id 
                    WHERE a.application_id = @application_id";

                var parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@application_id", applicationId)
                };

                var dataTable = DatabaseHelper.ExecuteQuery(query, parameters);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];

                    string details = $"=== ДЕТАЛИ ЗАЯВКИ #{row["application_id"]} ===\n\n" +
                                   $"ID заявки: {row["application_id"]}\n" +
                                   $"ФИО заявителя: {row["applicant_fio"]}\n" +
                                   $"ИНН заявителя: {row["inn"]}\n" +
                                   $"Телефон заявителя: {row["phone_number"]}\n" +
                                   $"Тип выплаты: {row["type_name"]}\n" +
                                   $"Сумма: {Convert.ToDouble(row["amount"]):F2} руб.\n" +
                                   $"Дата подачи: {Convert.ToDateTime(row["date_of_creation"]):dd.MM.yyyy HH:mm}\n" +
                                   $"Статус: {row["status"]}\n" +
                                   $"Ответственный сотрудник: {row["employee_fio"]}\n" +
                                   $"Должность: {row["position"]}\n" +
                                   $"ID заявителя: {row["applicant_id"]}\n\n" +
                                   $"Для принятия или отклонения заявки используйте кнопки ниже";

                    applicationText.Text = details;
                }
                else
                {
                    applicationText.Text = $"Заявка с ID {applicationId} не найдена в базе данных.";
                    MessageBox.Show($"Заявка с ID {applicationId} не найдена в базе данных.", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                applicationText.Text = $"Ошибка загрузки деталей заявки: {ex.Message}";
                MessageBox.Show($"Ошибка загрузки деталей заявки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private double GetApplicationAmount(int applicationId)
        {
            try
            {
                string query = "SELECT amount FROM application WHERE application_id = @id";
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

        private ApplicationInfo GetApplicationInfoForCertificate(int applicationId)
        {
            try
            {
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
                    LEFT JOIN employee e ON a.employee_id = e.employee_id 
                    LEFT JOIN employee_position ep ON e.employee_position_id = ep.employee_position_id 
                    WHERE a.application_id = @application_id";

                var parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@application_id", applicationId)
                };

                var dataTable = DatabaseHelper.ExecuteQuery(query, parameters);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];

                    return new ApplicationInfo
                    {
                        ApplicationId = applicationId,
                        ApplicantName = row["applicant_fio"]?.ToString() ?? "Не указано",
                        PaymentType = row["type_name"]?.ToString() ?? "Не указано",
                        CreationDate = row["date_of_creation"] != DBNull.Value ? Convert.ToDateTime(row["date_of_creation"]) : DateTime.Now,
                        EmployeeName = row["employee_fio"]?.ToString() ?? "Не указано",
                        EmployeePosition = row["position"]?.ToString() ?? "Не указано",
                        BranchName = "Отделение социальных выплат",
                        ApplicantInn = row["inn"]?.ToString() ?? "Не указано",
                        ApplicantPhone = row["phone_number"]?.ToString() ?? "Не указано"
                    };
                }
                else
                {
                    MessageBox.Show($"Не найдены данные для заявки #{applicationId}", "Ошибка данных",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных для сертификата: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
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
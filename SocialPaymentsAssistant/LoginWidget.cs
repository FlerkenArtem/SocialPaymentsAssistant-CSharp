using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace SocialPaymentsAssistant
{
    public partial class LoginWidget : Form
    {
        public LoginWidget()
        {
            InitializeComponent();

            loginEdit.MaxLength = 50;
            passwordEdit.MaxLength = 20;
            passwordEdit.PasswordChar = '*';
        }

        private void LoginWidget_Load(object sender, EventArgs e)
        {
            
        }

        private void endLoginButton_Click(object sender, EventArgs e)
        {
            string username = loginEdit.Text.Trim();
            string password = passwordEdit.Text;

            if (string.IsNullOrEmpty(loginEdit.Text.Trim()) || string.IsNullOrEmpty(passwordEdit.Text))
            {
                MessageBox.Show("Логин и пароль не могут быть пустыми", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Вызов хранимой функции authenticate_user из БД
                var query = "SELECT authenticate_user(@username, @password)";
                var parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@username", username),
                    new NpgsqlParameter("@password", password)
                };

                var result = DatabaseHelper.ExecuteScalar(query, parameters);

                if (result == null || result == DBNull.Value)
                {
                    MessageBox.Show("Ошибка при выполнении запроса", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int roleId = Convert.ToInt32(result);

                // В зависимости от роли открываем соответствующее окно
                if (roleId == 1) // Заявитель
                {
                    // Получаем ID заявителя
                    var applicantQuery = @"
                        SELECT a.applicant_id 
                        FROM applicant a 
                        JOIN account ac ON a.account_id = ac.account_id 
                        WHERE ac.username = @login";

                    var applicantParams = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("@login", username)
                    };

                    var applicantResult = DatabaseHelper.ExecuteScalar(applicantQuery, applicantParams);

                    if (applicantResult != null && applicantResult != DBNull.Value)
                    {
                        int applicantId = Convert.ToInt32(applicantResult);
                        MainWindow mainWindow = new MainWindow(applicantId);
                        mainWindow.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось получить данные заявителя", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (roleId == 2) // Сотрудник
                {
                    // Получаем ID сотрудника
                    var employeeQuery = @"
                        SELECT e.employee_id 
                        FROM employee e 
                        JOIN account ac ON e.account_id = ac.account_id 
                        WHERE ac.username = @login";

                    var employeeParams = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("@login", username)
                    };

                    var employeeResult = DatabaseHelper.ExecuteScalar(employeeQuery, employeeParams);

                    if (employeeResult != null && employeeResult != DBNull.Value)
                    {
                        int employeeId = Convert.ToInt32(employeeResult);
                        EmpMainWindow empWindow = new EmpMainWindow(employeeId);
                        empWindow.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось получить данные сотрудника", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (roleId == 0) // Аутентификация не прошла
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Неизвестная роль пользователя", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка входа: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void startRegistrationButton_Click(object sender, EventArgs e)
        {
            // Переход к окну регистрации
            RegistrationWidget regWidget = new RegistrationWidget();
            regWidget.Show();
            this.Hide(); // Закрываем окно входа
        }

        private void passwordEdit_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
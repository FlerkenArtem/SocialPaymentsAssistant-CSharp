using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;

namespace SocialPaymentsAssistant
{
    public partial class ApplicantReg : Form
    {
        // Данные сканов документов
        private byte[] passportScanData;
        private byte[] snilsScanData;
        private byte[] policyScanData;
        private byte[] adoptionCertificateScanData;

        public ApplicantReg()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Настраиваем даты по умолчанию
            passportDateOfIssue.Value = DateTime.Now;
            birthDate.Value = DateTime.Now.AddYears(-30);
            openningDate.Value = DateTime.Now;

            // Загружаем данные из БД
            LoadRegions();
            LoadDistricts();
            LoadCities();
            LoadAreas();
            LoadStreets();
            LoadHouses();
            LoadFlats();
            LoadBanks();
            LoadPassportDepartments();

            // Настраиваем комбобокс банка для ручного ввода
            bankName.DropDownStyle = ComboBoxStyle.DropDown;

            // Инициализируем данные сканов как пустые
            passportScanData = null;
            snilsScanData = null;
            policyScanData = null;
            adoptionCertificateScanData = null;

            // Инициализация состояний
            OnNoPatronymicCheckToggled(noPatronymicCheck.Checked);
            OnNoActualAddressToggled(noActualAddress.Checked);
            OnNoAdoptationCertificateToggled(noAdoptationCertificate.Checked);
        }

        #region Загрузка данных из БД

        private void LoadRegions()
        {
            regionComboBoxRegAddress.Items.Clear();
            regionComboBoxActualAddress.Items.Clear();

            regionComboBoxRegAddress.Items.Add("Выберите регион");
            regionComboBoxActualAddress.Items.Add("Выберите регион");

            string query = "SELECT region_id, name_region FROM region ORDER BY name_region";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string name = row["name_region"].ToString();
                    regionComboBoxRegAddress.Items.Add(name);
                    regionComboBoxActualAddress.Items.Add(name);
                }
            }

            regionComboBoxRegAddress.SelectedIndex = 0;
            regionComboBoxActualAddress.SelectedIndex = 0;
        }

        private void LoadDistricts()
        {
            districtComboBoxRegAddress.Items.Clear();
            districtComboBoxActualAddress.Items.Clear();

            string query = "SELECT name_district FROM district ORDER BY name_district";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string name = row["name_district"].ToString();
                    districtComboBoxRegAddress.Items.Add(name);
                    districtComboBoxActualAddress.Items.Add(name);
                }
            }
        }

        private void LoadCities()
        {
            cityComboBoxRegAddress.Items.Clear();
            cityComboBoxActualAddress.Items.Clear();

            string query = "SELECT name_city FROM city ORDER BY name_city";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string name = row["name_city"].ToString();
                    cityComboBoxRegAddress.Items.Add(name);
                    cityComboBoxActualAddress.Items.Add(name);
                }
            }
        }

        private void LoadAreas()
        {
            areaComboBoxRegAddress.Items.Clear();
            areaComboBoxActualAddress.Items.Clear();

            string query = "SELECT name_area FROM area ORDER BY name_area";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string name = row["name_area"].ToString();
                    areaComboBoxRegAddress.Items.Add(name);
                    areaComboBoxActualAddress.Items.Add(name);
                }
            }
        }

        private void LoadStreets()
        {
            StreetComboBoxRegAddress.Items.Clear();
            StreetComboBoxActualAddress.Items.Clear();

            string query = "SELECT name_street FROM street ORDER BY name_street";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string name = row["name_street"].ToString();
                    StreetComboBoxRegAddress.Items.Add(name);
                    StreetComboBoxActualAddress.Items.Add(name);
                }
            }
        }

        private void LoadHouses()
        {
            houseComboBoxRegAddress.Items.Clear();
            houseComboBoxActualAddress.Items.Clear();

            string query = "SELECT name_house FROM house ORDER BY name_house";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string name = row["name_house"].ToString();
                    houseComboBoxRegAddress.Items.Add(name);
                    houseComboBoxActualAddress.Items.Add(name);
                }
            }
        }

        private void LoadFlats()
        {
            flatComboBoxRegAddress.Items.Clear();
            flatComboBoxActualAddress.Items.Clear();

            string query = "SELECT name_flat FROM flat ORDER BY name_flat";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string name = row["name_flat"].ToString();
                    flatComboBoxRegAddress.Items.Add(name);
                    flatComboBoxActualAddress.Items.Add(name);
                }
            }
        }

        private void LoadBanks()
        {
            bankName.Items.Clear();
            bankName.Items.Add("Выберите банк или введите вручную");

            string query = "SELECT name_of_the_bank FROM name_of_the_recipient_bank ORDER BY name_of_the_bank";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string name = row["name_of_the_bank"].ToString();
                    bankName.Items.Add(name);
                }
            }

            bankName.SelectedIndex = 0;
        }

        private void LoadPassportDepartments()
        {
            passportIssuingDepartment.Items.Clear();
            passportIssuingDepartment.Items.Add("Выберите отдел");

            string query = "SELECT issuing_department FROM passport_issuing_department ORDER BY issuing_department";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string department = row["issuing_department"].ToString();
                    passportIssuingDepartment.Items.Add(department);
                }
            }

            passportIssuingDepartment.SelectedIndex = 0;
        }

        #endregion

        #region Обработчики событий чекбоксов

        private void OnNoPatronymicCheckToggled(bool checkedState)
        {
            patronymic.Enabled = !checkedState;
            if (checkedState)
            {
                patronymic.Text = "";
            }
        }

        private void OnNoActualAddressToggled(bool checkedState)
        {
            EnableActualAddressFields(!checkedState);
        }

        private void OnNoAdoptationCertificateToggled(bool checkedState)
        {
            EnableAdoptionCertificateFields(!checkedState);
        }

        private void EnableActualAddressFields(bool enabled)
        {
            regionComboBoxActualAddress.Enabled = enabled;
            districtComboBoxActualAddress.Enabled = enabled;
            cityComboBoxActualAddress.Enabled = enabled;
            areaComboBoxActualAddress.Enabled = enabled;
            StreetComboBoxActualAddress.Enabled = enabled;
            houseComboBoxActualAddress.Enabled = enabled;
            flatComboBoxActualAddress.Enabled = enabled;
            indexEditActualAddress.Enabled = enabled;
            districtCheckActualAddress.Enabled = enabled;
            areaCheckActualAddress.Enabled = enabled;
            flatCheckActualAddress.Enabled = enabled;

            if (!enabled)
            {
                regionComboBoxActualAddress.SelectedIndex = 0;
                districtComboBoxActualAddress.Text = "";
                cityComboBoxActualAddress.Text = "";
                areaComboBoxActualAddress.Text = "";
                StreetComboBoxActualAddress.Text = "";
                houseComboBoxActualAddress.Text = "";
                flatComboBoxActualAddress.Text = "";
                indexEditActualAddress.Text = "";
            }
        }

        private void EnableAdoptionCertificateFields(bool enabled)
        {
            adopatationCertificateSeriesEdit.Enabled = enabled;
            adopatationCertificateNumberEdit.Enabled = enabled;
            adopatationCertificateAddScan.Enabled = enabled;

            if (!enabled)
            {
                adopatationCertificateSeriesEdit.Text = "";
                adopatationCertificateNumberEdit.Text = "";
                adoptionCertificateScanData = null;
            }
        }

        #endregion

        #region Обработчики загрузки сканов

        private byte[] LoadScanFile(string title)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = title;
                openFileDialog.Filter = "PNG Images (*.png)|*.png";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        byte[] data = File.ReadAllBytes(openFileDialog.FileName);

                        // Проверяем, что файл действительно PNG
                        if (data.Length > 8 &&
                            data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47 &&
                            data[4] == 0x0D && data[5] == 0x0A && data[6] == 0x1A && data[7] == 0x0A)
                        {
                            return data;
                        }
                        else
                        {
                            MessageBox.Show("Файл должен быть в формате PNG", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось открыть файл: " + ex.Message, "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                }
            }
            return null;
        }

        private void PassportScan_Click(object sender, EventArgs e)
        {
            byte[] scanData = LoadScanFile("Выберите скан паспорта (PNG)");
            if (scanData != null && scanData.Length > 0)
            {
                passportScanData = scanData;
                MessageBox.Show("Скан паспорта загружен", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void snilsAddScan_Click(object sender, EventArgs e)
        {
            byte[] scanData = LoadScanFile("Выберите скан СНИЛС (PNG)");
            if (scanData != null && scanData.Length > 0)
            {
                snilsScanData = scanData;
                MessageBox.Show("Скан СНИЛС загружен", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void policyAddScan_Click(object sender, EventArgs e)
        {
            byte[] scanData = LoadScanFile("Выберите скан полиса ОМС (PNG)");
            if (scanData != null && scanData.Length > 0)
            {
                policyScanData = scanData;
                MessageBox.Show("Скан полиса ОМС загружен", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void adopatationCertificateAddScan_Click(object sender, EventArgs e)
        {
            byte[] scanData = LoadScanFile("Выберите скан свидетельства об усыновлении (PNG)");
            if (scanData != null && scanData.Length > 0)
            {
                adoptionCertificateScanData = scanData;
                MessageBox.Show("Скан свидетельства об усыновлении загружен", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Валидация и регистрация

        private bool ValidateFields()
        {
            // 1. Данные для входа
            if (string.IsNullOrWhiteSpace(login.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(eMail.Text))
            {
                MessageBox.Show("Введите email", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!IsValidEmail(eMail.Text))
            {
                MessageBox.Show("Введите корректный email", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(password.Text))
            {
                MessageBox.Show("Введите пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (password.Text != password2.Text)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 2. Основные данные
            if (string.IsNullOrWhiteSpace(surname.Text))
            {
                MessageBox.Show("Введите фамилию", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(name.Text))
            {
                MessageBox.Show("Введите имя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(phone.Text))
            {
                MessageBox.Show("Введите номер телефона", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 3. Паспортные данные
            if (string.IsNullOrWhiteSpace(passportSeriesEdit.Text))
            {
                MessageBox.Show("Введите серию паспорта", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(passportNumberEdit.Text))
            {
                MessageBox.Show("Введите номер паспорта", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (passportIssuingDepartment.SelectedIndex == 0)
            {
                MessageBox.Show("Введите отдел, выдавший паспорт", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(passportDivisionCode.Text))
            {
                MessageBox.Show("Введите код подразделения", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 4. Адрес регистрации
            if (regionComboBoxRegAddress.SelectedIndex == 0)
            {
                MessageBox.Show("Выберите регион регистрации", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(cityComboBoxRegAddress.Text))
            {
                MessageBox.Show("Введите город регистрации", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(StreetComboBoxRegAddress.Text))
            {
                MessageBox.Show("Введите улицу регистрации", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(houseComboBoxRegAddress.Text))
            {
                MessageBox.Show("Введите дом регистрации", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 5. Банковские данные
            if (string.IsNullOrWhiteSpace(bankName.Text) ||
                bankName.Text == "Выберите банк или введите вручную")
            {
                MessageBox.Show("Введите название банка", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(surnameBank.Text))
            {
                MessageBox.Show("Введите фамилию получателя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(nameBank.Text))
            {
                MessageBox.Show("Введите имя получателя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(bankPhone.Text))
            {
                MessageBox.Show("Введите номер телефона банка получателя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void applicantRegOK_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }

            try
            {
                // Подготовка параметров для хранимой процедуры
                var parameters = PrepareParameters();

                // Вызов хранимой процедуры
                string procedureCall = BuildProcedureCall();
                int result = DatabaseHelper.ExecuteNonQuery(procedureCall, parameters);

                if (result >= 0)
                {
                    // Получаем ID нового заявителя
                    string query = @"
                        SELECT a.applicant_id FROM applicant a 
                        JOIN account ac ON a.account_id = ac.account_id 
                        WHERE ac.username = @login";

                    var idParams = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("@login", login.Text)
                    };

                    object idResult = DatabaseHelper.ExecuteScalar(query, idParams);

                    if (idResult != null && int.TryParse(idResult.ToString(), out int applicantId))
                    {
                        MainWindow mainWindow = new MainWindow(applicantId);
                        this.Close();
                        mainWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось получить ID заявителя", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось зарегистрировать заявителя", "Ошибка регистрации",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string BuildProcedureCall()
        {
            return @"
        CALL new_applicant(
            CAST(@p_username AS VARCHAR(50)), 
            CAST(@p_password AS VARCHAR(20)), 
            CAST(@p_email AS VARCHAR(345)),
            CAST(@p_surname AS VARCHAR(140)), 
            CAST(@p_name AS VARCHAR(2250)), 
            CAST(@p_patronymic AS VARCHAR(2255)),
            CAST(@p_inn AS CHAR(12)), 
            CAST(@p_pension_coefficient AS DECIMAL(4,1)), 
            CAST(@p_phone_number AS CHAR(18)),
            CAST(@p_passport_series AS CHAR(4)), 
            CAST(@p_passport_number AS CHAR(6)), 
            CAST(@p_passport_issued_by AS VARCHAR(129)),
            CAST(@p_passport_issue_date AS DATE), 
            CAST(@p_passport_division_code AS CHAR(7)), 
            CAST(@p_passport_scan AS BYTEA),
            CAST(@p_birth_date AS DATE),
            CAST(@p_snils_number AS CHAR(14)), 
            CAST(@p_snils_scan AS BYTEA),
            CAST(@p_policy_number AS CHAR(16)), 
            CAST(@p_policy_scan AS BYTEA),
            CAST(@p_adoption_certificate_series AS VARCHAR(6)), 
            CAST(@p_adoption_certificate_number AS CHAR(6)), 
            CAST(@p_adoption_certificate_scan AS BYTEA),
            CAST(@p_reg_region AS VARCHAR(40)), 
            CAST(@p_reg_district AS VARCHAR(31)), 
            CAST(@p_reg_city AS VARCHAR(68)),
            CAST(@p_reg_area AS VARCHAR(58)), 
            CAST(@p_reg_street AS VARCHAR(145)), 
            CAST(@p_reg_house AS VARCHAR(35)),
            CAST(@p_reg_flat AS VARCHAR(5)), 
            CAST(@p_reg_postal_code AS CHAR(6)),
            CAST(@p_act_region AS VARCHAR(40)), 
            CAST(@p_act_district AS VARCHAR(31)), 
            CAST(@p_act_city AS VARCHAR(68)),
            CAST(@p_act_area AS VARCHAR(58)), 
            CAST(@p_act_street AS VARCHAR(145)), 
            CAST(@p_act_house AS VARCHAR(35)),
            CAST(@p_act_flat AS VARCHAR(5)), 
            CAST(@p_act_postal_code AS CHAR(6)),
            CAST(@p_recipient_surname AS VARCHAR(140)), 
            CAST(@p_recipient_name AS VARCHAR(2250)), 
            CAST(@p_recipient_patronymic AS VARCHAR(2255)),
            CAST(@p_recipient_account_number AS CHAR(25)), 
            CAST(@p_account_opening_date AS DATE),
            CAST(@p_bank_name AS VARCHAR(100)), 
            CAST(@p_bank_bic AS CHAR(9)), 
            CAST(@p_bank_inn AS CHAR(10)),
            CAST(@p_bank_kpp AS CHAR(9)), 
            CAST(@p_bank_correspondent_account AS CHAR(25)), 
            CAST(@p_bank_phone AS CHAR(18))
        )";
        }

        private NpgsqlParameter[] PrepareParameters()
        {
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();

            // 1. Данные для аккаунта
            parameters.Add(new NpgsqlParameter("@p_username", login.Text));
            parameters.Add(new NpgsqlParameter("@p_password", password.Text));
            parameters.Add(new NpgsqlParameter("@p_email", eMail.Text));

            // 2. ФИО и основные данные
            parameters.Add(new NpgsqlParameter("@p_surname", surname.Text));
            parameters.Add(new NpgsqlParameter("@p_name", name.Text));
            parameters.Add(new NpgsqlParameter("@p_patronymic",
                noPatronymicCheck.Checked ? string.Empty : patronymic.Text));

            // ИНН (12 символов)
            parameters.Add(new NpgsqlParameter("@p_inn",
                string.IsNullOrEmpty(inn.Text) ? string.Empty : inn.Text.PadRight(12).Substring(0, 12)));

            parameters.Add(new NpgsqlParameter("@p_pension_coefficient", (decimal)pension.Value));

            // Номер телефона (18 символов)
            parameters.Add(new NpgsqlParameter("@p_phone_number",
                phone.Text.PadRight(18).Substring(0, 18)));

            // 3. Паспортные данные
            // Серия паспорта (ровно 4 символа)
            string passportSeries = passportSeriesEdit.Text;
            if (passportSeries.Length < 4)
                passportSeries = passportSeries.PadRight(4);
            else if (passportSeries.Length > 4)
                passportSeries = passportSeries.Substring(0, 4);
            parameters.Add(new NpgsqlParameter("@p_passport_series", passportSeries));

            // Номер паспорта (ровно 6 символов)
            string passportNumber = passportNumberEdit.Text;
            if (passportNumber.Length < 6)
                passportNumber = passportNumber.PadRight(6);
            else if (passportNumber.Length > 6)
                passportNumber = passportNumber.Substring(0, 6);
            parameters.Add(new NpgsqlParameter("@p_passport_number", passportNumber));

            parameters.Add(new NpgsqlParameter("@p_passport_issued_by", passportIssuingDepartment.Text));
            parameters.Add(new NpgsqlParameter("@p_passport_issue_date", passportDateOfIssue.Value.Date));

            // Код подразделения (ровно 7 символов)
            string divisionCode = passportDivisionCode.Text;
            if (divisionCode.Length < 7)
                divisionCode = divisionCode.PadRight(7);
            else if (divisionCode.Length > 7)
                divisionCode = divisionCode.Substring(0, 7);
            parameters.Add(new NpgsqlParameter("@p_passport_division_code", divisionCode));

            // Скан паспорта
            NpgsqlParameter passportScanParam = new NpgsqlParameter("@p_passport_scan", NpgsqlDbType.Bytea);
            passportScanParam.Value = passportScanData ?? (object)DBNull.Value;
            parameters.Add(passportScanParam);

            parameters.Add(new NpgsqlParameter("@p_birth_date", birthDate.Value.Date));

            // 4. Данные СНИЛС (14 символов)
            string snilsNumber = snilsNumberEdit.Text;
            if (snilsNumber.Length < 14)
                snilsNumber = snilsNumber.PadRight(14);
            else if (snilsNumber.Length > 14)
                snilsNumber = snilsNumber.Substring(0, 14);
            parameters.Add(new NpgsqlParameter("@p_snils_number", snilsNumber));

            NpgsqlParameter snilsScanParam = new NpgsqlParameter("@p_snils_scan", NpgsqlDbType.Bytea);
            snilsScanParam.Value = snilsScanData ?? (object)DBNull.Value;
            parameters.Add(snilsScanParam);

            // 5. Данные полиса (16 символов)
            string policyNumber = policyNumberEdit.Text;
            if (policyNumber.Length < 16)
                policyNumber = policyNumber.PadRight(16);
            else if (policyNumber.Length > 16)
                policyNumber = policyNumber.Substring(0, 16);
            parameters.Add(new NpgsqlParameter("@p_policy_number", policyNumber));

            NpgsqlParameter policyScanParam = new NpgsqlParameter("@p_policy_scan", NpgsqlDbType.Bytea);
            policyScanParam.Value = policyScanData ?? (object)DBNull.Value;
            parameters.Add(policyScanParam);

            // 6. Данные свидетельства об усыновлении
            string adoptionSeries = noAdoptationCertificate.Checked ? string.Empty : adopatationCertificateSeriesEdit.Text;
            string adoptionNumber = noAdoptationCertificate.Checked ? string.Empty : adopatationCertificateNumberEdit.Text;

            if (adoptionSeries.Length > 6)
                adoptionSeries = adoptionSeries.Substring(0, 6);
            parameters.Add(new NpgsqlParameter("@p_adoption_certificate_series", adoptionSeries));

            if (adoptionNumber.Length < 6)
                adoptionNumber = adoptionNumber.PadRight(6);
            else if (adoptionNumber.Length > 6)
                adoptionNumber = adoptionNumber.Substring(0, 6);
            parameters.Add(new NpgsqlParameter("@p_adoption_certificate_number", adoptionNumber));

            NpgsqlParameter adoptionScanParam = new NpgsqlParameter("@p_adoption_certificate_scan", NpgsqlDbType.Bytea);
            adoptionScanParam.Value = adoptionCertificateScanData ?? (object)DBNull.Value;
            parameters.Add(adoptionScanParam);

            // 7. Адрес регистрации
            parameters.Add(new NpgsqlParameter("@p_reg_region", regionComboBoxRegAddress.Text));
            parameters.Add(new NpgsqlParameter("@p_reg_district",
                districtCheckRegAddress.Checked ? string.Empty : districtComboBoxRegAddress.Text));
            parameters.Add(new NpgsqlParameter("@p_reg_city", cityComboBoxRegAddress.Text));
            parameters.Add(new NpgsqlParameter("@p_reg_area",
                areaCheckRegAddress.Checked ? string.Empty : areaComboBoxRegAddress.Text));
            parameters.Add(new NpgsqlParameter("@p_reg_street", StreetComboBoxRegAddress.Text));
            parameters.Add(new NpgsqlParameter("@p_reg_house", houseComboBoxRegAddress.Text));
            parameters.Add(new NpgsqlParameter("@p_reg_flat",
                flatCheckRegAddress.Checked ? string.Empty : flatComboBoxRegAddress.Text));

            // Почтовый индекс (ровно 6 символов)
            string regPostalCode = indexEditRegAddress.Text;
            if (regPostalCode.Length < 6)
                regPostalCode = regPostalCode.PadRight(6);
            else if (regPostalCode.Length > 6)
                regPostalCode = regPostalCode.Substring(0, 6);
            parameters.Add(new NpgsqlParameter("@p_reg_postal_code", regPostalCode));

            // 8. Адрес фактического проживания
            string actRegion = noActualAddress.Checked ? regionComboBoxRegAddress.Text :
                regionComboBoxActualAddress.Text;
            string actDistrict = noActualAddress.Checked ?
                (districtCheckRegAddress.Checked ? string.Empty : districtComboBoxRegAddress.Text) :
                (districtCheckActualAddress.Checked ? string.Empty : districtComboBoxActualAddress.Text);
            string actCity = noActualAddress.Checked ? cityComboBoxRegAddress.Text :
                cityComboBoxActualAddress.Text;
            string actArea = noActualAddress.Checked ?
                (areaCheckRegAddress.Checked ? string.Empty : areaComboBoxRegAddress.Text) :
                (areaCheckActualAddress.Checked ? string.Empty : areaComboBoxActualAddress.Text);
            string actStreet = noActualAddress.Checked ? StreetComboBoxRegAddress.Text :
                StreetComboBoxActualAddress.Text;
            string actHouse = noActualAddress.Checked ? houseComboBoxRegAddress.Text :
                houseComboBoxActualAddress.Text;
            string actFlat = noActualAddress.Checked ?
                (flatCheckRegAddress.Checked ? string.Empty : flatComboBoxRegAddress.Text) :
                (flatCheckActualAddress.Checked ? string.Empty : flatComboBoxActualAddress.Text);
            string actPostalCode = noActualAddress.Checked ? indexEditRegAddress.Text :
                indexEditActualAddress.Text;

            if (actPostalCode.Length < 6)
                actPostalCode = actPostalCode.PadRight(6);
            else if (actPostalCode.Length > 6)
                actPostalCode = actPostalCode.Substring(0, 6);

            parameters.Add(new NpgsqlParameter("@p_act_region", actRegion));
            parameters.Add(new NpgsqlParameter("@p_act_district", actDistrict));
            parameters.Add(new NpgsqlParameter("@p_act_city", actCity));
            parameters.Add(new NpgsqlParameter("@p_act_area", actArea));
            parameters.Add(new NpgsqlParameter("@p_act_street", actStreet));
            parameters.Add(new NpgsqlParameter("@p_act_house", actHouse));
            parameters.Add(new NpgsqlParameter("@p_act_flat", actFlat));
            parameters.Add(new NpgsqlParameter("@p_act_postal_code", actPostalCode));

            // 9. Данные банковского счета
            parameters.Add(new NpgsqlParameter("@p_recipient_surname", surnameBank.Text));
            parameters.Add(new NpgsqlParameter("@p_recipient_name", nameBank.Text));
            parameters.Add(new NpgsqlParameter("@p_recipient_patronymic",
                noPatronymicCheckBank.Checked ? string.Empty : patronymicBank.Text));

            // Номер счета получателя (25 символов)
            string recipientAccount = recepientsAccount.Text;
            if (recipientAccount.Length < 25)
                recipientAccount = recipientAccount.PadRight(25);
            else if (recipientAccount.Length > 25)
                recipientAccount = recipientAccount.Substring(0, 25);
            parameters.Add(new NpgsqlParameter("@p_recipient_account_number", recipientAccount));

            parameters.Add(new NpgsqlParameter("@p_account_opening_date", openningDate.Value.Date));

            // 10. Данные банка
            parameters.Add(new NpgsqlParameter("@p_bank_name", bankName.Text));

            // БИК (9 символов)
            string bik = bikEdit.Text;
            if (bik.Length < 9)
                bik = bik.PadRight(9);
            else if (bik.Length > 9)
                bik = bik.Substring(0, 9);
            parameters.Add(new NpgsqlParameter("@p_bank_bic", bik));

            // ИНН банка (10 символов)
            string bankInn = innEdit.Text;
            if (bankInn.Length < 10)
                bankInn = bankInn.PadRight(10);
            else if (bankInn.Length > 10)
                bankInn = bankInn.Substring(0, 10);
            parameters.Add(new NpgsqlParameter("@p_bank_inn", bankInn));

            // КПП (9 символов)
            string kpp = kppEdit.Text;
            if (kpp.Length < 9)
                kpp = kpp.PadRight(9);
            else if (kpp.Length > 9)
                kpp = kpp.Substring(0, 9);
            parameters.Add(new NpgsqlParameter("@p_bank_kpp", kpp));

            // Корреспондентский счет (25 символов)
            string corrAccount = correspondentsAccountEdit.Text;
            if (corrAccount.Length > 25)
                corrAccount = corrAccount.Substring(0, 25);
            parameters.Add(new NpgsqlParameter("@p_bank_correspondent_account", corrAccount));

            // Телефон банка (18 символов)
            string bankPhoneNumber = bankPhone.Text;
            if (bankPhoneNumber.Length < 18)
                bankPhoneNumber = bankPhoneNumber.PadRight(18);
            else if (bankPhoneNumber.Length > 18)
                bankPhoneNumber = bankPhoneNumber.Substring(0, 18);
            parameters.Add(new NpgsqlParameter("@p_bank_phone", bankPhoneNumber));

            return parameters.ToArray();
        }

        #endregion

        #region Обработчики событий UI

        private void noPatronymicCheck_CheckedChanged(object sender, EventArgs e)
        {
            OnNoPatronymicCheckToggled(noPatronymicCheck.Checked);
        }

        private void noActualAddress_CheckedChanged(object sender, EventArgs e)
        {
            OnNoActualAddressToggled(noActualAddress.Checked);
        }

        private void noAdoptationCertificate_CheckedChanged(object sender, EventArgs e)
        {
            OnNoAdoptationCertificateToggled(noAdoptationCertificate.Checked);
        }

        // Остальные обработчики событий (для заполнения пустых методов в Designer.cs)
        private void button1_Click(object sender, EventArgs e)
        {
            // Это обработчик для кнопки applicantRegOK
            applicantRegOK_Click(sender, e);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            // Это обработчик для noActualAddress
            noActualAddress_CheckedChanged(sender, e);
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            // Это обработчик для noAdoptationCertificate
            noAdoptationCertificate_CheckedChanged(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Общий обработчик для кнопок сканирования
            if (sender == snilsAddScan)
                snilsAddScan_Click(sender, e);
            else if (sender == adopatationCertificateAddScan)
                adopatationCertificateAddScan_Click(sender, e);
            else if (sender == policyAddScan)
                policyAddScan_Click(sender, e);
        }

        #endregion
        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click_1(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void groupBox10_Enter(object sender, EventArgs e)
        {

        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void label52_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void inn_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
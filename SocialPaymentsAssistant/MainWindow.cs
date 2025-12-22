using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SocialPaymentsAssistant.Models;
using Npgsql;

namespace SocialPaymentsAssistant
{
    public partial class MainWindow : Form
    {
        private int _applicantId;
        private MyApplicationsModel _myApplicationsModel;
        private AcceptedApplicationsModel _acceptedApplicationsModel;
        private DataTable _myApplicationsDataTable;
        private DataView _myApplicationsDataView;
        private DataTable _acceptedApplicationsDataTable;
        private DataView _acceptedApplicationsDataView;
        private Dictionary<string, string> _paymentTypes = new Dictionary<string, string>();
        private Dictionary<string, string> _statuses = new Dictionary<string, string>();

        public MainWindow(int applicantId)
        {
            InitializeComponent();
            _applicantId = applicantId;

            InitializeData();
            SetupDataGridViews();
            LoadFilters();
            SetupEventHandlers();
            RefreshData();
        }

        private void InitializeData()
        {
            _myApplicationsModel = new MyApplicationsModel(_applicantId);
            _acceptedApplicationsModel = new AcceptedApplicationsModel(_applicantId);

            _myApplicationsDataTable = ConvertMyApplicationsModelToDataTable();
            _myApplicationsDataView = new DataView(_myApplicationsDataTable);

            _acceptedApplicationsDataTable = ConvertAcceptedApplicationsModelToDataTable();
            _acceptedApplicationsDataView = new DataView(_acceptedApplicationsDataTable);
        }

        private void SetupDataGridViews()
        {
            // Настройка таблицы "Мои заявки"
            myApplicationsTableView.DataSource = _myApplicationsDataView;
            myApplicationsTableView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            myApplicationsTableView.MultiSelect = false;
            myApplicationsTableView.ReadOnly = true;
            myApplicationsTableView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            myApplicationsTableView.RowHeadersVisible = false;

            // Настройка таблицы "Принятые заявки"
            acceptedApplicationsTableView.DataSource = _acceptedApplicationsDataView;
            acceptedApplicationsTableView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            acceptedApplicationsTableView.MultiSelect = false;
            acceptedApplicationsTableView.ReadOnly = true;
            acceptedApplicationsTableView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            acceptedApplicationsTableView.RowHeadersVisible = false;

            // Скрываем колонки с ID
            if (myApplicationsTableView.Columns.Contains("ID"))
                myApplicationsTableView.Columns["ID"].Visible = false;

            if (acceptedApplicationsTableView.Columns.Contains("ID"))
                acceptedApplicationsTableView.Columns["ID"].Visible = false;
        }

        private DataTable ConvertMyApplicationsModelToDataTable()
        {
            DataTable dataTable = new DataTable();

            // Создаем колонки
            for (int i = 0; i < _myApplicationsModel.ColumnCount; i++)
            {
                dataTable.Columns.Add(_myApplicationsModel.GetColumnName(i));
            }

            // Заполняем данные
            for (int row = 0; row < _myApplicationsModel.RowCount; row++)
            {
                DataRow dataRow = dataTable.NewRow();
                for (int col = 0; col < _myApplicationsModel.ColumnCount; col++)
                {
                    var value = _myApplicationsModel.GetData(row, col);
                    dataRow[col] = value ?? DBNull.Value;
                }
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        private DataTable ConvertAcceptedApplicationsModelToDataTable()
        {
            DataTable dataTable = new DataTable();

            // Создаем колонки
            for (int i = 0; i < _acceptedApplicationsModel.ColumnCount; i++)
            {
                dataTable.Columns.Add(_acceptedApplicationsModel.GetColumnName(i));
            }

            // Добавляем скрытые колонки для данных документа
            dataTable.Columns.Add("CertificateData", typeof(byte[]));
            dataTable.Columns.Add("CertificateFileName", typeof(string));

            // Заполняем данные
            for (int row = 0; row < _acceptedApplicationsModel.RowCount; row++)
            {
                DataRow dataRow = dataTable.NewRow();

                // Основные колонки
                for (int col = 0; col < _acceptedApplicationsModel.ColumnCount; col++)
                {
                    var value = _acceptedApplicationsModel.GetData(row, col);
                    dataRow[col] = value ?? DBNull.Value;
                }

                // Дополнительные данные (UserRole)
                var certificateData = _acceptedApplicationsModel.GetUserRoleData(row, 4);
                var fileName = _acceptedApplicationsModel.GetUserRoleData(row, 5);

                dataRow["CertificateData"] = certificateData as byte[];
                dataRow["CertificateFileName"] = fileName ?? string.Empty;

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        private void LoadFilters()
        {
            // Загрузка типов выплат
            LoadPaymentTypes();

            // Загрузка статусов
            LoadStatuses();

            // Установка начальных значений дат
            myApplicationDateEdit.Value = new DateTime(2000, 1, 1);
            acceptDateEdit.Value = DateTime.Today;
        }

        private void LoadPaymentTypes()
        {
            myApplicationTypeEdit.Items.Clear();
            paymentTypeComboBox.Items.Clear();

            myApplicationTypeEdit.Items.Add("Все");
            paymentTypeComboBox.Items.Add("Все");

            try
            {
                string query = "SELECT type_name FROM type_of_social_payment ORDER BY type_name";
                var dataTable = DatabaseHelper.ExecuteQuery(query);

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string typeName = row["type_name"].ToString();
                        myApplicationTypeEdit.Items.Add(typeName);
                        paymentTypeComboBox.Items.Add(typeName);
                    }
                }

                myApplicationTypeEdit.SelectedIndex = 0;
                paymentTypeComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки типов выплат: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStatuses()
        {
            myApplicationsStatusComboBox.Items.Clear();
            myApplicationsStatusComboBox.Items.Add("Все");

            try
            {
                string query = "SELECT status FROM application_status ORDER BY status";
                var dataTable = DatabaseHelper.ExecuteQuery(query);

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        myApplicationsStatusComboBox.Items.Add(row["status"].ToString());
                    }
                }

                myApplicationsStatusComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки статусов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupEventHandlers()
        {
            // Обработчики кнопок фильтрации
            myApplicationFilterOK.Click += MyApplicationFilterOK_Click;
            acceptedApplicationFilterOK.Click += AcceptedApplicationFilterOK_Click;

            // Обработчики кнопок документов
            showApplicationDoc.Click += ShowApplicationDoc_Click;
            saveApplicationDoc.Click += SaveApplicationDoc_Click;

            // Обработчики меню
            newApplicationAct.Click += NewApplicationAct_Click;
            myApplicationsAct.Click += MyApplicationsAct_Click;
            acceptedApplicationsAct.Click += AcceptedApplicationsAct_Click;
            logoutAct.Click += LogoutAct_Click;
            fullscreenAct.Click += FullscreenAct_Click;
            exitAct.Click += ExitAct_Click;
            branchOfficesAct.Click += BranchOfficesAct_Click;
            aboutAct.Click += AboutAct_Click;
        }

        private void RefreshData()
        {
            _myApplicationsModel.RefreshData();
            _acceptedApplicationsModel.RefreshData();

            _myApplicationsDataTable.Clear();
            _acceptedApplicationsDataTable.Clear();

            // Обновляем данные для "Мои заявки"
            for (int row = 0; row < _myApplicationsModel.RowCount; row++)
            {
                DataRow dataRow = _myApplicationsDataTable.NewRow();
                for (int col = 0; col < _myApplicationsModel.ColumnCount; col++)
                {
                    var value = _myApplicationsModel.GetData(row, col);
                    dataRow[col] = value ?? DBNull.Value;
                }
                _myApplicationsDataTable.Rows.Add(dataRow);
            }

            // Обновляем данные для "Принятые заявки"
            for (int row = 0; row < _acceptedApplicationsModel.RowCount; row++)
            {
                DataRow dataRow = _acceptedApplicationsDataTable.NewRow();

                // Основные колонки
                for (int col = 0; col < _acceptedApplicationsModel.ColumnCount; col++)
                {
                    var value = _acceptedApplicationsModel.GetData(row, col);
                    dataRow[col] = value ?? DBNull.Value;
                }

                // Дополнительные данные
                var certificateData = _acceptedApplicationsModel.GetUserRoleData(row, 4);
                var fileName = _acceptedApplicationsModel.GetUserRoleData(row, 5);

                dataRow["CertificateData"] = certificateData as byte[];
                dataRow["CertificateFileName"] = fileName ?? string.Empty;

                _acceptedApplicationsDataTable.Rows.Add(dataRow);
            }

            // Сбрасываем фильтры после обновления данных
            _myApplicationsDataView.RowFilter = string.Empty;
            _acceptedApplicationsDataView.RowFilter = string.Empty;
        }

        #region Обработчики событий фильтрации

        private void MyApplicationFilterOK_Click(object sender, EventArgs e)
        {
            string filter = string.Empty;
            List<string> filterParts = new List<string>();

            // Фильтр по типу выплаты
            if (myApplicationTypeEdit.SelectedItem?.ToString() != "Все" &&
                !string.IsNullOrEmpty(myApplicationTypeEdit.SelectedItem?.ToString()))
            {
                filterParts.Add($"Тип выплаты = '{myApplicationTypeEdit.SelectedItem}'");
            }

            // Фильтр по статусу
            if (myApplicationsStatusComboBox.SelectedItem?.ToString() != "Все" &&
                !string.IsNullOrEmpty(myApplicationsStatusComboBox.SelectedItem?.ToString()))
            {
                filterParts.Add($"Статус = '{myApplicationsStatusComboBox.SelectedItem}'");
            }

            // Фильтр по дате
            if (myApplicationDateEdit.Value != new DateTime(2000, 1, 1))
            {
                filterParts.Add($"Дата подачи = '{myApplicationDateEdit.Value:dd.MM.yyyy}'");
            }

            if (filterParts.Count > 0)
            {
                filter = string.Join(" AND ", filterParts);
            }

            _myApplicationsDataView.RowFilter = filter;
        }

        private void AcceptedApplicationFilterOK_Click(object sender, EventArgs e)
        {
            string filter = string.Empty;
            List<string> filterParts = new List<string>();

            // Фильтр по типу выплаты
            if (paymentTypeComboBox.SelectedItem?.ToString() != "Все" &&
                !string.IsNullOrEmpty(paymentTypeComboBox.SelectedItem?.ToString()))
            {
                filterParts.Add($"Тип выплаты = '{paymentTypeComboBox.SelectedItem}'");
            }

            // Фильтр по дате принятия
            if (acceptDateEdit.Value != new DateTime(2000, 1, 1))
            {
                filterParts.Add($"Дата принятия = '{acceptDateEdit.Value:dd.MM.yyyy}'");
            }

            if (filterParts.Count > 0)
            {
                filter = string.Join(" AND ", filterParts);
            }

            _acceptedApplicationsDataView.RowFilter = filter;
        }

        #endregion

        #region Обработчики работы с документами

        private void ShowApplicationDoc_Click(object sender, EventArgs e)
        {
            if (acceptedApplicationsTableView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заявку для просмотра", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rowIndex = acceptedApplicationsTableView.SelectedRows[0].Index;

            // Получаем данные через DataRowView
            DataRowView rowView = _acceptedApplicationsDataView[rowIndex];

            // Проверяем наличие данных документа
            if (rowView["CertificateData"] == DBNull.Value ||
                rowView["CertificateData"] == null)
            {
                MessageBox.Show("Документ отсутствует для выбранной заявки", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            byte[] documentData = (byte[])rowView["CertificateData"];

            // Создаем временный файл для просмотра
            string tempFilePath = Path.Combine(Path.GetTempPath(),
                $"Справка_{rowView["ID"]}_{DateTime.Now:ddMMyyyyHHmmss}.pdf");

            try
            {
                File.WriteAllBytes(tempFilePath, documentData);

                // Открываем файл стандартным способом
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = tempFilePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть документ: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveApplicationDoc_Click(object sender, EventArgs e)
        {
            if (acceptedApplicationsTableView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заявку для сохранения", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rowIndex = acceptedApplicationsTableView.SelectedRows[0].Index;
            DataRowView rowView = _acceptedApplicationsDataView[rowIndex];

            if (rowView["CertificateData"] == DBNull.Value ||
                rowView["CertificateData"] == null)
            {
                MessageBox.Show("Документ отсутствует для выбранной заявки", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            byte[] documentData = (byte[])rowView["CertificateData"];

            // Предлагаем выбрать место сохранения
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PDF документы (*.pdf)|*.pdf|Все файлы (*.*)|*.*";
                saveDialog.FileName = $"Справка_{rowView["ID"]}_{DateTime.Now:ddMMyyyy}.pdf";
                saveDialog.DefaultExt = ".pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllBytes(saveDialog.FileName, documentData);
                        MessageBox.Show("Документ успешно сохранен", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не удалось сохранить документ: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        #region Обработчики меню

        private void NewApplicationAct_Click(object sender, EventArgs e)
        {
            NewApplication newAppForm = new NewApplication();
            newAppForm.SetApplicantId(_applicantId);

            if (newAppForm.ShowDialog() == DialogResult.OK)
            {
                RefreshData();
            }
        }

        private void MyApplicationsAct_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = myApplicationsTab;
        }

        private void AcceptedApplicationsAct_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = acceptedApplications;
        }

        private void LogoutAct_Click(object sender, EventArgs e)
        {
            LoginWidget loginForm = new LoginWidget();
            loginForm.Show();
            this.Close();
        }

        private void FullscreenAct_Click(object sender, EventArgs e)
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

        private void ExitAct_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BranchOfficesAct_Click(object sender, EventArgs e)
        {
            BranchOfficesList branchForm = new BranchOfficesList();
            branchForm.ShowDialog();
        }

        private void AboutAct_Click(object sender, EventArgs e)
        {
            About aboutForm = new About();
            aboutForm.ShowDialog();
        }

        #endregion

        #region Существующие методы (оставляем как есть)

        private void button1_Click(object sender, EventArgs e)
        {
            // Существующий метод - оставляем пустым
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Существующий метод - оставляем пустым
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Существующий метод - оставляем пустым
        }

        #endregion
    }
}
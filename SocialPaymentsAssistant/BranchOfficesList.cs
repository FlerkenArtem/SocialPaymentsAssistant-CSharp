using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SocialPaymentsAssistant.Models;

namespace SocialPaymentsAssistant
{
    public partial class BranchOfficesList : Form
    {
        private BranchOfficesListModel _model;
        private DataView _dataView;
        private string _cityFilter = string.Empty;
        private string _nameFilter = string.Empty;

        public BranchOfficesList()
        {
            InitializeComponent();

            // Настройка обработчиков событий
            acceptBranchOfficeFilterButton.Click += AcceptBranchOfficeFilterButton_Click;
            branchOfficeClose.Click += BranchOfficeClose_Click;
            Load += BranchOfficesList_Load;

            // Настройка DataGridView
            SetupDataGridView();

            // Загрузка данных
            LoadData();
            LoadCitiesToComboBox();
        }

        private void SetupDataGridView()
        {
            branchOfficesTableView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            branchOfficesTableView.MultiSelect = false;
            branchOfficesTableView.ReadOnly = true;
            branchOfficesTableView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void LoadData()
        {
            try
            {
                _model = new BranchOfficesListModel();
                UpdateDataView();
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDataView()
        {
            // Создаем DataTable из модели
            var dataTable = new DataTable();

            // Добавляем колонки
            for (int i = 0; i < _model.ColumnCount; i++)
            {
                dataTable.Columns.Add(_model.GetColumnName(i));
            }

            // Добавляем строки
            for (int row = 0; row < _model.RowCount; row++)
            {
                var dataRow = dataTable.NewRow();
                for (int col = 0; col < _model.ColumnCount; col++)
                {
                    var value = _model.GetData(row, col);
                    dataRow[col] = value ?? DBNull.Value;
                }
                dataTable.Rows.Add(dataRow);
            }

            _dataView = new DataView(dataTable);
            branchOfficesTableView.DataSource = _dataView;

            // Скрываем ID колонку
            if (branchOfficesTableView.Columns.Contains("ID") && branchOfficesTableView.Columns["ID"] != null)
            {
                branchOfficesTableView.Columns["ID"].Visible = false;
            }
        }

        private void LoadCitiesToComboBox()
        {
            try
            {
                cityComboBox.Items.Clear();
                cityComboBox.Items.Add("Все города");

                // Загружаем города из модели
                if (_model != null)
                {
                    var cities = new System.Collections.Generic.HashSet<string>();
                    for (int i = 0; i < _model.RowCount; i++)
                    {
                        var city = _model.GetData(i, 2)?.ToString();
                        if (!string.IsNullOrEmpty(city) && !cities.Contains(city))
                        {
                            cities.Add(city);
                            cityComboBox.Items.Add(city);
                        }
                    }
                }

                cityComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки городов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilters()
        {
            if (_dataView == null) return;

            var filters = new System.Collections.Generic.List<string>();

            // Фильтр по городу
            if (!string.IsNullOrEmpty(_cityFilter) && _cityFilter != "Все города")
            {
                filters.Add($"[Город] LIKE '%{_cityFilter}%'");
            }

            // Фильтр по названию филиала
            if (!string.IsNullOrEmpty(_nameFilter))
            {
                filters.Add($"[Название филиала] LIKE '%{_nameFilter}%'");
            }

            _dataView.RowFilter = filters.Count > 0 ? string.Join(" AND ", filters) : string.Empty;
        }

        public void RefreshData()
        {
            LoadData();
            LoadCitiesToComboBox();
            ApplyFilters();
        }

        private void AcceptBranchOfficeFilterButton_Click(object sender, EventArgs e)
        {
            _cityFilter = cityComboBox.SelectedItem?.ToString() ?? string.Empty;
            _nameFilter = branchOfiiceName.Text.Trim();
            ApplyFilters();
        }

        private void BranchOfficeClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void BranchOfficesList_Load(object sender, EventArgs e)
        {
            // Автоматическое изменение размера столбцов
            if (branchOfficesTableView.Columns.Count > 0)
            {
                branchOfficesTableView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
        }
    }
}
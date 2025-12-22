using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace SocialPaymentsAssistant.Models
{
    public class MyApplicationsModel
    {
        private readonly int _applicantId;
        private List<ApplicationData> _applications = new();

        public MyApplicationsModel(int applicantId)
        {
            _applicantId = applicantId;
            RefreshData();
        }

        public int RowCount => _applications.Count;
        public int ColumnCount => 7; // ID, Тип выплаты, Статус, Дата подачи, Сумма, Сотрудник, Дата рассмотрения

        public string GetColumnName(int column)
        {
            return column switch
            {
                0 => "ID",
                1 => "Тип выплаты",
                2 => "Статус",
                3 => "Дата подачи",
                4 => "Сумма",
                5 => "Ответственный",
                6 => "Дата рассмотрения",
                _ => string.Empty
            };
        }

        public object GetData(int row, int column)
        {
            if (row < 0 || row >= _applications.Count)
                return null;

            var app = _applications[row];

            return column switch
            {
                0 => app.ApplicationId,
                1 => app.PaymentType,
                2 => app.Status,
                3 => app.ApplicationDate.ToString("dd.MM.yyyy"),
                4 => app.Amount.ToString("F2"),
                5 => string.IsNullOrEmpty(app.EmployeeName) ? "Не назначен" : app.EmployeeName,
                6 => app.ConsiderationDate.HasValue ?
                     app.ConsiderationDate.Value.ToString("dd.MM.yyyy") : "Не рассмотрена",
                _ => null
            };
        }

        public ApplicationData GetApplicationData(int row)
        {
            if (row >= 0 && row < _applications.Count)
                return _applications[row];
            return new ApplicationData();
        }

        public void RefreshData()
        {
            _applications.Clear();

            string query = @"
                SELECT a.application_id, tosp.type_name, appstat.status, 
                a.date_of_creation, a.amount, 
                COALESCE(e.surname || ' ' || LEFT(e.name, 1) || '. ' || LEFT(e.patronymic, 1) || '.', ''), 
                a.date_of_creation 
                FROM application a 
                JOIN type_of_social_payment tosp ON a.type_of_social_payment_id = tosp.type_of_social_payment_id 
                JOIN application_status appstat ON a.application_status_id = appstat.application_status_id 
                LEFT JOIN employee e ON a.employee_id = e.employee_id 
                WHERE a.applicant_id = @applicant_id 
                ORDER BY a.date_of_creation DESC";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@applicant_id", _applicantId)
            };

            var dataTable = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    var app = new ApplicationData
                    {
                        ApplicationId = Convert.ToInt32(row[0]),
                        PaymentType = row[1].ToString() ?? string.Empty,
                        Status = row[2].ToString() ?? string.Empty,
                        ApplicationDate = Convert.ToDateTime(row[3]),
                        Amount = Convert.ToDouble(row[4]),
                        EmployeeName = row[5].ToString() ?? string.Empty,
                        ConsiderationDate = row[6] != DBNull.Value ? Convert.ToDateTime(row[6]) : (DateTime?)null
                    };
                    _applications.Add(app);
                }
            }
        }
    }
}
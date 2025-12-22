using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace SocialPaymentsAssistant.Models
{
    public class EmpApplicationsModel
    {
        private readonly int _employeeId;
        private List<EmpApplicationData> _applications = new();

        public EmpApplicationsModel(int employeeId)
        {
            _employeeId = employeeId;
            RefreshData();
        }

        public int RowCount => _applications.Count;
        public int ColumnCount => 5; // ID заявки, Заявитель, Тип выплаты, Дата создания, Сумма

        public string GetColumnName(int column)
        {
            return column switch
            {
                0 => "ID заявки",
                1 => "Заявитель",
                2 => "Тип выплаты",
                3 => "Дата создания",
                4 => "Сумма",
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
                1 => app.ApplicantName,
                2 => app.PaymentType,
                3 => app.CreationDate.ToString("dd.MM.yyyy HH:mm"),
                4 => $"{app.Amount:F2} руб.",
                _ => null
            };
        }

        public EmpApplicationData GetApplicationData(int row)
        {
            if (row >= 0 && row < _applications.Count)
                return _applications[row];
            return new EmpApplicationData();
        }

        public void RefreshData()
        {
            _applications.Clear();

            string query = @"
                SELECT 
                    a.application_id, 
                    CONCAT(app.surname, ' ', app.name, ' ', COALESCE(app.patronymic, '')) as applicant_name, 
                    tosp.type_name, 
                    a.date_of_creation, 
                    a.amount, 
                    ast.status 
                FROM application a 
                JOIN applicant app ON a.applicant_id = app.applicant_id 
                JOIN type_of_social_payment tosp ON a.type_of_social_payment_id = tosp.type_of_social_payment_id 
                JOIN application_status ast ON a.application_status_id = ast.application_status_id 
                WHERE a.employee_id = @employee_id 
                  AND a.application_status_id = 1 
                ORDER BY a.date_of_creation DESC";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@employee_id", _employeeId)
            };

            var dataTable = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    var app = new EmpApplicationData
                    {
                        ApplicationId = Convert.ToInt32(row[0]),
                        ApplicantName = row[1].ToString() ?? string.Empty,
                        PaymentType = row[2].ToString() ?? string.Empty,
                        CreationDate = Convert.ToDateTime(row[3]),
                        Amount = Convert.ToDouble(row[4]),
                        Status = row[5].ToString() ?? string.Empty,
                        AdditionalInfo = GenerateAdditionalInfo(row)
                    };
                    _applications.Add(app);
                }
            }
        }

        private string GenerateAdditionalInfo(DataRow row)
        {
            return $@"<h2>Детали заявки #{row[0]}</h2>
                     <table border='1' cellpadding='5'>
                     <tr><td><b>ID заявки:</b></td><td>{row[0]}</td></tr>
                     <tr><td><b>Заявитель:</b></td><td>{row[1]}</td></tr>
                     <tr><td><b>Тип выплаты:</b></td><td>{row[2]}</td></tr>
                     <tr><td><b>Дата создания:</b></td><td>{Convert.ToDateTime(row[3]):dd.MM.yyyy HH:mm}</td></tr>
                     <tr><td><b>Сумма:</b></td><td>{Convert.ToDouble(row[4]):F2} руб.</td></tr>
                     <tr><td><b>Статус:</b></td><td>{row[5]}</td></tr>
                     </table>";
        }
    }
}
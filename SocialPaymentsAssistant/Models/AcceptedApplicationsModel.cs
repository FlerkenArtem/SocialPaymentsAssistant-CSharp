using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace SocialPaymentsAssistant.Models
{
    public class AcceptedApplicationsModel
    {
        private readonly int _applicantId;
        private List<AcceptedApplicationData> _applications = new();

        public AcceptedApplicationsModel(int applicantId)
        {
            _applicantId = applicantId;
            RefreshData();
        }

        public int RowCount => _applications.Count;
        public int ColumnCount => 4; // ID, Тип выплаты, Дата принятия, Сумма

        public string GetColumnName(int column)
        {
            return column switch
            {
                0 => "ID",
                1 => "Тип выплаты",
                2 => "Дата принятия",
                3 => "Сумма",
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
                2 => app.AcceptanceDate.ToString("dd.MM.yyyy"),
                3 => app.Amount.ToString("F2"),
                _ => null
            };
        }

        public object GetUserRoleData(int row, int column)
        {
            if (row < 0 || row >= _applications.Count)
                return null;

            var app = _applications[row];

            return column switch
            {
                4 => app.CertificateData,
                5 => app.CertificateFileName,
                _ => _applications[row].ApplicationId
            };
        }

        public AcceptedApplicationData GetApplicationData(int row)
        {
            if (row >= 0 && row < _applications.Count)
                return _applications[row];
            return new AcceptedApplicationData();
        }

        public void RefreshData()
        {
            _applications.Clear();

            string query = @"
                SELECT a.application_id, tosp.type_name, 
                c.date_and_time_of_creation, 
                a.amount, c.document 
                FROM application a 
                JOIN type_of_social_payment tosp ON a.type_of_social_payment_id = tosp.type_of_social_payment_id 
                JOIN certificate c ON a.application_id = c.application_id 
                WHERE a.applicant_id = @applicant_id 
                AND a.application_status_id = (SELECT application_status_id FROM application_status WHERE status = 'Принята') 
                ORDER BY c.date_and_time_of_creation DESC";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@applicant_id", _applicantId)
            };

            var dataTable = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    var app = new AcceptedApplicationData
                    {
                        ApplicationId = Convert.ToInt32(row[0]),
                        PaymentType = row[1].ToString() ?? string.Empty,
                        AcceptanceDate = Convert.ToDateTime(row[2]),
                        Amount = Convert.ToDouble(row[3]),
                        CertificateData = row[4] as byte[] ?? Array.Empty<byte>(),
                        CertificateFileName = string.Empty // В ЛР5 нет поля file_name в certificate
                    };
                    _applications.Add(app);
                }
            }
        }
    }
}
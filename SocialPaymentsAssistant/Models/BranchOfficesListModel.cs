using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace SocialPaymentsAssistant.Models
{
    public class BranchOfficesListModel
    {
        private List<BranchOffice> _branchOffices = new();

        public BranchOfficesListModel()
        {
            UpdateData();
        }

        public int RowCount => _branchOffices.Count;
        public int ColumnCount => 6; // ID, Название, Город, Адрес, Телефон, Email

        public string GetColumnName(int column)
        {
            return column switch
            {
                0 => "ID",
                1 => "Название филиала",
                2 => "Город",
                3 => "Адрес",
                4 => "Телефон",
                5 => "Email",
                _ => string.Empty
            };
        }

        public object GetData(int row, int column)
        {
            if (row < 0 || row >= _branchOffices.Count)
                return null;

            var office = _branchOffices[row];

            return column switch
            {
                0 => office.Id,
                1 => office.Name,
                2 => office.City,
                3 => office.Address,
                4 => office.Phone,
                5 => office.Email,
                _ => null
            };
        }

        public string GetToolTip(int row)
        {
            if (row < 0 || row >= _branchOffices.Count)
                return string.Empty;

            var office = _branchOffices[row];
            return $"Филиал: {office.Name}\nГород: {office.City}\nАдрес: {office.Address}\nТелефон: {office.Phone}\nEmail: {office.Email}";
        }

        public void UpdateData()
        {
            _branchOffices.Clear();

            string query = @"
                SELECT 
                    bo.branch_office_id, 
                    bo.branch_name, 
                    c.name_city, 
                    CONCAT(s.name_street, ', д. ', h.name_house, 
                        CASE WHEN f.name_flat IS NOT NULL THEN CONCAT(', кв. ', f.name_flat) ELSE '' END) AS address, 
                    bo.phone_number, 
                    bo.email 
                FROM branch_office bo 
                JOIN address a ON bo.address_id = a.address_id 
                JOIN city c ON a.city_id = c.city_id 
                JOIN street s ON a.street_id = s.street_id 
                JOIN house h ON a.house_id = h.house_id 
                LEFT JOIN flat f ON a.flat_id = f.flat_id 
                ORDER BY c.name_city, bo.branch_name";

            var dataTable = DatabaseHelper.ExecuteQuery(query);

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    var office = new BranchOffice
                    {
                        Id = Convert.ToInt32(row[0]),
                        Name = row[1].ToString() ?? string.Empty,
                        City = row[2].ToString() ?? string.Empty,
                        Address = row[3].ToString() ?? string.Empty,
                        Phone = row[4].ToString() ?? string.Empty,
                        Email = row[5].ToString() ?? string.Empty
                    };
                    _branchOffices.Add(office);
                }
            }
        }
    }
}
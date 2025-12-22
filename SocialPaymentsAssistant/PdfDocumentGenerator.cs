using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Npgsql;
using System;
using System.IO;

namespace SocialPaymentsAssistant
{
    public class PdfDocumentGenerator
    {
        public byte[] GenerateSimpleCertificate(ApplicationInfo appInfo)
        {
            if (appInfo == null)
                throw new ArgumentNullException(nameof(appInfo));

            // Заполняем пропущенные данные
            appInfo.ApplicantName = appInfo.ApplicantName ?? "Не указано";
            appInfo.PaymentType = appInfo.PaymentType ?? "Не указано";
            appInfo.EmployeeName = appInfo.EmployeeName ?? "Не указано";
            appInfo.EmployeePosition = appInfo.EmployeePosition ?? "Не указано";

            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(ms);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf, PageSize.A4);

                document.SetMargins(40, 40, 40, 40);

                // Шрифт с поддержкой кириллицы
                PdfFont font;
                try
                {
                    font = PdfFontFactory.CreateFont("c:/windows/fonts/arial.ttf", PdfEncodings.IDENTITY_H);
                }
                catch
                {
                    font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                }

                // Заголовок
                document.Add(new Paragraph("СПРАВКА")
                    .SetFontSize(16)
                    .SetTextAlignment(TextAlignment.CENTER)
                              .SetMarginBottom(10));

                document.Add(new Paragraph("о принятии заявки")
                    .SetFontSize(12)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetMarginBottom(20));

                // Основные данные
                AddDocumentLine(document, "Номер заявки:", appInfo.ApplicationId.ToString());
                AddDocumentLine(document, "Заявитель:", appInfo.ApplicantName);
                AddDocumentLine(document, "Тип выплаты:", appInfo.PaymentType);
                AddDocumentLine(document, "Дата подачи:", appInfo.CreationDate.ToString("dd.MM.yyyy"));
                AddDocumentLine(document, "Сумма:", $"{appInfo.ApprovedAmount:F2} руб.");
                AddDocumentLine(document, "Сотрудник:", appInfo.EmployeeName);
                AddDocumentLine(document, "Должность:", appInfo.EmployeePosition);

                // Подпись
                document.Add(new Paragraph("_________________________")
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetMarginTop(30));

                document.Add(new Paragraph(appInfo.EmployeeName)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetMarginBottom(10));

                document.Add(new Paragraph($"Дата: {DateTime.Now:dd.MM.yyyy}")
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetFontSize(10));

                document.Close();
                return ms.ToArray();
            }
        }

        private void AddDocumentLine(Document doc, string label, string value)
        {
            Paragraph p = new Paragraph()
                .SetMarginBottom(5);

            p.Add(new Text($"{label} "));
            p.Add(new Text(value));

            doc.Add(p);
        }

        public bool SaveToDatabase(int applicationId, byte[] pdfData)
        {
            try
            {
                string query = @"
                    INSERT INTO application_certificates (application_id, certificate_data, created_at) 
                    VALUES (@applicationId, @certificateData, @createdAt)
                    ON CONFLICT (application_id) DO UPDATE 
                    SET certificate_data = @certificateData, created_at = @createdAt";

                using (var connection = new NpgsqlConnection(DatabaseHelper.ConnectionString))
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@applicationId", applicationId);
                    command.Parameters.AddWithValue("@certificateData", pdfData);
                    command.Parameters.AddWithValue("@createdAt", DateTime.Now);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения в БД: {ex.Message}");
                return false;
            }
        }

        public bool SaveToFile(string filePath, byte[] pdfData)
        {
            try
            {
                File.WriteAllBytes(filePath, pdfData);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения файла: {ex.Message}");
                return false;
            }
        }
    }
}
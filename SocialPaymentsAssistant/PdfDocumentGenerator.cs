using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Kernel.Geom;

namespace SocialPaymentsAssistant
{
    public class PdfDocumentGenerator
    {
        public byte[] GenerateCertificate(ApplicationInfo appInfo)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Создаем PDF документ
                PdfWriter writer = new PdfWriter(memoryStream);
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document document = new Document(pdfDoc, PageSize.A4);

                document.SetMargins(50, 50, 50, 50);

                // Заголовок
                Paragraph title = new Paragraph("СПРАВКА")
                    .SetFontSize(20)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetMarginBottom(20);
                document.Add(title);

                // Подзаголовок
                Paragraph subtitle = new Paragraph("о принятии заявки на социальную выплату")
                    .SetFontSize(16)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetMarginBottom(30);
                document.Add(subtitle);

                // Информация о документе
                Paragraph docInfo = new Paragraph()
                    .SetFontSize(8)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetMarginBottom(20);
                docInfo.Add($"Дата формирования: {DateTime.Now:dd.MM.yyyy HH:mm}\n");
                docInfo.Add($"Номер документа: СП-{appInfo.ApplicationId}-{DateTime.Now:yyyyMMdd}");
                document.Add(docInfo);

                // Таблица с данными
                Table table = new Table(2, true);
                table.SetWidth(UnitValue.CreatePercentValue(100));
                table.SetMarginTop(10);
                table.SetMarginBottom(20);

                // Заголовки таблицы
                Cell header1 = new Cell()
                    .Add(new Paragraph("Поле")
                        .SetFontColor(ColorConstants.WHITE))
                    .SetBackgroundColor(ColorConstants.DARK_GRAY)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetPadding(5);

                Cell header2 = new Cell()
                    .Add(new Paragraph("Значение")
                        .SetFontColor(ColorConstants.WHITE))
                    .SetBackgroundColor(ColorConstants.DARK_GRAY)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetPadding(5);

                table.AddHeaderCell(header1);
                table.AddHeaderCell(header2);

                // Данные таблицы
                AddTableRow(table, "Номер заявки", appInfo.ApplicationId.ToString());
                AddTableRow(table, "Заявитель", appInfo.ApplicantName);
                AddTableRow(table, "Тип социальной выплаты", appInfo.PaymentType);
                AddTableRow(table, "Дата подачи заявки", appInfo.CreationDate.ToString("dd.MM.yyyy HH:mm"));
                AddTableRow(table, "Запрашиваемая сумма", $"{appInfo.RequestedAmount:F2} руб.");
                AddTableRow(table, "Утвержденная сумма", $"{appInfo.ApprovedAmount:F2} руб.");
                AddTableRow(table, "Дата принятия решения", DateTime.Now.ToString("dd.MM.yyyy"));
                AddTableRow(table, "Ответственный сотрудник", appInfo.EmployeeName);
                AddTableRow(table, "Должность", appInfo.EmployeePosition);
                AddTableRow(table, "ИНН заявителя", appInfo.ApplicantInn);
                AddTableRow(table, "Телефон заявителя", appInfo.ApplicantPhone);

                document.Add(table);

                // Основной текст
                Paragraph text1 = new Paragraph(
                    "Настоящая справка подтверждает, что заявка на получение социальной выплаты была рассмотрена и принята к исполнению.")
                    .SetMarginBottom(10);
                document.Add(text1);

                Paragraph text2 = new Paragraph(
                    "Справка является официальным документом и может быть использована для получения указанной социальной выплаты в соответствии с установленным порядком.")
                    .SetMarginBottom(40);
                document.Add(text2);

                // Подпись
                Paragraph signature = new Paragraph()
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetMarginBottom(20);
                signature.Add("_________________________\n");
                signature.Add("(Подпись сотрудника)\n\n");
                signature.Add("М.П.");
                document.Add(signature);

                // Футер
                Paragraph footer = new Paragraph()
                    .SetFontSize(8)
                    .SetTextAlignment(TextAlignment.CENTER);
                footer.Add("Документ сформирован автоматически системой\n");
                footer.Add("\"Интерактивный помощник для составления заявлений на получение социальных выплат\"\n");
                footer.Add($"Дата и время формирования: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                document.Add(footer);

                document.Close();

                return memoryStream.ToArray();
            }
        }

        private void AddTableRow(Table table, string field, string value)
        {
            Cell cell1 = new Cell()
                .Add(new Paragraph(field))
                .SetPadding(5)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetBorderBottom(new iText.Layout.Borders.SolidBorder(1));

            Cell cell2 = new Cell()
                .Add(new Paragraph(value))
                .SetPadding(5)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetBorderBottom(new iText.Layout.Borders.SolidBorder(1));

            table.AddCell(cell1);
            table.AddCell(cell2);
        }

        public bool SaveToFile(string filePath, ApplicationInfo appInfo)
        {
            try
            {
                byte[] pdfData = GenerateCertificate(appInfo);
                if (pdfData != null && pdfData.Length > 0)
                {
                    File.WriteAllBytes(filePath, pdfData);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения PDF: {ex.Message}");
            }

            return false;
        }
    }
}
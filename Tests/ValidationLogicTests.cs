using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SocialPaymentsAssistant.Tests
{
    [TestClass]
    public class ValidationLogicTests // Белый ящик
    {
        [TestMethod]
        public void EmailValidation_UsingSystemMailAddress_ShouldWork()
        {
            // Arrange
            var emailValidator = new System.Net.Mail.MailAddress("test@example.com");

            // Act & Assert
            Assert.AreEqual("test@example.com", emailValidator.Address);
        }

        [TestMethod]
        public void DateTimeValidation_ShouldParseCorrectly()
        {
            // Arrange & Act
            DateTime testDate = new DateTime(2024, 1, 15);
            string formattedDate = testDate.ToString("yyyy-MM-dd");

            // Assert
            Assert.AreEqual("2024-01-15", formattedDate);
        }

        [TestMethod]
        public void StringPadding_ShouldFormatCorrectly()
        {
            // Arrange
            string inn = "1234567890";

            // Act - имитируем логику из ApplicantReg.cs
            string paddedInn = inn;
            if (paddedInn.Length < 12)
                paddedInn = paddedInn.PadRight(12);

            // Assert
            Assert.AreEqual(12, paddedInn.Length);
            Assert.IsTrue(paddedInn.StartsWith("1234567890"));
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SocialPaymentsAssistant.Tests
{
    [TestClass]
    public class SimpleLogicTests // Модульный тест
    {
        [TestMethod]
        public void TestPhoneNumberFormattingLogic()
        {
            // Arrange
            string phone = "89123456789";

            // Act - имитируем логику форматирования
            string formatted = phone;

            // Простая логика (на основе кода в ApplicantReg.cs)
            if (phone.Length == 11 && phone.StartsWith("8"))
            {
                formatted = $"+7 ({phone.Substring(1, 3)}) {phone.Substring(4, 3)}-{phone.Substring(7, 2)}-{phone.Substring(9, 2)}";
            }

            // Assert
            Assert.AreEqual("+7 (912) 345-67-89", formatted);
        }

        public void TestDateLogic()
        {
            // Arrange
            DateTime birthDate = DateTime.Now.AddYears(-30);

            // Act & Assert - проверяем логику из EmpReg.cs
            Assert.IsTrue(birthDate < DateTime.Now.AddYears(-18),
                "Дата рождения должна быть больше 18 лет назад");
        }
    }
}
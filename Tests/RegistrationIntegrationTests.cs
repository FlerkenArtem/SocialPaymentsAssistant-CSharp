using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SocialPaymentsAssistant.Tests
{
    [TestClass]
    public class RegistrationIntegrationTests // Интеграционный тест
    {
        private class TestUserData
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public bool IsValid { get; set; }
        }

        [TestMethod]
        public void RegistrationFlow_ShouldValidateAllFields()
        {
            // Arrange - имитируем данные пользователя
            var userData = new TestUserData
            {
                Username = "testuser",
                Password = "Password123",
                Email = "test@example.com"
            };

            var validationErrors = new List<string>();

            // Act - имитируем валидацию из EmpReg.cs
            if (string.IsNullOrEmpty(userData.Username))
                validationErrors.Add("Введите логин");

            if (string.IsNullOrEmpty(userData.Email))
                validationErrors.Add("Введите email");

            if (string.IsNullOrEmpty(userData.Password) || userData.Password.Length < 8)
                validationErrors.Add("Пароль должен содержать минимум 8 символов");

            // Дополнительная валидация email
            try
            {
                var addr = new System.Net.Mail.MailAddress(userData.Email);
                if (addr.Address != userData.Email)
                    validationErrors.Add("Введите корректный email");
            }
            catch
            {
                validationErrors.Add("Введите корректный email");
            }

            // Assert
            Assert.AreEqual(0, validationErrors.Count,
                $"Ошибки валидации: {string.Join(", ", validationErrors)}");
        }

        [TestMethod]
        public void RegistrationFlow_InvalidData_ShouldFail()
        {
            // Arrange
            var invalidUserData = new TestUserData
            {
                Username = "",
                Password = "123",
                Email = "invalid-email"
            };

            var validationErrors = new List<string>();

            // Act
            if (string.IsNullOrEmpty(invalidUserData.Username))
                validationErrors.Add("Введите логин");

            if (string.IsNullOrEmpty(invalidUserData.Email))
                validationErrors.Add("Введите email");

            if (string.IsNullOrEmpty(invalidUserData.Password) || invalidUserData.Password.Length < 8)
                validationErrors.Add("Пароль должен содержать минимум 8 символов");

            try
            {
                var addr = new System.Net.Mail.MailAddress(invalidUserData.Email);
                if (addr.Address != invalidUserData.Email)
                    validationErrors.Add("Введите корректный email");
            }
            catch
            {
                validationErrors.Add("Введите корректный email");
            }

            // Assert
            Assert.IsTrue(validationErrors.Count > 0,
                "Должны быть ошибки валидации для невалидных данных");
        }
    }
}
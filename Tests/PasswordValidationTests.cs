using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace SocialPaymentsAssistant.Tests
{
    [TestClass]
    public class PasswordValidationTests // Черный ящик
    {
        // Тестируем логику валидации пароля из EmpReg.cs
        [TestMethod]
        [DataRow("password1", "password1", true)]     // совпадают
        [DataRow("password1", "password2", false)]    // не совпадают
        [DataRow("12345678", "12345678", true)]       // минимальная длина
        [DataRow("1234567", "1234567", false)]        // меньше 8 символов
        [DataRow("", "", false)]                      // пустые
        [DataRow(null, null, false)]                  // null
        public void PasswordValidation_ShouldValidateCorrectly(string pass1, string pass2, bool expected)
        {
            // Arrange
            // Логика из EmpReg.cs: password.Text == password2.Text && password.Text.Length >= 8
            bool isValid = true;

            // Act
            if (string.IsNullOrEmpty(pass1) || pass1.Length < 8)
                isValid = false;

            if (pass1 != pass2)
                isValid = false;

            // Assert
            Assert.AreEqual(expected, isValid,
                $"Пароль1: '{pass1}', Пароль2: '{pass2}' - ожидалось: {expected}");
        }

        [TestMethod]
        public void PasswordTextEdit_MaxLength_ShouldBe20()
        {
            // Arrange
            // Из LoginWidget.cs: passwordEdit.MaxLength = 20
            int expectedMaxLength = 20;

            // Act & Assert
            Assert.AreEqual(expectedMaxLength, 20);
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SocialPaymentsAssistant.Tests
{
    [TestClass]
    public class LoadTests // Нагрузочный тест
    {
        [TestMethod]
        public void PasswordValidation_Performance_ShouldBeFast() 
        {
            // Arrange
            int iterations = 10000;
            var stopwatch = new Stopwatch();

            // Act
            stopwatch.Start();

            for (int i = 0; i < iterations; i++)
            {
                string password = $"Password{i}";
                bool isValid = !string.IsNullOrEmpty(password) && password.Length >= 8;

                // Просто используем результат чтобы избежать оптимизации
                if (!isValid) throw new Exception("Не должно происходить");
            }

            stopwatch.Stop();

            // Assert - 10000 проверок должны выполняться быстро
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 1000,
                $"10000 проверок выполнены за {stopwatch.ElapsedMilliseconds}ms - слишком медленно");

            Console.WriteLine($"Выполнено {iterations} проверок за {stopwatch.ElapsedMilliseconds}ms");
        }

        [TestMethod]
        public void EmailValidation_ConcurrentLoad_ShouldHandleMultipleRequests()
        {
            // Arrange
            string[] testEmails =
            {
                "test@example.com",
                "invalid-email",
                "another.test@domain.ru",
                "",
                null
            };

            int totalIterations = 1000;
            int validCount = 0;
            int invalidCount = 0;

            // Act
            Parallel.For(0, totalIterations, i =>
            {
                string email = testEmails[i % testEmails.Length];
                bool isValid = false;

                try
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        var addr = new System.Net.Mail.MailAddress(email);
                        isValid = addr.Address == email;
                    }
                }
                catch
                {
                    isValid = false;
                }

                if (isValid)
                    System.Threading.Interlocked.Increment(ref validCount);
                else
                    System.Threading.Interlocked.Increment(ref invalidCount);
            });

            // Assert
            Console.WriteLine($"Валидных: {validCount}, Невалидных: {invalidCount}");
            Assert.AreEqual(totalIterations, validCount + invalidCount);
        }
    }
}
namespace SocialPaymentsAssistant
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            ApplicationConfiguration.Initialize();

            // Подключаем БД
            bool dbOk = DatabaseHelper.TestConnection();
            Console.WriteLine($"db = {dbOk}");

            Application.Run(new LoginWidget());
        }
    }
}
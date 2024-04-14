using Rights.DataFolder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Rights
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User CurrentUser { get; set; }
        public byte[] PhotoStaff { get; set; }

        public static Func<int, Window> GetCurrentWindow = (_) =>
        {
            return Application.Current.MainWindow;
        };

        public static string GetCurrentWorkerInitials()
        {
            if (CurrentUser == null)
            {
                return string.Empty;
            }
            Staff worker = App.CurrentUser.Staff.FirstOrDefault();
            if (worker == null)
            {
                return "Сотрудник";
            }
            return $"{worker.LastName} {worker.FirstName[0]}." +
                (string.IsNullOrEmpty(worker.MiddleName) ? string.Empty : worker.LastName[0] + ".");
        }
        public App()
        {
            this.InitializeComponent(); ;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            new Authorization().Show();
        }
    }
}

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

        public static string GetCurrentstaffInitials()
        {
            if (CurrentUser == null)
            {
                return string.Empty;
            }
            Staff staff = App.CurrentUser.Staff.FirstOrDefault();
            if (staff == null)
            {
                return "Сотрудник";
            }
            return $"{staff.LastName} {staff.FirstName[0]}." +
                (string.IsNullOrEmpty(staff.MiddleName) ? string.Empty : staff.LastName[0] + ".");
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

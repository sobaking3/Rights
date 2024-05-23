using Rights.DataFolder;
using Rights.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using Rights.ClassFolder;
using System.Data.Entity.Validation;

namespace Rights.PageFolder.PresidentWindow
{
    /// <summary>
    /// Логика взаимодействия для EditOrganization.xaml
    /// </summary>
    public partial class EditOrganization : Window
    {
        private Organizations _organizations = new Organizations();


        public EditOrganization(Organizations organizations)
        {
            InitializeComponent();
            DataContext = _organizations = organizations;
            StatusCb.ItemsSource = DBEntities.GetContext()
            .Status.Except(DBEntities.GetContext().Status.Where(r => r.StatusName == "Жалоба решена"
            || r.StatusName == "В процессе"
            || r.StatusName == "Приостановлена"
            || r.StatusName == "Отклонена"
            || r.StatusName == "Собрание закончено"
            || r.StatusName == "Собрание в процессе"))
            .ToList();

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigureWithUserAccess();
        }

        private void ConfigureWithUserAccess()
        {

            TitleTb.Text = "Изменить организацию";
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowHelper.DragMove(this, e);
        }

        private void EditOrganizationBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DBEntities.GetContext().SaveChanges();
                MBClass.InfoMB("Изменения сохранены!");
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }
    }
}

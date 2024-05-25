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
    /// Логика взаимодействия для EditAppealStatus.xaml
    /// </summary>
    public partial class EditAppealStatus : Window
    {
        AppealsAndComplaints _appealsAndComplaints = new AppealsAndComplaints();
        private Status _status = new Status();


        public EditAppealStatus(AppealsAndComplaints appealsAndComplaints)
        {
            InitializeComponent();
            DataContext = _appealsAndComplaints = appealsAndComplaints;
            StatusCb.ItemsSource = DBEntities.GetContext().Status.Where(x => x.StatusName == "Жалоба решена"
          || x.StatusName == "В процессе" || x.StatusName == "Приостановлена" || x.StatusName == "Отклонена").ToList();

        }

        private void EditStatusBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DBEntities.GetContext().SaveChanges();
                MBClass.InfoMB("Статус изменен!");
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
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

            TitleTb.Text = "Статус";
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowHelper.DragMove(this, e);
        }

    }
}

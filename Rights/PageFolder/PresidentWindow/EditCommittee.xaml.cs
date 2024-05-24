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
    /// Логика взаимодействия для EditCommittee.xaml
    /// </summary>
    public partial class EditCommittee : Window
    {

        private Committee _committee = new Committee();


        public EditCommittee(Committee committee)
        {
            InitializeComponent();
            DataContext = _committee = committee;
            //StaffCb.ItemsSource = DBEntities.GetContext()
            //.Staff.Except(DBEntities.GetContext().Staff.Where(r => r.User.Role.NameRole == "Админ"
            //|| r.User.Role.NameRole == "Член союза"
            //|| r.User.Role.NameRole == "Юрист"
            //|| r.User.Role.NameRole == "Менеджер"
            //|| r.User.Role.NameRole == "Президент"))
            //.ToList();

            //BRUH BRUH BRUH
            StaffCb.ItemsSource = DBEntities.GetContext().Staff.Where(x => x.User.Role.NameRole == "Директор" 
            || x.User.Role.NameRole == "Президент").ToList();

        }

        private void EditCommitteeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Staff newDirector = StaffCb.SelectedItem as Staff;
                _committee.IdStaff = newDirector.IdStaff;
                DBEntities.GetContext().SaveChanges();
                _committee.UpdateDirector();
                MBClass.InfoMB("Изменения сохранены!");
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

            TitleTb.Text = "Изменить комитет";
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowHelper.DragMove(this, e);
        }
    }
}

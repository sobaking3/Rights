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
using System.Data.Entity.Migrations;

namespace Rights.PageFolder.PresidentWindow
{
    /// <summary>
    /// Логика взаимодействия для EditDepartment.xaml
    /// </summary>
    public partial class EditDepartment : Window
    {
        private Departament _departament = new Departament();
        private Departament _departament2 = new Departament();
        private DBEntities _ctx = DBEntities.GetContext();

        public EditDepartment(Departament departament)
        {
            _departament = departament;
            InitializeComponent();
            _departament2 = DBEntities.GetContext().Departament.AsNoTracking().FirstOrDefault(o => o.IdDepartament == departament.IdDepartament);
            DataContext = _departament2;
            StaffCb.ItemsSource = DBEntities.GetContext().Staff.Where(x => x.User.Role.NameRole == "Директор"
           || x.User.Role.NameRole == "Президент").ToList();

        }

        private void EditDepartamentBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_ctx.Departament.FirstOrDefault(x => x.NameDepartament == _departament.NameDepartament && x.IdDepartament != _departament.IdDepartament) != null)
                {
                    MBClass.ErrorMB("Данный отдел уже есть!");
                    return;
                }
                else if (ElementsToolsClass.AllFieldsFilled(this))
                {
                    try
                    {
                        _ctx.Departament.AddOrUpdate(_departament2);
                        Staff newDirector = StaffCb.SelectedItem as Staff;
                        _departament.IdStaff = newDirector.IdStaff;
                        DBEntities.GetContext().SaveChanges();
                        _departament.UpdateDirector();
                        _ctx.SaveChanges();
                        MBClass.InfoMB("Изменения сохранены!");
                    }
                    catch (Exception ex)
                    {
                        MBClass.ErrorMB(ex);
                    }
                }
                else
                {
                    MBClass.ErrorMB("Вы не ввели все нужные данные!");
                }
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

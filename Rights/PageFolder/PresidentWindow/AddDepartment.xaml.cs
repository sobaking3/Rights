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
    /// Логика взаимодействия для AddDepartment.xaml
    /// </summary>
    public partial class AddDepartment : Window
    {
        private Staff Staff = new Staff();
        private Departament departament = new Departament();


        public AddDepartment()
        {
            InitializeComponent();
            StaffCb.ItemsSource = DBEntities.GetContext()
            .Staff.Except(DBEntities.GetContext().Staff.Where(r => r.User.Role.NameRole == "Админ"
            || r.User.Role.NameRole == "Член союза"
            || r.User.Role.NameRole == "Юрист"
            || r.User.Role.NameRole == "Менеджер"
            || r.User.Role.NameRole == "Президент"))
            .ToList();

        }

        private void DepartamentInfoAdd()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                var Departament = new Departament()
                {
                    NameDepartament = DepartamentNameTb.Text,
                    IdStaff = Int32.Parse(StaffCb.SelectedValue.ToString()),
                };
                DBEntities.GetContext().Departament.Add(Departament);
                DBEntities.GetContext().SaveChanges();
            }
        }

        private void AddCommitteeBtn_Click(object sender, RoutedEventArgs e)
        {

            if (DBEntities.GetContext().Committee.FirstOrDefault(u =>
            u.NameCommittee == DepartamentNameTb.Text) != null)
            {
                MBClass.ErrorMB($"Отдел c названием {DepartamentNameTb.Text} уже создан");

                DepartamentNameTb.Focus();
            }
            else if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    DepartamentInfoAdd();

                    MBClass.InfoMB("Отдел добавлен");
                    ElementsToolsClass.ClearAllControls(this);
                }
                catch (DbEntityValidationException ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
            else
            {
                MBClass.ErrorMB("Вы не ввели все нужные данные!");
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

            TitleTb.Text = "Добавить отдел";
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowHelper.DragMove(this, e);
        }
    }
}

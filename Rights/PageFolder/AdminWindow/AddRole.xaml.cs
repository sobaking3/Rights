using Rights.ClassFolder;
using Rights.DataFolder;
using Rights.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Rights.PageFolder.AdminWindow
{
    /// <summary>
    /// Логика взаимодействия для AddRole.xaml
    /// </summary>
    public partial class AddRole : Window
    {
        private Role Role = new Role();
        public AddRole()
        {
            InitializeComponent();
        }

        private void AddRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DBEntities.GetContext().Role.FirstOrDefault(u =>
            u.NameRole == NameRoleTb.Text) != null)
            {
                MBClass.ErrorMB($"Такая должность уже есть в базе данных");

                NameRoleTb.Focus();
                return;
            }
            else if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    AddRoles();

                    MBClass.InfoMB("Должность добавлена");
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
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowHelper.DragMove(this, e);
        }
        private void AddRoles()
        {
            var roleAdd = new Role()
            {
                NameRole = NameRoleTb.Text,

            };
            DBEntities.GetContext().Role.Add(roleAdd);
            DBEntities.GetContext().SaveChanges();
        }
        private void ConfigureWithUserAccess()
        {

            TitleTb.Text = "Добавить";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigureWithUserAccess();
        }
    }
}

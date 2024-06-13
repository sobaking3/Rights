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
    /// Логика взаимодействия для EditRole.xaml
    /// </summary>
    public partial class EditRole : Window
    {
        private Role _role;
        public EditRole(Role role)
        {
            DataContext = _role = role;
            InitializeComponent();
        }

        private void EditRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DBEntities.GetContext().Role.FirstOrDefault(u =>
            u.NameRole == NameRoleTb.Text && u.IdRole != _role.IdRole) != null)
            {
                MBClass.ErrorMB($"Такая должность уже есть в базе данных");

                NameRoleTb.Focus();
                return;
            }
            else if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    EditRoles();

                    MBClass.InfoMB("Должность изменена");
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
        private void EditRoles()
        {
            var roleAdd = new Role()
            {
                NameRole = NameRoleTb.Text,

            };
            DBEntities.GetContext().SaveChanges();
        }
        private void ConfigureWithUserAccess()
        {

            TitleTb.Text = "Изменить";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigureWithUserAccess();
        }
    }
}

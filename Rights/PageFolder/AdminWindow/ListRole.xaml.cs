using Rights.ClassFolder;
using Rights.DataFolder;
using Rights.Helpers;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rights.PageFolder.AdminWindow
{
    /// <summary>
    /// Логика взаимодействия для ListRole.xaml
    /// </summary>
    public partial class ListRole : Page
    {
        public ListRole()
        {
            InitializeComponent(); 
            ListRoleDG.ItemsSource = DBEntities.GetContext()
                .Role.ToList().OrderBy(u => u.IdRole);
        }
        private void UpdateList()
        {
            ListRoleDG.ItemsSource = DBEntities.GetContext()
                 .Role.Where(s => s.NameRole
                 .StartsWith(SearchStaffByFullNameTb.Text))
                 .ToList().OrderBy(s => s.NameRole);
        }

        private void AddBudgetBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.ShowDialogWithBlur(this, new AdminWindow.AddRole());
            UpdateList();
        }
        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                UpdateList();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }
        private void DeleteM1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Role Role = ListRoleDG.SelectedItem as Role;

                if (ListRoleDG.SelectedItem == null)
                {
                    MBClass.ErrorMB("Должность не выбрана");
                }
                else
                {
                    if (MBClass.QuestionMB($"Удалить должность " +
                    $"{Role.NameRole}?"))
                    {
                        DBEntities.GetContext().Role.Remove(ListRoleDG.SelectedItem as Role);
                        DBEntities.GetContext().SaveChanges();
                        MBClass.InfoMB("Должность удалена");
                        UpdateList();
                    }
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }

        private void EditM1_Click(object sender, RoutedEventArgs e)
        {
            Role role = ListRoleDG.SelectedItem as Role;
            WindowHelper.ShowDialogWithBlur(this, new AdminWindow.EditRole(role));

            UpdateList();
        }
    }
}

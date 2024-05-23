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
    /// Логика взаимодействия для AddOrganization.xaml
    /// </summary>
    public partial class AddOrganization : Window
    {
        private Staff Staff = new Staff();
        private Organizations organizations = new Organizations();


        public AddOrganization()
        {
            InitializeComponent();

        }

        private void OrganizationsInfoAdd()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                var Organizations = new Organizations()
                {
                    NameOrganization = NameOrganizationTb.Text,
                    IdStatus = 5,
                    PhoneNumber = PhoneNumberTb.Text,
                    Description = DescriptionTb.Text,

                };
                DBEntities.GetContext().Organizations.Add(Organizations);
                DBEntities.GetContext().SaveChanges();
            }
        }

        private void AddOrganizationBtn_Click(object sender, RoutedEventArgs e)
        {

            if (DBEntities.GetContext().Organizations.FirstOrDefault(u =>
            u.NameOrganization == NameOrganizationTb.Text) != null)
            {
                MBClass.ErrorMB($"Организация c названием {NameOrganizationTb.Text} уже создана");

                NameOrganizationTb.Focus();
            }
            else if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    OrganizationsInfoAdd();

                    MBClass.InfoMB("Организация добавлена");
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

            TitleTb.Text = "Добавить организацию";
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowHelper.DragMove(this, e);
        }
    }
}

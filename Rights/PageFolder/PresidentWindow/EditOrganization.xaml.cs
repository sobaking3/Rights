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
    /// Логика взаимодействия для EditOrganization.xaml
    /// </summary>
    public partial class EditOrganization : Window
    {
        private Organizations _organizations = new Organizations();
        private Organizations _organizations2 = new Organizations();
        private DBEntities _ctx = DBEntities.GetContext();

        public EditOrganization(Organizations organizations)
        {
            _organizations = organizations;
            InitializeComponent();     
            _organizations2 = DBEntities.GetContext().Organizations.FirstOrDefault(o => o.IdOrganization == organizations.IdOrganization);
            DataContext = _organizations2;
            StatusCb.ItemsSource = DBEntities.GetContext()
            .Status.AsNoTracking().Where(r => r.StatusName == "Договор в силе"
            || r.StatusName == "Договор рассторгнут")
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
                if (_ctx.Organizations.FirstOrDefault(x => x.NameOrganization == _organizations2.NameOrganization && x.IdOrganization != _organizations2.IdOrganization) != null)
                {
                    MBClass.ErrorMB("Данная организация уже есть!");
                    return;
                }
                else if (ElementsToolsClass.AllFieldsFilled(this))
                {
                    try
                    {
                        _ctx.Organizations.AddOrUpdate(_organizations2);
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
    }
}

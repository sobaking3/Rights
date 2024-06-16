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
    /// Логика взаимодействия для EditCommittee.xaml
    /// </summary>
    public partial class EditCommittee : Window
    {
        private DBEntities _ctx = DBEntities.GetContext();
        private Committee _committee = new Committee();
        private Committee _committee2 = new Committee();



        public EditCommittee(Committee committee)
        {
            _committee = committee;
            InitializeComponent();
            _committee2 = DBEntities.GetContext().Committee.AsNoTracking().FirstOrDefault(o => o.IdCommittee == committee.IdCommittee);
            DataContext = _committee2;
            StaffCb.ItemsSource = DBEntities.GetContext()
            .Staff.Where(x => x.User.Role.NameRole == "Директор"
            || x.User.Role.NameRole == "Президент").ToList();

        }

        private void EditCommitteeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_ctx.Committee.FirstOrDefault(x => x.NameCommittee == _committee.NameCommittee && x.IdCommittee != _committee.IdCommittee) != null)
                {
                    MBClass.ErrorMB("Данный комитет уже есть!");
                    return;
                }
                else if (ElementsToolsClass.AllFieldsFilled(this))
                {
                    try
                    {
                        _ctx.Committee.AddOrUpdate(_committee2); 
                        Staff newDirector = StaffCb.SelectedItem as Staff;
                        _committee.IdStaff = newDirector.IdStaff;
                        DBEntities.GetContext().SaveChanges();
                        _committee.UpdateDirector();
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

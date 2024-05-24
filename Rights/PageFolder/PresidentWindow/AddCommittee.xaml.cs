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
    /// Логика взаимодействия для AddCommittee.xaml
    /// </summary>
    public partial class AddCommittee : Window
    {
        private Staff Staff = new Staff();
        private Committee committee = new Committee();


        public AddCommittee()
        {
            InitializeComponent();
            StaffCb.ItemsSource = DBEntities.GetContext().Staff.Where(x => x.User.Role.NameRole == "Директор"
           || x.User.Role.NameRole == "Президент").ToList();

        }
        
        private void CommitteeInfoAdd()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                var Committee = new Committee()
                {
                    NameCommittee = CommitteeNameTb.Text,
                    IdStaff = Int32.Parse(StaffCb.SelectedValue.ToString()),      
                };
                DBEntities.GetContext().Committee.Add(Committee);
                DBEntities.GetContext().SaveChanges();
            }
        }
      
        private void AddCommitteeBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (DBEntities.GetContext().Committee.FirstOrDefault(u =>
            u.NameCommittee == CommitteeNameTb.Text) != null)
            {
                MBClass.ErrorMB($"Комитет c названием {CommitteeNameTb.Text} уже создан");

                CommitteeNameTb.Focus();
            }
            else if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    CommitteeInfoAdd();

                    MBClass.InfoMB("Комитет добавлен");
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

            TitleTb.Text = "Добавить комитет";
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowHelper.DragMove(this, e);
        }
    }
}

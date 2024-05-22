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

namespace Rights.PageFolder.WindowForAll
{
    /// <summary>
    /// Логика взаимодействия для AddAppeal.xaml
    /// </summary>
    public partial class AddAppeal : Window
    {

        private AppealsAndComplaints AppealsAndComplaints = new AppealsAndComplaints();



        public AddAppeal()
        {
            InitializeComponent();

        }
        private void AppealInfoAdd()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                Staff staff = App.CurrentStaff;
                var AppealsAndComplaints = new AppealsAndComplaints()
                {

                    IdStaff = staff.IdStaff,
                    Date = DateTime.Now,
                    Discription = DiscriptionTb.Text,
                    IdStatus = 2,
                };
                DBEntities.GetContext().AppealsAndComplaints.Add(AppealsAndComplaints);
                DBEntities.GetContext().SaveChanges();
            }
        }
    


        private void AddStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    AppealInfoAdd();

                    MBClass.InfoMB("Жалоба отправлена");
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

            TitleTb.Text = "Отправка жалобы";
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowHelper.DragMove(this, e);
        }

    }
}

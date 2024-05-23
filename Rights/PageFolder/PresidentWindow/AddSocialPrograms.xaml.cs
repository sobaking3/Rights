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
    /// Логика взаимодействия для AddSocialPrograms.xaml
    /// </summary>
    public partial class AddSocialPrograms : Window
    {
        private Organizations organizations = new Organizations();


        public AddSocialPrograms()
        {
            InitializeComponent();
            CommitteeCb.ItemsSource = DBEntities.GetContext().Committee.ToList();
        }

        private void SocialProgramsInfoAdd()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                var SocialPrograms = new SocialPrograms()
                {
                    ProgramName = NameSocialProgramsTb.Text,
                    IdCommitte = Int32.Parse(CommitteeCb.SelectedValue.ToString()),
                    ProgramDiscription = DescriptionTb.Text,

                };
                DBEntities.GetContext().SocialPrograms.Add(SocialPrograms);
                DBEntities.GetContext().SaveChanges();
            }
        }

        private void AddSocialProgramsBtn_Click(object sender, RoutedEventArgs e)
        {

            if (DBEntities.GetContext().SocialPrograms.FirstOrDefault(u =>
            u.ProgramName == NameSocialProgramsTb.Text) != null)
            {
                MBClass.ErrorMB($"Программа c названием {NameSocialProgramsTb.Text} уже создана");

                NameSocialProgramsTb.Focus();
            }
            else if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    SocialProgramsInfoAdd();

                    MBClass.InfoMB("Социальная программа добавлена");
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

            TitleTb.Text = "Добавить соц. программу";
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowHelper.DragMove(this, e);
        }
    }
}

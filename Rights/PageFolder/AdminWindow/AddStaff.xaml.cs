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

namespace Rights.PageFolder.AdminWindow
{
    /// <summary>
    /// Логика взаимодействия для AddStaff.xaml
    /// </summary>
    public partial class AddStaff : Window
    {
        private Staff Staff = new Staff();
        private User user = new User();
        private Gender gender = new Gender();
        private Departament departament = new Departament();
        private Committee committee = new Committee();


        public AddStaff()
        {
            InitializeComponent();
            RoleCb.ItemsSource = DBEntities.GetContext().Role.ToList();
            GenderCb.ItemsSource = DBEntities.GetContext().Gender.ToList();
            DepartamentCb.ItemsSource = DBEntities.GetContext().Departament.ToList();
            CommitteeCb.ItemsSource = DBEntities.GetContext().Committee.ToList();
        }
        private void AddImage()
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.InitialDirectory = "";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                    "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                    "Portable Network Graphic (*.png)|*.png";

                if (op.ShowDialog() == true)
                {
                    selectedFileName = op.FileName;
                    Staff.PhotoStaff = ImageClass.ConvertImageToByteArray(selectedFileName);
                    PhotoStaffImgBrush.ImageSource = ImageClass.ConvertByteArrayToImage(Staff.PhotoStaff);
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }
        private void WorkerInfoAdd()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                var Staff = new Staff()
                {
                    LastName = LastNameTb.Text,
                    FirstName = FirstNameTb.Text,
                    MiddleName = MiddleNameTb.Text,
                    DateOfBirth = DateOfBirthDP.SelectedDate.Value,
                    Number = PhoneNumberTb.Text,
                    WorkStartDate = WorkStartDateDP.SelectedDate.Value,
                    IdGender = Int32.Parse(GenderCb.SelectedValue.ToString()),
                    IdDepartment = Int32.Parse(DepartamentCb.SelectedValue.ToString()),
                    IdCommittee = Int32.Parse(CommitteeCb.SelectedValue.ToString()),
                    IdUser = user.IdUser,
                    PhotoStaff = !string.IsNullOrEmpty(selectedFileName) ? ImageClass.ConvertImageToByteArray(selectedFileName) : null
                };
                DBEntities.GetContext().Staff.Add(Staff);
                DBEntities.GetContext().SaveChanges();
            }
        }
        private string selectedFileName = "";
        private void AddUser()
        {
            var userAdd = new User()
            {
                Login = LoginTb.Text,
                Password = PasswordTb.Text,
                IdRole = Int32.Parse(RoleCb.SelectedValue.ToString())
            };
            DBEntities.GetContext().User.Add(userAdd);
            DBEntities.GetContext().SaveChanges();
            user.IdUser = userAdd.IdUser;
        }
        private void AddStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DBEntities.GetContext()
               .Staff.FirstOrDefault(w => w.LastName == LastNameTb.Text
               && w.FirstName == FirstNameTb.Text && w.MiddleName == MiddleNameTb.Text) != null)
            {
                MBClass.ErrorMB($"Такой сотрудник уже есть");

                FirstNameTb.Focus();
                LastNameTb.Focus();
                MiddleNameTb.Focus();
            }
            else if (DBEntities.GetContext().Staff.FirstOrDefault(u =>
            u.Number == PhoneNumberTb.Text) != null)
            {
                MBClass.ErrorMB($"Пользователь c номером {PhoneNumberTb.Text} уже создан");

                PhoneNumberTb.Focus();
            }
            else if (DBEntities.GetContext()
                        .User
                        .FirstOrDefault(
                u => u.Login == LoginTb.Text) != null)
            {
                MBClass.ErrorMB($"Пользователь {LoginTb.Text} уже создан");
                LoginTb.Focus();
            }
            else if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    AddUser();
                    WorkerInfoAdd();

                    MBClass.InfoMB("Сотрудник добавлен");
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
            GenderCb.ItemsSource = DBEntities.GetContext().Gender.ToList();
        }

        private void ConfigureWithUserAccess()
        {

            TitleTb.Text = "Добавить сотрудника";
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowHelper.DragMove(this, e);
        }
        private void ChangePhotoStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            AddImage();
        }
    }
}

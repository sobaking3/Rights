﻿using Rights.ClassFolder;
using Rights.DataFolder;
using Rights.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для EditStaff.xaml
    /// </summary>
    public partial class EditStaff : Window
    {
        private Staff _staff;
        public EditStaff(Staff staff)
        {
            InitializeComponent();
            DataContext = _staff = staff;
            RoleCb.ItemsSource = DBEntities.GetContext().Role.ToList();
            GenderCb.ItemsSource = DBEntities.GetContext().Gender.ToList();
            DepartamentCb.ItemsSource = DBEntities.GetContext().Departament.ToList();
            CommitteeCb.ItemsSource = DBEntities.GetContext().Committee.ToList();
        }
        private void ChangeStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DBEntities.GetContext().SaveChanges();
                MBClass.InfoMB("Изменения сохранены!");
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

        private void ChangePhotoStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            var dlg = OpenFileDialogHelper.GetImageDialog();
            // Show open file dialog box 
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                string fileName = dlg.FileName;
                // Do something with fileName  
                _staff.PhotoStaff = File.ReadAllBytes(fileName);

                PhotoStaffImgBrush.ImageSource = new BitmapImage(new Uri(fileName, UriKind.RelativeOrAbsolute));

                // Сделать чтобы снова отобразилось фотка
                //PhotoStaffImg.GetBindingExpression(ImageBrush.ImageSourceProperty)
                //      .UpdateTarget();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigureWithUserAccess();
            GenderCb.ItemsSource = DBEntities.GetContext().Gender.ToList();
        }

        private void ConfigureWithUserAccess()
        {

            TitleTb.Text = "Изменить сотрудника";

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowHelper.DragMove(this, e);
        }
    }
}

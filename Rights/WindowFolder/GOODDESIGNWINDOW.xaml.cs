﻿using MaterialDesignThemes.Wpf;
using Rights.ClassFolder;
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
using System.Windows.Shapes;

namespace Rights.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для GOODDESIGNWINDOW.xaml
    /// </summary>
    public partial class GOODDESIGNWINDOW : Window
    {
        public GOODDESIGNWINDOW()
        {
            InitializeComponent();
            EmpName.Text = App.GetCurrentstaffInitials();

            if (App.CurrentStaff.PhotoStaff != null)
            {
                EmpImage.ImageSource = ImageClass.ConvertByteArrayToImage(App.CurrentStaff.PhotoStaff);
            }

            //EmpImage.ImageSource 
        }



        private void ListStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.PresidentWindow.ListStaff());
        }

        private void CommitteList_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.PresidentWindow.ListCommittee());
        }

        private void DepartmentList_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.PresidentWindow.ListDepartment());
        }

        private void OrganizationsList_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.PresidentWindow.ListOrganization());
        }

        private void SocialProgramsList_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.PresidentWindow.ListSocialPrograms());
        }

        private void MeetingsList_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.PresidentWindow.ListMeeting());
        }

        private void BudgetList_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.PresidentWindow.ListBudget());
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.MBLogOut(this);
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.MBExit();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowTransitionHelper.OpenWindow(this, this);
        }

        private void AppealsList_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.ManagerWindow.ListAppeals());
        }

        private void FullSizeWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                (FullSizeWindowBtn.Content as PackIcon).Kind = PackIconKind.WindowRestore;
            }
            else
            {
                WindowState = WindowState.Normal;
                (FullSizeWindowBtn.Content as PackIcon).Kind = PackIconKind.CropSquare;
            }
        }
    }
}
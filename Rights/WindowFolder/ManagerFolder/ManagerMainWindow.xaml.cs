using Rights.ClassFolder;
using Rights.PageFolder;
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

namespace Rights.WindowFolder.ManagerFolder
{
    /// <summary>
    /// Логика взаимодействия для ManagerMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : Window
    {
        public ManagerMainWindow()
        {
            InitializeComponent();
            EmpName.Text = App.GetCurrentstaffInitials();

            if(App.CurrentStaff.PhotoStaff != null)
            {
                EmpImage.ImageSource = ImageClass.ConvertByteArrayToImage(App.CurrentStaff.PhotoStaff);
            }

            //EmpImage.ImageSource 
        }

       

        private void ListStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.ManagerWindow.ListStaff());
        }

        private void CommitteList_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.ManagerWindow.ListCommitte());
        }

        private void DepartmentList_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.ManagerWindow.ListDepartment());
        }

        private void OrganizationsList_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.ManagerWindow.ListOrganization());
        }

        private void SocialProgramsList_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.ManagerWindow.ListSocialPrograms());
        }

        private void MeetingsList_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.ManagerWindow.ListMeetings());
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
    }
}

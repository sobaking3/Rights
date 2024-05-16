﻿using Rights.ClassFolder;
using Rights.DataFolder;
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

namespace Rights
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowTransitionHelper.CloseWindow(this);
            Task.Delay(500).ContinueWith(_ => // Задержка в 1 секунду
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Application.Current.Shutdown();
                });
            });
        }


        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTb.Text) || string.IsNullOrEmpty(PasswordPb.Password))
            {
                MBClass.ErrorMB("Пожалуйста, введите логин и пароль");
            }
            else
            {
                try
                {
                    var user = DBEntities.GetContext()
                        .User
                        .FirstOrDefault(u => u.Login == LoginTb.Text);
                    if (user == null)
                    {
                        MBClass.ErrorMB("Пароль или логин введен неверно");
                        LoginTb.Focus();
                    }
                    else if (user.Password != PasswordPb.Password)
                    {
                        MBClass.ErrorMB("Пароль или логин введен неверно");
                    }
                    else
                    {
                        App.CurrentUser = user;
                        Hide();
                        Window window = null;
                        switch (user.IdRole)
                        {
                            case 1:
                                window = new WindowFolder.PresidentFolder.PresidentMainWindow();
                                break;

                            case 2:
                                window = new WindowFolder.DirectorFolder.DirectorMainWindow();
                                break;

                            case 3:
                                window = new WindowFolder.ManagerFolder.ManagerMainWindow();
                                break;

                            case 4:
                                window = new WindowFolder.EmployeeFolder.EmployeeMainWindow();
                                break;

                            case 5:
                                window = new WindowFolder.AdminFolder.AdminMainWindow();
                                break;
                        }

                        if (window != null)
                        {
                            //Application.Current.MainWindow = window;
                            window.Show();
                        }
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
    }
}
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
    /// Логика взаимодействия для EditBudget.xaml
    /// </summary>
    public partial class EditBudget : Window
    {
        private Budget _budget = new Budget();


        public EditBudget(Budget budget)
        {
            InitializeComponent();

            DataContext = _budget = budget;
            //BRUH BRUH BRUH
            CommitteeCb.ItemsSource = DBEntities.GetContext().Committee.ToList();

        }
        private void EditBudgetBtn_Click(object sender, RoutedEventArgs e)
        {
            int amount = Convert.ToInt32(MoneyTb.Text);
            int usedMoney = Convert.ToInt32(MinusMoneyTb.Text);


            if (usedMoney > amount)
            {
                // Выводим сообщение об ошибке, если сумма использованных денег больше доступной
                MBClass.ErrorMB("Сумма использованных денег не может быть больше общей суммы бюджета.");
                return;
            }
            else
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

            TitleTb.Text = "Изменить";
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowHelper.DragMove(this, e);
        }


    }


}


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
    /// Логика взаимодействия для AddBudget.xaml
    /// </summary>
    public partial class AddBudget : Window
    {
        private Budget Budget = new Budget();
        private Committee committee = new Committee();


        public AddBudget()
        {
            InitializeComponent();
            CommitteeCb.ItemsSource = DBEntities.GetContext().Committee.ToList();

        }

        private void CommitteeInfoAdd()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {

                var Budget = new Budget()
                {
                    Year = Convert.ToInt32(YearTb.Text),
                    Amount = Convert.ToInt32(MoneyTb.Text),
                    UsageMoney = Convert.ToInt32(MinusMoneyTb.Text),
                    UnUsageMoney = Convert.ToInt32(MoneyTb.Text) - Convert.ToInt32(MinusMoneyTb.Text),
                    IdCommitte = Int32.Parse(CommitteeCb.SelectedValue.ToString()),
                };
                DBEntities.GetContext().Budget.Add(Budget);
                DBEntities.GetContext().SaveChanges();
            }
        }

        private void AddBudgetBtn_Click(object sender, RoutedEventArgs e)
        {
            int amount = Convert.ToInt32(MoneyTb.Text);
            int usedMoney = Convert.ToInt32(MinusMoneyTb.Text);
            var budget = DBEntities.GetContext().Budget.FirstOrDefault(u =>
    u.Committee.NameCommittee == CommitteeCb.Text &&
    u.Year.ToString() == YearTb.Text);

            if (budget != null)
            {
                MBClass.ErrorMB($"Запись о комитете c названием {CommitteeCb.Text} уже создана за год {YearTb.Text}");

                CommitteeCb.Focus();
                YearTb.Focus();
            }

            else if (usedMoney > amount)
            {
                // Выводим сообщение об ошибке, если сумма использованных денег больше доступной
                MBClass.ErrorMB("Сумма использованных денег не может быть больше общей суммы бюджета.");
                return;
            }
            else if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    CommitteeInfoAdd();

                    MBClass.InfoMB("Запись добавлена");
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

            TitleTb.Text = "Добавить запись";
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowHelper.DragMove(this, e);
        }
    }
}

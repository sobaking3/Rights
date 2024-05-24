using Rights.ClassFolder;
using Rights.DataFolder;
using Rights.Helpers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rights.PageFolder.PresidentWindow
{
    /// <summary>
    /// Логика взаимодействия для ListBudget.xaml
    /// </summary>
    public partial class ListBudget : Page
    {
        public ListBudget()
        {
            InitializeComponent();
            ListBudgetDG.ItemsSource = DBEntities.GetContext()
                .Budget.ToList().OrderBy(u => u.IdBudget);
        }
        private void UpdateList()
        {
            ListBudgetDG.ItemsSource = DBEntities.GetContext()
                 .Budget.Where(s => s.Committee.NameCommittee
                 .StartsWith(SearchStaffByFullNameTb.Text))
                 .ToList().OrderBy(s => s.Committee.NameCommittee);
        }

        private void AddBudgetBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.ShowDialogWithBlur(this, new PresidentWindow.AddBudget());
            UpdateList();
        }
        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                UpdateList();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }
        private void DeleteM1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Budget Budget = ListBudgetDG.SelectedItem as Budget;

                if (ListBudgetDG.SelectedItem == null)
                {
                    MBClass.ErrorMB("Запись не выбрана");
                }
                else
                {
                    if (MBClass.QuestionMB($"Удалить запись " +
                    $"бюджета комитета {Budget.Committee.NameCommittee}?"))
                    {
                        DBEntities.GetContext().Budget.Remove(ListBudgetDG.SelectedItem as Budget);
                        DBEntities.GetContext().SaveChanges();
                        MBClass.InfoMB("Запись удалена");
                        UpdateList();
                    }
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }

        private void EditM1_Click(object sender, RoutedEventArgs e)
        {
            Budget budget = ListBudgetDG.SelectedItem as Budget;
            WindowHelper.ShowDialogWithBlur(this, new PresidentWindow.EditBudget(budget));

                UpdateList();
        }
    }
}

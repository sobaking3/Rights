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
    /// Логика взаимодействия для EditMeetings.xaml
    /// </summary>
    public partial class EditMeetings : Window
    {
        private Meetings _meetings = new Meetings();


        public EditMeetings(Meetings meetings)
        {
            InitializeComponent();
            DataContext = _meetings = meetings;
            StatusCb.ItemsSource = DBEntities.GetContext()
            .Status.Except(DBEntities.GetContext().Status.Where(r => r.StatusName == "Жалоба решена"
            || r.StatusName == "В процессе"
            || r.StatusName == "Приостановлена"
            || r.StatusName == "Отклонена"
            || r.StatusName == "Договор в силе"
            || r.StatusName == "Договор рассторгнут"))
            .ToList();
            CommitteeCb.ItemsSource = DBEntities.GetContext().Committee.ToList();
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

            TitleTb.Text = "Изменить собрание";
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowHelper.DragMove(this, e);
        }

        private void EditMeetingBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DBEntities.GetContext().Meetings.FirstOrDefault(u =>
               u.Committee.NameCommittee == CommitteeCb.Text & u.MeetingsDate == MeetingsDateDP.SelectedDate) != null)
            {
                MBClass.ErrorMB($"Собрание на эту дату и для этого комитета уже было создано");

                MeetingsDateDP.Focus();
                CommitteeCb.Focus();
            }
            else if (ElementsToolsClass.AllFieldsFilled(this))
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
            else
            {
                MBClass.ErrorMB("Вы не ввели все нужные данные!");
            }       
        }
    }
}

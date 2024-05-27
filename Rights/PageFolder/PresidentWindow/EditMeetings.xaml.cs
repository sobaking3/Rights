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
    /// Логика взаимодействия для EditMeetings.xaml
    /// </summary>
    public partial class EditMeetings : Window
    {
        private Meetings _meetings = new Meetings();
        private Meetings _meetings2 = new Meetings();
        private DBEntities _ctx = DBEntities.GetContext();


        public EditMeetings(Meetings meetings)
        {
            _meetings = meetings;
            InitializeComponent();
            _meetings2 = DBEntities.GetContext().Meetings.AsNoTracking().FirstOrDefault(o => o.IdMeetings == meetings.IdMeetings);
            DataContext = _meetings2;
            StatusCb.ItemsSource = DBEntities.GetContext()
            .Status.AsNoTracking().Where(r => r.StatusName == "Собрание закончено"
            || r.StatusName == "Собрание скоро" || r.StatusName == "В процессе")
            .ToList();
            CommitteeCb.ItemsSource = DBEntities.GetContext().Committee.AsNoTracking().ToList();
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
            if (_ctx.Meetings.FirstOrDefault(x => x.Committee.NameCommittee == _meetings2.Committee.NameCommittee && x.IdMeetings != _meetings2.IdMeetings && x.MeetingsDate == _meetings2.MeetingsDate) != null)
            {
                 MBClass.ErrorMB("Собрание для этого комитета и в это время уже есть!");
                 MeetingsDateDP.Focus();
                    CommitteeCb.Focus();
                    return;
                }
                else if (ElementsToolsClass.AllFieldsFilled(this))
                {
                    try
                    {
                        _ctx.Meetings.AddOrUpdate(_meetings2);
                        _ctx.SaveChanges();
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

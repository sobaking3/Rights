using Rights.ClassFolder;
using Rights.DataFolder;
using Rights.Helpers;
using Rights.PageFolder.ManagerWindow;
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
    /// Логика взаимодействия для ListMeeting.xaml
    /// </summary>
    public partial class ListMeeting : Page
    {
        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (!object.Equals(value, _searchText))
                {
                    _searchText = value;
                    UpdateStaffList();
                }

            }
        }

        private List<FrameworkElement> _onLoadingBlockingControls = new List<FrameworkElement>();
        public ListMeeting()
        {
            InitializeComponent();
            DataContext = this;
            _onLoadingBlockingControls.Add(SearchStaffByFullNameTb);
            _onLoadingBlockingControls.Add(AddMeetingsBtn);
        }

        private void UpdateStaffList()
        {
            var query = DBEntities.GetContext().Meetings.Select(x => x);

            if (!string.IsNullOrEmpty(_searchText))
            {
                query = query.Where(x => (x.Committee.NameCommittee).Contains(_searchText));
            }


            List<Meetings> result = query.ToList();

            StaffListItemsControl.ItemsSource = result;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateStaffList();
        }

        private void AddMeetingBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.ShowDialogWithBlur(this, new PresidentWindow.AddMeetings());
            UpdateStaffList();
        }
        private void DeleteM1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((sender as FrameworkElement).DataContext is Meetings meetings)
                {
                    if (meetings == null)
                    {
                        MBClass.ErrorMB("Собрание не выбрано");
                    }
                    else
                    {
                        if (MBClass.QuestionMB($"Удалить информацию о собрании " +
                        $"для комитета {meetings.Committee.NameCommittee}?"))
                        {
                            DBEntities.GetContext().Meetings.Remove(meetings);
                            DBEntities.GetContext().SaveChanges();
                            MBClass.InfoMB("Собрание удалено из списка");
                            UpdateStaffList();
                        }
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
            Grid grid = sender as Grid;
            if ((sender as FrameworkElement).DataContext is Meetings meetings)
            {
                WindowHelper.ShowDialogWithBlur(this, new PresidentWindow.EditMeetings(meetings));

                UpdateStaffList();
            }
        }
    }
}

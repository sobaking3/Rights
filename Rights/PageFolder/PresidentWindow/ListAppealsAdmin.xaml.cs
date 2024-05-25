using Rights.ClassFolder;
using Rights.DataFolder;
using Rights.Helpers;
using Rights.PageFolder.ManagerWindow;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rights.PageFolder.PresidentWindow
{
    /// <summary>
    /// Логика взаимодействия для ListAppealsAdmin.xaml
    /// </summary>
    public partial class ListAppealsAdmin : Page
    {
        private string _searchText;
        public Status _selectedStatus;
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
        public Status SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                if (!object.Equals(value, _selectedStatus))
                {
                    _selectedStatus = value;
                    UpdateStaffList();
                }

            }
        }
        private List<FrameworkElement> _onLoadingBlockingControls = new List<FrameworkElement>();
        public ListAppealsAdmin()
        {
            InitializeComponent();
            DataContext = this;
            _onLoadingBlockingControls.Add(SearchStaffByFullNameTb);
            _onLoadingBlockingControls.Add(StatusFilterCb);

        }
        private void UpdateStaffList()
        {
            var query = DBEntities.GetContext().AppealsAndComplaints.Select(x => x);


            if (!string.IsNullOrEmpty(_searchText))
            {
                query = query.Where(x => (x.Staff.MiddleName + " " + x.Staff.FirstName + " " + x.Staff.LastName).Contains(_searchText));
            }
            if (_selectedStatus != null)
            {
                query = query.Where(x => x.IdStatus == _selectedStatus.IdStatus);
            }

            List<AppealsAndComplaints> result = query.ToList();

            StaffListItemsControl.ItemsSource = result;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateStaffList();
            StatusFilterCb.ItemsSource = DBEntities.GetContext().Status.Where(x => x.StatusName == "Заявка решена"
          || x.StatusName == "В процессе" || x.StatusName == "Заявка приостановлена" || x.StatusName == "Заявка отклонена"
          || x.StatusName == "Заявка удалена отправителем").ToList();
        }

        private void EditM1_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = sender as Grid;
            if ((sender as FrameworkElement).DataContext is AppealsAndComplaints appealsAndComplaints)
            {
                WindowHelper.ShowDialogWithBlur(this, new PresidentWindow.EditAppealStatus(appealsAndComplaints));

                UpdateStaffList();
            }
        }
    }
}

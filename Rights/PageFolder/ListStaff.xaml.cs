using Rights.DataFolder;
using Rights.Helpers;
using Rights.WindowFolder.OtherWindows;
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

namespace Rights.PageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListStaff.xaml
    /// </summary>
    public partial class ListStaff : Page
    {
        private string _searchText;
        public Department _selectedDepartament;
        public Position _selectedPosition;
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
        public Department SelectedDepartament
        {
            get => _selectedDepartament;
            set
            {
                if (!object.Equals(value, _selectedDepartament))
                {
                    _selectedDepartament = value;
                    UpdateStaffList();
                }

            }
        }
        public Position SelectedPosition
        {
            get => _selectedPosition;
            set
            {
                if (!object.Equals(value, _selectedPosition))
                {
                    _selectedPosition = value;
                    UpdateStaffList();
                }

            }
        }
        private List<FrameworkElement> _onLoadingBlockingControls = new List<FrameworkElement>();
        public ListStaff()
        {
            InitializeComponent();
            DataContext = this;
            _onLoadingBlockingControls.Add(SearchStaffByFullNameTb);
            _onLoadingBlockingControls.Add(DepartamentFilterCb);
            _onLoadingBlockingControls.Add(PositionFilterCb);
            _onLoadingBlockingControls.Add(AddStaffBtn);
        }

        private void AddStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Не заплачено за это", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UpdateStaffList()
        {
            var query = DBEntities.GetContext().Staff.Select(x => x);

            if (!string.IsNullOrEmpty(_searchText))
            {
                query = query.Where(x => (x.MiddleName + " " + x.FirstName + " " + x.LastName).Contains(_searchText));
            }

            if (_selectedDepartament != null)
            {
                query = query.Where(x => x.IdDepartment == _selectedDepartament.IdDepartment);
            }

            if (_selectedPosition != null)
            {
                query = query.Where(x => x.IdPosition == _selectedPosition.IdPosition);
            }

            List<Staff> result = query.ToList();

            int count = result.Count;

            // Размножаем данные для теста
            for (int i = 0; count > 0 && i < 3; i++)
            {
                for (int k = 0; k < count; k++)
                {
                    result.Add(result[k]);
                }
            }

            StaffListItemsControl.ItemsSource = result;
        }

        private void StaffGridInfo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Staff staff = (sender as Grid).DataContext as Staff;

            WindowHelper.ShowDialogWithBlur(this, new StaffWindow(staff));

            UpdateStaffList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateStaffList();
            DepartamentFilterCb.ItemsSource = DBEntities.GetContext().Department.ToList();
            PositionFilterCb.ItemsSource = DBEntities.GetContext().Position.ToList();

        }
    }
}

using Rights.ClassFolder;
using Rights.DataFolder;
using Rights.Helpers;
using Rights.PageFolder.ManagerWindow;
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
        public Departament _selectedDepartament;
        public Role _selectedRole;
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
        public Departament SelectedDepartament
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
        public Role SelectedRole
        {
            get => _selectedRole;
            set
            {
                if (!object.Equals(value, _selectedRole))
                {
                    _selectedRole = value;
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
            _onLoadingBlockingControls.Add(RoleFilterCb);
            _onLoadingBlockingControls.Add(AddStaffBtn);
        }

        private void AddStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddStaff().ShowDialog();
            UpdateStaffList();
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
                query = query.Where(x => x.IdDepartment == _selectedDepartament.IdDepartament);
            }

            if (_selectedRole != null)
            {
                query = query.Where(x => x.User.IdRole == _selectedRole.IdRole);
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
            DepartamentFilterCb.ItemsSource = DBEntities.GetContext().Departament.ToList();
            RoleFilterCb.ItemsSource = DBEntities.GetContext().Role.ToList();

        }

        private void EditM1_Click(object sender, RoutedEventArgs e)
        {
            Staff staff = (sender as Grid).DataContext as Staff;

            WindowHelper.ShowDialogWithBlur(this, new StaffWindow(staff));

            UpdateStaffList();
        }

        private void DeleteM1_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    DBEntities.GetContext().Staff.Remove();
            //    DBEntities.GetContext().SaveChanges();
            //    MessageBox.Show("Сотрудник удален!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void InfoM1_Click(object sender, RoutedEventArgs e)
        {
            Staff staff = (sender as Grid).DataContext as Staff;

            WindowHelper.ShowDialogWithBlur(this, new StaffWindow(staff));
              
            UpdateStaffList();
        }
    }
}

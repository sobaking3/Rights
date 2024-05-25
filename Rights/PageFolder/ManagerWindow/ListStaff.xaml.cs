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


namespace Rights.PageFolder.ManagerWindow
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
            WindowHelper.ShowDialogWithBlur(this, new ManagerWindow.AddStaff());
            
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

            List<Staff> result = query.Where(x => x.User.Role.NameRole != "Админ" &&
                    x.User.Role.NameRole != "Директор" && x.User.Role.NameRole != "Менеджер" && x.User.Role.NameRole != "Президент").ToList();

            StaffListItemsControl.ItemsSource = result;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateStaffList();
            DepartamentFilterCb.ItemsSource = DBEntities.GetContext().Departament.ToList();
            RoleFilterCb.ItemsSource = DBEntities.GetContext().Role.Except(DBEntities.GetContext().Role.Where(r => r.NameRole == "Админ"
           || r.NameRole == "Директор" || r.NameRole == "Менеджер" || r.NameRole == "Президент"))
           .ToList();
        }

        private void EditM1_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = sender as Grid;
            if ((sender as FrameworkElement).DataContext is Staff staff)
            {
                WindowHelper.ShowDialogWithBlur(this, new ManagerWindow.EditStaff(staff));

                UpdateStaffList();
            }
        }

        private void DeleteM1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((sender as FrameworkElement).DataContext is Staff staff)
                {
                    if (staff == null)
                    {
                        MBClass.ErrorMB("Пользователь не выбран");
                    }
                    else
                    {
                        if (MBClass.QuestionMB($"Удалить пользователя " +
                        $"с Фамилией {staff.LastName}?"))
                        {
                            DBEntities.GetContext().User.Remove(staff.User);
                            DBEntities.GetContext().Staff.Remove(staff);
                            DBEntities.GetContext().SaveChanges();
                            MBClass.InfoMB("Пользователь удален");
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


        private void InfoM1_Click(object sender, RoutedEventArgs e)
        {

            Grid grid = sender as Grid;
            if ((sender as FrameworkElement).DataContext is Staff staff)
            {
                WindowHelper.ShowDialogWithBlur(this, new ManagerWindow.InfoStaff(staff));

                UpdateStaffList();
            }
        }
    }
}
